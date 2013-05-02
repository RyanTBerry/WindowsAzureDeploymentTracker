namespace DeploymentTracker.App.Windows
{
    partial class ReportScreen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxPerformedBy = new System.Windows.Forms.ComboBox();
            this.buildsRecordTableBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.deploymentTrackerLocalDBDataSet = new DeploymentTracker.DeploymentTrackerLocalDBDataSet();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxEnvironment = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxApplication = new System.Windows.Forms.ComboBox();
            this.buildsRecordTableBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fromDateControl = new System.Windows.Forms.DateTimePicker();
            this.toDateControl = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvLogView = new System.Windows.Forms.DataGridView();
            this.buildsRecordTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buildsRecordTableTableAdapter = new DeploymentTracker.DeploymentTrackerLocalDBDataSetTableAdapters.BuildsRecordTableTableAdapter();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tFSLabelUsedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeploymentNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSucessDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.applicationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.environmentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Actions = new System.Windows.Forms.DataGridViewLinkColumn();
            this.RollbackLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnApplyFilters);
            this.groupBox1.Controls.Add(this.txtVersion);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxStatus);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxPerformedBy);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbxEnvironment);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxApplication);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fromDateControl);
            this.groupBox1.Controls.Add(this.toDateControl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filters";
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Location = new System.Drawing.Point(570, 103);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(125, 34);
            this.btnApplyFilters.TabIndex = 15;
            this.btnApplyFilters.Text = "&Apply Filters";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(613, 51);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(65, 22);
            this.txtVersion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(562, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Version:";
            // 
            // cbxStatus
            // 
            this.cbxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStatus.FormattingEnabled = true;
            this.cbxStatus.Location = new System.Drawing.Point(375, 83);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Size = new System.Drawing.Size(166, 21);
            this.cbxStatus.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Status:";
            // 
            // cbxPerformedBy
            // 
            this.cbxPerformedBy.DataSource = this.buildsRecordTableBindingSource2;
            this.cbxPerformedBy.DisplayMember = "PerformedBy";
            this.cbxPerformedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPerformedBy.FormattingEnabled = true;
            this.cbxPerformedBy.Location = new System.Drawing.Point(101, 83);
            this.cbxPerformedBy.Name = "cbxPerformedBy";
            this.cbxPerformedBy.Size = new System.Drawing.Size(166, 21);
            this.cbxPerformedBy.TabIndex = 10;
            this.cbxPerformedBy.ValueMember = "PerformedBy";
            // 
            // buildsRecordTableBindingSource2
            // 
            this.buildsRecordTableBindingSource2.DataMember = "BuildsRecordTable";
            this.buildsRecordTableBindingSource2.DataSource = this.deploymentTrackerLocalDBDataSet;
            // 
            // deploymentTrackerLocalDBDataSet
            // 
            this.deploymentTrackerLocalDBDataSet.DataSetName = "DeploymentTrackerLocalDBDataSet";
            this.deploymentTrackerLocalDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Performed By:";
            // 
            // cbxEnvironment
            // 
            this.cbxEnvironment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEnvironment.FormattingEnabled = true;
            this.cbxEnvironment.Location = new System.Drawing.Point(375, 52);
            this.cbxEnvironment.Name = "cbxEnvironment";
            this.cbxEnvironment.Size = new System.Drawing.Size(166, 21);
            this.cbxEnvironment.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Environment:";
            // 
            // cbxApplication
            // 
            this.cbxApplication.DataSource = this.buildsRecordTableBindingSource1;
            this.cbxApplication.DisplayMember = "Application";
            this.cbxApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxApplication.FormattingEnabled = true;
            this.cbxApplication.Location = new System.Drawing.Point(101, 52);
            this.cbxApplication.Name = "cbxApplication";
            this.cbxApplication.Size = new System.Drawing.Size(166, 21);
            this.cbxApplication.TabIndex = 6;
            this.cbxApplication.ValueMember = "Application";
            // 
            // buildsRecordTableBindingSource1
            // 
            this.buildsRecordTableBindingSource1.DataMember = "BuildsRecordTable";
            this.buildsRecordTableBindingSource1.DataSource = this.deploymentTrackerLocalDBDataSet;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Application:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From:";
            // 
            // fromDateControl
            // 
            this.fromDateControl.Location = new System.Drawing.Point(100, 19);
            this.fromDateControl.Name = "fromDateControl";
            this.fromDateControl.Size = new System.Drawing.Size(200, 22);
            this.fromDateControl.TabIndex = 2;
            this.fromDateControl.ValueChanged += new System.EventHandler(this.fromDateControl_ValueChanged);
            // 
            // toDateControl
            // 
            this.toDateControl.Location = new System.Drawing.Point(375, 19);
            this.toDateControl.Name = "toDateControl";
            this.toDateControl.Size = new System.Drawing.Size(200, 22);
            this.toDateControl.TabIndex = 1;
            this.toDateControl.ValueChanged += new System.EventHandler(this.fromDateControl_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvLogView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 152);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(756, 401);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report";
            // 
            // dgvLogView
            // 
            this.dgvLogView.AllowUserToAddRows = false;
            this.dgvLogView.AllowUserToDeleteRows = false;
            this.dgvLogView.AllowUserToOrderColumns = true;
            this.dgvLogView.AutoGenerateColumns = false;
            this.dgvLogView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvLogView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvLogView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tFSLabelUsedDataGridViewTextBoxColumn,
            this.DeploymentNotes,
            this.isSucessDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.applicationDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn,
            this.environmentDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.performedByDataGridViewTextBoxColumn,
            this.Details,
            this.Actions,
            this.RollbackLink});
            this.dgvLogView.DataSource = this.buildsRecordTableBindingSource;
            this.dgvLogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogView.Location = new System.Drawing.Point(3, 18);
            this.dgvLogView.Name = "dgvLogView";
            this.dgvLogView.ReadOnly = true;
            this.dgvLogView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLogView.Size = new System.Drawing.Size(750, 380);
            this.dgvLogView.TabIndex = 2;
            this.dgvLogView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogView_CellContentClick);
            // 
            // buildsRecordTableBindingSource
            // 
            this.buildsRecordTableBindingSource.DataMember = "BuildsRecordTable";
            this.buildsRecordTableBindingSource.DataSource = this.deploymentTrackerLocalDBDataSet;
            // 
            // buildsRecordTableTableAdapter
            // 
            this.buildsRecordTableTableAdapter.ClearBeforeFill = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select folder to download";
            // 
            // tFSLabelUsedDataGridViewTextBoxColumn
            // 
            this.tFSLabelUsedDataGridViewTextBoxColumn.DataPropertyName = "TFSLabelUsed";
            this.tFSLabelUsedDataGridViewTextBoxColumn.HeaderText = "TFSLabelUsed";
            this.tFSLabelUsedDataGridViewTextBoxColumn.Name = "tFSLabelUsedDataGridViewTextBoxColumn";
            this.tFSLabelUsedDataGridViewTextBoxColumn.ReadOnly = true;
            this.tFSLabelUsedDataGridViewTextBoxColumn.Visible = false;
            // 
            // DeploymentNotes
            // 
            this.DeploymentNotes.DataPropertyName = "DeploymentNotes";
            this.DeploymentNotes.HeaderText = "DeploymentNotes";
            this.DeploymentNotes.Name = "DeploymentNotes";
            this.DeploymentNotes.ReadOnly = true;
            this.DeploymentNotes.Visible = false;
            // 
            // isSucessDataGridViewTextBoxColumn
            // 
            this.isSucessDataGridViewTextBoxColumn.DataPropertyName = "IsSucess";
            this.isSucessDataGridViewTextBoxColumn.HeaderText = "IsSucess";
            this.isSucessDataGridViewTextBoxColumn.Name = "isSucessDataGridViewTextBoxColumn";
            this.isSucessDataGridViewTextBoxColumn.ReadOnly = true;
            this.isSucessDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // applicationDataGridViewTextBoxColumn
            // 
            this.applicationDataGridViewTextBoxColumn.DataPropertyName = "Application";
            this.applicationDataGridViewTextBoxColumn.HeaderText = "Application";
            this.applicationDataGridViewTextBoxColumn.Name = "applicationDataGridViewTextBoxColumn";
            this.applicationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            this.versionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // environmentDataGridViewTextBoxColumn
            // 
            this.environmentDataGridViewTextBoxColumn.DataPropertyName = "Environment";
            this.environmentDataGridViewTextBoxColumn.HeaderText = "Environment";
            this.environmentDataGridViewTextBoxColumn.Name = "environmentDataGridViewTextBoxColumn";
            this.environmentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // performedByDataGridViewTextBoxColumn
            // 
            this.performedByDataGridViewTextBoxColumn.DataPropertyName = "PerformedBy";
            this.performedByDataGridViewTextBoxColumn.HeaderText = "PerformedBy";
            this.performedByDataGridViewTextBoxColumn.Name = "performedByDataGridViewTextBoxColumn";
            this.performedByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Details
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Details.DefaultCellStyle = dataGridViewCellStyle3;
            this.Details.HeaderText = "";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Details.Text = "Details";
            this.Details.UseColumnTextForLinkValue = true;
            // 
            // Actions
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Actions.DefaultCellStyle = dataGridViewCellStyle4;
            this.Actions.HeaderText = "Actions";
            this.Actions.Name = "Actions";
            this.Actions.ReadOnly = true;
            this.Actions.Text = "Log";
            this.Actions.UseColumnTextForLinkValue = true;
            // 
            // RollbackLink
            // 
            this.RollbackLink.DataPropertyName = "IsLatestVersion";
            this.RollbackLink.HeaderText = "";
            this.RollbackLink.Name = "RollbackLink";
            this.RollbackLink.ReadOnly = true;
            // 
            // ReportScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 553);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ReportScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ReportScreen";
            this.Load += new System.EventHandler(this.ReportScreen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildsRecordTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvLogView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker fromDateControl;
        private System.Windows.Forms.DateTimePicker toDateControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxApplication;
        private System.Windows.Forms.ComboBox cbxEnvironment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxPerformedBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnApplyFilters;
        private DeploymentTrackerLocalDBDataSet deploymentTrackerLocalDBDataSet;
        private System.Windows.Forms.BindingSource buildsRecordTableBindingSource;
        private DeploymentTrackerLocalDBDataSetTableAdapters.BuildsRecordTableTableAdapter buildsRecordTableTableAdapter;
        private System.Windows.Forms.BindingSource buildsRecordTableBindingSource1;
        private System.Windows.Forms.BindingSource buildsRecordTableBindingSource2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn tFSLabelUsedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeploymentNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn isSucessDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn applicationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn environmentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn performedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn Details;
        private System.Windows.Forms.DataGridViewLinkColumn Actions;
        private System.Windows.Forms.DataGridViewLinkColumn RollbackLink;
    }
}