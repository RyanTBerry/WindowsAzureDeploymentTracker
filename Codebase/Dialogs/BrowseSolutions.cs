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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using DeploymentTracker.Services.Entities;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using DeploymentTracker.App.Utilities;
    using System.Net;
    using System.Reflection;

    /// <summary>
    /// Form to browse solutions in TFS label
    /// </summary>
    public partial class BrowseSolutions : Form
    {
        /// <summary>
        /// TFS Url to connect
        /// </summary>
        private string tfsUrl;

        /// <summary>
        /// Flags to cancel search
        /// </summary>
        private bool mCompleted, mClosePending;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseSolutions"/> class.
        /// </summary>
        /// <param name="tfsUrl">The TFS URL.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        public BrowseSolutions(string tfsUrl, TFSLabel searchCriteria)
        {
            this.tfsUrl = tfsUrl;
            this.InitializeComponent();
            this.btnSelectLabel.Enabled = false;
            this.ValidSearchCriteria = searchCriteria;
            this.txtLabelFilter.Text = searchCriteria.LabelName;
            this.txtOwnerFilter.Text = searchCriteria.OwnerName;
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
        /// Gets or sets the valid search criteria.
        /// </summary>
        /// <value>
        /// The valid search criteria.
        /// </value>
        private TFSLabel ValidSearchCriteria
        {
            get;
            set;
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

                TFSLabel searchCriteria = e.Argument as TFSLabel;
                using (TfsTeamProjectCollection tfs = new TfsTeamProjectCollection(new Uri(this.tfsUrl)))
                {
                    VersionControlServer vcserver = tfs.GetService<VersionControlServer>();
                    VersionControlLabel labels = vcserver.QueryLabels(
                        string.IsNullOrEmpty(searchCriteria.LabelName) ? null : searchCriteria.LabelName,
                        "$/",
                        string.IsNullOrEmpty(searchCriteria.OwnerName) ? null : searchCriteria.OwnerName,
                        true,
                        null,
                        VersionSpec.Latest,
                        true).First();

                    if (labels == null)
                    {
                        e.Result = null;
                        throw new KeyNotFoundException("No label exist with given search criteria. Please try again.");
                    }
                    else
                    {
                        var serverItems = from item in labels.Items.Where(x => x.ServerItem.Contains(".sln"))
                                          select new
                                              {
                                                  SolutionName = item.ServerItem.Split('/').Last()
                                              };

                        e.Result = serverItems.Count() == 0 ? null : serverItems;
                        this.toolStripStatusLabel1.Text = "Search Completed";
                    }
                }
            }
            catch (KeyNotFoundException keyEx)
            {
                keyEx.ShowUIException();
            }
            catch (WebException ex)
            {
                ex.ShowUIException();
            }
            catch (Exception exception)
            {
                this.toolStripStatusLabel1.Text = exception.Message;
                exception.ShowGenericException();
            }
            finally
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.Cursor = Cursors.Default;
                    });
                }
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // To cancel
                mCompleted = true;
                if (mClosePending)
                {
                    this.Close();
                }

                if (e.Result != null)
                {
                    var result = e.Result;
                    bindingSource1.DataSource = result;
                    this.gdvTFSSolutions.AutoGenerateColumns = true;
                    this.gdvTFSSolutions.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                    this.gdvTFSSolutions.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    this.gdvTFSSolutions.Refresh();
                    this.gdvTFSSolutions.ReadOnly = true;
                    this.btnSelectLabel.Enabled = true;
                }
                else
                {
                    this.bindingSource1.DataSource = null;
                    this.gdvTFSSolutions.Refresh();
                    this.btnSelectLabel.Enabled = false;
                    this.toolStripStatusLabel1.Text = "No results found...";
                }
            }
            catch (TargetInvocationException ex)
            {
                ex.WriteToLog();
            }
            catch (Exception ex)
            {
                ex.ShowGenericException();
            }
            finally
            {
                this.btnFind.Enabled = true;
                this.Cursor = Cursors.Default;  
            }
        }

        /// <summary>
        /// Handles the Click event of the btnFind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnFind.Enabled = false;
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

        private void BrowseSolutions_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mCompleted)
            {
                this.toolStripStatusLabel1.Text = "Please wait while form is closing...";
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                    this.Enabled = false;
                    e.Cancel = true;
                    mClosePending = true;
                }
            }

            this.Hide();
        }
    }
}
