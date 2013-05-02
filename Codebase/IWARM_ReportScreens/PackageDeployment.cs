// -----------------------------------------------------------------------
// <copyright file="PackageDeployment.cs" company="Microsoft IT">
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
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using DeploymentTracker.Properties;
    using DeploymentTracker.Utilities;
    using System.Security.Cryptography.X509Certificates;
    using System.Xml.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Package Deployment Form
    /// </summary>
    public partial class PackageDeployment : Form
    {
        /// <summary>
        /// Ms build log path
        /// </summary>
        private string msbuildLogPath;

        /// <summary>
        /// TF Log path
        /// </summary>
        private string teamfoundationExeLogPath;

        /// <summary>
        /// To calculate eta
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// Number of steps identified for deployment. Used to calculate ETA
        /// </summary>
        private short numberOfStepsInDeployment = 5;

        /// <summary>
        /// TFS label search criteria
        /// </summary>
        private LabelSearchCriteria labelSearchCriteria;

        /// <summary>
        /// Selected TFS connection string
        /// </summary>
        private TFSConnectionString selectedtfsConnectionString;
        private string ManagementCertificateBase64;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageDeployment"/> class.
        /// </summary>
        public PackageDeployment()
        {
            this.InitializeComponent();
            this.btnDeploy.Enabled = false;
            this.btnLoadTFSlabel.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the btnDeploy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            this.Text = string.Concat(this.Text, "-[", this.txtSolutionName.Text, "]");

            this.groupBox2.Enabled = false;
            this.groupBox3.Enabled = false;

            // create TFS workspace and get the TFS label to that
            this.PerformOperations();

            this.groupBox2.Enabled = true;
            this.groupBox3.Enabled = true;
        }

        /// <summary>
        /// Performs the TFS operations.
        /// </summary>
        private void PerformOperations()
        {
            this.progressBar.Value = this.progressBar.Minimum;
            this.progressBar.Maximum = this.numberOfStepsInDeployment;  // number of tasks
            this.progressBar.Step = 1;
            tvCheckList.Nodes[0].ImageIndex = 0;
            if (!this.backgroundWorker.IsBusy)
            {
                this.backgroundWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool buildSuccessful = true;
            this.startTime = DateTime.Now;

            // tasks started. Initializing Tasks object here.
            this.backgroundWorker.ReportProgress(1, TaskStatus.Success);

            using (BuildTasks tasks = new BuildTasks(this.selectedtfsConnectionString.GenerateTFSUrl(), txtSolutionName.Text))
            {
                try
                {
                    // step 1. Creating a temporary workspace in TFS
                    // Note: TF.exe path; as of now for framework 4.0 and on x64 bit machines only
                    tasks.CreateDedicatedTfsWorkSpace();
                    this.backgroundWorker.ReportProgress(2, TaskStatus.Success);

                    // step 2. Get latest of label provided (from TFS)
                    // Note: TF.exe path; as of now for framework 4.0 and on x64 bit machines only
                    tasks.GetTfsLabelCodeToLocal(txtTFSLabelName.Text);
                    this.backgroundWorker.ReportProgress(3, TaskStatus.Success);

                    // step 3. Build it using msbuild.exe and get packages in packages folder
                    // Note: As of now for framework 4.0 and on x64 bit machines only
                    tasks.ExecMsBuild();
                    this.backgroundWorker.ReportProgress(4, TaskStatus.Success);

                    // Show required screens
                    this.ShowRequiredDialogs(tasks.AzurePackagesPath);

                    MessageBox.Show("Packages created succesfully", "Package Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (NotSupportedException exception)
                {
                    buildSuccessful = false;
                    this.backgroundWorker.ReportProgress(this.progressBar.Maximum, TaskStatus.Fail);
                    MessageBox.Show(exception.Message, "Deployment Successful with warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception exception)
                {
                    buildSuccessful = false;
                    this.backgroundWorker.ReportProgress(this.progressBar.Maximum, TaskStatus.Fail);
                    MessageBox.Show(exception.Message, "Deployment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // step 5: Finally delete workspace created. Backup code deployed and clean up code deployed.
                    tasks.DeleteDedicatedTfsWorkSpace();
                    this.backgroundWorker.ReportProgress(5, TaskStatus.Success);

                    // Assign log paths so that user can see them
                    this.msbuildLogPath = tasks.MSbuildLogPath;
                    this.teamfoundationExeLogPath = tasks.TFLogPath;

                    this.LogDeploymentDetailsIntoDatabase(buildSuccessful, tasks.AzurePackagesPath);
                    e.Result = tasks.AzurePackagesPath;
                }

                this.backgroundWorker.ReportProgress(this.progressBar.Maximum);
            }
        }

        /// <summary>
        /// Logs the details into database.
        /// </summary>
        /// <param name="isSuccessful">if set to <c>true</c> [is successful].</param>
        /// <param name="dropLocation">The drop location.</param>
        private void LogDeploymentDetailsIntoDatabase(bool isSuccessful, string dropLocation)
        {
            BuildItem buildItem = new BuildItem
            {
                DeploymentDateTime = DateTime.Now,
                DeploymentNotes = txtDeploymentNotes.Text,
                DropLocationPath = dropLocation,
                IsSuccess = isSuccessful,
                SolutionName = txtSolutionName.Text,
                SubmittedBy = string.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName),
                Tfslabelused = txtTFSLabelName.Text
            };

            buildItem.SaveThisInDB();
        }

        /// <summary>
        /// Shows the required dialogs.
        /// </summary>
        /// <param name="azurePackagePath">The azure package path.</param>
        private void ShowRequiredDialogs(string azurePackagePath)
        {
            // Contains only 1 config file. So show it
            ServiceConfigViewer serviceConfigViewer = new ServiceConfigViewer(Directory.GetFiles(azurePackagePath, "*.cscfg", SearchOption.AllDirectories).First());

            this.Invoke((MethodInvoker)delegate
            {
                // this bit runs on the UI thread
                DialogResult result = serviceConfigViewer.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    throw new OperationCanceledException("Operation cancelled by user.");
                }
            });

            // Now show Azure configuration form also
        }

        /// <summary>
        /// Handles the ProgressChanged event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                if (e.UserState.ToString() == TaskStatus.Success.ToString())
                {
                    // Making image working
                    tvCheckList.Nodes[e.ProgressPercentage - 1].ImageIndex = 1; // current step completed. 1 - tick

                    // next one started. 0 - loading. Not for the final step
                    if (e.ProgressPercentage != this.progressBar.Maximum) 
                    {
                        tvCheckList.Nodes[e.ProgressPercentage].ImageIndex = 0; 
                    }
                }
                else if (e.UserState.ToString() == TaskStatus.Fail.ToString())
                {
                    for (int i = 1; i < this.progressBar.Maximum - 1; i++)
                    {
                        if (tvCheckList.Nodes[i].ImageIndex != 1)
                        {
                            tvCheckList.Nodes[i].ImageIndex = 3;
                        }
                    }
                }
            }

            // do stuff
            TimeSpan timespent = DateTime.Now - this.startTime;

            progressBar.PerformStep();
            int secondsremaining = (int)(timespent.TotalSeconds / this.progressBar.Value * (progressBar.Maximum - progressBar.Value));
            int minsremaining = (int)(timespent.TotalMinutes / progressBar.Value * (progressBar.Maximum - progressBar.Value));
            int hrssremaining = (int)(timespent.TotalHours / progressBar.Value * (progressBar.Maximum - progressBar.Value));
            string timeremaining = string.Format("{0}:{1}:{2}", hrssremaining, minsremaining, secondsremaining);

            toolStripStatusLabel.Text = string.Format("Toal time spent: " + timespent.ToString()) + string.Format(" Remaining : " + timeremaining.ToString());
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.backgroundWorkerToCloud.RunWorkerAsync(e.Result);

            // enable ths user to see log paths
            this.btnViewBuildLog.Enabled = this.btnViewTFLog.Enabled = true;

            // bring to front once completed
            this.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the btnViewTFLog and btnViewBuildLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnViewLog_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            string path = string.Empty;
            switch (button.Name)
            {
                case "btnViewBuildLog":
                    path = this.msbuildLogPath;
                    break;
                case "btnViewTFLog":
                    path = this.teamfoundationExeLogPath;
                    break;
            }

            if (string.IsNullOrEmpty(path))
            {
                // This case doesnot exist
                return;
            }

            LogViewer viewer = new LogViewer(path);
            viewer.ShowDialog(this);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (!this.backgroundWorker.IsBusy)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the PackageDeployment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void PackageDeployment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorker.IsBusy)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLoadTFSlabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnLoadTFSlabel_Click(object sender, EventArgs e)
        {
            try
            {
                using (BrowseLabels browselabel = new BrowseLabels(this.selectedtfsConnectionString.GenerateTFSUrl()) { StartPosition = FormStartPosition.CenterParent })
                {
                    if (browselabel.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.labelSearchCriteria = browselabel.ValidSearchCriteria;
                        if (labelSearchCriteria != null)
                        {
                            this.txtTFSLabelName.Text = labelSearchCriteria.LabelName;
                            this.btnLoadSolutions.Enabled = true;
                        }
                    }
                }
            }
            catch(Exception exception )
            {
                MessageBox.Show(exception.Message, "Invalid Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLoadSolutions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnLoadSolutions_Click(object sender, EventArgs e)
        {
            try
            {
                using (BrowseSolutions browseSolutions = new BrowseSolutions(this.selectedtfsConnectionString.GenerateTFSUrl(), this.labelSearchCriteria) { StartPosition = FormStartPosition.CenterParent })
                {
                    if (browseSolutions.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.txtSolutionName.Text = Path.GetFileNameWithoutExtension(browseSolutions.SolutionName);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Invalid Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the PackageDeployment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PackageDeployment_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections' table. You can move, or remove it, as needed.
            this.tFSConnectionsTableAdapter.Fill(this.deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections);
            this.cbxServerName.SelectedIndex = -1;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbxServerName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnLoadTFSlabel.Enabled = this.cbxServerName.SelectedIndex > -1;

            if (cbxServerName.SelectedValue != null)
            {
                txtTFSDefaultCollection.Text = Convert.ToString(cbxServerName.SelectedValue);
                this.selectedtfsConnectionString = this.cbxServerName.Text.GetTFSConnectionString();
                if (this.selectedtfsConnectionString == null)
                {
                    MessageBox.Show("Invalid TFS connection for given server name.", "Invalid Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnManageTFSsettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnManageTFSsettings_Click(object sender, EventArgs e)
        {
            using (AppConfigManager appManager = new AppConfigManager { StartPosition = FormStartPosition.CenterParent })
            {
                appManager.ShowDialog(this);
            }
            
            this.tFSConnectionsTableAdapter.Fill(this.deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections);
            this.cbxServerName.Refresh();
        }

        /// <summary>
        /// Handles the TextChanged event of the txtSolutionName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtSolutionName_TextChanged(object sender, EventArgs e)
        {
            this.btnDeploy.Enabled = !string.IsNullOrEmpty(this.txtSolutionName.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnBrowsePackage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBrowsePackage_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            switch (button.Name)
            {
                case "btnBrowsePackage":
                    openFileDialog.Filter = "Azure package file | *.cspkg";
                    openFileDialog.DefaultExt = Constants.AzurePackageExtension;
                    break;
                case "btnBrowseConfig":
                    openFileDialog.Filter = "Service config file | *.cscfg";
                    openFileDialog.DefaultExt = Constants.ServiceConfigurationExtension;
                    break;
                case "btnLoadPublishSettings":
                    openFileDialog.Filter = "Publish settings file | *.publishsettings";
                    openFileDialog.DefaultExt = Constants.PublishSettingsExtension;
                    break;
            }

            openFileDialog.ShowDialog(this);
        }

        /// <summary>
        /// Handles the FileOk event of the openFileDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if(e.Cancel)
            {
                return;
            }

            if (openFileDialog.DefaultExt == Constants.AzurePackageExtension)
            {
                this.txtPackagePath.Text = openFileDialog.FileName;
            }
            else if (openFileDialog.DefaultExt == Constants.ServiceConfigurationExtension)
            {
                this.txtServiceConfigPath.Text = openFileDialog.FileName;
            }
            else if (openFileDialog.DefaultExt == Constants.PublishSettingsExtension)
            {
                this.txtPublishSettingsFilePath.Text = openFileDialog.FileName;
                XDocument xdoc = XDocument.Load(this.txtPublishSettingsFilePath.Text);
                this.ManagementCertificateBase64 = xdoc.Descendants("PublishProfile").Single().Attribute("ManagementCertificate").Value;
                cbxSubscriptions.DataSource = xdoc.Descendants("Subscription").Select(x => new { Id = x.Attribute("Id").Value, Name = x.Attribute("Name").Value }).ToList();
                cbxSubscriptions.DisplayMember = "Name";
                cbxSubscriptions.ValueMember = "Id";
            }
        }

        /// <summary>
        /// Handles the Selecting event of the tabControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TabControlCancelEventArgs"/> instance containing the event data.</param>
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to cancel current operation?", "Cancel Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                switch (e.TabPageIndex)
                {
                    case 0:
                        this.txtServiceConfigPath.Text = this.txtPackagePath.Text = string.Empty;
                        break;
                    case 1:
                        this.cbxServerName.SelectedIndex = -1;
                        txtTFSLabelName.Text = txtTFSDefaultCollection.Text = txtSolutionName.Text = string.Empty;
                        btnLoadSolutions.Enabled = false;
                        this.btnLoadTFSlabel.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                e.Cancel = true;
            }

            this.BringToFront();
        }

        private void backgroundWorkerToCloud_DoWork(object sender, DoWorkEventArgs e)
        {
            string azurePackagePath = e.Argument as string;
            CloudDeploymentTasks cloudTasks = new CloudDeploymentTasks();

            try
            {
                // Collect Azure parameters
                AzureParams azureParams = this.GetAzureParamsFromForm(azurePackagePath);
                
                // Collect required values
                string hostedServiceName = azureParams.HostedServiceName;
                string storageName = azureParams.StorageName;
                var managementCertbase64string = azureParams.ManagementCertificatebase64string;
                var managementCert = new X509Certificate2(Convert.FromBase64String(managementCertbase64string));
                string subscriptionId = azureParams.SubscriptionId;
                bool isUpgrade = azureParams.IsUpgrade;
                string deploymentSlot = azureParams.DeploymentSlot;

                // Get storage primary key in either case
                string storagePrimaryKey = string.Empty;

                // If its upgrade donot create hosted serviceand storage primary keys
                if (!isUpgrade)
                {
                    cloudTasks.InitHostedService(hostedServiceName, storageName, managementCert, subscriptionId);
                    storagePrimaryKey = cloudTasks.InitStorageAccount(storageName, managementCert, subscriptionId);
                }
                else
                {
                    storagePrimaryKey = cloudTasks.GetStoragePrimaryKey(managementCert, subscriptionId, storageName);
                }

                cloudTasks.CopyCspkg(storageName, storagePrimaryKey);
                cloudTasks.DeployPackageInCloud(managementCert, subscriptionId, storageName, hostedServiceName, storagePrimaryKey, deploymentSlot, isUpgrade);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! We encountered the following problem: {0}", ex.Message);
                Console.WriteLine("details: {0}", ex.ToString());
            }
        }

        private AzureParams GetAzureParamsFromForm(string azurePackagePath)
        {
            string cspkgFilePath = Directory.GetFiles(azurePackagePath, "*.cspkg").First();
            string configFilePath = Directory.GetFiles(azurePackagePath, "*.cscfg").First();
            string publishSettingsFilePath = txtPublishSettingsFilePath.Text;
            XDocument xdoc = XDocument.Load(publishSettingsFilePath);

            AzureParams azureParams = new AzureParams
                {
                    ServiceConfigFileUrl = configFilePath,
                    PackageFileUrl = cspkgFilePath,
                    NumberOfTries = int.Parse(Settings.Default.NumberOfTries),
                    StorageName = txtStorageName.Text,
                    HostedServiceName = txtHostedServiceName.Text,
                    DeploymentSlot = cbxDeploymentSlot.SelectedItem.ToString(),
                    AzureLocation = cbxLocations.SelectedItem.ToString(),
                    IsUpgrade = chbxIsUpgrade.Checked,
                    ManagementCertificatebase64string = this.ManagementCertificateBase64,
                    SubscriptionId = cbxSubscriptions.SelectedItem.ToString()
                };
            return azureParams;
        }

       
        private void backgroundWorkerToCloud_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SubscriptionDownloadPage subPage = new SubscriptionDownloadPage { StartPosition = FormStartPosition.CenterParent };
            subPage.ShowDialog(this);
        }

        private void cbxSubscriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string,string> locations = CloudDeploymentTasks.ListLocationsForSubscription(cbxSubscriptions.SelectedValue.ToString(), new X509Certificate2(Convert.FromBase64String(this.ManagementCertificateBase64)), "2011-10-01");
            if (locations != null)
            {
                this.cbxLocations.DataSource = locations;
                this.cbxLocations.DisplayMember = "value";
                this.cbxLocations.ValueMember = "key";
            }
        }

    }
}
