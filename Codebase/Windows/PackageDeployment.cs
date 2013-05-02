// -----------------------------------------------------------------------
// <copyright file="PackageDeployment.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;
using System.Xml;

namespace DeploymentTracker.App.Windows
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using Dialogs;
    using Utilities;
    using Properties;
    using Services.Entities;
    using Services.Tasks;
    using System.Net;

    /// <summary>
    /// Package Deployment Form
    /// </summary>
    public partial class PackageDeployment : Form
    {
        #region Form variables
        /// <summary>
        /// Ms build log path
        /// </summary>
        private string msbuildLogPath;

        /// <summary>
        /// TF Log path
        /// </summary>
        private string teamfoundationExeLogPath;

        /// <summary>
        /// Cloud deployment log
        /// </summary>
        private string cloudDeploymentLogPath;

        /// <summary>
        /// Total steps identified in process
        /// </summary>
        private int totalStepsIdentified = 11;

        /// <summary>
        /// TFS label search criteria
        /// </summary>
        private TFSLabel labelSearchCriteria;

        /// <summary>
        /// Selected TFS connection string
        /// </summary>
        private TFSConnectionString selectedtfsConnectionString;

        /// <summary>
        /// Management certificate from publishSettings file
        /// </summary>
        private string managementCertificateBase64;

        /// <summary>
        /// Rollback version number
        /// </summary>
        private string rollbackVersionToLog;

        /// <summary>
        /// To encapsulate cloud arguments
        /// </summary>
        private CloudArgs cloudArgs;

        /// <summary>
        /// To encapsulate build arguments
        /// </summary>
        private BuildArgs buildArgs;

        /// <summary>
        /// Service certifiates to deploy
        /// </summary>
        private List<string> certsToDeploy;

        /// <summary>
        /// Thread to load azure settings
        /// </summary>
        private Thread backgroundThread;

        /// <summary>
        /// Indicated new storage account selection
        /// </summary>
        private bool isNewStorage;

        /// <summary>
        /// To avoid STA. store package name
        /// </summary>
        private string pkgNameForLocalDeployment;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageDeployment"/> class.
        /// </summary>
        /// <param name="isUpgrade">if set to <c>true</c> [is upgrade].</param>
        /// <param name="rollbackVersion">The rollback version.</param>
        /// <param name="versionToDeploy">The version to deploy.</param>
        /// <param name="application">The application.</param>
        public PackageDeployment(bool isUpgrade = false, string rollbackVersion = "", string versionToDeploy = "", string application = "")
        {
            this.InitializeComponent();
            this.btnDeploy.Enabled = true;
            this.btnLoadTFSlabel.Enabled = false;
            this.tvCheckList.ExpandAll();
            this.BindEnvironmentCombo();

            if (isUpgrade)
            {
                this.tabControl1.TabPages.Remove(this.tabControl1.TabPages[0]);
                this.cbxIsRollback.Checked = this.txtConfigPath.ReadOnly = this.txtPackagePath.ReadOnly = true;
                this.btnBrowseConfig.Enabled = this.btnBrowsePackage.Enabled = false;
                this.rollbackVersionToLog = versionToDeploy;
                string sourceFolder = Path.Combine(Settings.Default.TFSWorkingPath, string.Concat(application, "-", rollbackVersion), Constants.AzurePackagesFolderName);
                string.Format("Initiated rollback of {0} to {1}", application, rollbackVersion).WriteToLog();

                ////MessageBox.Show(sourceFolder);
                if (!Directory.Exists(sourceFolder))
                {
                    throw new DirectoryNotFoundException("TFS working path is changed, moved or renamed. Cannot continue with rollback.");
                }

                this.txtPackagePath.Text = Directory.GetFiles(sourceFolder, "*.cspkg", SearchOption.AllDirectories)[0];
                this.txtConfigPath.Text = Directory.GetFiles(sourceFolder, "*.cscfg", SearchOption.AllDirectories)[0];
            }
        }
        #endregion

        #region PrivateFunctions
        /// <summary>
        /// Binds the environment combo.
        /// </summary>
        private void BindEnvironmentCombo()
        {
            this.cbxDeploymentSlot.Items.Clear();
            Array values = Enum.GetValues(typeof(DeploymentEnvironment));
            foreach (DeploymentStatus val in values)
            {
                this.cbxDeploymentSlot.Items.Add(Enum.GetName(typeof(DeploymentEnvironment), val));
            }
        }

        /// <summary>
        /// Resets the tree view nodes.
        /// </summary>
        /// <param name="topnode">The topnode.</param>
        private void ResetTreeViewNodes(TreeNode topnode)
        {
            topnode.ImageIndex = 2; // blank
            if (topnode.Level == 0)
            {
                foreach (TreeNode item in topnode.Nodes)
                {
                    this.ResetTreeViewNodes(item);
                }
            }
        }
   
        /// <summary>
        /// Sets the user action on from.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void SetUserActionOnFrom(bool status)
        {
            this.btnCancel.Enabled = this.tabControl1.Enabled = this.groupBox3.Enabled = status;
            this.btnDeploy.Enabled = !this.cbxIsRollback.Checked;
        }

        /// <summary>
        /// Assigns the cloud args no build.
        /// </summary>
        /// <param name="currentVersionForlocalDeploy">The current version forlocal deploy.</param>
        private void AssignCloudArgsNoBuild(string currentVersionForlocalDeploy)
        {
            string solutionName = Path.GetFileNameWithoutExtension(this.txtPackagePath.Text);
            string internalversion = this.cbxIsRollback.Checked ? this.rollbackVersionToLog : currentVersionForlocalDeploy;
            string modifiedSolutionName = this.cbxIsRollback.Checked ?
                        string.Concat(solutionName, "-", internalversion, Constants.RollbackFolderExtension) :
                        string.Concat(solutionName, "-", internalversion);
            string backupPath = Path.Combine(Settings.Default.TFSWorkingPath, modifiedSolutionName, Constants.AzurePackagesFolderName);
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }

            string backUpPackagePath = Path.Combine(backupPath, Path.GetFileName(this.txtPackagePath.Text));
            string backUpConfigPath = Path.Combine(backupPath, Path.GetFileName(this.txtConfigPath.Text));
            File.Copy(this.txtPackagePath.Text, backUpPackagePath, true);
            File.Copy(this.txtConfigPath.Text, backUpConfigPath, true);

            // This is for cloud deployment'
            this.cloudArgs.IsBuildCompleted = true;
            this.cloudArgs.PackageFilePath = backUpPackagePath;
            this.cloudArgs.ServiceConfigFilePath = backUpConfigPath;
            this.cloudArgs.InternalVersionNumber = internalversion;
            this.cloudArgs.AzureLogFilePath = Path.Combine(Settings.Default.DeploymentLogPath,
                string.Concat(string.Concat(solutionName, "-", this.cloudArgs.InternalVersionNumber, this.cbxIsRollback.Checked ? Constants.RollbackFolderExtension : string.Empty), Constants.LogsFolderExtension),
                string.Concat(Path.GetFileNameWithoutExtension(backUpPackagePath), "-", this.cloudArgs.InternalVersionNumber, Constants.AzureLogIdentifier, Constants.LogExtension));
            this.cloudDeploymentLogPath = this.cloudArgs.AzureLogFilePath;
        }

        /// <summary>
        /// Peforms the cloud deployment.
        /// </summary>
        /// <returns>
        /// true if successful else false
        /// </returns>
        private bool PerformCloudDeployment()
        {
            bool isSuccessful = true;
            int stepNumber = 20;

            // Get storage primary key in either case
            string storagePrimaryKey = string.Empty;

            try
            {
                using (CloudDeploymentTasks cloudTasks = new CloudDeploymentTasks(this.cloudArgs))
                {
                    // If its upgrade donot create hosted serviceand storage primary keys
                    if (!cloudTasks.IsUpgrade)
                    {   
                        if (this.isNewStorage)
                        {
                            this.backgroundWorker.ReportProgress(++stepNumber, "Creating storage account in cloud....");
                            storagePrimaryKey = cloudTasks.InitStorageAccount();
                        }
                        else
                        {
                            this.backgroundWorker.ReportProgress(++stepNumber, "Updating storage account in cloud....");
                            storagePrimaryKey = cloudTasks.GetStoragePrimaryKey();
                        }

                        this.backgroundWorker.ReportProgress(++stepNumber, "Creating hosted service in cloud....");
                        cloudTasks.InitHostedService();
                    }
                    else
                    {
                        this.backgroundWorker.ReportProgress(++stepNumber, "Receiving storage account primary key from cloud....");
                        storagePrimaryKey = cloudTasks.GetStoragePrimaryKey();

                        // If storage primary key is not found implies need to create a storage account
                        if (string.IsNullOrEmpty(storagePrimaryKey))
                        {
                            storagePrimaryKey = cloudTasks.InitStorageAccount();
                        }

                        this.backgroundWorker.ReportProgress(++stepNumber, "Creating hosted service in cloud....");
                    }
                    
                    this.backgroundWorker.ReportProgress(++stepNumber, "Copying package to cloud....");
                    cloudTasks.CopyCspkg(storagePrimaryKey);
                    this.backgroundWorker.ReportProgress(++stepNumber, "Deploying package in cloud....");
                    cloudTasks.DeployPackageInCloud();

                    this.backgroundWorker.ReportProgress(++stepNumber, "Creating service certificates. User action required....");

                    if (cloudTasks.CertificatePaths != null && cloudTasks.CertificatePaths.Any())
                    {
                        foreach (var item in cloudTasks.CertificatePaths)
                        {
                            // doit variable is to bypass STA
                            bool doit = false;
                            string fileName = Path.GetFileName(item);
                            PasswordPrompt prompt = new PasswordPrompt(fileName);
                            this.Invoke((MethodInvoker)delegate
                            {
                                prompt.TopMost = true;
                                prompt.StartPosition = FormStartPosition.CenterParent;
                                if (prompt.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    doit = true;
                                }
                            });

                            if (doit)
                            {
                                cloudTasks.AddCertificateToCloud(item, prompt.GivenPassword);
                            }
                            else
                            {
                                string.Format("Cannot install certificate : {0}. Cancelled by User", fileName).WriteToLog();
                                string.Format("Cannot install certificate : {0}. Cancelled by User", fileName).ShowUIInformation(FormStartPosition.CenterScreen);
                            }
                        }
                    }

                    this.backgroundWorker.ReportProgress(++stepNumber, "Cloud deployment completed successfully....");
                }
            }
            catch (WebException exception)
            {
                isSuccessful = false;
                this.backgroundWorker.ReportProgress(stepNumber, ReportStatus.Fail);
                exception.WriteToLog();
                this.Invoke((MethodInvoker)(() => exception.ShowGenericException("Deployment to Cloud failed.")));
            }
            catch (Exception exception)
            {
                isSuccessful = false;
                this.backgroundWorker.ReportProgress(stepNumber, ReportStatus.Fail);
                exception.WriteToLog();
                this.Invoke((MethodInvoker)(() => exception.ShowGenericException("Deployment to Cloud failed.")));
            }

            return isSuccessful;
        }

        /// <summary>
        /// Performs the build operation.
        /// </summary>
        /// <param name="isLocalDeployment">if set to <c>true</c> [is local deployment].</param>
        /// <param name="currentVersionForlocalDeploy">The current version forlocal deploy.</param>
        /// <returns>
        /// true if build is successful
        /// </returns>
        private bool PerformBuildOperation(bool isLocalDeployment, string currentVersionForlocalDeploy = "")
        {
            bool buildSuccessful = true;
            int stepNumber = 10;

            if (isLocalDeployment)
            {
                if (string.IsNullOrEmpty(currentVersionForlocalDeploy))
                {
                    throw new ArgumentNullException("Unable to get next successful version to deploy.");
                }

                try
                {
                    this.AssignCloudArgsNoBuild(currentVersionForlocalDeploy);
                }
                catch (IOException ex)
                {
                    ex.ShowUIException();
                    return false;
                }

                return true;
            }

            // Initialize
            this.backgroundWorker.ReportProgress(++stepNumber, "Initializing build parameters....");
            using (BuildTasks tasks = new BuildTasks(this.buildArgs))
            {
                try
                {
                    this.backgroundWorker.ReportProgress(++stepNumber, "Creating temporary TFS workspace....");
                    tasks.CreateDedicatedTfsWorkSpace();

                    this.backgroundWorker.ReportProgress(++stepNumber, "Downloading code for TFS label....");
                    tasks.GetTfsLabelCodeToLocal(txtTFSLabelName.Text);
                   
                    this.backgroundWorker.ReportProgress(++stepNumber, "Compiling and building solution.....");
                    if (tasks.ExecMsBuild())
                    {
                        using (AdvancedMessageBox adv = new AdvancedMessageBox("Build suceeded but has warnings. Do you want to continue?", ReportStatus.Information, true))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                if (DialogResult.Cancel == adv.ShowDialog(this))
                                {
                                    this.AssignCloudArgsAfterBuild(tasks.MSbuildLogPath, tasks.TFLogPath, tasks.AzurePackagesPath);
                                    throw new OperationCanceledException("Operation cancelled.");
                                }
                            });
                        }
                    }

                    this.AssignCloudArgsAfterBuild(tasks.MSbuildLogPath, tasks.TFLogPath, tasks.AzurePackagesPath);
                    this.ShowRequiredDialogs(tasks.AzurePackagesPath);
                }
                catch (FormatException formatexception)
                {
                    buildSuccessful = false;
                    this.Invoke((MethodInvoker)(() => formatexception.ShowUIException()));
                    this.backgroundWorker.ReportProgress(stepNumber, ReportStatus.Fail);
                }
                catch (OperationCanceledException exception)
                {
                    buildSuccessful = false;
                    this.Invoke((MethodInvoker)(() => exception.ShowUIException()));
                    this.backgroundWorker.ReportProgress(stepNumber, ReportStatus.Fail);
                }
                catch (Exception exception)
                {
                    buildSuccessful = false;
                    this.Invoke((MethodInvoker)(() => exception.ShowGenericException("Error in building application.")));
                    this.backgroundWorker.ReportProgress(stepNumber, ReportStatus.Fail);
                }
                finally
                {
                    // in case there is an exception and we are came here directly then check for log paths to enable.
                    if (this.msbuildLogPath == null)
                    {
                        this.msbuildLogPath = tasks.MSbuildLogPath;
                    }

                    if (this.teamfoundationExeLogPath == null)
                    {
                        this.teamfoundationExeLogPath = tasks.TFLogPath;
                    }

                    this.backgroundWorker.ReportProgress(++stepNumber, "Deleting temporary TFS workspace....");
                    tasks.DeleteDedicatedTfsWorkSpace();
                }
            }

            // enable ths user to see log paths
            this.Invoke((MethodInvoker)delegate
            {
                this.btnViewBuildLog.Enabled = File.Exists(this.msbuildLogPath);
                this.btnViewTFLog.Enabled = File.Exists(this.teamfoundationExeLogPath);
            });

            return buildSuccessful;
        }

        /// <summary>
        /// Assigns the cloud args after build.
        /// </summary>
        /// <param name="msbuildLogPath">The msbuild log path.</param>
        /// <param name="tflogPath">The tf log path.</param>
        /// <param name="azurePackagesPath">The azure packages path.</param>
        private void AssignCloudArgsAfterBuild(string msbuildLogPath, string tflogPath, string azurePackagesPath)
        {
            if (msbuildLogPath == null) throw new ArgumentNullException("msbuildLogPath");
            if (tflogPath == null) throw new ArgumentNullException("tflogPath");
            if (azurePackagesPath == null) throw new ArgumentNullException("azurePackagesPath");

            // Assign log paths so that user can see them. See view log button click for details
            this.msbuildLogPath = msbuildLogPath;
            this.teamfoundationExeLogPath = tflogPath;
        
            // This is for cloud deployment'
            this.cloudArgs.IsBuildCompleted = true;
            string cspkgFilePath = Directory.GetFiles(azurePackagesPath, "*.cspkg").First();
            string configFilePath = Directory.GetFiles(azurePackagesPath, "*.cscfg").First();
            this.cloudArgs.PackageFilePath = cspkgFilePath;
            this.cloudArgs.ServiceConfigFilePath = configFilePath;
            this.cloudArgs.AzureLogFilePath = tflogPath.Replace(Constants.TFSLogIdentifier, Constants.AzureLogIdentifier);
            this.cloudDeploymentLogPath = this.cloudArgs.AzureLogFilePath;
        }

        /// <summary>
        /// Prepares the build args.
        /// </summary>
        /// <returns>
        /// Build arguments that enable the application to be built
        /// </returns>
        private BuildArgs PrepareBuildArgs()
        {
            if (this.selectedtfsConnectionString == null)
            {
                throw new ArgumentNullException("Invalid TFS settings");
            }

            return new BuildArgs
            {
                TFSUrl = this.selectedtfsConnectionString.GenerateTFSUrl(), 
                SolutionName = txtSolutionName.Text, 
                DeploymentLogPath = Settings.Default.DeploymentLogPath, 
                TFSWorkingPath = Settings.Default.TFSWorkingPath,
                Version = UtilityLibrary.GetUniqueVersionForSolution(this.txtSolutionName.Text)
            };
        }

        /// <summary>
        /// Logs the details into database.
        /// </summary>
        /// <param name="isSuccessful">if set to <c>true</c> [is successful].</param>
        /// <param name="status">The status.</param>
        /// <param name="version">The version.</param>
        /// <param name="environment">The environment.</param>
        private void LogDeploymentDetailsIntoDatabase(bool isSuccessful, DeploymentStatus status, string version, string environment)
        {
            string solutionNameFromPackage = Path.GetFileNameWithoutExtension(txtPackagePath.Text);
            BuildTransactionEntry buildItem = new BuildTransactionEntry
            {
                DeploymentDateTime = DateTime.Now,
                DeploymentNotes = txtDeploymentNotes.Text,
                IsSuccess = isSuccessful,
                SolutionName = string.IsNullOrEmpty(txtSolutionName.Text) ? solutionNameFromPackage : txtSolutionName.Text,
                PerformedBy = string.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName),
                Tfslabelused = string.IsNullOrEmpty(txtTFSLabelName.Text) ? Constants.EmptyTFSLabel : txtTFSLabelName.Text,
                Status = status,
                Version= version,
                Environment = environment
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
            serviceConfigViewer.TopMost = true;

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
        /// Prepares the cloud args.
        /// </summary>
        /// <returns>
        /// Cloud arguments collected from winforms
        /// </returns>
        private CloudArgs PrepareCloudArgs()
        {
            // Service config file url and azure package file need to be set later
            return new CloudArgs
            {
                NumberOfTries = int.Parse(Settings.Default.NumberOfTries),
                StorageName = this.cbxStorageNames.SelectedValue == null ? this.cbxStorageNames.Text : this.cbxStorageNames.SelectedValue.ToString(),
                HostedServiceName = this.cbxHostedNames.SelectedValue == null ? this.cbxHostedNames.Text : this.cbxHostedNames.SelectedValue.ToString(),
                DeploymentSlot = this.cbxDeploymentSlot.SelectedItem == null ? string.Empty : this.cbxDeploymentSlot.SelectedItem.ToString(),
                AzureLocation = this.cbxLocations.SelectedValue == null ? string.Empty : this.cbxLocations.SelectedValue.ToString(),
                IsUpgrade = chbxIsUpgrade.Checked,
                ManagementCertificatebase64string = this.managementCertificateBase64,
                SubscriptionId = this.cbxSubscriptions.SelectedValue == null ? string.Empty : this.cbxSubscriptions.SelectedValue.ToString(),
                IsBuildCompleted = false,
                CertificatePaths = this.certsToDeploy
            };
        }

        /// <summary>
        /// Loads the subscriptions details.
        /// </summary>
        private void LoadSubscriptionsDetails()
        {
            this.Invoke((MethodInvoker)delegate
            {
                // File is not more than 10kb.
                using (WaitingDialog waitingDialog = new WaitingDialog("Please wait while we get subscription details"))
                {
                    waitingDialog.ShowInTaskbar = false;
                    waitingDialog.StartPosition = FormStartPosition.CenterParent;
                    try
                    {
                        this.Enabled = false;
                        if (cbxSubscriptions.SelectedValue != null)
                        {
                            string selectedItem = cbxSubscriptions.SelectedValue.ToString();

                            // Some fix to overcome anonymous object
                            string subscriptionID = selectedItem.Split(' ').Count() > 1 ? selectedItem.Split(' ')[3].Trim(' ', ',') : selectedItem;

                            Dictionary<string, string> locations = null;
                            List<string> hostedServices = null;
                            List<string> storageNames = null;
                            do
                            {
                                waitingDialog.ShowDialog(this);
                                locations = CloudDeploymentTasks.ListLocationsForSubscription(subscriptionID, new X509Certificate2(Convert.FromBase64String(this.managementCertificateBase64)), "2011-10-01");
                                if (locations != null)
                                {
                                    this.cbxLocations.DataSource = locations.ToList();
                                    this.cbxLocations.DisplayMember = "value";
                                    this.cbxLocations.ValueMember = "key";
                                }

                                hostedServices = CloudDeploymentTasks.ListHostedServicesForSubscription(subscriptionID, new X509Certificate2(Convert.FromBase64String(this.managementCertificateBase64)), "2011-10-01");
                                if (hostedServices != null)
                                {
                                    this.cbxHostedNames.DataSource = hostedServices;
                                    this.cbxHostedNames.SelectedIndex = -1;
                                }

                                storageNames = CloudDeploymentTasks.ListStorageAccountsForSubscription(subscriptionID, new X509Certificate2(Convert.FromBase64String(this.managementCertificateBase64)), "2011-10-01");
                                if (storageNames != null)
                                {
                                    this.cbxStorageNames.DataSource = storageNames;
                                    this.cbxStorageNames.SelectedIndex = -1;
                                }
                            }
                            while (locations == null && hostedServices == null && storageNames == null);
                        }
                    }
                    finally
                    {
                        this.Enabled = true;
                    }
                }
            });
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Handles the Click event of the btnDeploy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            try
            {
                bool isLocalDeployment = this.tabControl1.SelectedTab.Name.Equals("tabLocalDeployment");

                this.Text = this.Text.Contains(this.txtSolutionName.Text) ? this.Text : string.Concat(this.Text, "-[", this.txtSolutionName.Text, "]");
                this.progressBar.Maximum = this.progressBar.Step * this.totalStepsIdentified;  // number of task
                this.btnViewBuildLog.Enabled = this.btnViewDeployLog.Enabled = this.btnViewTFLog.Enabled = false;

                if (!isLocalDeployment)
                {
                    this.buildArgs = this.PrepareBuildArgs();
                    this.buildArgs.Validate();

                    //replace with new name
                    this.buildArgs.ModifiedSolutionName = string.Concat(this.buildArgs.SolutionName, "-", this.buildArgs.Version);
                }
                else
                {
                    if (string.IsNullOrEmpty(this.txtPackagePath.Text) || !File.Exists(this.txtPackagePath.Text))
                    {
                        throw new ArgumentException("Invalid package file path", "Package Path");
                    }

                    if (string.IsNullOrEmpty(this.txtConfigPath.Text) || !File.Exists(this.txtConfigPath.Text))
                    {
                        throw new ArgumentException("Invalid configuration file path", "Config Path");
                    }

                    // save this in form variable to avoid STA
                    this.pkgNameForLocalDeployment = Path.GetFileNameWithoutExtension(this.txtPackagePath.Text);
                }

                this.cloudArgs = this.PrepareCloudArgs();

                // validate the publish setting path and subscriptions combo
                if (string.IsNullOrEmpty(this.txtPublishSettingsFilePath.Text) || !File.Exists(this.txtPublishSettingsFilePath.Text))
                {
                    throw new ArgumentException("Invalid publish settings file path", "Publish Settings File");
                }

                if (this.cbxSubscriptions.SelectedIndex == -1)
                {
                    throw new ArgumentException("Atleast one subscriptions should be selected", "Subscriptions");
                }

                this.cloudArgs.Validate();

                this.ResetTreeViewNodes(this.tvCheckList.Nodes["10"]);
                this.ResetTreeViewNodes(this.tvCheckList.Nodes["20"]);
                this.progressBar.Value = this.progressBar.Minimum;


                if (!this.backgroundWorker.IsBusy)
                {
                    this.SetUserActionOnFrom(false);
                    this.backgroundWorker.RunWorkerAsync(isLocalDeployment);
                }
            }
            catch (ArgumentNullException argEx)
            {
                argEx.ShowUIException();
            }
            catch (ArgumentException argEx)
            {
                argEx.ShowUIException();
            }
        }

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool isLocalDeployment = e.Argument != null && (bool)e.Argument;
            this.backgroundWorker.ReportProgress(10, "Initializing deployment tasks....");

            // log details into database 
            string updatedversion = this.buildArgs == null ? (!string.IsNullOrEmpty(this.rollbackVersionToLog) ? this.rollbackVersionToLog : UtilityLibrary.GetUniqueVersionForSolution(this.pkgNameForLocalDeployment)) : this.buildArgs.Version;
            this.LogDeploymentDetailsIntoDatabase(true, DeploymentStatus.Deploying, updatedversion, this.cloudArgs.DeploymentSlot);
            if (this.PerformBuildOperation(isLocalDeployment, updatedversion))
            {
                this.backgroundWorker.ReportProgress(20, "Starting cloud deployment task....");
                string solutionNameFromPackage = Path.GetFileNameWithoutExtension(this.txtPackagePath.Text);

                if (this.PerformCloudDeployment())
                {
                    if (isLocalDeployment)
                    {
                        UtilityLibrary.UpdateDeploymentLogStatus(solutionNameFromPackage, this.cloudArgs.InternalVersionNumber, this.cbxIsRollback.Checked ? DeploymentStatus.Rollback : DeploymentStatus.Deployed);
                    }
                    else
                    {
                        UtilityLibrary.UpdateDeploymentLogStatus(this.buildArgs.SolutionName, this.buildArgs.Version, DeploymentStatus.Deployed);
                    }

                    this.Invoke((MethodInvoker)(() => string.Format("Application deployed successfully on cloud.Will be up in a moment.").ShowUIInformation(FormStartPosition.CenterScreen)));
                }
                else
                {
                    if (isLocalDeployment)
                    {
                        UtilityLibrary.UpdateDeploymentLogStatus(solutionNameFromPackage, this.cloudArgs.InternalVersionNumber, DeploymentStatus.Failed);
                    }
                    else
                    {
                        UtilityLibrary.UpdateDeploymentLogStatus(this.buildArgs.SolutionName, this.buildArgs.Version, DeploymentStatus.Failed);
                    }
                }
            }
            else
            {
                UtilityLibrary.UpdateDeploymentLogStatus(this.buildArgs.SolutionName, this.buildArgs.Version, DeploymentStatus.Failed);
                this.backgroundWorker.ReportProgress(20, ReportStatus.Fail);
            }
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
                if (e.UserState.ToString() == "Fail")
                {
                    this.progressBar.Value = this.progressBar.Maximum;
                    this.toolStripStatusLabel.Text = "Process failed.";
                    for (int i = e.ProgressPercentage; i <= (e.ProgressPercentage >= 20 ? 25 : 15); i++)
                    {
                        this.tvCheckList.Nodes.Find("15", true).FirstOrDefault().ImageIndex = 1;
                        this.tvCheckList.Nodes.Find(i.ToString(), true).FirstOrDefault().ImageIndex = 3;
                    }

                    return;
                }

                this.toolStripStatusLabel.Text = e.UserState.ToString();
            }

            this.progressBar.PerformStep();
            if (this.tvCheckList.Nodes.Find(e.ProgressPercentage >= 25 ? "25" : e.ProgressPercentage.ToString(CultureInfo.InvariantCulture), true).FirstOrDefault().ImageIndex != 3)
            {
                this.tvCheckList.Nodes.Find(e.ProgressPercentage >= 25 ? "25" : e.ProgressPercentage.ToString(CultureInfo.InvariantCulture), true).
                    FirstOrDefault().ImageIndex = 0;
            }

            int nodenum = e.ProgressPercentage - 1;
            
            // These are exceptions
            nodenum = e.ProgressPercentage == 20 ? 15 : nodenum;
            nodenum = e.ProgressPercentage == 10 ? 10 : nodenum;
            TreeNode node = this.tvCheckList.Nodes.Find(nodenum.ToString(CultureInfo.InvariantCulture), true).FirstOrDefault();
            if (node != null && node.ImageIndex != 3)
            {
                node.ImageIndex = 1;
            }

            // Need this dirty tweak to show the UI that build never happened
            if (this.tabControl1.SelectedTab.Name.Equals("tabLocalDeployment"))
            {
                this.ResetTreeViewNodes(this.tvCheckList.Nodes["10"]);
                if (e.ProgressPercentage >= 25)
                {
                    this.progressBar.Value = this.progressBar.Maximum;
                }
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (File.Exists(this.cloudDeploymentLogPath))
            {
                this.btnViewDeployLog.Enabled = true;
            }

            // this is to reload the azure settings on screen
            this.ExtractLoadPubSettingsFile();
            this.SetUserActionOnFrom(true);

            // bring to front once completed
            this.BringToFront();
        }

        /// <summary>
        /// Handles the Click event of the btnViewTFLog, btnViewDeployLog and btnViewBuildLog control.
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
                case "btnViewDeployLog":
                    path = this.cloudDeploymentLogPath;
                    break;
            }

            if (string.IsNullOrEmpty(path))
            {
                // This case doesnot exist
                return;
            }

            using (LogViewer viewer = new LogViewer(path))
            {
                viewer.ShowDialog(this);
            }
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
                "Please wait until the process completes and try again.".ShowUIInformation();
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLoadTFSlabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnLoadTFSlabel_Click(object sender, EventArgs e)
        {
            try
            {
                using (BrowseLabels browselabel = new BrowseLabels(this.selectedtfsConnectionString.GenerateTFSUrl()))
                {
                    browselabel.StartPosition = FormStartPosition.CenterParent;
                    if (browselabel.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.labelSearchCriteria = browselabel.ValidSearchCriteria;
                        if (this.labelSearchCriteria != null)
                        {
                            this.txtTFSLabelName.Text = this.labelSearchCriteria.LabelName;
                            this.btnLoadSolutions.Enabled = true;
                            this.txtSolutionName.Text = string.Empty;
                        }
                    }
                }
            }
            catch (ArgumentNullException exception)
            {
                exception.ShowUIException();
            }
            catch (ArgumentException exception)
            {
                exception.ShowUIException();
            }
            finally
            {
                this.BringToFront();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLoadSolutions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnLoadSolutions_Click(object sender, EventArgs e)
        {
            try
            {
                using (BrowseSolutions browseSolutions = new BrowseSolutions(this.selectedtfsConnectionString.GenerateTFSUrl(), this.labelSearchCriteria))
                {
                    if (browseSolutions.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        this.txtSolutionName.Text = Path.GetFileNameWithoutExtension(browseSolutions.SolutionName);
                    }
                }
            }
            catch (ArgumentNullException exception)
            {
                exception.ShowUIException();
            }
            finally
            {
                this.BringToFront();
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
        private void CbxServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.btnLoadTFSlabel.Enabled = this.cbxServerName.SelectedIndex > -1;

                if (cbxServerName.SelectedValue != null)
                {
                    txtTFSDefaultCollection.Text = Convert.ToString(cbxServerName.SelectedValue);
                    this.selectedtfsConnectionString = this.cbxServerName.Text.GetTFSConnectionString();
                    if (this.selectedtfsConnectionString == null)
                    {
                        throw new NullReferenceException("Cannot find TFS settings for given server name.");
                    }
                }
            }
            catch (NullReferenceException exception)
            {
                exception.ShowUIException();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnManageTFSsettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnManageTFSsettings_Click(object sender, EventArgs e)
        {
            using (AppConfigManager appManager = new AppConfigManager())
            {
                appManager.StartPosition = FormStartPosition.CenterParent;
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
        private void TxtSolutionName_TextChanged(object sender, EventArgs e)
        {
            this.btnDeploy.Enabled = !string.IsNullOrEmpty(this.txtSolutionName.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnBrowsePackage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnBrowsePackage_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
            {
                return;
            }

            openFileDialog.Reset();
            openFileDialog.FileName = string.Empty;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                case "btnLoadCerts":
                    openFileDialog.Multiselect = true;
                    openFileDialog.Filter = "Certificate file(s) | *.pfx";
                    openFileDialog.DefaultExt = Constants.ServiceCertificateExtension;
                    break;
            }

            openFileDialog.ShowDialog(this);
        }

        /// <summary>
        /// Handles the FileOk event of the openFileDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            if (openFileDialog.DefaultExt == Constants.AzurePackageExtension)
            {
                this.txtPackagePath.Text = openFileDialog.FileName;
            }
            else if (openFileDialog.DefaultExt == Constants.ServiceConfigurationExtension)
            {
                this.txtConfigPath.Text = openFileDialog.FileName;
            }
            else if (openFileDialog.DefaultExt == Constants.PublishSettingsExtension)
            {
                this.txtPublishSettingsFilePath.Text = openFileDialog.FileName;

                try
                {
                    this.ExtractLoadPubSettingsFile();
                }
                catch (InvalidOperationException exec)
                {
                    this.txtPublishSettingsFilePath.Text = string.Empty;
                    exec.WriteToLog();
                    "Invalid settings file".ShowUIInformation();
                }
                catch (XmlException exec)
                {
                    this.txtPublishSettingsFilePath.Text = string.Empty;
                    exec.WriteToLog();
                    "Invalid settings file".ShowUIInformation();
                }
            }
            else if (openFileDialog.DefaultExt == Constants.ServiceCertificateExtension)
            {
                string files = openFileDialog.FileNames.Where(x => !string.IsNullOrEmpty(x)).Aggregate(string.Empty, (current, item) => string.Concat(current, ";", item));

                this.txtCertificatePaths.Text = files.TrimStart(';');
                this.certsToDeploy = openFileDialog.FileNames.ToList<string>();
            }
        }

        /// <summary>
        /// Extracts the load pub settings file.
        /// </summary>
        private void ExtractLoadPubSettingsFile()
        {
            XDocument xdoc = XDocument.Load(this.txtPublishSettingsFilePath.Text);
            this.managementCertificateBase64 =
                xdoc.Descendants("PublishProfile").Single().Attribute("ManagementCertificate").Value;
            this.cbxSubscriptions.DataSource = xdoc.Descendants("Subscription").Select(x =>
                                                                                           {
                                                                                               var xAttribute = x.Attribute("Id");
                                                                                               var attribute =
                                                                                                   x.Attribute("Name");
                                                                                               if (attribute != null)
                                                                                                   return (xAttribute != null
                                                                                                               ? new
                                                                                                                     {
                                                                                                                         Id =
                                                                                                                     xAttribute.
                                                                                                                     Value,
                                                                                                                         Name =
                                                                                                                     attribute.
                                                                                                                     Value
                                                                                                                     }
                                                                                                               : null);
                                                                                               return null;
                                                                                           }).ToList();
            this.cbxSubscriptions.DisplayMember = "Name";
            this.cbxSubscriptions.ValueMember = "Id";
        }

        /// <summary>
        /// Handles the Selecting event of the tabControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TabControlCancelEventArgs"/> instance containing the event data.</param>
        private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            using(AdvancedMessageBox advbox = new AdvancedMessageBox("Are you sure you want to cancel current operation?", ReportStatus.Information, true))
            {
                if (DialogResult.OK == advbox.ShowDialog())
                {
                    this.ResetTreeViewNodes(this.tvCheckList.Nodes["10"]);
                    this.ResetTreeViewNodes(this.tvCheckList.Nodes["20"]);
                    this.progressBar.Value = this.progressBar.Minimum;
                    this.btnViewBuildLog.Enabled = this.btnViewDeployLog.Enabled = this.btnViewTFLog.Enabled = false;
                    this.toolStripStatusLabel.Text = string.Empty;
                    this.rollbackVersionToLog = string.Empty;
                    this.pkgNameForLocalDeployment = string.Empty;
                    this.buildArgs = null;

                    switch (e.TabPageIndex)
                    {
                        case 0:
                            this.txtConfigPath.Text = this.txtPackagePath.Text = string.Empty;
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
            }
            this.BringToFront();
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkDownload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void LnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (SubscriptionDownloadPage subPage = new SubscriptionDownloadPage())
                {
                    subPage.StartPosition = FormStartPosition.CenterParent;
                    subPage.ShowDialog(this);
                }
            }
            catch (UriFormatException formatEx)
            {
                formatEx.ShowUIException();
            }
            catch (Exception ex)
            {
                ex.ShowGenericException();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbxSubscriptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CbxSubscriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (backgroundThread == null || !backgroundThread.IsAlive)
            {
                backgroundThread = new Thread(new ThreadStart(this.LoadSubscriptionsDetails));
                backgroundThread.Start();
            }
        }

        /// <summary>
        /// Handles the BeforeSelect event of the tvCheckList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewCancelEventArgs"/> instance containing the event data.</param>
        private void tvCheckList_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the TextChanged event of the cbxHostedNames control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxHostedNames_TextChanged(object sender, EventArgs e)
        {
            this.chbxIsUpgrade.Checked = this.cbxHostedNames.Items.Contains(this.cbxHostedNames.Text);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtPackagePath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtPackagePath_TextChanged(object sender, EventArgs e)
        {
            this.btnDeploy.Enabled = this.tabControl1.SelectedTab.Name.Equals("tabLocalDeployment")
                && !string.IsNullOrEmpty(this.txtConfigPath.Text) 
                && !string.IsNullOrEmpty(this.txtPackagePath.Text);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the cbxIsRollback control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxIsRollback_CheckedChanged(object sender, EventArgs e)
        {
            this.btnDeploy.Text = this.cbxIsRollback.Checked ? "R&ollback" : this.btnDeploy.Text = "&Deploy";
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbxStorageNames control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxStorageNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.isNewStorage = this.cbxStorageNames.SelectedValue == null;
        }
        #endregion
    }
}
