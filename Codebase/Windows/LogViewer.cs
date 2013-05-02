// -----------------------------------------------------------------------
// <copyright file="LogViewer.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.App.Windows
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using DeploymentTracker.App.Utilities;

    /// <summary>
    /// LogViewer to view the logs generated
    /// </summary>
    public partial class LogViewer : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewer"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public LogViewer(string filePath)
        {
            this.InitializeComponent();
            this.InitializeTextbox(filePath);
        }

        /// <summary>
        /// Handles the Click event of the BtnCopyToClipboard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCopyToClipboard_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in this.rtbxLogbox.Lines)
            {
                sb.AppendLine(line);
            }

            Clipboard.SetText(sb.ToString());
        }

        /// <summary>
        /// Handles the Click event of the BtnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Initializes the textbox.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        private void InitializeTextbox(string filePath)
        {
            try
            {
                this.rtbxLogbox.Text = File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (DirectoryNotFoundException ex)
            {
                ex.ShowUIException();
            }
            catch (FileNotFoundException ex)
            {
                ex.ShowUIException();
            }
            catch (UnauthorizedAccessException ex)
            {
                ex.ShowUIException();
            }
            catch (Exception ex)
            {
                ex.ShowGenericException();
            }
        }
    }
}
