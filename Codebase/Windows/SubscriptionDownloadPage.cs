// -----------------------------------------------------------------------
// <copyright file="SubscriptionDownloadPage.cs" company="Microsoft IT">
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
    using System.Windows.Forms;
    using DeploymentTracker.Properties;

    /// <summary>
    /// PublishSettings file download page
    /// </summary>
    public partial class SubscriptionDownloadPage : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionDownloadPage"/> class.
        /// </summary>
        public SubscriptionDownloadPage()
        {
            this.InitializeComponent();
            this.webBrowser1.Url = new Uri(Settings.Default.AzurePublishSettingsDownloadLink);
        }
    }
}
