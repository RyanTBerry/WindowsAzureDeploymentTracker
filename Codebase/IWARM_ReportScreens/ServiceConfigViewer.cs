// -----------------------------------------------------------------------
// <copyright file="ServiceConfigViewer.cs" company="Microsoft IT">
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
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Linq;

    /// <summary>
    /// Service configuration UI
    /// </summary>
    public partial class ServiceConfigViewer : Form
    {
        /// <summary>
        /// Xdocument object of configuration obtained after build
        /// </summary>
        private XDocument xdoc = null;

        /// <summary>
        /// Selected role name during working
        /// </summary>
        private string selectedRoleName;

        /// <summary>
        /// Path of congfiguration file
        /// </summary>
        private string configurationPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceConfigViewer"/> class.
        /// </summary>
        /// <param name="configurationPath">The configuration path.</param>
        public ServiceConfigViewer(string configurationPath)
        {
            this.InitializeComponent();
            this.configurationPath = configurationPath;
            this.xdoc = XDocument.Load(this.configurationPath);
        }

        /// <summary>
        /// Handles the Load event of the ConfigBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ConfigBuilder_Load(object sender, EventArgs e)
        {
            this.BindRolesGrid();
            this.LoadConfigLevelSettings();
        }

        /// <summary>
        /// Loads the config level settings.
        /// </summary>
        private void LoadConfigLevelSettings()
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            var osfamily = this.xdoc.Root.Attributes("osFamily").First().Value;
            var osversion = this.xdoc.Root.Attributes("osVersion").First().Value;
            this.txtOsfamily.Text = osfamily;
            this.txtOsversion.Text = osversion;
        }

        /// <summary>
        /// Binds the roles grid.
        /// </summary>
        private void BindRolesGrid()
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            var configurationSettings = from setting in this.xdoc.Root.Descendants(ns + "Role")
                                        select new
                                        {
                                            Role = setting.FirstAttribute.Value.ToString()
                                            ////Value = setting.LastAttribute.Value.ToString()
                                        };
            this.rolesBindingSource.DataSource = configurationSettings;
            this.listboxRoles.DisplayMember = "Role";
        }

        /// <summary>
        /// Binds the settings grid.
        /// </summary>
        private void BindSettingsGrid()
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            var configurationSettings = from setting in this.xdoc.Root.Descendants(ns + "Role").Elements(ns + "ConfigurationSettings").Elements(ns + "Setting").Where(x => x.Parent.Parent.FirstAttribute.Value == this.selectedRoleName)
                                        select new
                                        {
                                            ////Role = setting.Parent.Parent.FirstAttribute.Value.ToString(),
                                            SettingKey = setting.FirstAttribute.Value.ToString(),
                                            SettingValue = setting.LastAttribute.Value.ToString()
                                        };

            this.settingsBindingSource.DataSource = configurationSettings;
            this.dgvRoleSettings.AutoGenerateColumns = true;
            this.dgvRoleSettings.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            this.dgvRoleSettings.Refresh();
            this.dgvRoleSettings.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            this.dgvCertificates.ReadOnly = false;

            // Set instance count attributes
            var instancesCount = this.xdoc.Root.Descendants(ns + "Role").Where(x => x.FirstAttribute.Value == this.selectedRoleName).Elements(ns + "Instances").Attributes().First().Value;
            this.txtInstanceRoleCount.Text = instancesCount; 
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listboxRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ListboxRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedRoleName = this.listboxRoles.SelectedValue.ToString().Trim(new char[] { '{', '}', '"' }).Replace("Role =", string.Empty).Trim();
            this.BindSettingsGrid();
            this.BindCertificateGrid();
        }

        /// <summary>
        /// Binds the certificate grid.
        /// </summary>
        private void BindCertificateGrid()
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            var configurationSettings = from setting in this.xdoc.Root.Descendants(ns + "Role").Elements(ns + "Certificates").Elements(ns + "Certificate").Where(x => x.Parent.Parent.FirstAttribute.Value == this.selectedRoleName)
                                        select new
                                        {
                                            CertificateName = setting.FirstAttribute.Value.ToString(),
                                            ThumbPrint = setting.Attributes("thumbprint").First().Value.ToString(),
                                            Algorithm = setting.LastAttribute.Value.ToString()
                                        };

            this.certsbindingSource.DataSource = configurationSettings;
            this.dgvCertificates.AutoGenerateColumns = true;
            this.dgvCertificates.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            this.dgvCertificates.Refresh();
            this.dgvCertificates.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        /// <summary>
        /// Handles the Click event of the btnAcceptSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnAcceptSettings_Click(object sender, EventArgs e)
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();

            // save text box values 
            this.xdoc.Root.Descendants(ns + "Role").Where(x => x.FirstAttribute.Value == this.selectedRoleName).Elements(ns + "Instances").Attributes().First().Value = this.txtInstanceRoleCount.Text;
            this.xdoc.Root.Attributes("osFamily").First().Value = this.txtOsfamily.Text;
            this.xdoc.Root.Attributes("osVersion").First().Value = this.txtOsversion.Text;

            // Overwrite existing config file
            this.xdoc.Save(this.configurationPath);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the CellClick event of the dgvRoleSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DgvRoleSettings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvRoleSettings.CurrentCell.ReadOnly = this.dgvRoleSettings.CurrentCell.ColumnIndex == 1 ? false : true;
        }

        /// <summary>
        /// Handles the CellClick event of the dgvCertificates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DgvCertificates_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvCertificates.CurrentCell.ReadOnly = this.dgvCertificates.CurrentCell.ColumnIndex == 1 ? false : true;
        }

        /// <summary>
        /// Handles the CellValidating event of the dgvRoleSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void DgvRoleSettings_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            string newValue = e.FormattedValue.ToString();
            string key = this.dgvRoleSettings.Rows[e.RowIndex].Cells[0].Value.ToString();

            this.xdoc.Root.Descendants(ns + "Role")
                 .Elements(ns + "ConfigurationSettings")
                 .Elements(ns + "Setting")
                 .Where(x => x.Parent.Parent.FirstAttribute.Value == this.selectedRoleName && x.FirstAttribute.Value.ToString() == key).First().Attributes("value").First().Value = newValue;
        }

        /// <summary>
        /// Handles the CellEndEdit event of the dgvRoleSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DgvRoleSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.BindSettingsGrid();
        }

        /// <summary>
        /// Handles the CellValidating event of the dgvCertificates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void DgvCertificates_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            XNamespace ns = this.xdoc.Root.GetDefaultNamespace();
            string newValue = e.FormattedValue.ToString();
            string key = this.dgvCertificates.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.xdoc.Root.Descendants(ns + "Role")
                .Elements(ns + "Certificates")
                .Elements(ns + "Certificate")
                .Where(x => x.Parent.Parent.FirstAttribute.Value == this.selectedRoleName && x.FirstAttribute.Value.ToString() == key).First().Attributes("thumbprint").First().Value = newValue;
        }

        /// <summary>
        /// Handles the CellEndEdit event of the dgvCertificates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void DgvCertificates_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.BindCertificateGrid();
        }
    }
}
