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

namespace DeploymentTracker.Services.Entities
{
    using DeploymentTracker.Services.Utilities;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloudArgs
    {
        /// <summary>
        /// Gets or sets the azure log file path.
        /// </summary>
        /// <value>
        /// The azure log file path.
        /// </value>
        public string AzureLogFilePath { get; set; }

        /// <summary>
        /// Gets or sets the azure physical location like north america etc.
        /// </summary>
        /// <value>
        /// The azure location.
        /// </value>
        public string AzureLocation { get; set; }

        /// <summary>
        /// Gets or sets the number of tries.
        /// </summary>
        /// <value>
        /// The number of tries.
        /// </value>
        public int NumberOfTries { get; set; }

        /// <summary>
        /// Gets or sets the package file path.
        /// </summary>
        /// <value>
        /// The package file path.
        /// </value>
        public string PackageFilePath { get; set; }

        /// <summary>
        /// Gets or sets the service config file path.
        /// </summary>
        /// <value>
        /// The service config file path.
        /// </value>
        public string ServiceConfigFilePath { get; set; }

        /// <summary>
        /// Gets or sets the management certificatebase64string.
        /// </summary>
        /// <value>
        /// The management certificatebase64string.
        /// </value>
        public string ManagementCertificatebase64string { get; set; }

        /// <summary>
        /// Gets or sets the subscription id.
        /// </summary>
        /// <value>
        /// The subscription id.
        /// </value>
        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the storage.
        /// </summary>
        /// <value>
        /// The name of the storage.
        /// </value>
        public string StorageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the hosted service.
        /// </summary>
        /// <value>
        /// The name of the hosted service.
        /// </value>
        public string HostedServiceName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is upgrade.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is upgrade; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpgrade { get; set; }

        /// <summary>
        /// Gets or sets the deployment slot.
        /// </summary>
        /// <value>
        /// The deployment slot.
        /// </value>
        public string DeploymentSlot { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is build completed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is build completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsBuildCompleted { get; set; }

        /// <summary>
        /// Gets or sets the certificate paths.
        /// </summary>
        /// <value>
        /// The certificate paths.
        /// </value>
        public List<string> CertificatePaths { get; set; }

        /// <summary>
        /// Gets or sets the internal version number.
        /// </summary>
        /// <value>
        /// The internal version number.
        /// </value>
        public string InternalVersionNumber { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            if (this.IsBuildCompleted)
            {
                this.PackageFilePath.VerifyNotNull("Package File URL", "Package File URL must be specified");
                this.ServiceConfigFilePath.VerifyNotNull("Service Configuration File URL", "Service Configuration File URL must be specified");
                this.AzureLogFilePath.VerifyNotNull("Azure log File Url", "Azure log file Url must be specified");
            }

            this.StorageName.VerifyNotNull("Storage Name", "Storage Name can not be null");
            this.HostedServiceName.VerifyNotNull("Hosted Service Name", "Hosted Service Name can not be null");
            this.AzureLocation.VerifyNotNull("Azure Location", "Azure Location can not be null");
            this.ManagementCertificatebase64string.VerifyNotNull("Management Certification", "Management Ceritification can not be null");
            this.SubscriptionId.VerifyNotNull("Subscription Id", "Subscription id can not null");
            this.NumberOfTries.VerifyNonZero("Number of tries", "No of tries can not be zero");
            this.DeploymentSlot.VerifyNotNull("Environment", "Atleast one environment has to be selected");

            if (this.CertificatePaths != null && this.CertificatePaths.Count > 0)
            {
                foreach (var item in this.CertificatePaths)
                {
                    item.VerifyPhysicalPath("Certificate Paths", "Path doesnot exist or invalid");
                }
            }
        }
    }
}
