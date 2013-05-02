// -----------------------------------------------------------------------
// <copyright file="DeploymentReport.Designer.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.IWARM_ReportScreens
{
    using System.Security;
    using System.Security.Permissions;

    partial class DeploymentReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.deploymentTrackerLocalDBDataSet = new DeploymentTracker.DeploymentTrackerLocalDBDataSet();
            this.deploymentTrackerLocalDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildsRecordTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildsRecordTableTableAdapter = new DeploymentTracker.DeploymentTrackerLocalDBDataSetTableAdapters.BuildsRecordTableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DeploymentTrackerDataSet";
            reportDataSource1.Value = this.buildsRecordTableBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "DeploymentTracker.IWARM_ReportScreens.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(438, 440);
            this.reportViewer1.TabIndex = 0;
            // 
            // deploymentTrackerLocalDBDataSet
            // 
            this.deploymentTrackerLocalDBDataSet.DataSetName = "DeploymentTrackerLocalDBDataSet";
            this.deploymentTrackerLocalDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // deploymentTrackerLocalDBDataSetBindingSource
            // 
            this.deploymentTrackerLocalDBDataSetBindingSource.DataSource = this.deploymentTrackerLocalDBDataSet;
            this.deploymentTrackerLocalDBDataSetBindingSource.Position = 0;
            // 
            // buildsRecordTableBindingSource
            // 
            this.buildsRecordTableBindingSource.DataMember = "BuildsRecordTable";
            this.buildsRecordTableBindingSource.DataSource = this.deploymentTrackerLocalDBDataSetBindingSource;
            // 
            // buildsRecordTableTableAdapter
            // 
            this.buildsRecordTableTableAdapter.ClearBeforeFill = true;
            // 
            // DeploymentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 440);
            this.Controls.Add(this.reportViewer1);
            this.Name = "DeploymentReport";
            this.Text = "DeployReport";
            this.Load += new System.EventHandler(this.DeployReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private DeploymentTracker.DeploymentTrackerLocalDBDataSet deploymentTrackerLocalDBDataSet;
        private System.Windows.Forms.BindingSource deploymentTrackerLocalDBDataSetBindingSource;
        private System.Windows.Forms.BindingSource buildsRecordTableBindingSource;
        private DeploymentTracker.DeploymentTrackerLocalDBDataSetTableAdapters.BuildsRecordTableTableAdapter buildsRecordTableTableAdapter;
    }
}