
namespace DeploymentTracker.App.Windows
{
    using System.Security;

    partial class MainForm
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
            this.picboxDeploy = new System.Windows.Forms.PictureBox();
            this.picboxManageAzureConfigurations = new System.Windows.Forms.PictureBox();
            this.picboxConfigureDeploymentTool = new System.Windows.Forms.PictureBox();
            this.picboxReports = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picboxDeploy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxManageAzureConfigurations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxConfigureDeploymentTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxReports)).BeginInit();
            this.SuspendLayout();
            // 
            // picboxDeploy
            // 
            this.picboxDeploy.Image = global::DeploymentTracker.Properties.Resources.Deploy;
            this.picboxDeploy.Location = new System.Drawing.Point(21, 134);
            this.picboxDeploy.Name = "picboxDeploy";
            this.picboxDeploy.Size = new System.Drawing.Size(105, 106);
            this.picboxDeploy.TabIndex = 5;
            this.picboxDeploy.TabStop = false;
            this.picboxDeploy.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // picboxManageAzureConfigurations
            // 
            this.picboxManageAzureConfigurations.Image = global::DeploymentTracker.Properties.Resources.ManageLogs;
            this.picboxManageAzureConfigurations.Location = new System.Drawing.Point(143, 12);
            this.picboxManageAzureConfigurations.Name = "picboxManageAzureConfigurations";
            this.picboxManageAzureConfigurations.Size = new System.Drawing.Size(105, 106);
            this.picboxManageAzureConfigurations.TabIndex = 4;
            this.picboxManageAzureConfigurations.TabStop = false;
            this.picboxManageAzureConfigurations.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // picboxConfigureDeploymentTool
            // 
            this.picboxConfigureDeploymentTool.Image = global::DeploymentTracker.Properties.Resources.Config_Util;
            this.picboxConfigureDeploymentTool.Location = new System.Drawing.Point(21, 12);
            this.picboxConfigureDeploymentTool.Name = "picboxConfigureDeploymentTool";
            this.picboxConfigureDeploymentTool.Size = new System.Drawing.Size(105, 106);
            this.picboxConfigureDeploymentTool.TabIndex = 3;
            this.picboxConfigureDeploymentTool.TabStop = false;
            this.picboxConfigureDeploymentTool.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // picboxReports
            // 
            this.picboxReports.Image = global::DeploymentTracker.Properties.Resources.Reports;
            this.picboxReports.Location = new System.Drawing.Point(143, 134);
            this.picboxReports.Name = "picboxReports";
            this.picboxReports.Size = new System.Drawing.Size(105, 106);
            this.picboxReports.TabIndex = 6;
            this.picboxReports.TabStop = false;
            this.picboxReports.Click += new System.EventHandler(this.PictureBox_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 261);
            this.Controls.Add(this.picboxReports);
            this.Controls.Add(this.picboxDeploy);
            this.Controls.Add(this.picboxManageAzureConfigurations);
            this.Controls.Add(this.picboxConfigureDeploymentTool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deployment Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picboxDeploy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxManageAzureConfigurations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxConfigureDeploymentTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxReports)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picboxConfigureDeploymentTool;
        private System.Windows.Forms.PictureBox picboxManageAzureConfigurations;
        private System.Windows.Forms.PictureBox picboxDeploy;
        private System.Windows.Forms.PictureBox picboxReports;
    }
}