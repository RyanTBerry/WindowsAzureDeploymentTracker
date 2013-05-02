using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeploymentTracker.Utilities;

namespace DeploymentTracker.IWARM_ReportScreens
{
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
            this.InitializeComponent();
            this.dgvTFSsettings.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
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
        }

        /// <summary>
        /// Handles the Click event of the btnAddTFS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAddTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Add);
        }

        /// <summary>
        /// Shows the TFS connection string manager.
        /// </summary>
        /// <param name="operation">The operation.</param>
        private void ShowTFSConnectionStringManager(OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Add:
                    using (TFSConnectionEditor editor = new TFSConnectionEditor { StartPosition = FormStartPosition.CenterParent })
                    {
                        editor.ShowDialog(this);
                    }
                    break;
                case OperationType.Edit:
                    using (TFSConnectionEditor editor = new TFSConnectionEditor(this.selectedTFSConnectionString) { StartPosition = FormStartPosition.CenterParent })
                    {
                        editor.ShowDialog(this);
                    }
                    break;
                case OperationType.Delete:
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        this.selectedTFSConnectionString.DeleteTFSConnectionStringToDB();
                    }
                    break;
                default:
                    break;
            }

            this.tFSConnectionsTableAdapter.Fill(this.deploymentTrackerLocalDBTFSConnectionsDataSet.TFSConnections);
            this.dgvTFSsettings.Refresh();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgvTFSsettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void dgvTFSsettings_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dgvTFSsettings.SelectedRows.Count > 0)
            {
                this.selectedTFSConnectionString = new TFSConnectionString
                {
                    ServerName = this.dgvTFSsettings.SelectedRows[0].Cells[0].Value.ToString(),
                    PortNumber = int.Parse(this.dgvTFSsettings.SelectedRows[0].Cells[1].Value.ToString()),
                    DefaultCollection = this.dgvTFSsettings.SelectedRows[0].Cells[2].Value.ToString()
                };
            }
        }

        private void btnEditTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Edit);
        }

        private void btnDeleteTFS_Click(object sender, EventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Delete);
        }

        private void dgvTFSsettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ShowTFSConnectionStringManager(OperationType.Edit);
        }
    }
}
