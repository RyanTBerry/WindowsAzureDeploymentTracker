
namespace DeploymentTracker.App.Windows
{
    using System.Security;

    partial class PackageDeployment
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Initializing Workspace");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Creating temporary TFS workspace");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Downloading latest version of TFS label");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Building Solution");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Deleting temporary TFS workspace");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Task 1: Compile and  Publish solution", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Creating storage account");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Creating hosted service");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Copying package to cloud");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Deploying package on cloud");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Creating service cerificates (if any)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Task 2: Deploying on Cloud", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageDeployment));
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxHostedNames = new System.Windows.Forms.ComboBox();
            this.cbxStorageNames = new System.Windows.Forms.ComboBox();
            this.btnLoadCerts = new System.Windows.Forms.Button();
            this.txtCertificatePaths = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbxDeploymentSlot = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbxSubscriptions = new System.Windows.Forms.ComboBox();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.btnLoadPublishSettings = new System.Windows.Forms.Button();
            this.txtPublishSettingsFilePath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chbxIsUpgrade = new System.Windows.Forms.CheckBox();
            this.cbxLocations = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtDeploymentNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTFSDeployment = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadSolutions = new System.Windows.Forms.Button();
            this.btnLoadTFSlabel = new System.Windows.Forms.Button();
            this.txtSolutionName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTFSLabelName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxServerName = new System.Windows.Forms.ComboBox();
            this.tFSConnectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.deploymentTrackerLocalDBTFSConnectionsDataSet = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSet();
            this.txtTFSDefaultCollection = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabLocalDeployment = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxIsRollback = new System.Windows.Forms.CheckBox();
            this.btnBrowseConfig = new System.Windows.Forms.Button();
            this.btnBrowsePackage = new System.Windows.Forms.Button();
            this.txtConfigPath = new System.Windows.Forms.TextBox();
            this.txtPackagePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tvCheckList = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewDeployLog = new System.Windows.Forms.Button();
            this.btnViewBuildLog = new System.Windows.Forms.Button();
            this.btnViewTFLog = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tFSConnectionsTableAdapter = new DeploymentTracker.DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter();
            this.tabLocalDeploy = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTFSDeployment.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).BeginInit();
            this.tabLocalDeployment.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tvCheckList);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(884, 474);
            this.splitContainer1.SplitterDistance = 517;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.cbxHostedNames);
            this.groupBox3.Controls.Add(this.cbxStorageNames);
            this.groupBox3.Controls.Add(this.btnLoadCerts);
            this.groupBox3.Controls.Add(this.txtCertificatePaths);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cbxDeploymentSlot);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cbxSubscriptions);
            this.groupBox3.Controls.Add(this.lnkDownload);
            this.groupBox3.Controls.Add(this.btnLoadPublishSettings);
            this.groupBox3.Controls.Add(this.txtPublishSettingsFilePath);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.chbxIsUpgrade);
            this.groupBox3.Controls.Add(this.cbxLocations);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.txtDeploymentNotes);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnDeploy);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 163);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 311);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Azure Settings";
            // 
            // cbxHostedNames
            // 
            this.cbxHostedNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxHostedNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxHostedNames.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxHostedNames.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxHostedNames.FormattingEnabled = true;
            this.cbxHostedNames.Location = new System.Drawing.Point(125, 147);
            this.cbxHostedNames.Name = "cbxHostedNames";
            this.cbxHostedNames.Size = new System.Drawing.Size(167, 21);
            this.cbxHostedNames.TabIndex = 43;
            this.cbxHostedNames.TextChanged += new System.EventHandler(this.cbxHostedNames_TextChanged);
            // 
            // cbxStorageNames
            // 
            this.cbxStorageNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbxStorageNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStorageNames.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxStorageNames.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxStorageNames.FormattingEnabled = true;
            this.cbxStorageNames.Location = new System.Drawing.Point(125, 118);
            this.cbxStorageNames.Name = "cbxStorageNames";
            this.cbxStorageNames.Size = new System.Drawing.Size(167, 21);
            this.cbxStorageNames.TabIndex = 42;
            this.cbxStorageNames.SelectedIndexChanged += new System.EventHandler(this.cbxStorageNames_SelectedIndexChanged);
            // 
            // btnLoadCerts
            // 
            this.btnLoadCerts.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadCerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadCerts.Location = new System.Drawing.Point(460, 188);
            this.btnLoadCerts.Name = "btnLoadCerts";
            this.btnLoadCerts.Size = new System.Drawing.Size(37, 23);
            this.btnLoadCerts.TabIndex = 10;
            this.btnLoadCerts.Text = "...";
            this.btnLoadCerts.UseVisualStyleBackColor = false;
            this.btnLoadCerts.Click += new System.EventHandler(this.BtnBrowsePackage_Click);
            // 
            // txtCertificatePaths
            // 
            this.txtCertificatePaths.Location = new System.Drawing.Point(123, 177);
            this.txtCertificatePaths.Multiline = true;
            this.txtCertificatePaths.Name = "txtCertificatePaths";
            this.txtCertificatePaths.Size = new System.Drawing.Size(328, 41);
            this.txtCertificatePaths.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Certificate Paths:";
            // 
            // cbxDeploymentSlot
            // 
            this.cbxDeploymentSlot.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxDeploymentSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeploymentSlot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxDeploymentSlot.FormattingEnabled = true;
            this.cbxDeploymentSlot.Location = new System.Drawing.Point(382, 147);
            this.cbxDeploymentSlot.Name = "cbxDeploymentSlot";
            this.cbxDeploymentSlot.Size = new System.Drawing.Size(121, 21);
            this.cbxDeploymentSlot.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(305, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Environment:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(37, 84);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Subscriptions:";
            // 
            // cbxSubscriptions
            // 
            this.cbxSubscriptions.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxSubscriptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSubscriptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxSubscriptions.FormattingEnabled = true;
            this.cbxSubscriptions.Location = new System.Drawing.Point(123, 81);
            this.cbxSubscriptions.Name = "cbxSubscriptions";
            this.cbxSubscriptions.Size = new System.Drawing.Size(328, 21);
            this.cbxSubscriptions.TabIndex = 2;
            this.cbxSubscriptions.SelectedIndexChanged += new System.EventHandler(this.CbxSubscriptions_SelectedIndexChanged);
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.Location = new System.Drawing.Point(327, 56);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(170, 13);
            this.lnkDownload.TabIndex = 1;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "Download PublishSettings File?";
            this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkDownload_LinkClicked);
            // 
            // btnLoadPublishSettings
            // 
            this.btnLoadPublishSettings.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadPublishSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadPublishSettings.Location = new System.Drawing.Point(460, 28);
            this.btnLoadPublishSettings.Name = "btnLoadPublishSettings";
            this.btnLoadPublishSettings.Size = new System.Drawing.Size(37, 23);
            this.btnLoadPublishSettings.TabIndex = 0;
            this.btnLoadPublishSettings.Text = "...";
            this.btnLoadPublishSettings.UseVisualStyleBackColor = false;
            this.btnLoadPublishSettings.Click += new System.EventHandler(this.BtnBrowsePackage_Click);
            // 
            // txtPublishSettingsFilePath
            // 
            this.txtPublishSettingsFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtPublishSettingsFilePath.Location = new System.Drawing.Point(123, 29);
            this.txtPublishSettingsFilePath.Name = "txtPublishSettingsFilePath";
            this.txtPublishSettingsFilePath.Size = new System.Drawing.Size(328, 22);
            this.txtPublishSettingsFilePath.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Publish Settings Path:";
            // 
            // chbxIsUpgrade
            // 
            this.chbxIsUpgrade.AutoSize = true;
            this.chbxIsUpgrade.Enabled = false;
            this.chbxIsUpgrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbxIsUpgrade.Location = new System.Drawing.Point(425, 247);
            this.chbxIsUpgrade.Name = "chbxIsUpgrade";
            this.chbxIsUpgrade.Size = new System.Drawing.Size(76, 17);
            this.chbxIsUpgrade.TabIndex = 13;
            this.chbxIsUpgrade.Text = "IsUpgrade";
            this.chbxIsUpgrade.UseVisualStyleBackColor = true;
            // 
            // cbxLocations
            // 
            this.cbxLocations.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxLocations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLocations.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxLocations.FormattingEnabled = true;
            this.cbxLocations.Location = new System.Drawing.Point(382, 114);
            this.cbxLocations.Name = "cbxLocations";
            this.cbxLocations.Size = new System.Drawing.Size(121, 21);
            this.cbxLocations.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(317, 118);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Locations:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Hosted Service Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Storage Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(267, 275);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txtDeploymentNotes
            // 
            this.txtDeploymentNotes.Location = new System.Drawing.Point(123, 227);
            this.txtDeploymentNotes.Multiline = true;
            this.txtDeploymentNotes.Name = "txtDeploymentNotes";
            this.txtDeploymentNotes.Size = new System.Drawing.Size(292, 37);
            this.txtDeploymentNotes.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 236);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Deployment Notes:";
            // 
            // btnDeploy
            // 
            this.btnDeploy.BackColor = System.Drawing.SystemColors.Control;
            this.btnDeploy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeploy.Location = new System.Drawing.Point(388, 275);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(109, 23);
            this.btnDeploy.TabIndex = 14;
            this.btnDeploy.Text = "&Deploy";
            this.btnDeploy.UseVisualStyleBackColor = false;
            this.btnDeploy.Click += new System.EventHandler(this.BtnDeploy_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabTFSDeployment);
            this.tabControl1.Controls.Add(this.tabLocalDeployment);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(517, 163);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl1_Selecting);
            // 
            // tabTFSDeployment
            // 
            this.tabTFSDeployment.BackColor = System.Drawing.SystemColors.Control;
            this.tabTFSDeployment.Controls.Add(this.groupBox2);
            this.tabTFSDeployment.Location = new System.Drawing.Point(4, 25);
            this.tabTFSDeployment.Name = "tabTFSDeployment";
            this.tabTFSDeployment.Size = new System.Drawing.Size(509, 134);
            this.tabTFSDeployment.TabIndex = 0;
            this.tabTFSDeployment.Text = "Deploy from TFS";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.btnLoadSolutions);
            this.groupBox2.Controls.Add(this.btnLoadTFSlabel);
            this.groupBox2.Controls.Add(this.txtSolutionName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtTFSLabelName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbxServerName);
            this.groupBox2.Controls.Add(this.txtTFSDefaultCollection);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(509, 129);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TFS Settings";
            // 
            // btnLoadSolutions
            // 
            this.btnLoadSolutions.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadSolutions.Enabled = false;
            this.btnLoadSolutions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadSolutions.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnLoadSolutions.Location = new System.Drawing.Point(289, 83);
            this.btnLoadSolutions.Name = "btnLoadSolutions";
            this.btnLoadSolutions.Size = new System.Drawing.Size(111, 23);
            this.btnLoadSolutions.TabIndex = 2;
            this.btnLoadSolutions.Text = "Load Sol&utions";
            this.btnLoadSolutions.UseVisualStyleBackColor = false;
            this.btnLoadSolutions.Click += new System.EventHandler(this.BtnLoadSolutions_Click);
            // 
            // btnLoadTFSlabel
            // 
            this.btnLoadTFSlabel.BackColor = System.Drawing.SystemColors.Control;
            this.btnLoadTFSlabel.Enabled = false;
            this.btnLoadTFSlabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTFSlabel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnLoadTFSlabel.Location = new System.Drawing.Point(289, 56);
            this.btnLoadTFSlabel.Name = "btnLoadTFSlabel";
            this.btnLoadTFSlabel.Size = new System.Drawing.Size(111, 23);
            this.btnLoadTFSlabel.TabIndex = 1;
            this.btnLoadTFSlabel.Text = "F&ind";
            this.btnLoadTFSlabel.UseVisualStyleBackColor = false;
            this.btnLoadTFSlabel.Click += new System.EventHandler(this.BtnLoadTFSlabel_Click);
            // 
            // txtSolutionName
            // 
            this.txtSolutionName.Location = new System.Drawing.Point(133, 85);
            this.txtSolutionName.Name = "txtSolutionName";
            this.txtSolutionName.ReadOnly = true;
            this.txtSolutionName.Size = new System.Drawing.Size(149, 22);
            this.txtSolutionName.TabIndex = 38;
            this.txtSolutionName.TabStop = false;
            this.txtSolutionName.TextChanged += new System.EventHandler(this.TxtSolutionName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Solution Name:";
            // 
            // txtTFSLabelName
            // 
            this.txtTFSLabelName.Location = new System.Drawing.Point(134, 57);
            this.txtTFSLabelName.Name = "txtTFSLabelName";
            this.txtTFSLabelName.ReadOnly = true;
            this.txtTFSLabelName.Size = new System.Drawing.Size(149, 22);
            this.txtTFSLabelName.TabIndex = 36;
            this.txtTFSLabelName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "TFS Label Name:";
            // 
            // cbxServerName
            // 
            this.cbxServerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxServerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxServerName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cbxServerName.DataSource = this.tFSConnectionsBindingSource;
            this.cbxServerName.DisplayMember = "ServerName";
            this.cbxServerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxServerName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxServerName.FormattingEnabled = true;
            this.cbxServerName.Location = new System.Drawing.Point(129, 23);
            this.cbxServerName.Name = "cbxServerName";
            this.cbxServerName.Size = new System.Drawing.Size(150, 21);
            this.cbxServerName.TabIndex = 0;
            this.cbxServerName.ValueMember = "DefaultCollection";
            this.cbxServerName.SelectedIndexChanged += new System.EventHandler(this.CbxServerName_SelectedIndexChanged);
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
            // txtTFSDefaultCollection
            // 
            this.txtTFSDefaultCollection.Location = new System.Drawing.Point(302, 22);
            this.txtTFSDefaultCollection.Name = "txtTFSDefaultCollection";
            this.txtTFSDefaultCollection.ReadOnly = true;
            this.txtTFSDefaultCollection.Size = new System.Drawing.Size(68, 22);
            this.txtTFSDefaultCollection.TabIndex = 6;
            this.txtTFSDefaultCollection.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "\\";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Server Name:";
            // 
            // tabLocalDeployment
            // 
            this.tabLocalDeployment.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabLocalDeployment.Controls.Add(this.groupBox1);
            this.tabLocalDeployment.Location = new System.Drawing.Point(4, 25);
            this.tabLocalDeployment.Name = "tabLocalDeployment";
            this.tabLocalDeployment.Size = new System.Drawing.Size(509, 134);
            this.tabLocalDeployment.TabIndex = 1;
            this.tabLocalDeployment.Text = "Deploy from Local Package";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.cbxIsRollback);
            this.groupBox1.Controls.Add(this.btnBrowseConfig);
            this.groupBox1.Controls.Add(this.btnBrowsePackage);
            this.groupBox1.Controls.Add(this.txtConfigPath);
            this.groupBox1.Controls.Add(this.txtPackagePath);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Package Paths";
            // 
            // cbxIsRollback
            // 
            this.cbxIsRollback.AutoSize = true;
            this.cbxIsRollback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxIsRollback.Location = new System.Drawing.Point(329, 102);
            this.cbxIsRollback.Name = "cbxIsRollback";
            this.cbxIsRollback.Size = new System.Drawing.Size(78, 17);
            this.cbxIsRollback.TabIndex = 6;
            this.cbxIsRollback.Text = "Is Rollback";
            this.cbxIsRollback.UseVisualStyleBackColor = true;
            this.cbxIsRollback.Visible = false;
            this.cbxIsRollback.CheckedChanged += new System.EventHandler(this.cbxIsRollback_CheckedChanged);
            // 
            // btnBrowseConfig
            // 
            this.btnBrowseConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrowseConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseConfig.Location = new System.Drawing.Point(402, 69);
            this.btnBrowseConfig.Name = "btnBrowseConfig";
            this.btnBrowseConfig.Size = new System.Drawing.Size(45, 23);
            this.btnBrowseConfig.TabIndex = 5;
            this.btnBrowseConfig.Text = "....";
            this.btnBrowseConfig.UseVisualStyleBackColor = false;
            this.btnBrowseConfig.Click += new System.EventHandler(this.BtnBrowsePackage_Click);
            // 
            // btnBrowsePackage
            // 
            this.btnBrowsePackage.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrowsePackage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowsePackage.Location = new System.Drawing.Point(402, 34);
            this.btnBrowsePackage.Name = "btnBrowsePackage";
            this.btnBrowsePackage.Size = new System.Drawing.Size(45, 23);
            this.btnBrowsePackage.TabIndex = 4;
            this.btnBrowsePackage.Text = "....";
            this.btnBrowsePackage.UseVisualStyleBackColor = false;
            this.btnBrowsePackage.Click += new System.EventHandler(this.BtnBrowsePackage_Click);
            // 
            // txtConfigPath
            // 
            this.txtConfigPath.Location = new System.Drawing.Point(113, 70);
            this.txtConfigPath.Name = "txtConfigPath";
            this.txtConfigPath.Size = new System.Drawing.Size(283, 22);
            this.txtConfigPath.TabIndex = 3;
            this.txtConfigPath.TextChanged += new System.EventHandler(this.txtPackagePath_TextChanged);
            // 
            // txtPackagePath
            // 
            this.txtPackagePath.Location = new System.Drawing.Point(112, 35);
            this.txtPackagePath.Name = "txtPackagePath";
            this.txtPackagePath.Size = new System.Drawing.Size(283, 22);
            this.txtPackagePath.TabIndex = 2;
            this.txtPackagePath.TextChanged += new System.EventHandler(this.txtPackagePath_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Config Path:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Package Path: ";
            // 
            // tvCheckList
            // 
            this.tvCheckList.BackColor = System.Drawing.SystemColors.Control;
            this.tvCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCheckList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvCheckList.ImageIndex = 2;
            this.tvCheckList.ImageList = this.imageList1;
            this.tvCheckList.Indent = 25;
            this.tvCheckList.Location = new System.Drawing.Point(0, 0);
            this.tvCheckList.Name = "tvCheckList";
            treeNode1.Name = "11";
            treeNode1.Text = "Initializing Workspace";
            treeNode2.Name = "12";
            treeNode2.Text = "Creating temporary TFS workspace";
            treeNode3.Name = "13";
            treeNode3.Text = "Downloading latest version of TFS label";
            treeNode4.Name = "14";
            treeNode4.Text = "Building Solution";
            treeNode5.Name = "15";
            treeNode5.Text = "Deleting temporary TFS workspace";
            treeNode6.Name = "10";
            treeNode6.Text = "Task 1: Compile and  Publish solution";
            treeNode7.Name = "21";
            treeNode7.Text = "Creating storage account";
            treeNode8.Name = "22";
            treeNode8.Text = "Creating hosted service";
            treeNode9.Name = "23";
            treeNode9.Text = "Copying package to cloud";
            treeNode10.Name = "24";
            treeNode10.Text = "Deploying package on cloud";
            treeNode11.Name = "25";
            treeNode11.Text = "Creating service cerificates (if any)";
            treeNode12.Name = "20";
            treeNode12.Text = "Task 2: Deploying on Cloud";
            this.tvCheckList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode12});
            this.tvCheckList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tvCheckList.SelectedImageIndex = 2;
            this.tvCheckList.ShowLines = false;
            this.tvCheckList.Size = new System.Drawing.Size(363, 390);
            this.tvCheckList.TabIndex = 2;
            this.tvCheckList.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvCheckList_BeforeSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "loading-gif.gif");
            this.imageList1.Images.SetKeyName(1, "Checked.ico");
            this.imageList1.Images.SetKeyName(2, "square.ico");
            this.imageList1.Images.SetKeyName(3, "failed.ico");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewDeployLog);
            this.panel1.Controls.Add(this.btnViewBuildLog);
            this.panel1.Controls.Add(this.btnViewTFLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 39);
            this.panel1.TabIndex = 4;
            // 
            // btnViewDeployLog
            // 
            this.btnViewDeployLog.Enabled = false;
            this.btnViewDeployLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDeployLog.Location = new System.Drawing.Point(5, 8);
            this.btnViewDeployLog.Name = "btnViewDeployLog";
            this.btnViewDeployLog.Size = new System.Drawing.Size(111, 23);
            this.btnViewDeployLog.TabIndex = 2;
            this.btnViewDeployLog.Text = "View &Deploy Log";
            this.btnViewDeployLog.UseVisualStyleBackColor = true;
            this.btnViewDeployLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnViewBuildLog
            // 
            this.btnViewBuildLog.Enabled = false;
            this.btnViewBuildLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewBuildLog.Location = new System.Drawing.Point(124, 8);
            this.btnViewBuildLog.Name = "btnViewBuildLog";
            this.btnViewBuildLog.Size = new System.Drawing.Size(111, 23);
            this.btnViewBuildLog.TabIndex = 1;
            this.btnViewBuildLog.Text = "View &Build Log";
            this.btnViewBuildLog.UseVisualStyleBackColor = true;
            this.btnViewBuildLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // btnViewTFLog
            // 
            this.btnViewTFLog.Enabled = false;
            this.btnViewTFLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewTFLog.Location = new System.Drawing.Point(243, 8);
            this.btnViewTFLog.Name = "btnViewTFLog";
            this.btnViewTFLog.Size = new System.Drawing.Size(111, 23);
            this.btnViewTFLog.TabIndex = 0;
            this.btnViewTFLog.Text = "View &TF Log";
            this.btnViewTFLog.UseVisualStyleBackColor = true;
            this.btnViewTFLog.Click += new System.EventHandler(this.BtnViewLog_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 429);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(363, 23);
            this.progressBar.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(363, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tFSConnectionsTableAdapter
            // 
            this.tFSConnectionsTableAdapter.ClearBeforeFill = true;
            // 
            // tabLocalDeploy
            // 
            this.tabLocalDeploy.Location = new System.Drawing.Point(4, 25);
            this.tabLocalDeploy.Name = "tabLocalDeploy";
            this.tabLocalDeploy.Size = new System.Drawing.Size(429, 128);
            this.tabLocalDeploy.TabIndex = 1;
            this.tabLocalDeploy.Text = "Deploy from Local Package";
            this.tabLocalDeploy.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(429, 128);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Deploy from Local Package";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.ReadOnlyChecked = true;
            this.openFileDialog.ShowReadOnly = true;
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialog_FileOk);
            // 
            // PackageDeployment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(884, 474);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PackageDeployment";
            this.Text = "Deploy Package";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PackageDeployment_FormClosing);
            this.Load += new System.EventHandler(this.PackageDeployment_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabTFSDeployment.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tFSConnectionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deploymentTrackerLocalDBTFSConnectionsDataSet)).EndInit();
            this.tabLocalDeployment.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtDeploymentNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TreeView tvCheckList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewTFLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnViewBuildLog;
        private System.Windows.Forms.ImageList imageList1;
        private DeploymentTrackerLocalDBTFSConnectionsDataSet deploymentTrackerLocalDBTFSConnectionsDataSet;
        private System.Windows.Forms.BindingSource tFSConnectionsBindingSource;
        private DeploymentTrackerLocalDBTFSConnectionsDataSetTableAdapters.TFSConnectionsTableAdapter tFSConnectionsTableAdapter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLocalDeploy;
        private System.Windows.Forms.TabPage tabTFSDeployment;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadSolutions;
        private System.Windows.Forms.Button btnLoadTFSlabel;
        private System.Windows.Forms.TextBox txtSolutionName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTFSLabelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxServerName;
        private System.Windows.Forms.TextBox txtTFSDefaultCollection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabLocalDeployment;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConfigPath;
        private System.Windows.Forms.TextBox txtPackagePath;
        private System.Windows.Forms.Button btnBrowsePackage;
        private System.Windows.Forms.Button btnBrowseConfig;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxLocations;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chbxIsUpgrade;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnLoadPublishSettings;
        private System.Windows.Forms.TextBox txtPublishSettingsFilePath;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbxSubscriptions;
        private System.Windows.Forms.Button btnViewDeployLog;
        private System.Windows.Forms.ComboBox cbxDeploymentSlot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLoadCerts;
        private System.Windows.Forms.TextBox txtCertificatePaths;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbxStorageNames;
        private System.Windows.Forms.ComboBox cbxHostedNames;
        private System.Windows.Forms.CheckBox cbxIsRollback;
    }
}