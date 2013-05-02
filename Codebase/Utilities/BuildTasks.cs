// -----------------------------------------------------------------------
// <copyright file="Tasks.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Permissions;
    using DeploymentTracker.Properties;
    using System.Security;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BuildTasks : IDisposable
    {
        #region private variables
        /// <summary>
        /// Path of TF.exe. Here used for VS2010
        /// </summary>
        private readonly string tfexePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Microsoft Visual Studio 10.0\Common7\IDE\TF.exe");

        /// <summary>
        /// Path of TF.exe. Here used for VS2010
        /// </summary>
        private readonly string msbuildexePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe");

        /// <summary>
        /// Workspace path to get the code local
        /// </summary>
        private readonly string dedicatedWorkspacePath;

        /// <summary>
        /// TFS url
        /// </summary>
        private readonly string tfsurl;

        /// <summary>
        /// Solution Name to be deployed. This has to be unique identifier.
        /// </summary>
        private readonly string solutionName;

        /// <summary>
        /// Log file for msbuild
        /// </summary>
        private readonly string msbuildLogFilePath;

        /// <summary>
        /// Log file for TF
        /// </summary>
        private readonly string teamfoundationLogFilePath;

        /// <summary>
        /// Deployment folder for MSbuild
        /// </summary>
        private readonly string msbuildDeploymentFolder;

        /// <summary>
        /// Folder to store packages
        /// </summary>
        private readonly string packagesFolder;

        /// <summary>
        /// Stream writer for log file. To be used private to class
        /// </summary>
        private StreamWriter logStreamWriter;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildTasks"/> class.
        /// </summary>
        /// <param name="tfsurl">The tfsurl.</param>
        /// <param name="solutionName">Name of the solution.</param>
        public BuildTasks(string tfsurl, string solutionName)
        {
            // Initialize
            this.tfsurl = tfsurl;
            
            this.solutionName = CreateUniqueBuildDirectory(solutionName);

            // TFS log files
            this.teamfoundationLogFilePath = Path.Combine(Settings.Default.DeploymentLogPath, string.Concat(this.solutionName, "Logs"), string.Concat(this.solutionName, "-TFS", Constants.LogExtension));

            // Default workspace folder
            this.dedicatedWorkspacePath = Path.Combine(Settings.Default.TFSWorkingPath, this.solutionName);
            
            // MSbuild files
            this.msbuildLogFilePath = Path.Combine(Settings.Default.DeploymentLogPath, string.Concat(this.solutionName, "Logs"), string.Concat(this.solutionName, "-MSBUILD", Constants.LogExtension));
            this.msbuildDeploymentFolder = Path.Combine(this.dedicatedWorkspacePath, "DeploymentCode");
            this.packagesFolder = Path.Combine(this.dedicatedWorkspacePath, "AzurePackages");

            // create required folder-setup
            this.CreateRequiredFolders();

            // Now intialize log stream to log file
            this.logStreamWriter = File.AppendText(this.teamfoundationLogFilePath);
        }

        #region Properties
        /// <summary>
        /// Gets the Msbuild log path.
        /// </summary>
        public string MSbuildLogPath
        {
            get
            {
                return this.msbuildLogFilePath;
            }
        }

        /// <summary>
        /// Gets the TF log path.
        /// </summary>
        public string TFLogPath
        {
            get
            {
                return this.teamfoundationLogFilePath;
            }
        }

        /// <summary>
        /// Gets the azure packages path.
        /// </summary>
        public string AzurePackagesPath
        {
            get
            {
                return this.packagesFolder;
            }
        }
        #endregion

        #region Tasks - public functions
        /// <summary>
        /// Creates the dedicated TFS work space.
        /// </summary>
        public void CreateDedicatedTfsWorkSpace()
        {
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, "workspace /new /noprompt /computer:{0} {1} /collection:{2}", Environment.MachineName, this.solutionName, this.tfsurl);

            // Start the child process.
            this.ExecuteProcessWithTF(tfsParameters);

            //// Check for any errors
            //if (File.ReadAllText(this.teamfoundationLogFilePath).Contains("already mapped in workspace"))
            //{
            //    throw new InvalidOperationException("Error while creating workspace. Please review TF logs for additional details");
            //}
        }

        /// <summary>
        /// Deletes the dedicated TFS work space.
        /// </summary>
        public void DeleteDedicatedTfsWorkSpace()
        {
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, @"workspace /delete /noprompt {0} /collection:{1}", this.solutionName, this.tfsurl);

            // Start the child process.
            this.ExecuteProcessWithTF(tfsParameters);

            //// Check for any errors
            //if (File.ReadAllText(this.teamfoundationLogFilePath).Contains("TF14061"))
            //{
            //    throw new InvalidOperationException("Error while deleting workspace. Please review TF logs for additional details");
            //}
        }

        /// <summary>
        /// Gets the TFS label code to local.
        /// </summary>
        /// <param name="tfsLabelName">Name of the TFS label.</param>
        public void GetTfsLabelCodeToLocal(string tfsLabelName)
        {
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, @"get /noprompt -version:L{0} -force -recursive", tfsLabelName);

            // Start the child process.
            this.ExecuteProcessWithTF(tfsParameters);
        }

        /// <summary>
        /// Execs the ms build.
        /// </summary>
        [SecurityCritical]
        public void ExecMsBuild()
        {
            string solutionPath = this.GetSolutionFilePath();
            const string MSbuildparameters = "/nologo /noconsolelogger \"{0}\"  /m:2 /fl /flp:\"logfile={1};encoding=Unicode;verbosity=normal\"  /p:VisualStudioVersion=10.0  /p:SkipInvalidConfigurations=true /t:Publish /p:OutDir=\"{2}\" /p:Configuration=\"Release\" /p:Platform=\"Any CPU\" /p:VCBuildOverride=\"{0}.Any CPU.Release.vsprops\"";
            string formattedParameters = string.Format(CultureInfo.CurrentCulture, MSbuildparameters, Path.GetFileName(solutionPath), this.msbuildLogFilePath, string.Concat(this.msbuildDeploymentFolder, @"\\"));

            // Start the child process.
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = Directory.GetParent(solutionPath).ToString();
                p.StartInfo.FileName = this.msbuildexePath;
                p.StartInfo.Arguments = formattedParameters;
                p.Start();
                p.WaitForExit();
            }

            // copy package to deployment folder
            string packageFolderPath = this.GetPackageFilePath();
            if (!string.IsNullOrEmpty(packageFolderPath))
            {
                foreach (var item in Directory.GetFiles(packageFolderPath))
                {
                    File.Copy(item, Path.Combine(this.packagesFolder, Path.GetFileName(item)));
                }
            }

            if (!File.ReadAllText(this.msbuildLogFilePath).Contains("0 Error(s)"))
            {
                throw new FormatException("Build Failed. Please review build log for details.");
            }

        #if (!DEBUG)
            if (!File.ReadAllText(this.msbuildLogFilePath).Contains("0 Warning(s)"))
            {
                throw new NotSupportedException("Build Successful with warnings. Please review build log for details.");
            }
        #endif
        }

        #endregion

        #region IDisposible functions
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.logStreamWriter != null)
                {
                    this.logStreamWriter.Close();
                    this.logStreamWriter = null;
                }
            }
        }
        #endregion

        #region private functions
        /// <summary>
        /// Logs to file.
        /// </summary>
        /// <param name="logData">The log data.</param>
        private void LogToFile(string logData)
        {
            this.logStreamWriter.WriteLine(logData);
        }

        /// <summary>
        /// Executes TF.exe using parameters
        /// </summary>
        /// <param name="tfsParameters">The TFS parameters.</param>
        private void ExecuteProcessWithTF(string tfsParameters)
        {
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.WorkingDirectory = this.dedicatedWorkspacePath;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = this.tfexePath;
                p.StartInfo.Arguments = tfsParameters;
                p.Start();
                this.LogToFile(p.StandardOutput.ReadToEnd());
                this.LogToFile(p.StandardError.ReadToEnd());
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Creates the required folders.
        /// </summary>
        private void CreateRequiredFolders()
        {
            // Create necessary folders
            if (!Directory.Exists(this.dedicatedWorkspacePath))
            {
                Directory.CreateDirectory(this.dedicatedWorkspacePath);
            }

            if (!Directory.Exists(this.msbuildDeploymentFolder))
            {
                Directory.CreateDirectory(this.msbuildDeploymentFolder);
            }

            if (!Directory.Exists(this.packagesFolder))
            {
                Directory.CreateDirectory(this.packagesFolder);
            }

            if (!File.Exists(this.teamfoundationLogFilePath))
            {
                Directory.CreateDirectory(Directory.GetParent(this.teamfoundationLogFilePath).ToString());
                using (File.Create(this.teamfoundationLogFilePath))
                {
                    // do nothing. Just create file.
                }
            }
        }

        /// <summary>
        /// Gets the package file path.
        /// </summary>
        /// <returns>Returns package file path</returns>
        private string GetPackageFilePath()
        {
            var solutionFilePath = from file in Directory.GetFiles(this.dedicatedWorkspacePath, "*.cspkg", SearchOption.AllDirectories)
                                   where file.Contains("Release\\app.publish")
                                   select file;

            if (solutionFilePath.Count() == 1)
            {
                return Directory.GetParent(solutionFilePath.First().ToString()).ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the solution file path.
        /// </summary>
        /// <returns>Return solution file path</returns>
        private string GetSolutionFilePath()
        {
            var solutionFilePath = from file in Directory.GetFiles(this.dedicatedWorkspacePath, "*.sln", SearchOption.AllDirectories)
                                   where string.Compare(this.solutionName.Substring(0, this.solutionName.IndexOf("Build", StringComparison.OrdinalIgnoreCase)), Path.GetFileNameWithoutExtension(file), true, CultureInfo.CurrentCulture) == 0
                                   select file;

            if (solutionFilePath.Count() == 1)
            {
                return solutionFilePath.First().ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates the unique build directory for the solution.
        /// </summary>
        /// <param name="actualsolutionName">Name of the solution.</param>
        /// <returns>Solutionname with build extension</returns>
        private static string CreateUniqueBuildDirectory(string actualsolutionName)
        {
            string directoryName = string.Concat(actualsolutionName, "Build1");
            int postFix = 2;
            while (Directory.Exists(Path.Combine(Settings.Default.TFSWorkingPath, directoryName)))
            {
                directoryName = string.Concat(actualsolutionName, "Build", postFix.ToString(CultureInfo.CurrentCulture));
                postFix++;
            }

            return directoryName;
        }
        #endregion
    }
}
