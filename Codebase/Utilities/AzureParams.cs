// -----------------------------------------------------------------------
// <copyright file="AzureParams.cs" company="MSIT">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AzureParams
    {
        /// <summary>
        /// Gets or sets the azure physical location like north america etc.
        /// </summary>
        /// <value>
        /// The azure location.
        /// </value>
        public string AzureLocation { get; set; }

        public int NumberOfTries { get; set; }

        public string PackageFileUrl { get; set; }

        public string ServiceConfigFileUrl { get; set; }

        public string ManagementCertificatebase64string
        {
            get; 
            set;
        }

        public string SubscriptionId { get; set; }

        internal void Validate()
        {
            if(string.IsNullOrEmpty(AzureLocation) || NumberOfTries == 0
                || string.IsNullOrEmpty(PackageFileUrl) || string.IsNullOrEmpty(ServiceConfigFileUrl)
                || string.IsNullOrEmpty(ManagementCertificatebase64string)
                || string.IsNullOrEmpty(SubscriptionId))
            {
                throw new ArgumentNullException(string.Format(@"Invalid Azure Parameters. Data: AzureLocation: [{0}] | NumberOfTries: [{1}] | PackageUrl: [{2}] | ServiceConfigFileUrl: [{3}] | PublishSettingFileUrl: [{4}]",
                    this.AzureLocation, this.NumberOfTries, this.PackageFileUrl, this.ServiceConfigFileUrl, this,ManagementCertificatebase64string));
            }
        }


        public string StorageName { get; set; }

        public string HostedServiceName { get; set; }

        public bool IsUpgrade { get; set; }

        public string DeploymentSlot { get; set; }
    }
}
