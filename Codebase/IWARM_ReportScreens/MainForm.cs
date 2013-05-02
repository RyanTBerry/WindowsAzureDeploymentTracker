// -----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.IWARM_ReportScreens
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DeploymentTracker.Properties;
    using DeploymentTracker.Utilities;
    using System.Globalization;

    /// <summary>
    /// MainForm Class
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Tracks multiple deployment windows
        /// </summary>
        private int deploymentWindows = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();   
        }

        /// <summary>
        /// Handles the Click event of the pictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox picturebox = sender as PictureBox;
            if (picturebox == null)
            {
                return;
            }

            switch (picturebox.Name)
            {
                case "picboxConfigureDeploymentTool":
                    this.Hide();
                    AppConfigManager configManager = new AppConfigManager() { StartPosition = FormStartPosition.CenterParent };
                    configManager.FormClosed += new FormClosedEventHandler(this.FormReopen);
                    configManager.ShowDialog(this);

                    break;
                case "picboxDeploy":
                    if (this.deploymentWindows < 5)
                    {
                        this.Hide();
                        PackageDeployment packageDeployment = new PackageDeployment() { StartPosition = FormStartPosition.CenterParent };
                        packageDeployment.Load += new EventHandler(this.PackageDeployment_Load);
                        packageDeployment.FormClosed += new FormClosedEventHandler(this.PackageDeployment_FormClosed);
                        packageDeployment.Show();
                        this.deploymentWindows++;
                    }
                    else
                    {
                        MessageBox.Show(string.Format(CultureInfo.CurrentUICulture, "Cannot exceed more than {0} sessions. Please wait until one or more session to complete.", Settings.Default.MaxSessionPermitted), "Maximum sessions exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case "picboxReports": 
                    this.Hide();
                    using (DeploymentReport deployReport = new DeploymentReport() { StartPosition = FormStartPosition.CenterParent })
                    {
                        deployReport.FormClosed += new FormClosedEventHandler(this.FormReopen);
                        deployReport.ShowDialog(this);
                    }
                    break;
                case "picboxManageAzureConfigurations": 
                    this.Hide();
                    using (AzureSubscriptions azureSubcriptions = new AzureSubscriptions() { StartPosition = FormStartPosition.CenterParent })
                    {
                        azureSubcriptions.FormClosed += new FormClosedEventHandler(this.FormReopen);
                        azureSubcriptions.ShowDialog(this);
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the packageDeployment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void PackageDeployment_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.deploymentWindows--;
            this.BringToFront();
        }

        /// <summary>
        /// Handles the Load event of the packageDeployment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PackageDeployment_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// Forms the reopen.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void FormReopen(object sender, FormClosedEventArgs e)
        {
            this.Show();
            this.BringToFront();
        }

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.deploymentWindows > 0)
            {
                MessageBox.Show("Cannot perform requested operation. \n One or more deployments are in progress.", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
