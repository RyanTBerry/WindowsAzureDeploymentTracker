namespace DeploymentTracker.App.Windows
{
    partial class AppConfigManager
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvTFSsettings = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteTFS = new System.Windows.Forms.Button();
            this.btnEditTFS = new System.Windows.Forms.Button();
            this.btnAddTFS = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAppLogsFolder = new System.Windows.Forms.Button();
            this.btnTFSWorkingFolder = new System.Windows.Forms.Button();
            this.btnDeployLogFolder = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtAzurePublishSettings = new System.Windows.Forms.TextBox();
            this.txtNumberOfTries = new System.Windows.Forms.TextBox();
            this.txtLocalDBConnection = new System.Windows.Forms.TextBox();
            this.txtParallelTasks = new System.Windows.Forms.TextBox();
            this.txtAppLogFolder = new System.Windows.Forms.TextBox();
            this.txtTFSWorkingFolder = new System.Windows.Forms.TextBox();
            this.txtDeploymentLogFolder = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tFSConnectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deploymentTrackerLocalDBTFSConnectionsDataSet = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSet();
            this.tFSConnectionsTableAdapter = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter();
            this.serverNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultCollectionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsHttps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTFSsettings)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvTFSsettings);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TFS Settings";
            // 
            // dgvTFSsettings
            // 
            this.dgvTFSsettings.AllowUserToAddRows = false;
            this.dgvTFSsettings.AutoGenerateColumns = false;
            this.dgvTFSsettings.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvTFSsettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTFSsettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverNameDataGridViewTextBoxColumn,
            this.portNumberDataGridViewTextBoxColumn,
            this.defaultCollectionDataGridViewTextBoxColumn,
            this.IsHttps});
            this.dgvTFSsettings.DataSource = this.tFSConnectionsBindingSource;
            this.dgvTFSsettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTFSsettings.Location = new System.Drawing.Point(3, 31);
            this.dgvTFSsettings.MultiSelect = false;
            this.dgvTFSsettings.Name = "dgvTFSsettings";
            this.dgvTFSsettings.ReadOnly = true;
            this.dgvTFSsettings.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvTFSsettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTFSsettings.Size = new System.Drawing.Size(349, 292);
            this.dgvTFSsettings.TabIndex = 0;
            this.dgvTFSsettings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvTFSsettings_CellDoubleClick);
            this.dgvTFSsettings.SelectionChanged += new System.EventHandler(this.DgvTFSsettings_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of all available TFS connection strings:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteTFS);
            this.panel1.Controls.Add(this.btnEditTFS);
            this.panel1.Controls.Add(this.btnAddTFS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 323);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(349, 36);
            this.panel1.TabIndex = 2;
            // 
            // btnDeleteTFS
            // 
            this.btnDeleteTFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTFS.Location = new System.Drawing.Point(262, 6);
            this.btnDeleteTFS.Name = "btnDeleteTFS";
            this.btnDeleteTFS.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTFS.TabIndex = 2;
            this.btnDeleteTFS.Text = "&Delete";
            this.btnDeleteTFS.UseVisualStyleBackColor = true;
            this.btnDeleteTFS.Click += new System.EventHandler(this.BtnDeleteTFS_Click);
            // 
            // btnEditTFS
            // 
            this.btnEditTFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTFS.Location = new System.Drawing.Point(168, 6);
            this.btnEditTFS.Name = "btnEditTFS";
            this.btnEditTFS.Size = new System.Drawing.Size(75, 23);
            this.btnEditTFS.TabIndex = 1;
            this.btnEditTFS.Text = "&Edit";
            this.btnEditTFS.UseVisualStyleBackColor = true;
            this.btnEditTFS.Click += new System.EventHandler(this.BtnEditTFS_Click);
            // 
            // btnAddTFS
            // 
            this.btnAddTFS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTFS.Location = new System.Drawing.Point(74, 6);
            this.btnAddTFS.Name = "btnAddTFS";
            this.btnAddTFS.Size = new System.Drawing.Size(75, 23);
            this.btnAddTFS.TabIndex = 0;
            this.btnAddTFS.Text = "&Add";
            this.btnAddTFS.UseVisualStyleBackColor = true;
            this.btnAddTFS.Click += new System.EventHandler(this.BtnAddTFS_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAppLogsFolder);
            this.groupBox2.Controls.Add(this.btnTFSWorkingFolder);
            this.groupBox2.Controls.Add(this.btnDeployLogFolder);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.txtAzurePublishSettings);
            this.groupBox2.Controls.Add(this.txtNumberOfTries);
            this.groupBox2.Controls.Add(this.txtLocalDBConnection);
            this.groupBox2.Controls.Add(this.txtParallelTasks);
            this.groupBox2.Controls.Add(this.txtAppLogFolder);
            this.groupBox2.Controls.Add(this.txtTFSWorkingFolder);
            this.groupBox2.Controls.Add(this.txtDeploymentLogFolder);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(358, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 362);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General Settings";
            // 
            // btnAppLogsFolder
            // 
            this.btnAppLogsFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppLogsFolder.Location = new System.Drawing.Point(414, 83);
            this.btnAppLogsFolder.Name = "btnAppLogsFolder";
            this.btnAppLogsFolder.Size = new System.Drawing.Size(43, 21);
            this.btnAppLogsFolder.TabIndex = 5;
            this.btnAppLogsFolder.Text = "...";
            this.btnAppLogsFolder.UseVisualStyleBackColor = true;
            this.btnAppLogsFolder.Click += new System.EventHandler(this.BtnShowFolder_Click);
            // 
            // btnTFSWorkingFolder
            // 
            this.btnTFSWorkingFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTFSWorkingFolder.Location = new System.Drawing.Point(414, 55);
            this.btnTFSWorkingFolder.Name = "btnTFSWorkingFolder";
            this.btnTFSWorkingFolder.Size = new System.Drawing.Size(43, 21);
            this.btnTFSWorkingFolder.TabIndex = 3;
            this.btnTFSWorkingFolder.Text = "...";
            this.btnTFSWorkingFolder.UseVisualStyleBackColor = true;
            this.btnTFSWorkingFolder.Click += new System.EventHandler(this.BtnShowFolder_Click);
            // 
            // btnDeployLogFolder
            // 
            this.btnDeployLogFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeployLogFolder.Location = new System.Drawing.Point(414, 27);
            this.btnDeployLogFolder.Name = "btnDeployLogFolder";
            this.btnDeployLogFolder.Size = new System.Drawing.Size(43, 21);
            this.btnDeployLogFolder.TabIndex = 1;
            this.btnDeployLogFolder.Text = "...";
            this.btnDeployLogFolder.UseVisualStyleBackColor = true;
            this.btnDeployLogFolder.Click += new System.EventHandler(this.BtnShowFolder_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(333, 323);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(245, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtAzurePublishSettings
            // 
            this.txtAzurePublishSettings.Location = new System.Drawing.Point(153, 221);
            this.txtAzurePublishSettings.Name = "txtAzurePublishSettings";
            this.txtAzurePublishSettings.Size = new System.Drawing.Size(255, 22);
            this.txtAzurePublishSettings.TabIndex = 9;
            // 
            // txtNumberOfTries
            // 
            this.txtNumberOfTries.Location = new System.Drawing.Point(153, 193);
            this.txtNumberOfTries.Name = "txtNumberOfTries";
            this.txtNumberOfTries.Size = new System.Drawing.Size(75, 22);
            this.txtNumberOfTries.TabIndex = 8;
            // 
            // txtLocalDBConnection
            // 
            this.txtLocalDBConnection.Location = new System.Drawing.Point(153, 139);
            this.txtLocalDBConnection.Multiline = true;
            this.txtLocalDBConnection.Name = "txtLocalDBConnection";
            this.txtLocalDBConnection.Size = new System.Drawing.Size(255, 48);
            this.txtLocalDBConnection.TabIndex = 7;
            // 
            // txtParallelTasks
            // 
            this.txtParallelTasks.Location = new System.Drawing.Point(153, 111);
            this.txtParallelTasks.Name = "txtParallelTasks";
            this.txtParallelTasks.Size = new System.Drawing.Size(75, 22);
            this.txtParallelTasks.TabIndex = 6;
            // 
            // txtAppLogFolder
            // 
            this.txtAppLogFolder.Location = new System.Drawing.Point(153, 83);
            this.txtAppLogFolder.Name = "txtAppLogFolder";
            this.txtAppLogFolder.Size = new System.Drawing.Size(255, 22);
            this.txtAppLogFolder.TabIndex = 4;
            // 
            // txtTFSWorkingFolder
            // 
            this.txtTFSWorkingFolder.Location = new System.Drawing.Point(153, 55);
            this.txtTFSWorkingFolder.Name = "txtTFSWorkingFolder";
            this.txtTFSWorkingFolder.Size = new System.Drawing.Size(255, 22);
            this.txtTFSWorkingFolder.TabIndex = 2;
            // 
            // txtDeploymentLogFolder
            // 
            this.txtDeploymentLogFolder.Location = new System.Drawing.Point(153, 27);
            this.txtDeploymentLogFolder.Name = "txtDeploymentLogFolder";
            this.txtDeploymentLogFolder.Size = new System.Drawing.Size(255, 22);
            this.txtDeploymentLogFolder.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Azure Publishsettings link:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Number of Tries:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "App Logs Folder:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Local DB connection string:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Maximum parallel tasks:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "TFS Working Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Deployment Logs Folder:";
            // 
            // tFSConnectionsBindingSource
            // 
            this.tFSConnectionsBindingSource.DataMember = "TFSConnections";
            this.tFSConnectionsBindingSource.DataSource = this.deploymentTrackerLocalDBTFSConnectionsDataSet;
            // 
            // deploymentTrackerLocalDBTFSConnectionsDataSet
            // 
            this.deploymentTrackerLocalDBTFSConnectionsDataSet.DataSetName = "DeploymentTrackerLocalDBTFSConnectionsDataSet";
            this.deploymentTrackerLocalDBTFSConnectionsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tFSConnectionsTableAdapter
            // 
            this.tFSConnectionsTableAdapter.ClearBeforeFill = true;
            // 
            // serverNameDataGridViewTextBoxColumn
            // 
            this.serverNameDataGridViewTextBoxColumn.DataPropertyName = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.HeaderText = "ServerName";
            this.serverNameDataGridViewTextBoxColumn.Name = "serverNameDataGridViewTextBoxColumn";
            this.serverNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // portNumberDataGridViewTextBoxColumn
            // 
            this.portNumberDataGridViewTextBoxColumn.DataPropertyName = "PortNumber";
            this.portNumberDataGridViewTextBoxColumn.HeaderText = "PortNumber";
            this.portNumberDataGridViewTextBoxColumn.Name = "portNumberDataGridViewTextBoxColumn";
            this.portNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // defaultCollectionDataGridViewTextBoxColumn
            // 
            this.defaultCollectionDataGridViewTextBoxColumn.DataPropertyName = "DefaultCollection";
            this.defaultCollectionDataGridViewTextBoxColumn.HeaderText = "DefaultCollection";
            this.defaultCollectionDataGridViewTextBoxColumn.Name = "defaultCollectionDataGridViewTextBoxColumn";
            this.defaultCollectionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // IsHttps
            // 
            this.IsHttps.DataPropertyName = "IsHttps";
            this.IsHttps.HeaderText = "IsHttps";
            this.IsHttps.Name = "IsHttps";
            this.IsHttps.ReadOnly = true;
            this.IsHttps.Visible = false;
            // 
            // AppConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(837, 362);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AppConfigManager";
            this.Text = "AppConfigManager";
            this.Load += new System.EventHandler(this.AppConfigManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTFSsettings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTFSsettings;
        private DeploymentTrackerLocalDBTFSConnectionsDataSet deploymentTrackerLocalDBTFSConnectionsDataSet;
        private System.Windows.Forms.BindingSource tFSConnectionsBindingSource;
        private DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter tFSConnectionsTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteTFS;
        private System.Windows.Forms.Button btnEditTFS;
        private System.Windows.Forms.Button btnAddTFS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAzurePublishSettings;
        private System.Windows.Forms.TextBox txtNumberOfTries;
        private System.Windows.Forms.TextBox txtLocalDBConnection;
        private System.Windows.Forms.TextBox txtParallelTasks;
        private System.Windows.Forms.TextBox txtAppLogFolder;
        private System.Windows.Forms.TextBox txtTFSWorkingFolder;
        private System.Windows.Forms.TextBox txtDeploymentLogFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAppLogsFolder;
        private System.Windows.Forms.Button btnTFSWorkingFolder;
        private System.Windows.Forms.Button btnDeployLogFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultCollectionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsHttps;
    }
}