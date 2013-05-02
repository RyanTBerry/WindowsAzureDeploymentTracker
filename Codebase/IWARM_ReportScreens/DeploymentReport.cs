// -----------------------------------------------------------------------
// <copyright file="DeploymentReport.cs" company="Microsoft IT">
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
    using System.Windows.Forms;
    using System.Security;
    using System.Security.Permissions;

    /// <summary>
    /// Deployment Report Form
    /// </summary>
    public partial class DeploymentReport : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentReport"/> class.
        /// </summary>
        public DeploymentReport()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the DeployReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeployReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'deploymentTrackerLocalDBDataSet.BuildsRecordTable' table. You can move, or remove it, as needed.
            this.buildsRecordTableTableAdapter.Fill(this.deploymentTrackerLocalDBDataSet.BuildsRecordTable);
            this.reportViewer1.RefreshReport();
        }
    }
}
