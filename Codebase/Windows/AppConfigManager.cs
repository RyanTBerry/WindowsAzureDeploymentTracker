// -----------------------------------------------------------------------
// <copyright file="AppConfigManager.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

using System.Xml;

namespace DeploymentTracker.App.Windows
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using DeploymentTracker.App.Dialogs;
    using DeploymentTracker.App.Utilities;
    using DeploymentTracker.Properties;
    using DeploymentTracker.Services.Entities;
    using DeploymentTracker.Services.Utilities;
    using System.Linq;

    /// <summary>
    /// Application config manager screen
    /// </summary>
    public partial class AppConfigManager : Form
    {
        /// <summary>
        /// Selected TFS connection string in data grid
        /// </summary>
        private TFSConnectionString selectedTFSConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigManager"/> class.
        /// </summary>
        public AppConfigManager()
        {
            try
            {
                UtilityLibrary.ValidateAppSettings();
                this.InitializeComponent();
                this.LoadSettings();
                this.dgvTFSsettings.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            catch (Exception e)
            {
                e.ShowGenericException("Application settings file corrupted or contain one or more invalid values");
            }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        private void LoadSettings()
        {
            this.txtAppLogFolder.Text = Settings.Default.ApplicationLogPath;
            this.txtAzurePublishSettings.Text = Settings.Default.AzurePublishSettingsDownloadLink;
            this.txtDeploymentLogFolder.Text = Settings.Default.DeploymentLogPath;
            this.txtParallelTasks.Text = Settings.Default.MaxSessionsPermitted;
            this.txtNumberOfTries.Text = Settings.Default.NumberOfTries;
            this.txtTFSWorkingFolder.Text = Settings.Default.TFSWorkingPath;
            this.txtLocalDBConnection.Text = Settings.Default.DeploymentTrackerLocalDBConnectionString;
            this.txtLocalDBConnection.ReadOnly = true;
        }

        /// <summary>
        /// Handles the Load event of the AppConfigManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AppConfigManager_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections' table. You can move, or remove it, as needed.
            this.tFSConnectionsTableAdapter.Fill(this.deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections);

            this.CheckForNoRows();
        }

        private void CheckForNoRows()
        {
            // Fix : 251237
            if (this.dgvTFSsettings.RowCount <= 0)
            {
                this.btnDeleteTFS.Enabled = this.btnEditTFS.Enabled = false;
            }
            else
            {
                this.btnDeleteTFS.Enabled = this.btnEditTFS.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddTFS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnAddTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Add);
        }

        /// <summary>
        /// Shows the TFS connection string manager.
        /// </summary>
        /// <param name="operation">The operation.</param>
        private void ShowTFSConnectionStringManager(OperationType operation)
        {
            try
            {
                switch (operation)
                {
                    case OperationType.Add:
                        using (TFSConnectionEditor editor = new TFSConnectionEditor())
                        {
                            editor.StartPosition = FormStartPosition.CenterParent;
                            editor.ShowDialog(this);
                        }

                        break;
                    case OperationType.Edit:
                        using (TFSConnectionEditor editor = new TFSConnectionEditor(this.selectedTFSConnectionString, true))
                        {
                            editor.StartPosition = FormStartPosition.CenterParent;
                            editor.ShowDialog(this);
                        }

                        break;
                    case OperationType.Delete:
                        using (AdvancedMessageBox advBox = new AdvancedMessageBox("Are you sure you want to continue?", ReportStatus.Information, true))
                        {
                            if (DialogResult.OK == advBox.ShowDialog(this))
                            {
                                this.selectedTFSConnectionString.DeleteTFSConnectionStringToDB();
                            }
                        }

                        break;
                    default:
                        break;
                }
                string selectedServername = this.selectedTFSConnectionString != null ? this.selectedTFSConnectionString.ServerName : string.Empty;
                this.tFSConnectionsTableAdapter.Fill(this.deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections);
                this.dgvTFSsettings.Refresh();
                if (!string.IsNullOrEmpty(selectedServername))
                {
                    foreach (DataGridViewRow item in this.dgvTFSsettings.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == selectedServername)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (InvalidOperationException invalidOps)
            {
                invalidOps.ShowUIException();
            }
            catch (ArgumentException argexception)
            {
                argexception.ShowUIException();
            }
            catch (DBConcurrencyException dbexception)
            {
                dbexception.ShowUIException();
            }
            catch (Exception e)
            {                
                e.ShowGenericException();
            }
            finally
            {
                this.CheckForNoRows();
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgvTFSsettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DgvTFSsettings_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvTFSsettings.SelectedRows.Count > 0)
            {
                this.selectedTFSConnectionString = new TFSConnectionString
                {
                    ServerName = this.dgvTFSsettings.SelectedRows[0].Cells[0].Value.ToString(),
                    PortNumber = int.Parse(this.dgvTFSsettings.SelectedRows[0].Cells[1].Value.ToString()),
                    DefaultCollection = this.dgvTFSsettings.SelectedRows[0].Cells[2].Value.ToString(),
                    IsHttps = bool.Parse(this.dgvTFSsettings.SelectedRows[0].Cells[3].Value.ToString())
                };
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEditTFS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnEditTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Edit);
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteTFS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDeleteTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Delete);
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvTFSsettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DgvTFSsettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            this.ShowTFSConnectionStringManager(OperationType.Edit);
        }

        /// <summary>
        /// Handles the Click event of the btnShowFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnShowFolder_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null)
            {
                return;
            }

            string selectedPath = string.Empty;
            if (DialogResult.OK == folderBrowserDialog.ShowDialog(this))
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }

            if (!string.IsNullOrEmpty(selectedPath))
            {
                switch (btn.Name)
                {
                    case "btnDeployLogFolder":
                        this.txtDeploymentLogFolder.Text = selectedPath;
                        break;
                    case "btnTFSWorkingFolder":
                        this.txtTFSWorkingFolder.Text = selectedPath;
                        break;
                    case "btnAppLogsFolder":
                        this.txtAppLogFolder.Text = selectedPath;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {
                // Check
                txtAppLogFolder.Text.VerifyNotNull("Application Log directory value", "Application Log directory value cannot be empty");
                txtDeploymentLogFolder.Text.VerifyNotNull("Deployment Log directory value", "Deployment Log directory value cannot be empty");
                txtTFSWorkingFolder.Text.VerifyNotNull("TFS Working path value", "TFS Working path value cannot be empty");
                txtParallelTasks.Text.VerifyInteger("Invalid MaxSesion permitted value");
                txtParallelTasks.Text.VerifyGreaterThanZero("Number of tasks should be greater than zero");
                txtNumberOfTries.Text.VerifyInteger("Invalid Number of tries value");
                txtNumberOfTries.Text.VerifyGreaterThanZero("Number of tries should be greater than zero");
                txtAzurePublishSettings.Text.VerifyNotNull("Azure publish Settings.", "Azure publishSettings Url cannot be empty");
                txtAzurePublishSettings.Text.VerifyUri(string.Format("Invalid Azure publishSettings Url : {0}", txtAzurePublishSettings.Text));
                txtAppLogFolder.Text.VerifyPath("Invalid path value Application Log directory");
                txtDeploymentLogFolder.Text.VerifyPath("Invalid path value Deployment Log Path");
                txtTFSWorkingFolder.Text.VerifyPath("Invalid path value TFS Working Path");


                // Save
                Settings.Default.ApplicationLogPath = txtAppLogFolder.Text;
                Settings.Default.AzurePublishSettingsDownloadLink = txtAzurePublishSettings.Text;
                Settings.Default.DeploymentLogPath = txtDeploymentLogFolder.Text;
                Settings.Default.MaxSessionsPermitted = txtParallelTasks.Text;
                Settings.Default.NumberOfTries = txtNumberOfTries.Text;
                Settings.Default.TFSWorkingPath = txtTFSWorkingFolder.Text;
                Settings.Default.Save();
                "Configurations saved successfully".ShowUIInformation();
            }
            catch (FormatException ex)
            {
                ex.ShowUIException();
            }
            catch (ArgumentNullException ex)
            {
                ex.ShowUIException();
            }
            catch (ArgumentException ex)
            {
                ex.ShowUIException();
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
