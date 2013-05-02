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

namespace DeploymentTracker.Services.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;
    using DeploymentTracker.Services.Entities;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// Contains all cloud deployment tasks
    /// </summary>
    public class CloudDeploymentTasks : IDisposable
    {
        #region Private Variables
        /// <summary>
        /// Number of tries
        /// </summary>
        private readonly int numberOfTries;

        /// <summary>
        /// Subscription id
        /// </summary>
        private readonly string subscriptionId;

        /// <summary>
        /// Certificate to connect to cloud
        /// </summary>
        private readonly X509Certificate2 managementCert;

        /// <summary>
        /// Hosted service name
        /// </summary>
        private readonly string hostedServiceName;

        /// <summary>
        /// Storage name
        /// </summary>
        private readonly string storageName;

        /// <summary>
        /// Location to deploy
        /// </summary>
        private readonly string location;

        /// <summary>
        /// Deployment slot. Should be 'production' or 'staging'
        /// </summary>
        private readonly string deploymentSlot;

        /// <summary>
        /// Config file path
        /// </summary>
        private readonly string configFile;

        /// <summary>
        /// Denotes if this operation is a upgrade
        /// </summary>
        private readonly bool isUpgrade;

        /// <summary>
        /// Package url
        /// </summary>
        private readonly string packageUrl;

        /// <summary>
        /// Log stream writer to log all steps
        /// </summary>
        private readonly StreamWriter logStreamWriter;

        /// <summary>
        /// Service certificate paths
        /// </summary>
        private readonly List<string> certificatePaths;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudDeploymentTasks"/> class.
        /// </summary>
        /// <param name="cloudArgs">The cloud args.</param>
        public CloudDeploymentTasks(CloudArgs cloudArgs)
        {
            cloudArgs.Validate();
            this.subscriptionId = cloudArgs.SubscriptionId;
            this.managementCert = new X509Certificate2(Convert.FromBase64String(cloudArgs.ManagementCertificatebase64string));
            this.hostedServiceName = cloudArgs.HostedServiceName.ToLower(CultureInfo.CurrentCulture);
            this.storageName = cloudArgs.StorageName.ToLower(CultureInfo.CurrentCulture);
            this.location = cloudArgs.AzureLocation;
            this.deploymentSlot = cloudArgs.DeploymentSlot.ToLower(CultureInfo.CurrentCulture);
            this.configFile = cloudArgs.ServiceConfigFilePath;
            this.packageUrl = cloudArgs.PackageFilePath;
            this.isUpgrade = cloudArgs.IsUpgrade;
            this.numberOfTries = cloudArgs.NumberOfTries;
            this.certificatePaths = cloudArgs.CertificatePaths;
            if (!Directory.Exists(Path.GetDirectoryName(cloudArgs.AzureLogFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(cloudArgs.AzureLogFilePath));
            }

            this.logStreamWriter = File.AppendText(cloudArgs.AzureLogFilePath);
            this.logStreamWriter.WriteLine(string.Format("Cloud deployment started at {0}", DateTime.Now));
        }

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is upgrade.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is upgrade; otherwise, <c>false</c>.
        /// </value>
        public bool IsUpgrade
        {
            get { return this.isUpgrade; }
        }

        /// <summary>
        /// Gets the certificate paths.
        /// </summary>
        public List<string> CertificatePaths
        {
            get { return this.certificatePaths; }
        }
        #endregion
        
        #region Public Functions
        /// <summary>
        /// Lists the locations for subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="version">The version.</param>
        /// <returns>Locations for subscriptions</returns>
        public static Dictionary<string, string> ListLocationsForSubscription(string subscriptionId, X509Certificate2 certificate, string version)
        {
            Dictionary<string, string> dlocation = new Dictionary<string, string>();
            string uriFormat = "https://management.core.windows.net/{0}/locations";
            Uri uri = new Uri(string.Format(uriFormat, subscriptionId));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", version);
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            XDocument responseBody = null;
            HttpStatusCode statusCode;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // GetResponse throws a WebException for 400 and 500 status codes
                response = (HttpWebResponse)ex.Response;
            }

            statusCode = response.StatusCode;
            if (response.ContentLength > 0)
            {
                using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                {
                    responseBody = XDocument.Load(reader);
                }
            }

            response.Close();
            XNamespace wa = "http://schemas.microsoft.com/windowsazure";
            if (statusCode.Equals(HttpStatusCode.OK))
            {
                XElement locations = responseBody.Element(wa + "Locations");
                foreach (XElement location in locations.Elements(wa + "Location"))
                {
                    string name = location.Element(wa + "Name").Value;
                    string displayName = location.Element(wa + "DisplayName").Value;
                    dlocation.Add(name, displayName);
                }
            }
            else
            {
                throw new ApplicationException(string.Format(
                    "Call to List Locations returned an error: Status Code: {0} ({1}):{2}{3}",
                    (int)statusCode, 
                    statusCode, 
                    Environment.NewLine,
                    responseBody.ToString(SaveOptions.OmitDuplicateNamespaces)));
            }

            return dlocation;
        }

        /// <summary>
        /// Lists the hosted services for subscriptions.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="version">The version.</param>
        /// <returns>List of hosted service names</returns>
        public static List<string> ListHostedServicesForSubscription(string subscriptionId, X509Certificate2 certificate, string version)
        {
            List<string> dhostedServices = new List<string>();
            string uriFormat = "https://management.core.windows.net/{0}/services/hostedservices";
            Uri uri = new Uri(String.Format(uriFormat, subscriptionId));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", version);
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            XDocument responseBody = null;
            HttpStatusCode statusCode;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // GetResponse throws a WebException for 400 and 500 status codes
                response = (HttpWebResponse)ex.Response;
            }
            statusCode = response.StatusCode;
            if (response.ContentLength > 0)
            {
                using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                {
                    responseBody = XDocument.Load(reader);
                }
            }
            response.Close();
            if (statusCode.Equals(HttpStatusCode.OK))
            {
                XNamespace wa = "http://schemas.microsoft.com/windowsazure";
                XElement hostedServices = responseBody.Element(wa + "HostedServices");
                //Console.WriteLine(
                //    "Hosted Services for Subscription ID {0}:{1}{2}",
                //    subscriptionId,
                //    Environment.NewLine,
                //    hostedServices.ToString(SaveOptions.OmitDuplicateNamespaces));
                foreach (var item in hostedServices.Elements(wa+"HostedService"))
                {
                    dhostedServices.Add(item.Element(wa + "ServiceName").Value);    
                }
            }
            else
            {
                throw new ApplicationException(string.Concat("Call to List Hosted Services returned an error:",Environment.NewLine, 
                    string.Format("Status Code: {0} ({1}):{2}{3}",
                    (int)statusCode, statusCode, Environment.NewLine,
                    responseBody.ToString(SaveOptions.OmitDuplicateNamespaces))));
            }

            if (dhostedServices.Count == 0)
            {
                dhostedServices.Add("--NIL--");
            }

            return dhostedServices;
        }


        /// <summary>
        /// Lists the hosted services for subscriptions.
        /// </summary>
        /// <param name="subscriptionId">The subscription id.</param>
        /// <param name="certificate">The certificate.</param>
        /// <param name="version">The version.</param>
        /// <returns>
        /// List of storage names
        /// </returns>
        public static List<string> ListStorageAccountsForSubscription(string subscriptionId, X509Certificate2 certificate, string version)
        {
            List<string> storageAccountList = new List<string>();
            string uriFormat = "https://management.core.windows.net/{0}/services/storageservices";
            Uri uri = new Uri(String.Format(uriFormat, subscriptionId));

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.Headers.Add("x-ms-version", version);
            request.ClientCertificates.Add(certificate);
            request.ContentType = "application/xml";

            XDocument responseBody = null;
            HttpStatusCode statusCode;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                // GetResponse throws a WebException for 400 and 500 status codes
                response = (HttpWebResponse)ex.Response;
            }

            statusCode = response.StatusCode;
            if (response.ContentLength > 0)
            {
                using (XmlReader reader = XmlReader.Create(response.GetResponseStream()))
                {
                    responseBody = XDocument.Load(reader);
                }
            }

            response.Close();
            if (statusCode.Equals(HttpStatusCode.OK))
            {
                XNamespace wa = "http://schemas.microsoft.com/windowsazure";
                XElement storageServices = responseBody.Element(wa + "StorageServices");
                foreach (XElement storageService in storageServices.Elements(wa + "StorageService"))
                {
                    string url = storageService.Element(wa + "Url").Value;
                    string serviceName = storageService.Element(wa + "ServiceName").Value;
                    storageAccountList.Add(serviceName);
                }
            }
            else
            {
                throw new ApplicationException (string.Concat("Call to List Storage Accounts returned an error:", Environment.NewLine,
                string.Format("Status Code: {0} ({1}):{2}{3}",
                    (int)statusCode, statusCode, Environment.NewLine,
                    responseBody.ToString(SaveOptions.OmitDuplicateNamespaces))));
            }

            if (storageAccountList.Count == 0)
            {
                storageAccountList.Add("--NIL--");
            }

            return storageAccountList;
        }

        /// <summary>
        /// Adds the certificate to cloud.
        /// </summary>
        /// <param name="pfxPath">The PFX path.</param>
        /// <param name="password">The password.</param>
        public void AddCertificateToCloud(string pfxPath, string password)
        {
            string pfxFileName = Path.GetFileName(pfxPath);
            int remainingTries = this.numberOfTries;
            while (remainingTries-- > 0)
            {
                this.logStreamWriter.WriteLine("Attempting to create the service certificate {0} on hosted service {1}.cloudapp.net ...", pfxFileName, this.hostedServiceName);
                try
                {
                    string request = this.AddCertificate(pfxPath, password);
                    this.WaitForOperationCompletion(request);
                    this.logStreamWriter.WriteLine("{0} service certificate created", pfxFileName);
                    break;
                }
                catch (Exception ex)
                {
                    if (remainingTries == 0)
                    {
                        throw;
                    }
                    else
                    {
                        this.logStreamWriter.WriteLine("Warning (Trying again): {0}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Inits the hosted service.
        /// </summary>
        public void InitHostedService()
        {
            int remainingTries = this.numberOfTries;
            while (remainingTries-- > 0)
            {
                this.logStreamWriter.WriteLine("Attempting to create the hosted service {0}.cloudapp.net. Remaining Tries = {1} ...", this.hostedServiceName, remainingTries);
                try
                {
                    string requestId = this.CreateHostedService();
                    this.WaitForOperationCompletion(requestId);
                    this.logStreamWriter.WriteLine("Hosted service '{0}.cloudapp.net' created.", this.hostedServiceName);
                    break;
                }
                catch (Exception ex)
                {
                    if (remainingTries == 0)
                    {
                        throw;
                    }
                    else
                    {
                        this.logStreamWriter.WriteLine("Warning (Trying again): {0}", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Inits the storage account.
        /// </summary>
        /// <returns>Storage primary key</returns>
        public string InitStorageAccount()
        {
            int remainingTries = this.numberOfTries;
            string storagePrimaryKey = string.Empty;

            while (remainingTries-- > 0)
            {
                this.logStreamWriter.WriteLine("Attempting to create the storage '{0}' . Remaining Tries =  {1}...", this.storageName, remainingTries);
                try
                {
                    string requestId1 = this.CreateStorageAccount();
                    this.WaitForOperationCompletion(requestId1);
                    storagePrimaryKey = this.GetStoragePrimaryKey();
                    this.logStreamWriter.WriteLine("Storage account {0} primary key created and recovered", this.storageName);
                    break;
                }
                catch (Exception ex)
                {
                    if (remainingTries == 0)
                    {
                        throw;
                    }
                    else
                    {
                        this.logStreamWriter.WriteLine("Warning (Trying again): {0}", ex);
                    }
                }
            }

            return storagePrimaryKey;
        }

        /// <summary>
        /// Deploys the package in cloud.
        /// </summary>
        /// <param name="storagePrimaryKey">The storage primary key.</param>
        public void DeployPackageInCloud()
        {
            string requestId3 = this.DeployPackage();
            this.WaitForOperationCompletion(requestId3);
        }

        /// <summary>
        /// Copies the CSPKG.
        /// </summary>
        /// <param name="storagePrimaryKey">The storage primary key.</param>
        public void CopyCspkg(string storagePrimaryKey)
        {
            this.logStreamWriter.WriteLine(string.Format("Copying Package to storage account '{0}' started", this.storageName));

            CloudStorageAccount account = new CloudStorageAccount(
                new StorageCredentialsAccountAndKey(this.storageName, storagePrimaryKey), true);

            CloudBlobClient blobStorage = account.CreateCloudBlobClient();

            blobStorage.GetContainerReference("package").CreateIfNotExist();
            var blobReference = blobStorage.GetContainerReference("package").GetBlobReference(Path.GetFileName(this.packageUrl));

            using (Stream packageStream = File.OpenRead(this.packageUrl))
            {
                BlobRequestOptions options = new BlobRequestOptions() { Timeout = new TimeSpan(1, 0, 0) };
                blobReference.UploadFromStream(packageStream, options);
            }

            this.logStreamWriter.WriteLine(string.Format("Package copied successfully to storage account '{0}'", this.storageName));
        }

        /// <summary>
        /// Gets the storage primary key.
        /// </summary>
        /// <returns>Storage primary key</returns>
        public string GetStoragePrimaryKey()
        {
            this.logStreamWriter.WriteLine(string.Format("Trying to get primary key for storage account '{0}'...", this.storageName));

            string primaryKey = string.Empty;

            try
            {
                XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

                var req = (HttpWebRequest)WebRequest.Create(
                    string.Format(
                    "https://management.core.windows.net/{0}/services/storageservices/{1}/keys",
                        this.subscriptionId,
                        this.storageName));

                req.Headers["x-ms-version"] = "2011-10-01";
                req.ClientCertificates.Add(this.managementCert);

                using (var response = req.GetResponse() as HttpWebResponse)
                {
                    XDocument documentResponse = XDocument.Load(response.GetResponseStream());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(
                            string.Format(
                            "Unable to retrieve the primary key for the storage account {0}. Status = {1}",
                            this.storageName,
                            response.StatusCode));
                    }

                    primaryKey = documentResponse.Descendants(xmlns.GetName("Primary")).First().Value;
                }
            }
            catch (Exception)
            {
                this.logStreamWriter.WriteLine(string.Format("Unable to find storage key for storage account {0}. Hence trying to create a new account", this.storageName));
            }

            return primaryKey;
        }

        #endregion

        #region IDisposible functions
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (this.logStreamWriter != null)
                {
                    this.logStreamWriter.Close();
                }
            }
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Deploys the package.
        /// </summary>
        /// <param name="storagePrimaryKey">The storage primary key.</param>
        /// <returns>Request id</returns>
        private string DeployPackage()
        {
            this.logStreamWriter.WriteLine(string.Format("Deploying package in hosted service '{0}' started...", this.hostedServiceName));
            var req = (HttpWebRequest)WebRequest.Create(
                string.Format(
                this.isUpgrade ? "https://management.core.windows.net/{0}/services/hostedservices/{1}/deploymentslots/{2}/?comp=upgrade" :
                "https://management.core.windows.net/{0}/services/hostedservices/{1}/deploymentslots/{2}",
                this.subscriptionId, 
                this.hostedServiceName, 
                this.deploymentSlot));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(this.managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            string updatedConfiguration = File.ReadAllText(this.configFile);

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();

            string packageUrl = string.Format(
                "https://{0}.blob.core.windows.net/package/{1}",
                this.storageName, 
                Path.GetFileName(this.packageUrl));

            xreq.Add(
            !this.isUpgrade ? 
            new XElement(xmlns.GetName("CreateDeployment"),
                new XElement(xmlns.GetName("Name"), hostedServiceName),
                new XElement(xmlns.GetName("PackageUrl"), packageUrl),
                new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(Path.GetFileNameWithoutExtension(this.packageUrl)))),
                new XElement(xmlns.GetName("Configuration"), Convert.ToBase64String(Encoding.UTF8.GetBytes(updatedConfiguration))),
                new XElement(xmlns.GetName("StartDeployment"), "true"),
                new XElement(xmlns.GetName("TreatWarningsAsError"), "false"))
                :
                new XElement(xmlns.GetName("UpgradeDeployment"),
                new XElement(xmlns.GetName("Mode"), "auto"),
                new XElement(xmlns.GetName("PackageUrl"), packageUrl),
                new XElement(xmlns.GetName("Configuration"), Convert.ToBase64String(Encoding.UTF8.GetBytes(updatedConfiguration))),
                new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(Path.GetFileNameWithoutExtension(this.packageUrl)))),
                new XElement(xmlns.GetName("Force"), "true"))
                );


            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                this.logStreamWriter.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    this.logStreamWriter.WriteLine(string.Format("Deploying package in hosted service '{0}' failed. See App log for further details.", this.hostedServiceName));
                    throw new ApplicationException(
                        string.Format("Error during deployment. status= {0}", response.StatusCode));
                }

                this.logStreamWriter.WriteLine(string.Format("Deploying package in hosted service '{0}' successful...", this.hostedServiceName));
                return response.Headers["x-ms-request-id"];
            }
        }

        /// <summary>
        /// Adds the certificate.
        /// </summary>
        /// <param name="pfxPath">The PFX path.</param>
        /// <param name="password">The password.</param>
        /// <returns>Request id</returns>
        private string AddCertificate(string pfxPath, string password)
        {
            this.logStreamWriter.WriteLine(string.Format("Adding certificate {0} to cloud started..", Path.GetFileNameWithoutExtension(pfxPath)));

            // Construct the request URI.    
            var req = (HttpWebRequest)WebRequest.Create(string.Format("https://management.core.windows.net/{0}/services/hostedservices/{1}/certificates", this.subscriptionId, this.hostedServiceName));

            // Set the request method and the content type for the request.
            req.Method = "POST";
            req.ContentType = "application/xml";

            // Add the x-ms-version header.
            req.Headers.Add("x-ms-version", "2011-10-01");

            // Add the certificate.
            req.ClientCertificates.Add(this.managementCert);

            // Construct the request body.
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.Write(
                    string.Format(
                            @"<?xml version=""1.0"" encoding=""utf-8""?>
                                   <CertificateFile xmlns=""http://schemas.microsoft.com/windowsazure"">
                                   <Data>{0}</Data>
                                   <CertificateFormat>pfx</CertificateFormat>
                                   <Password>{1}</Password>
                                   </CertificateFile>",
                                           Convert.ToBase64String(File.ReadAllBytes(pfxPath)),
                                           password));
            }

            this.logStreamWriter.WriteLine(string.Format("Adding certificate {0} to cloud successful..", Path.GetFileNameWithoutExtension(pfxPath)));

            // Submit the request and return the request ID.
            return req.GetResponse().Headers["x-ms-request-id"];
        }

        /// <summary>
        /// Creates the hosted service.
        /// </summary>
        /// <returns>Request id</returns>
        private string CreateHostedService()
        {
            // documentation: http://msdn.microsoft.com/en-us/library/gg441304.aspx
            var req = (HttpWebRequest)WebRequest.Create(
                string.Format(
                "https://management.core.windows.net/{0}/services/hostedservices",
                this.subscriptionId));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(this.managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();
            xreq.Add(
                new XElement(
                    xmlns.GetName("CreateHostedService"),
                    new XElement(xmlns.GetName("ServiceName"), this.hostedServiceName),
                    new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(this.hostedServiceName))),
                    new XElement(xmlns.GetName("Description"), this.hostedServiceName + " created. Automation by Deployment Tracker"),
                    new XElement(xmlns.GetName("Location"), this.location)));

            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                this.logStreamWriter.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    this.logStreamWriter.WriteLine(string.Format("Creating hosted service '{0}' failed ....", this.hostedServiceName));
                    throw new ApplicationException(
                        string.Format("Error creating a hosted service. status ={0}", response.StatusCode));
                }

                this.logStreamWriter.WriteLine(string.Format("Created hosted service '{0}' successfully....", this.hostedServiceName));
                return response.Headers["x-ms-request-id"];
            }
        }

        /// <summary>
        /// Waits for operation completion.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        private void WaitForOperationCompletion(string requestId)
        {
            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";
            this.logStreamWriter.WriteLine("pending request {0}", requestId);

            while (true)
            {
                var req = (HttpWebRequest)WebRequest.Create(
                        string.Format(
                        "https://management.core.windows.net/{0}/operations/{1}",
                        this.subscriptionId, 
                        requestId));

                req.Headers["x-ms-version"] = "2011-10-01";
                req.ClientCertificates.Add(this.managementCert);

                using (var response = req.GetResponse() as HttpWebResponse)
                {
                    XDocument documentResponse = XDocument.Load(response.GetResponseStream());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(
                            string.Format(
                            "Could not get request status for request ID {0}: status={1}",
                            requestId, 
                            response.StatusCode));
                    }

                    string asyncStatus = documentResponse.Descendants(xmlns.GetName("Status")).First().Value;
                    if (asyncStatus == "InProgress")
                    {
                        Thread.Sleep(3000);
                        this.logStreamWriter.WriteLine("waiting for response....."); // These dots denote time taken
                        continue;
                    }

                    if (asyncStatus == "Failed")
                    {
                        this.logStreamWriter.WriteLine("Operation failed. Check app logs for details.....");
                        throw new ApplicationException(
                            string.Format(
                            "Request with ID {0} failed: {1}",
                            requestId, 
                            documentResponse.ToString()));
                    }

                    if (asyncStatus == "Succeeded")
                    {
                        this.logStreamWriter.WriteLine("Operation successful. Response received.");
                        break;
                    }

                    throw new ApplicationException(
                        string.Format(
                        "Unexpected async status for Request with ID {0}: '{1}'",
                        requestId, 
                        asyncStatus));
                }
            }
        }

        /// <summary>
        /// Creates the storage account.
        /// </summary>
        /// <returns>Request id</returns>
        private string CreateStorageAccount()
        {
            // create storage account http://msdn.microsoft.com/en-us/library/hh264518.aspx
            var req = (HttpWebRequest)WebRequest.Create(
                string.Format(
                "https://management.core.windows.net/{0}/services/storageservices",
                this.subscriptionId));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(this.managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();
            xreq.Add(
                new XElement(
                    xmlns.GetName("CreateStorageServiceInput"),
                    new XElement(xmlns.GetName("ServiceName"), this.storageName),
                    new XElement(xmlns.GetName("Description"), "Storage created by DeploymentTracker."),
                    new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(this.storageName))),
                    new XElement(xmlns.GetName("Location"), this.location)));

            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                this.logStreamWriter.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    this.logStreamWriter.WriteLine(string.Format("Failed to create storage account '{0}' ....", this.storageName));
                    throw new ApplicationException(
                        string.Format("Error creating storage account. status ={0}", response.StatusCode));
                }

                this.logStreamWriter.WriteLine(string.Format("Created storage account '{0}' successfully ....", this.storageName));
                return response.Headers["x-ms-request-id"];
            }
        }
        #endregion
    }
}
