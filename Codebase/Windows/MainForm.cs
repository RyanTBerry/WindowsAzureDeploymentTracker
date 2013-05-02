// -----------------------------------------------------------------------
// <copyright file="MainForm.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.App.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using Utilities;
    using Properties;

    /// <summary>
    /// MainForm Class
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Tracks multiple deployment windows
        /// </summary>
        private int deploymentWindows;

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
            try
            {
                PictureBox picturebox = sender as PictureBox;
                if (picturebox == null)
                {
                    return;
                }

                switch (picturebox.Name)
                {
                    case "picboxConfigureDeploymentTool":
                        if (this.deploymentWindows != 0)
                        {
                            throw new InvalidOperationException("Cannot edit settings while deployments are going on. Please try again.");
                        }

                        this.Hide();
                        using (AppConfigManager configManager = new AppConfigManager())
                        {
                            configManager.StartPosition = FormStartPosition.CenterParent;
                            configManager.FormClosed += new FormClosedEventHandler(this.FormReopen);
                            configManager.ShowDialog(this);
                        }

                        break;
                    case "picboxDeploy":
                        if (this.deploymentWindows < int.Parse(Settings.Default.MaxSessionsPermitted))
                        {
                            this.Hide();
                            PackageDeployment packageDeployment = new PackageDeployment();
                            packageDeployment.StartPosition = FormStartPosition.CenterParent;
                            packageDeployment.Load += new EventHandler(this.PackageDeployment_Load);
                            packageDeployment.FormClosed += new FormClosedEventHandler(this.PackageDeployment_FormClosed);
                            packageDeployment.Show();
                            this.deploymentWindows++;
                        }
                        else
                        {
                            MessageBox.Show(string.Format(CultureInfo.CurrentUICulture, "Cannot exceed more than {0} sessions. Please wait until one or more session to complete.", Settings.Default.MaxSessionsPermitted), "Maximum sessions exceeded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    case "picboxReports":
                        this.Hide();
                        using (ReportScreen deployReport = new ReportScreen())
                        {
                            deployReport.StartPosition = FormStartPosition.CenterParent;
                            deployReport.FormClosed += new FormClosedEventHandler(this.FormReopen);
                            deployReport.ShowDialog(this);
                        }

                        break;
                    case "picboxManageAzureConfigurations":
                        this.Hide();
                        using (AppLogViewer applogviewer = new AppLogViewer())
                        {
                            applogviewer.StartPosition = FormStartPosition.CenterParent;
                            applogviewer.FormClosed += new FormClosedEventHandler(this.FormReopen);
                            applogviewer.ShowDialog(this);
                        }

                        break;
                }
            }
            catch (InvalidOperationException invalidOp)
            {
                invalidOp.ShowUIException();
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
            ((Form)sender).Dispose();
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
            this.Refresh();
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
