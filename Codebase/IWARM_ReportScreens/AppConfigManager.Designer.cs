namespace DeploymentTracker.IWARM_ReportScreens
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTFSsettings = new System.Windows.Forms.DataGridView();
            this.serverNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultCollectionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tFSConnectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deploymentTrackerLocalDBTFSConnectionsDataSet = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteTFS = new System.Windows.Forms.Button();
            this.btnEditTFS = new System.Windows.Forms.Button();
            this.btnAddTFS = new System.Windows.Forms.Button();
            this.tFSConnectionsTableAdapter = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTFSsettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvTFSsettings);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 285);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TFS Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of all available TFS connection strings:";
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
            this.defaultCollectionDataGridViewTextBoxColumn});
            this.dgvTFSsettings.DataSource = this.tFSConnectionsBindingSource;
            this.dgvTFSsettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTFSsettings.Location = new System.Drawing.Point(3, 38);
            this.dgvTFSsettings.MultiSelect = false;
            this.dgvTFSsettings.Name = "dgvTFSsettings";
            this.dgvTFSsettings.ReadOnly = true;
            this.dgvTFSsettings.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgvTFSsettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTFSsettings.Size = new System.Drawing.Size(442, 208);
            this.dgvTFSsettings.TabIndex = 0;
            this.dgvTFSsettings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTFSsettings_CellDoubleClick);
            this.dgvTFSsettings.SelectionChanged += new System.EventHandler(this.dgvTFSsettings_SelectionChanged);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDeleteTFS);
            this.panel1.Controls.Add(this.btnEditTFS);
            this.panel1.Controls.Add(this.btnAddTFS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 36);
            this.panel1.TabIndex = 2;
            // 
            // btnDeleteTFS
            // 
            this.btnDeleteTFS.Location = new System.Drawing.Point(330, 6);
            this.btnDeleteTFS.Name = "btnDeleteTFS";
            this.btnDeleteTFS.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTFS.TabIndex = 2;
            this.btnDeleteTFS.Text = "&Delete";
            this.btnDeleteTFS.UseVisualStyleBackColor = true;
            this.btnDeleteTFS.Click += new System.EventHandler(this.btnDeleteTFS_Click);
            // 
            // btnEditTFS
            // 
            this.btnEditTFS.Location = new System.Drawing.Point(239, 6);
            this.btnEditTFS.Name = "btnEditTFS";
            this.btnEditTFS.Size = new System.Drawing.Size(75, 23);
            this.btnEditTFS.TabIndex = 1;
            this.btnEditTFS.Text = "&Edit";
            this.btnEditTFS.UseVisualStyleBackColor = true;
            this.btnEditTFS.Click += new System.EventHandler(this.btnEditTFS_Click);
            // 
            // btnAddTFS
            // 
            this.btnAddTFS.Location = new System.Drawing.Point(150, 7);
            this.btnAddTFS.Name = "btnAddTFS";
            this.btnAddTFS.Size = new System.Drawing.Size(75, 23);
            this.btnAddTFS.TabIndex = 0;
            this.btnAddTFS.Text = "&Add";
            this.btnAddTFS.UseVisualStyleBackColor = true;
            this.btnAddTFS.Click += new System.EventHandler(this.btnAddTFS_Click);
            // 
            // tFSConnectionsTableAdapter
            // 
            this.tFSConnectionsTableAdapter.ClearBeforeFill = true;
            // 
            // AppConfigManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 531);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AppConfigManager";
            this.Text = "AppConfigManager";
            this.Load += new System.EventHandler(this.AppConfigManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTFSsettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTFSsettings;
        private DeploymentTrackerLocalDBTFSConnectionsDataSet deploymentTrackerLocalDBTFSConnectionsDataSet;
        private System.Windows.Forms.BindingSource tFSConnectionsBindingSource;
        private DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter tFSConnectionsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultCollectionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteTFS;
        private System.Windows.Forms.Button btnEditTFS;
        private System.Windows.Forms.Button btnAddTFS;
    }
}