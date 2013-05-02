namespace DeploymentTracker.IWARM_ReportScreens
{
    partial class ServiceConfigViewer
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
            this.settingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInstanceRoleCount = new System.Windows.Forms.TextBox();
            this.txtOsfamily = new System.Windows.Forms.TextBox();
            this.txtOsversion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listboxRoles = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvRoleSettings = new System.Windows.Forms.DataGridView();
            this.dgvCertificates = new System.Windows.Forms.DataGridView();
            this.certsbindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRejectSettings = new System.Windows.Forms.Button();
            this.btnAcceptSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.certsbindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.listboxRoles);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 538);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 22;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInstanceRoleCount);
            this.groupBox1.Controls.Add(this.txtOsfamily);
            this.groupBox1.Controls.Add(this.txtOsversion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 289);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(230, 249);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration Details";
            // 
            // txtInstanceRoleCount
            // 
            this.txtInstanceRoleCount.Location = new System.Drawing.Point(123, 96);
            this.txtInstanceRoleCount.Name = "txtInstanceRoleCount";
            this.txtInstanceRoleCount.Size = new System.Drawing.Size(100, 23);
            this.txtInstanceRoleCount.TabIndex = 8;
            // 
            // txtOsfamily
            // 
            this.txtOsfamily.Location = new System.Drawing.Point(123, 67);
            this.txtOsfamily.Name = "txtOsfamily";
            this.txtOsfamily.Size = new System.Drawing.Size(100, 23);
            this.txtOsfamily.TabIndex = 7;
            // 
            // txtOsversion
            // 
            this.txtOsversion.Location = new System.Drawing.Point(123, 37);
            this.txtOsversion.Name = "txtOsversion";
            this.txtOsversion.Size = new System.Drawing.Size(100, 23);
            this.txtOsversion.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "OS Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "OS Family:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 101);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Role Instance Count:";
            // 
            // listboxRoles
            // 
            this.listboxRoles.DataSource = this.rolesBindingSource;
            this.listboxRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.listboxRoles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listboxRoles.FormattingEnabled = true;
            this.listboxRoles.ItemHeight = 17;
            this.listboxRoles.Location = new System.Drawing.Point(0, 30);
            this.listboxRoles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listboxRoles.Name = "listboxRoles";
            this.listboxRoles.Size = new System.Drawing.Size(230, 259);
            this.listboxRoles.TabIndex = 22;
            this.listboxRoles.SelectedIndexChanged += new System.EventHandler(this.ListboxRoles_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 30);
            this.panel1.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(63, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Roles Available";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(230, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(49, 538);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::DeploymentTracker.Properties.Resources.rightarrow;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 560);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvRoleSettings);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvCertificates);
            this.splitContainer2.Size = new System.Drawing.Size(741, 538);
            this.splitContainer2.SplitterDistance = 330;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // dgvRoleSettings
            // 
            this.dgvRoleSettings.AllowUserToAddRows = false;
            this.dgvRoleSettings.AllowUserToDeleteRows = false;
            this.dgvRoleSettings.AutoGenerateColumns = false;
            this.dgvRoleSettings.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRoleSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoleSettings.DataSource = this.settingsBindingSource;
            this.dgvRoleSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoleSettings.Location = new System.Drawing.Point(0, 0);
            this.dgvRoleSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvRoleSettings.Name = "dgvRoleSettings";
            this.dgvRoleSettings.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvRoleSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRoleSettings.Size = new System.Drawing.Size(741, 330);
            this.dgvRoleSettings.TabIndex = 1;
            this.dgvRoleSettings.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRoleSettings_CellClick);
            this.dgvRoleSettings.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRoleSettings_CellEndEdit);
            this.dgvRoleSettings.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DgvRoleSettings_CellValidating);
            // 
            // dgvCertificates
            // 
            this.dgvCertificates.AllowUserToAddRows = false;
            this.dgvCertificates.AllowUserToDeleteRows = false;
            this.dgvCertificates.AutoGenerateColumns = false;
            this.dgvCertificates.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificates.DataSource = this.certsbindingSource;
            this.dgvCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCertificates.Location = new System.Drawing.Point(0, 0);
            this.dgvCertificates.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvCertificates.Name = "dgvCertificates";
            this.dgvCertificates.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvCertificates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCertificates.Size = new System.Drawing.Size(741, 203);
            this.dgvCertificates.TabIndex = 2;
            this.dgvCertificates.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCertificates_CellClick);
            this.dgvCertificates.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCertificates_CellEndEdit);
            this.dgvCertificates.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DgvCertificates_CellValidating);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRejectSettings);
            this.panel3.Controls.Add(this.btnAcceptSettings);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 538);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1024, 49);
            this.panel3.TabIndex = 23;
            // 
            // btnRejectSettings
            // 
            this.btnRejectSettings.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnRejectSettings.Location = new System.Drawing.Point(825, 6);
            this.btnRejectSettings.Name = "btnRejectSettings";
            this.btnRejectSettings.Size = new System.Drawing.Size(164, 37);
            this.btnRejectSettings.TabIndex = 1;
            this.btnRejectSettings.Text = "&Cancel Deployment";
            this.btnRejectSettings.UseVisualStyleBackColor = true;
            // 
            // btnAcceptSettings
            // 
            this.btnAcceptSettings.Location = new System.Drawing.Point(646, 6);
            this.btnAcceptSettings.Name = "btnAcceptSettings";
            this.btnAcceptSettings.Size = new System.Drawing.Size(164, 37);
            this.btnAcceptSettings.TabIndex = 0;
            this.btnAcceptSettings.Text = "&Save Settings";
            this.btnAcceptSettings.UseVisualStyleBackColor = true;
            this.btnAcceptSettings.Click += new System.EventHandler(this.BtnAcceptSettings_Click);
            // 
            // ServiceConfigViewer
            // 
            this.AcceptButton = this.btnAcceptSettings;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnRejectSettings;
            this.ClientSize = new System.Drawing.Size(1024, 587);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "ServiceConfigViewer";
            this.Text = "Configuration Builder";
            this.Load += new System.EventHandler(this.ConfigBuilder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rolesBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoleSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.certsbindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource rolesBindingSource;
        private System.Windows.Forms.BindingSource settingsBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listboxRoles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvRoleSettings;
        private System.Windows.Forms.DataGridView dgvCertificates;
        private System.Windows.Forms.BindingSource certsbindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRejectSettings;
        private System.Windows.Forms.Button btnAcceptSettings;
        private System.Windows.Forms.TextBox txtInstanceRoleCount;
        private System.Windows.Forms.TextBox txtOsfamily;
        private System.Windows.Forms.TextBox txtOsversion;
    }
}