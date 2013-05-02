using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DeploymentTracker.Properties;
using DeploymentTracker.Utilities;
using System.Xml.Linq;

namespace DeploymentTracker.IWARM_ReportScreens
{
    public partial class AppLogViewer : Form
    {
        string selectedLogFile;

        public AppLogViewer()
        {
            InitializeComponent();
            this.dgvLogView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLogView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            
             selectedLogFile = Path.Combine(Settings.Default.ApplicationLogPath,
                             string.Concat(Constants.AppLogFileName, "-", dateTimePicker.Value.ToString("MMddyyyy"), ".Xml"));
            XDocument xdoc = XDocument.Load(selectedLogFile);;
            XNamespace ns = xdoc.Root.GetDefaultNamespace();
            List<string> result = xdoc.Descendants(ns + "LogEntry").AsParallel().Select(element => element.Element(ns + "UserName")
                .Value).Distinct().WithDegreeOfParallelism(5).ToList<string>();
            result.Add("---All---");
            cbxUsers.DataSource = result;
            cbxUsers.SelectedItem = cbxLogType.SelectedItem = "---All---";
        }

        private void btnLoadLogFile_Click(object sender, EventArgs e)
        {
            DataSet myDataSet = new DataSet();
            myDataSet.ReadXml(selectedLogFile);
            var dv = myDataSet.Tables[0].DefaultView;
            dv.RowFilter = GetFilterExpression();
            dv.Sort = "DateTime DESC";;
            dgvLogView.DataSource = dv;
            dgvLogView.Refresh();
        }

        private string GetFilterExpression()
        {
            StringBuilder strExpr = new StringBuilder();
            if (cbxLogType.SelectedItem != null && cbxLogType.SelectedItem.ToString() != "---All---")
            {
                strExpr.Append(string.Format("LogType ='{0}'",
                cbxLogType.SelectedItem == null ? string.Empty : cbxLogType.SelectedItem.ToString()));
            }

            if (cbxUsers.SelectedItem != null && cbxUsers.SelectedItem.ToString() != "---All---")
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("UserName ='{0}'",
                cbxUsers.SelectedItem == null ? string.Empty : cbxUsers.SelectedItem.ToString()));
            }

            if (!string.IsNullOrEmpty(txtStackTrace.Text))
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("StackTrace like '%{0}%'", txtStackTrace.Text));
            }

            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                if (strExpr.Length != 0)
                {
                    strExpr.Append(" AND ");
                }

                strExpr.Append(string.Format("Message like '%{0}%'", txtMessage.Text));
            }
            return strExpr.ToString();
        }

        private void dgvLogView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
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
    }
}
