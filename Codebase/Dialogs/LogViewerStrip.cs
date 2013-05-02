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
namespace DeploymentTracker.App.Dialogs
{
    using System;
    using System.Timers;
    using System.Windows.Forms;
    using DeploymentTracker.Services.Entities;
    using System.IO;
    using DeploymentTracker.Properties;
    using DeploymentTracker.App.Windows;
    using DeploymentTracker.App.Utilities;
    using System.Text;


    /// <summary>
    /// Advanced message box
    /// </summary>
    public partial class LogViewerStrip : Form
    {
        /// <summary>
        /// Log folder path. App settings
        /// </summary>
        private readonly string logFolderPath;

        /// <summary>
        /// Solution folder name
        /// </summary>
        private readonly string solutionFolderName;

        /// <summary>
        /// Generic message to show
        /// </summary>
        private readonly string GenericMessageForLogFiles = "Cannot find log files. Data might be purged, renamed or moved";

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedMessageBox"/> class.
        /// </summary>
        public LogViewerStrip()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedMessageBox"/> class.
        /// </summary>
        /// <param name="solutionFolderName">Name of the solution folder. This is with Version</param>
        /// <param name="headerText">The header text.</param>
        public LogViewerStrip(string solutionFolderName)
            : this()
        {
            this.label1.Text = string.Format("Click for logs - {0}:", solutionFolderName);
            this.logFolderPath = Path.Combine(Settings.Default.DeploymentLogPath, string.Concat(solutionFolderName, Constants.LogsFolderExtension));
            if (!Directory.Exists(this.logFolderPath))
            {
                throw new DirectoryNotFoundException("Cannot find TFS working directory. App settings might be changed");
            }

            this.solutionFolderName = solutionFolderName.Replace(Constants.RollbackFolderExtension, string.Empty);
        }

        private void BtnViewLog_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return; 
            }

            string logfileName = string.Empty;
            switch (button.Name)
            {
                case "btnViewBuildLog":
                    logfileName = string.Concat(solutionFolderName, Constants.MSBuildLogIdentifier, Constants.LogExtension);
                    break;
                case "btnViewTFLog":
                    logfileName = string.Concat(solutionFolderName, Constants.TFSLogIdentifier, Constants.LogExtension);
                    break;
                case "btnViewDeployLog":
                    logfileName = string.Concat(solutionFolderName, Constants.AzureLogIdentifier, Constants.LogExtension);
                    break;
            }

            if (string.IsNullOrEmpty(logfileName))
            {
                // This case doesnot exist
                return;
            }

            try
            {
                string logFilePath = Path.Combine(this.logFolderPath, logfileName);
                using (File.OpenRead(logFilePath)) { } // This is to test the file access
                using (LogViewer viewer = new LogViewer(logFilePath))
                {
                    viewer.ShowDialog(this);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                ex.WriteToLog();
                GenericMessageForLogFiles.ShowUIInformation();
            }
            catch (FileNotFoundException ex)
            {
                ex.WriteToLog();
                GenericMessageForLogFiles.ShowUIInformation();
            }
            catch (UnauthorizedAccessException ex)
            {
                ex.WriteToLog();
                GenericMessageForLogFiles.ShowUIInformation();
            }
            catch (Exception ex)
            {
                ex.WriteToLog();
                ex.ShowGenericException();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
