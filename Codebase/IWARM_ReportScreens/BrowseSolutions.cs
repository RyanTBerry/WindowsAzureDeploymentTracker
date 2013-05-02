using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System.Collections;
using DeploymentTracker.Utilities;

namespace DeploymentTracker.IWARM_ReportScreens
{
    public partial class BrowseSolutions : Form
    {
        /// <summary>
        /// TFS Url to connect
        /// </summary>
        private string tfsUrl;

        /// <summary>
        /// Gets the valid search criteria.
        /// </summary>
        private LabelSearchCriteria ValidSearchCriteria
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the solution.
        /// </summary>
        /// <value>
        /// The name of the solution.
        /// </value>
        public string SolutionName
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseLabels"/> class.
        /// </summary>
        /// <param name="tfsUrl">The TFS URL.</param>
        public BrowseSolutions(string tfsUrl, LabelSearchCriteria searchCriteria)
        {
            this.tfsUrl = tfsUrl;
            this.InitializeComponent();
            this.btnSelectLabel.Enabled = false;
            this.ValidSearchCriteria = searchCriteria;
            this.txtLabelFilter.Text = searchCriteria.LabelName;
            this.txtOwnerFilter.Text = searchCriteria.OwnerName;
        }

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Cursor = Cursors.WaitCursor;
            });
            try
            {
                this.toolStripStatusLabel1.Text = "Searching solutions in TFS label....";

                LabelSearchCriteria searchCriteria = e.Argument as LabelSearchCriteria;
                TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(this.tfsUrl));
                VersionControlServer vcServer = tfs.GetService<VersionControlServer>();
                VersionControlLabel labels = vcServer.QueryLabels(
                    string.IsNullOrEmpty(searchCriteria.LabelName) ? null : searchCriteria.LabelName,
                    "$/", string.IsNullOrEmpty(searchCriteria.OwnerName) ? null: searchCriteria.OwnerName, 
                    true, null, VersionSpec.Latest, true).First();

                if (labels == null)
                {
                    MessageBox.Show("No label exist with given search criteria. Please try again.", "Invalid Filters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Result = null;
                }
                else
                {
                    var serverItems = from item in labels.Items.Where(x => x.ServerItem.Contains(".sln"))
                                    select new
                                        {
                                            SolutionName = item.ServerItem.Split('/').Last()
                                        };

                    e.Result = serverItems;
                }
            }
            finally
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Cursor = Cursors.Default;
                });
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripStatusLabel1.Text = "Search Completed";

            if (e.Result != null)
            {
                var result = e.Result;
                bindingSource1.DataSource = result;
                this.gdvTFSSolutions.AutoGenerateColumns = true;
                this.gdvTFSSolutions.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                this.gdvTFSSolutions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                this.gdvTFSSolutions.Refresh();
                this.gdvTFSSolutions.ReadOnly = true;
            }

            btnSelectLabel.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the btnFind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                backgroundWorker1.RunWorkerAsync(this.ValidSearchCriteria);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSelectLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnSelectLabel_Click(object sender, EventArgs e)
        {
            this.GetSelectedCriteria();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }


        /// <summary>
        /// Handles the CellMouseDoubleClick event of the gdvTFSLabels control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void GdvTFSLabels_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.GetSelectedCriteria();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Gets the selected criteria.
        /// </summary>
        private void GetSelectedCriteria()
        {
            this.SolutionName = this.gdvTFSSolutions.SelectedRows[0].Cells[0].Value.ToString();
        }
    }
}
