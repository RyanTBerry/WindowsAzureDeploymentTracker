//Copyright (c) Microsoft Corporation 
//All rights reserved. 
//Microsoft Platform and Azure License
//This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//
//1. Definitions
//The terms “reproduce,” “reproduction,” “derivative works,” and “distribution” have the same meaning here as under U.S. copyright law.
//A “contribution” is the original software, or any additions or changes to the software.
//A “contributor” is any person that distributes its contribution under this license.
//“Licensed patents” are a contributor’s patent claims that read directly on its contribution.
//
//2. Grant of Rights
//(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//
//3. Conditions and Limitations
//(A) No Trademark License- This license does not grant you rights to use any contributors’ name, logo, or trademarks.
//(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//(E) The software is licensed “as-is.” You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
//(F) Platform Limitation- The licenses granted in sections 2(A) & 2(B) extend only to the software or derivative works that (1) runs on a Microsoft Windows operating system product, and (2) operates with Microsoft Windows Azure.

namespace DeploymentTracker.Services.Tasks
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using DeploymentTracker.Services.Entities;

    /// <summary>
    /// Contains all build tasks
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
        private readonly string modifiedSolutionName;

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
        public BuildTasks(BuildArgs buildArgs)
        {
            buildArgs.Validate();

            // Initialize
            this.tfsurl = buildArgs.TFSUrl;

            this.modifiedSolutionName = buildArgs.ModifiedSolutionName; // CreateUniqueBuildDirectory(buildArgs.SolutionName, buildArgs.TFSWorkingPath);

            // TFS log files
            this.teamfoundationLogFilePath = Path.Combine(buildArgs.DeploymentLogPath, string.Concat(this.modifiedSolutionName, Constants.LogsFolderExtension), string.Concat(this.modifiedSolutionName, Constants.TFSLogIdentifier, Constants.LogExtension));

            // Default workspace folder
            this.dedicatedWorkspacePath = Path.Combine(buildArgs.TFSWorkingPath, this.modifiedSolutionName);
            
            // MSbuild files
            this.msbuildLogFilePath = Path.Combine(buildArgs.DeploymentLogPath, string.Concat(this.modifiedSolutionName, Constants.LogsFolderExtension), string.Concat(this.modifiedSolutionName, Constants.MSBuildLogIdentifier, Constants.LogExtension));
            this.msbuildDeploymentFolder = Path.Combine(this.dedicatedWorkspacePath, Constants.DeploymentCodeFolderName);
            this.packagesFolder = Path.Combine(this.dedicatedWorkspacePath, Constants.AzurePackagesFolderName);

            // create required folder-setup
            this.CreateRequiredFolders();

            // Now intialize log stream to log file
            this.logStreamWriter = File.AppendText(this.teamfoundationLogFilePath);
            this.logStreamWriter.WriteLine(string.Format("Deployment process started at {0}", DateTime.Now));
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
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, "workspace /new /noprompt /computer:{0} {1} /collection:{2}", Environment.MachineName, this.modifiedSolutionName, this.tfsurl);

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
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, @"workspace /delete /noprompt {0} /collection:{1}", this.modifiedSolutionName, this.tfsurl);

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
            string tfsParameters = string.Format(CultureInfo.CurrentCulture, "get /noprompt -version:\"L{0}\" -force -recursive", tfsLabelName);

            // Start the child process.
            this.ExecuteProcessWithTF(tfsParameters);

            string solutionPath = this.GetSolutionFilePath();
            if (string.IsNullOrEmpty(solutionPath))
            {
                this.logStreamWriter.WriteLine("Cannot find solution file in the TFS label or Cannot download TFS label. Path might be too long. Process failed.");
                throw new OperationCanceledException("TFS download failed. Operation Aborted.");
            }
        }

        /// <summary>
        /// Execs the ms build.
        /// </summary>
        /// <returns>true if there are any warnings. False if there are no warnings</returns>
        public bool ExecMsBuild()
        {
            string solutionPath = this.GetSolutionFilePath();
            if (string.IsNullOrEmpty(solutionPath))
            {
                this.logStreamWriter.WriteLine("Cannot find solution file in the TFS label. Process failed.");
            }

            const string MSbuildparameters = "/nologo /noconsolelogger \"{0}\"  /m:2 /fl /flp:\"logfile={1};encoding=Unicode;verbosity=normal\"  /p:VisualStudioVersion=10.0  /p:SkipInvalidConfigurations=true /t:Publish /p:OutDir=\"{2}\" /p:Configuration=\"Release\" /p:Platform=\"Any CPU\" /p:VCBuildOverride=\"{0}.Any CPU.Release.vsprops\"";
            string formattedParameters = string.Format(CultureInfo.CurrentCulture, MSbuildparameters, Path.GetFileName(solutionPath), this.msbuildLogFilePath, string.Concat(this.msbuildDeploymentFolder, @"\\"));
            string errorText;

            // Start the child process.
            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WorkingDirectory = Directory.GetParent(solutionPath).ToString();
                p.StartInfo.FileName = this.msbuildexePath;
                p.StartInfo.Arguments = formattedParameters;
                p.StartInfo.RedirectStandardError = true;
                p.Start();
                errorText = p.StandardError.ReadToEnd();
                p.WaitForExit();
            }

            // copy package to deployment folder
            string packageFolderPath = this.GetPackageFilePath();
            if (!string.IsNullOrEmpty(packageFolderPath))
            {
                foreach (var item in Directory.GetFiles(packageFolderPath).Where(item => item != null))
                {
                    File.Copy(item, Path.Combine(this.packagesFolder, Path.GetFileName(item)));
                }
            }

            if (!File.Exists(this.msbuildLogFilePath) && !string.IsNullOrEmpty((errorText)))
            {
                using (StreamWriter sw = File.CreateText(this.msbuildLogFilePath))
                {
                    sw.WriteLine(errorText);
                }
            }

            if (File.Exists(this.msbuildLogFilePath) && !File.ReadAllText(this.msbuildLogFilePath).Contains("0 Error(s)"))
            {
                throw new FormatException("Build Failed. Please review build log for details.");
            }

            if (File.Exists(this.msbuildLogFilePath) && !File.ReadAllText(this.msbuildLogFilePath).Contains("0 Warning(s)"))
            {
                return true;
            }

            this.logStreamWriter.WriteLine("Build failed. Cannot find label or workspace. Reasons can also be attributed to paths length in configuration");

            return false;
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
                return Directory.GetParent(solutionFilePath.First().ToString(CultureInfo.InvariantCulture)).ToString();
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
                                   where string.Compare(this.modifiedSolutionName.Substring(0, this.modifiedSolutionName.IndexOf("-", StringComparison.OrdinalIgnoreCase)), Path.GetFileNameWithoutExtension(file), true, CultureInfo.CurrentCulture) == 0
                                   select file;

            if (solutionFilePath.Count() == 1)
            {
                return solutionFilePath.First().ToString(CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates the unique build directory for the solution.
        /// </summary>
        /// <param name="actualsolutionName">Name of the solution.</param>
        /// <returns>Solutionname with build extension</returns>
        private static string CreateUniqueBuildDirectory(string actualsolutionName, string tfsWorkingPath)
        {
            string directoryName = string.Concat(actualsolutionName, "Build1");
            int postFix = 2;
            while (Directory.Exists(Path.Combine(tfsWorkingPath, directoryName)))
            {
                directoryName = string.Concat(actualsolutionName, "Build", postFix.ToString(CultureInfo.CurrentCulture));
                postFix++;
            }

            return directoryName;
        }
        #endregion
    }
}
