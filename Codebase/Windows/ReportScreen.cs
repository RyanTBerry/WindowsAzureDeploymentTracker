// -----------------------------------------------------------------------
// <copyright file="ReportScreen.cs" company="Microsoft IT">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Utilities;
    using System.IO;
    using Services.Entities;
    using Dialogs;

    public partial class ReportScreen : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportScreen"/> class.
        /// </summary>
        public ReportScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the ReportScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReportScreen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'deploymentTrackerLocalDBDataSet.BuildsRecordTable' table. You can move, or remove it, as needed.
            this.buildsRecordTableTableAdapter.Fill(this.deploymentTrackerLocalDBDataSet.BuildsRecordTable);
            this.dgvLogView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            this.BindStatusCombo();
            this.BindEnvironmentCombo();
            this.RemoveApplicationDuplicates();
            this.RemoveNameDuplicates();
        }

        private void RemoveNameDuplicates()
        {
            List<string> app = new List<string>();
            app.Add(Constants.AllItemsFilterValue);
            foreach (var item in this.cbxPerformedBy.Items)
            {
                app.Add(cbxPerformedBy.GetItemText(item));
            }
            
            this.cbxPerformedBy.DataSource = app.Distinct().ToList();
        }

        /// <summary>
        /// Removes the application duplicates.
        /// </summary>
        private void RemoveApplicationDuplicates()
        {
            List<string> app = new List<string>();
            app.Add(Constants.AllItemsFilterValue);
            foreach (var item in this.cbxApplication.Items)
            {
                app.Add(cbxApplication.GetItemText(item));
            }

            this.cbxApplication.DataSource = app.Distinct().ToList();
        }

        /// <summary>
        /// Binds the environment combo.
        /// </summary>
        private void BindEnvironmentCombo()
        {
            this.cbxEnvironment.Items.Clear();
            Array values = Enum.GetValues(typeof(DeploymentEnvironment));
            this.cbxEnvironment.Items.Add(Constants.AllItemsFilterValue);
            foreach (DeploymentStatus val in values)
            {
                this.cbxEnvironment.Items.Add(Enum.GetName(typeof(DeploymentEnvironment), val));
            }

            this.cbxEnvironment.SelectedItem = Constants.AllItemsFilterValue;
        }

        /// <summary>
        /// Binds the status combo.
        /// </summary>
        private void BindStatusCombo()
        {
            this.cbxStatus.Items.Clear();
            Array values = Enum.GetValues(typeof(DeploymentStatus));
            this.cbxStatus.Items.Add(Constants.AllItemsFilterValue);
            foreach( DeploymentStatus val in values )
            {
                this.cbxStatus.Items.Add(Enum.GetName(typeof(DeploymentStatus), val));
            }

            this.cbxStatus.SelectedItem = Constants.AllItemsFilterValue;
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgvLogView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvLogView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 || e.ColumnIndex ==-1 || this.dgvLogView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    return;
                }
                string status = Convert.ToString(this.dgvLogView.Rows[e.RowIndex].Cells["statusDataGridViewTextBoxColumn"].Value);
                if (status.Equals(DeploymentStatus.Deploying.ToString()))
                {
                    "Deployment is going on. Please wait until it complete".ShowUIInformation();
                    return;
                }

                string cmdValue = this.dgvLogView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().ToLower();
                string application = Convert.ToString(this.dgvLogView.Rows[e.RowIndex].Cells["applicationDataGridViewTextBoxColumn"].Value);
                string version = this.dgvLogView.Rows[e.RowIndex].Cells["versionDataGridViewTextBoxColumn"].Value.ToString();

                switch (cmdValue)
                {
                    case "details":
                        Convert.ToString(this.dgvLogView.Rows[e.RowIndex].Cells["DeploymentNotes"].FormattedValue).PopInformationDialog(string.Format("Deployment Notes: {0}v{1}", application, version));
                        break;
                    case "log":
                        string solutionFolderName =
                            string.Concat(application, "-", version,
                                          status.Equals(DeploymentStatus.Rollback.ToString()) ||
                                          (status.Equals(DeploymentStatus.Failed.ToString()) &&
                                           UtilityLibrary.IsRollbackVersion(application, version))
                                              ? Constants.RollbackFolderExtension
                                              : string.Empty);

                        using (LogViewerStrip logStrip = new LogViewerStrip(solutionFolderName))
                        {
                            logStrip.StartPosition = FormStartPosition.CenterParent;
                            logStrip.ShowDialog();
                        }

                        break;
                    case "rollback":
                        // This is for action column only. Status column value is interfering here
                        string headerTextForVersion = this.dgvLogView.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.HeaderText;
                        if (!string.IsNullOrEmpty(headerTextForVersion))
                        {
                            return;
                        }

                        if (!UtilityLibrary.CanRollback(application))
                        {
                            string.Format("Please wait until all deployements of {0} are completed.", application).ShowUIInformation();
                            return;
                        }

                        string lastVersion = UtilityLibrary.GetLastSuccessfulVersionForSolution(application, version);
                        using (PackageDeployment deployScreen = new PackageDeployment(true, lastVersion, version, application) { StartPosition = FormStartPosition.CenterScreen })
                        {
                            deployScreen.ShowDialog(this);
                        }

                        break;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                // show this is info instead of error
                ex.Message.ShowUIInformation();
            }
            catch (Exception ex)
            {
                ex.ShowGenericException();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnApplyFilters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            if (this.fromDateControl.Value > this.toDateControl.Value)
            {
                "From date cannot be greater than to date".ShowUIInformation();
                return;
            }

            // TODO: This line of code loads data into the 'deploymentTrackerLocalDBDataSet.BuildsRecordTable' table. You can move, or remove it, as needed.
            this.buildsRecordTableTableAdapter.Fill(this.deploymentTrackerLocalDBDataSet.BuildsRecordTable);
            var dv = this.deploymentTrackerLocalDBDataSet.BuildsRecordTable.DefaultView;
            dv.RowFilter = this.GetFilterExpression();
            dv.Sort = "Date DESC";
            dgvLogView.DataSource = dv;
            dgvLogView.Refresh();
            this.dgvLogView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// Gets the filter expression.
        /// </summary>
        /// <returns>Filter expression</returns>
        private string GetFilterExpression()
        {
            StringBuilder strExpr = new StringBuilder();

            if (this.cbxApplication.SelectedItem != null && this.cbxApplication.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                strExpr.Append(string.Format("Application ='{0}'", this.cbxApplication.SelectedItem == null ? string.Empty : this.cbxApplication.SelectedItem.ToString()));
            }

            if (this.fromDateControl.Value.Date != DateTime.Now.Date)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Date >= '{0}'", this.fromDateControl.Value.Date.ToString()));
            }

            if (this.toDateControl.Value.Date != DateTime.Now.Date)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Date <= '{0}'", this.toDateControl.Value.Date.ToString()));
            }

            if (this.cbxPerformedBy.SelectedItem != null && this.cbxPerformedBy.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("PerformedBy ='{0}'", this.cbxPerformedBy.SelectedItem == null ? string.Empty : this.cbxPerformedBy.SelectedItem.ToString()));
            }

            if (!string.IsNullOrEmpty(this.txtVersion.Text))
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Version = '{0}'", this.txtVersion.Text));
            }

            if (this.cbxEnvironment.SelectedItem != null && this.cbxEnvironment.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Environment ='{0}'", this.cbxEnvironment.SelectedItem == null ? string.Empty : this.cbxEnvironment.SelectedItem.ToString()));
            }

            if (this.cbxStatus.SelectedItem != null && this.cbxStatus.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Status ='{0}'", this.cbxStatus.SelectedItem == null ? string.Empty : this.cbxStatus.SelectedItem.ToString()));
            }

            return strExpr.ToString();
        }

        /// <summary>
        /// Handles the ValueChanged event of the fromDateControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void fromDateControl_ValueChanged(object sender, EventArgs e)
        {
            if (this.fromDateControl.Value > this.toDateControl.Value)
            {
                "From date cannot be greater than to date".ShowUIInformation();
            }
        }
    }
}
