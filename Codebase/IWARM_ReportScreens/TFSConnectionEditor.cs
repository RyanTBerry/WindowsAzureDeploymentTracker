using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeploymentTracker.Utilities;
using System.Text.RegularExpressions;

namespace DeploymentTracker.IWARM_ReportScreens
{
    public partial class TFSConnectionEditor : Form
    {
        public TFSConnectionEditor()
        {
            this.InitializeComponent();
        }

        public TFSConnectionEditor(TFSConnectionString tfsConectionString) : this()
        {
            tfsConectionString.Validate();
            this.txtServerName.Text = tfsConectionString.ServerName;
            this.txtPortNumber.Text = tfsConectionString.PortNumber.ToString();
            this.txtDefaultCollection.Text = tfsConectionString.DefaultCollection;
        }

        private bool IsValidTFSConfiguration()
        {
            this.errorProvider.Clear();
            if (string.IsNullOrEmpty(this.txtServerName.Text))
            {
                this.errorProvider.SetError(this.txtServerName, "Invalid TFS server name");
                return false;
            }

            if (string.IsNullOrEmpty(this.txtPortNumber.Text) || !Regex.IsMatch(this.txtPortNumber.Text, @"^\d+$"))
            {
                this.errorProvider.SetError(this.txtPortNumber, "Invalid TFS port number");
                return false;
            }

            if (string.IsNullOrEmpty(this.txtDefaultCollection.Text))
            {
                this.errorProvider.SetError(this.txtDefaultCollection, "Invalid TFS collection");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidTFSConfiguration())
            {
                return;
            }

            try
            {
                TFSConnectionString newtfsConnectionString = new TFSConnectionString
                {
                    ServerName = this.txtServerName.Text,
                    PortNumber = int.Parse(this.txtPortNumber.Text),
                    DefaultCollection = this.txtDefaultCollection.Text
                };

                newtfsConnectionString.Validate();
                newtfsConnectionString.SaveOrUpdateTFSConnectionStringToDB();
                this.Close();
            }
            catch (ArgumentNullException argumentex)
            {
                MessageBox.Show(argumentex.Message, "Invalid TFS connection parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException formatException)
            {
                MessageBox.Show(formatException.Message, "Invalid TFS connection parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid TFS connection parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
