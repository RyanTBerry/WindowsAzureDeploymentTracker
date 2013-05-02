// -----------------------------------------------------------------------
// <copyright file="AppLogViewer.cs" company="Microsoft IT">
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
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using DeploymentTracker.Properties;
    using DeploymentTracker.Services.Entities;
    using DeploymentTracker.App.Utilities;
    using DeploymentTracker.Dialogs;

    /// <summary>
    /// Application logs browser
    /// </summary>
    public partial class AppLogViewer : Form
    {
        /// <summary>
        /// Selected log file to browse
        /// </summary>
        private string selectedLogFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLogViewer"/> class.
        /// </summary>
        public AppLogViewer()
        {
            this.InitializeComponent();
            this.dgvLogView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLogView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.BindControls();
        }

        #region PrivateFunctions
        
        /// <summary>
        /// Loads the log data.
        /// </summary>
        private void LoadLogData()
        {
            try
            {
                using (DataSet myDataSet = new DataSet())
                {
                    myDataSet.ReadXml(this.selectedLogFile);
                    if (myDataSet.Tables.Count <= 0)
                    {
                        "No Log Data Available".ShowUIInformation();
                        return;
                    }

                    var dv = myDataSet.Tables[0].DefaultView;
                    dv.RowFilter = this.GetFilterExpression();
                    dv.Sort = "DateTime DESC";
                    dgvLogView.DataSource = dv;
                    dgvLogView.Refresh();
                }
            }
            catch (FileNotFoundException)
            {
                "No log found on selected date".ShowUIInformation();
            }
        }

        /// <summary>
        /// Binds the controls.
        /// </summary>
        private void BindControls()
        {
            try
            {
                this.selectedLogFile = Path.Combine(
                    Settings.Default.ApplicationLogPath,
                                string.Concat(Constants.AppLogFileName, "-", dateTimePicker.Value.ToString("MMddyyyy"), ".Xml"));
                XDocument xdoc = XDocument.Load(this.selectedLogFile);
                XNamespace ns = xdoc.Root.GetDefaultNamespace();
                List<string> result = xdoc.Descendants(ns + "LogEntry").AsParallel().Select(element => element.Element(ns + "UserName")
                    .Value).Distinct().WithDegreeOfParallelism(5).ToList<string>();
                result.Insert(0, Constants.AllItemsFilterValue);
                cbxUsers.DataSource = result;
                this.BindLogTypeCombo();
                cbxUsers.SelectedItem = cbxLogType.SelectedItem = Constants.AllItemsFilterValue;
            }
            catch (FileNotFoundException)
            {
                "No log found on selected date".ShowUIInformation();
            }
        }

        /// <summary>
        /// Binds the log type combo.
        /// </summary>
        private void BindLogTypeCombo()
        {
            this.cbxLogType.Items.Clear();
            Array values = Enum.GetValues(typeof(LogType));
            this.cbxLogType.Items.Add(Constants.AllItemsFilterValue);
            foreach (LogType val in values)
            {
                this.cbxLogType.Items.Add(Enum.GetName(typeof(LogType), val));
            }

            this.cbxLogType.SelectedItem = Constants.AllItemsFilterValue;
        }
       
        /// <summary>
        /// Gets the filter expression.
        /// </summary>
        /// <returns>Returns filter expression string</returns>
        private string GetFilterExpression()
        {
            StringBuilder strExpr = new StringBuilder();
            if (this.cbxLogType.SelectedItem != null && this.cbxLogType.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                strExpr.Append(string.Format("LogType ='{0}'", this.cbxLogType.SelectedItem == null ? string.Empty : this.cbxLogType.SelectedItem.ToString()));
            }

            if (cbxUsers.SelectedItem != null && cbxUsers.SelectedItem.ToString() != Constants.AllItemsFilterValue)
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("UserName ='{0}'", this.cbxUsers.SelectedItem == null ? string.Empty : this.cbxUsers.SelectedItem.ToString()));
            }

            if (!string.IsNullOrEmpty(this.txtStackTrace.Text))
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("StackTrace like '%{0}%'", this.txtStackTrace.Text));
            }

            if (!string.IsNullOrEmpty(this.txtMessage.Text))
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Message like '%{0}%'", txtMessage.Text));
            }

            return strExpr.ToString();
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Handles the Click event of the btnLoadLogFile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnLoadLogFile_Click(object sender, EventArgs e)
        {
            this.LoadLogData();
        }

        /// <summary>
        /// Handles the RowPrePaint event of the dgvLogView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowPrePaintEventArgs"/> instance containing the event data.</param>
        private void DgvLogView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            string rowType = this.dgvLogView.Rows[e.RowIndex].Cells[0].Value.ToString();
            switch (rowType)
            {
                case "Information":
                    this.dgvLogView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    break;
                case "Error":
                    this.dgvLogView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                    break;
                case "Warning":
                    this.dgvLogView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.CadetBlue;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the dateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.BindControls();
        }
        #endregion

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvLogView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvLogView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            DataGridViewRow row = this.dgvLogView.Rows[e.RowIndex];
            LogEntry logentry = new LogEntry
            {
                Logtype = this.GetLogType(this.dgvLogView.Rows[e.RowIndex].Cells["Logtype"].Value.ToString()),
                Message = this.dgvLogView.Rows[e.RowIndex].Cells["Message"].Value.ToString(),
                Source = this.dgvLogView.Rows[e.RowIndex].Cells["Source"].Value.ToString(),
                StackTrace = this.dgvLogView.Rows[e.RowIndex].Cells["StackTrace"].Value.ToString()
            };

            using (LogviewDialog dialog = new LogviewDialog(logentry))
            {
                dialog.TopMost = true;
                if (DialogResult.OK == dialog.ShowDialog(this))
                {
                    this.BringToFront();
                }
            }
        }

        /// <summary>
        /// Gets the type of the log.
        /// </summary>
        /// <param name="logTypeText">The log type text.</param>
        /// <returns></returns>
        private LogType GetLogType(string logTypeText)
        {
            switch (logTypeText)
            {
                case "Information":
                    return LogType.Information;
                case "Error":
                    return LogType.Error;
                case "Warning":
                    return LogType.Warning;
                default:
                    return LogType.Error;
            }
        }
    }
}
