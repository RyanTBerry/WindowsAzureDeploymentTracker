using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DeploymentTracker.Properties;

namespace DeploymentTracker.IWARM_ReportScreens
{
    public partial class SubscriptionDownloadPage : Form
    {
        public SubscriptionDownloadPage()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri(Settings.Default.AzurePublishSettingsDownloadLink);
        }
    }
}
