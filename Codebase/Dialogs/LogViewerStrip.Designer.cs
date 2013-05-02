namespace DeploymentTracker.App.Dialogs
{
    partial class LogViewerStrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewerStrip));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewDeployLog = new System.Windows.Forms.Button();
            this.btnViewBuildLog = new System.Windows.Forms.Button();
            this.btnViewTFLog = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1339399136_dialog-error.png");
            this.imageList1.Images.SetKeyName(1, "1339399101_button_ok.png");
            this.imageList1.Images.SetKeyName(2, "1339399062_preferences-desktop-notification.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // btnViewDeployLog
            // 
            this.btnViewDeployLog.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnViewDeployLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDeployLog.Location = new System.Drawing.Point(24, 47);
            this.btnViewDeployLog.Name = "btnViewDeployLog";
            this.btnViewDeployLog.Size = new System.Drawing.Size(138, 27);
            this.btnViewDeployLog.TabIndex = 1;
            this.btnViewDeployLog.Text = "View &Deploy Log";
            this.btnViewDeployLog.UseVisualStyleBackColor = false;
            this.btnViewDeployLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnViewBuildLog
            // 
            this.btnViewBuildLog.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnViewBuildLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewBuildLog.Location = new System.Drawing.Point(176, 47);
            this.btnViewBuildLog.Name = "btnViewBuildLog";
            this.btnViewBuildLog.Size = new System.Drawing.Size(138, 27);
            this.btnViewBuildLog.TabIndex = 6;
            this.btnViewBuildLog.Text = "View  &Build Log";
            this.btnViewBuildLog.UseVisualStyleBackColor = false;
            this.btnViewBuildLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnViewTFLog
            // 
            this.btnViewTFLog.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnViewTFLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewTFLog.Location = new System.Drawing.Point(328, 47);
            this.btnViewTFLog.Name = "btnViewTFLog";
            this.btnViewTFLog.Size = new System.Drawing.Size(138, 27);
            this.btnViewTFLog.TabIndex = 7;
            this.btnViewTFLog.Text = "View &TF Log";
            this.btnViewTFLog.UseVisualStyleBackColor = false;
            this.btnViewTFLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(176, 102);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(138, 27);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LogViewerStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(531, 141);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnViewTFLog);
            this.Controls.Add(this.btnViewBuildLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnViewDeployLog);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LogViewerStrip";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WaitingDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewDeployLog;
        private System.Windows.Forms.Button btnViewBuildLog;
        private System.Windows.Forms.Button btnViewTFLog;
        private System.Windows.Forms.Button btnClose;

    }
}