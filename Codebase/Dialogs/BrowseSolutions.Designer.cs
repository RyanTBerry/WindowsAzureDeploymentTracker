namespace DeploymentTracker.App.Dialogs
{
    partial class BrowseSolutions
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
            this.txtLabelFilter = new System.Windows.Forms.TextBox();
            this.gdvTFSSolutions = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOwnerFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelectLabel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gdvTFSSolutions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLabelFilter
            // 
            this.txtLabelFilter.Location = new System.Drawing.Point(78, 21);
            this.txtLabelFilter.Name = "txtLabelFilter";
            this.txtLabelFilter.ReadOnly = true;
            this.txtLabelFilter.Size = new System.Drawing.Size(250, 22);
            this.txtLabelFilter.TabIndex = 2;
            // 
            // gdvTFSSolutions
            // 
            this.gdvTFSSolutions.AutoGenerateColumns = false;
            this.gdvTFSSolutions.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.gdvTFSSolutions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gdvTFSSolutions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvTFSSolutions.DataSource = this.bindingSource1;
            this.gdvTFSSolutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvTFSSolutions.Location = new System.Drawing.Point(0, 111);
            this.gdvTFSSolutions.MultiSelect = false;
            this.gdvTFSSolutions.Name = "gdvTFSSolutions";
            this.gdvTFSSolutions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvTFSSolutions.Size = new System.Drawing.Size(340, 208);
            this.gdvTFSSolutions.TabIndex = 1;
            this.gdvTFSSolutions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GdvTFSLabels_CellMouseDoubleClick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 357);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(340, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtOwnerFilter);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLabelFilter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 111);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find Options";
            // 
            // txtOwnerFilter
            // 
            this.txtOwnerFilter.Location = new System.Drawing.Point(78, 50);
            this.txtOwnerFilter.Name = "txtOwnerFilter";
            this.txtOwnerFilter.ReadOnly = true;
            this.txtOwnerFilter.Size = new System.Drawing.Size(250, 22);
            this.txtOwnerFilter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Owner Name:";
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Location = new System.Drawing.Point(7, 82);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "&Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Label Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelectLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 319);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(340, 38);
            this.panel1.TabIndex = 4;
            // 
            // btnSelectLabel
            // 
            this.btnSelectLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectLabel.Location = new System.Drawing.Point(223, 7);
            this.btnSelectLabel.Name = "btnSelectLabel";
            this.btnSelectLabel.Size = new System.Drawing.Size(75, 23);
            this.btnSelectLabel.TabIndex = 0;
            this.btnSelectLabel.Text = "&Select";
            this.btnSelectLabel.UseVisualStyleBackColor = true;
            this.btnSelectLabel.Click += new System.EventHandler(this.BtnSelectLabel_Click);
            // 
            // BrowseSolutions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(340, 379);
            this.Controls.Add(this.gdvTFSSolutions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "BrowseSolutions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Solution";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowseSolutions_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gdvTFSSolutions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLabelFilter;
        private System.Windows.Forms.DataGridView gdvTFSSolutions;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOwnerFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectLabel;
    }
}