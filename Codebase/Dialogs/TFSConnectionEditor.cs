//Copyright (c) Microsoft Corporation 
//All rights reserved. 
//Microsoft Platform and Azure License
//This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//
//1. Definitions
//The terms “reproduce,” “reproduction,” “derivative works,” and “distribution” have the same meaning here as under U.S. copyright law.
//A “contribution” is the original software, or any additions or changes to the software.
//A “contributor” is any person that distributes its contribution under this license.
//“Licensed patents” are a contributor’s patent claims that read directly on its contribution.
//
//2. Grant of Rights
//(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//
//3. Conditions and Limitations
//(A) No Trademark License- This license does not grant you rights to use any contributors’ name, logo, or trademarks.
//(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//(E) The software is licensed “as-is.” You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
//(F) Platform Limitation- The licenses granted in sections 2(A) & 2(B) extend only to the software or derivative works that (1) runs on a Microsoft Windows operating system product, and (2) operates with Microsoft Windows Azure.

using System.Globalization;

namespace DeploymentTracker.App.Dialogs
{
    using System;
    using System.Data;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using DeploymentTracker.App.Utilities;
    using DeploymentTracker.Services.Entities;
    using System.IO;

    /// <summary>
    /// TFS connection string editor frm
    /// </summary>
    public partial class TFSConnectionEditor : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TFSConnectionEditor"/> class.
        /// </summary>
        public TFSConnectionEditor()
        {
            this.InitializeComponent();
            this.rbtHttp.Checked = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TFSConnectionEditor"/> class.
        /// </summary>
        /// <param name="tfsConectionString">The TFS conection string.</param>
        /// <param name="isEdit">if set to <c>true</c> [is edit].</param>
        public TFSConnectionEditor(TFSConnectionString tfsConectionString, bool isEdit = false) : this()
        {
            tfsConectionString.Validate();
            this.txtServerName.Text = tfsConectionString.ServerName;
            this.txtPortNumber.Text = tfsConectionString.PortNumber.ToString(CultureInfo.InvariantCulture);
            this.txtDefaultCollection.Text = tfsConectionString.DefaultCollection;
            this.rbtHttps.Checked = tfsConectionString.IsHttps;
            this.rbtHttp.Checked = !tfsConectionString.IsHttps;

            // Fix: 251237
            if (isEdit)
            {
                this.txtServerName.ReadOnly = true;
            }
        }

        /// <summary>
        /// Determines whether [is valid TFS configuration].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid TFS configuration]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidTFSConfiguration()
        {
            this.errorProvider.Clear();
            if (string.IsNullOrEmpty(this.txtServerName.Text) || this.txtServerName.Text.Contains(" "))
            {
                this.errorProvider.SetError(this.txtServerName, "Invalid TFS server name");
                return false;
            }

            foreach (var item in this.txtServerName.Text.ToCharArray())
            {
                if ("\"/:<>\\|*?@".Contains(item.ToString(CultureInfo.InvariantCulture)))
                {
                    this.errorProvider.SetError(this.txtServerName, "Invalid TFS server name");
                    return false;
                }
            }
            try
            {
                if (string.IsNullOrEmpty(this.txtPortNumber.Text) || !Regex.IsMatch(this.txtPortNumber.Text, @"^\d+$") || int.Parse(this.txtPortNumber.Text) <= 0)
                {
                    this.errorProvider.SetError(this.txtPortNumber, "Invalid TFS port number");
                    return false;
                }
            }
            catch (OverflowException)
            {
                this.errorProvider.SetError(this.txtPortNumber, "Invalid TFS port number");
                return false;
            }
            catch (FormatException)
            {
                this.errorProvider.SetError(this.txtPortNumber, "Invalid TFS port number");
                return false;
            }


            if (string.IsNullOrEmpty(this.txtDefaultCollection.Text) || this.txtDefaultCollection.Text.Contains(" "))
            {
                this.errorProvider.SetError(this.txtDefaultCollection, "Invalid TFS collection");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // This ensures proper values for database
            if (!this.IsValidTFSConfiguration())
            {
                return;
            }

            // throw any kind of db error. Don't show here since this is a dialog.
            try
            {
                TFSConnectionString newtfsConnectionString = new TFSConnectionString
                {
                    IsHttps = this.rbtHttps.Checked,
                    ServerName = this.txtServerName.Text,
                    PortNumber = int.Parse(this.txtPortNumber.Text),
                    DefaultCollection = this.txtDefaultCollection.Text
                };

                newtfsConnectionString.Validate();
                newtfsConnectionString.SaveOrUpdateTFSConnectionStringToDB(!this.txtServerName.ReadOnly);
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                ex.ShowUIException();
            }
            catch (ArgumentException ex)
            {
                ex.ShowUIException();
            }
            catch
            {
                new DBConcurrencyException("Unable to write changes to database. Unexpected error. Please try again.").ShowUIException();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
