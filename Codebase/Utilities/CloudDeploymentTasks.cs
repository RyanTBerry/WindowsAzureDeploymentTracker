// -----------------------------------------------------------------------
// <copyright file="CloudDeploymentTasks.cs" company="MSIT">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Xml.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography;
    using System.Threading;
    using Microsoft.WindowsAzure.StorageClient;
    using Microsoft.WindowsAzure;
    using System.Xml;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloudDeploymentTasks
    {
        const string WINDOWS_AZURE_LOCATION = "North Europe";
        const int NB_TRIES = 4;
        const string PACKAGE_URL = "https://stockageazure.blob.core.windows.net/raytracer/AzureRaytracer.cspkg";
        const string CSCFG_TEMPLATE = "";

        public static Dictionary<string,string> ListLocationsForSubscription(
       string subscriptionId,
       X509Certificate2 certificate,
       string version)
        {
            Dictionary<string, string> dlocation = new Dictionary<string, string>();
            string uriFormat = "https://management.core.windows.net/{0}/locations";
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
                throw new ApplicationException(string.Format("Call to List Locations returned an error: Status Code: {0} ({1}):{2}{3}",
                    (int)statusCode, statusCode, Environment.NewLine,
                    responseBody.ToString(SaveOptions.OmitDuplicateNamespaces)));
            }
            return dlocation;
        }

        public void InitHostedService(string hostedServiceName, string storageName, X509Certificate2 managementCert, string subscriptionId)
        {
            int remainingTries = NB_TRIES;
            while (remainingTries-- > 0)
            {
                Console.WriteLine("Attempting to create the hosted service {0}.cloudapp.net ...", hostedServiceName);
                try
                {
                    string requestId = CreateHostedService(managementCert, subscriptionId, hostedServiceName);
                    WaitForOperationCompletion(managementCert, subscriptionId, storageName, requestId);

                    Console.WriteLine("{0}.cloudapp.net created", hostedServiceName);

                    break;

                }
                catch (Exception ex)
                {
                    if (remainingTries == 0)
                    {
                        throw (ex);
                    }
                    else
                    {
                        Console.WriteLine("Warning (we'll try again): {0}", ex);
                    }
                }
            }
        }

        public string InitStorageAccount(string storageName,X509Certificate2 managementCert, string subscriptionId)
        {
            int remainingTries = NB_TRIES;
            string storagePrimaryKey = string.Empty;

            while (remainingTries-- > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Attempting to create the storage '{0}' ...", storageName);
                try
                {
                    string requestId1 = CreateStorageAccount(managementCert, subscriptionId, storageName);
                    WaitForOperationCompletion(managementCert, subscriptionId, storageName, requestId1);

                    storagePrimaryKey = GetStoragePrimaryKey(managementCert, subscriptionId, storageName);

                    Console.WriteLine("Storage account {0} primary key created and recovered", storageName);

                    break;
                }
                catch (Exception ex)
                {
                    if (remainingTries == 0)
                    {
                        throw (ex);
                    }
                    else
                    {
                        Console.WriteLine("Warning (we'll try again): {0}", ex);
                    }
                }
            }
            return storagePrimaryKey;
        }

        public void DeployPackageInCloud(X509Certificate2 managementCert, string subscriptionId, string storageName, string hostedServiceName, string storagePrimaryKey, string deploymentSlot, bool isUpgrade)
        {
            string requestId3 = DeployPackage(managementCert, subscriptionId, storageName, hostedServiceName, storagePrimaryKey, deploymentSlot, isUpgrade);
            WaitForOperationCompletion(managementCert, subscriptionId, storageName, requestId3);
        }

        public string DeployPackage(X509Certificate2 managementCert, string subscriptionId, string storageName, string hostedServiceName, string storagePrimaryKey, string deploymentSlot, bool isUpgrade)
        {
            var req = (HttpWebRequest)WebRequest.Create(
                string.Format(isUpgrade ? "https://management.core.windows.net/{0}/services/hostedservices/{1}/deploymentslots/{2}/?comp=upgrade" : 
                "https://management.core.windows.net/{0}/services/hostedservices/{1}/deploymentslots/{2}",
                subscriptionId, hostedServiceName, deploymentSlot));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            string updatedConfiguration = CSCFG_TEMPLATE
                .Replace("__rw__storageaccount__", storageName)
                .Replace("__rw__storagekey__", storagePrimaryKey);

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();

            string packageUrl = string.Format("https://{0}.blob.core.windows.net/package/AzureRaytracer.cspkg",
                storageName);

            xreq.Add(
                new XElement(xmlns.GetName("CreateDeployment"),
                    new XElement(xmlns.GetName("Name"), hostedServiceName),
                    new XElement(xmlns.GetName("PackageUrl"), packageUrl),
                    new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes("RayTracer"))),
                    new XElement(xmlns.GetName("Configuration"), Convert.ToBase64String(Encoding.UTF8.GetBytes(updatedConfiguration))),
                    new XElement(xmlns.GetName("StartDeployment"), "true"),
                    new XElement(xmlns.GetName("TreatWarningsAsError"), "false")));

            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                Console.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new ApplicationException(
                        string.Format("Error during deployment. status= {0}", response.StatusCode));
                }

                return response.Headers["x-ms-request-id"];
            }

        }

        public void CopyCspkg(string storageName, string storagePrimaryKey)
        {
            CloudStorageAccount account = new CloudStorageAccount(
                new StorageCredentialsAccountAndKey(storageName, storagePrimaryKey), true);

            CloudBlobClient blobStorage = account.CreateCloudBlobClient();

            blobStorage.GetContainerReference("package").CreateIfNotExist();
            var blobReference = blobStorage.GetContainerReference("package").GetBlobReference("AzureRaytracer.cspkg");

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(PACKAGE_URL); // Load package from local location
            webRequest.Timeout = 1000000;
            using (Stream packageStream = webRequest.GetResponse().GetResponseStream())
            {
                BlobRequestOptions options = new BlobRequestOptions() { Timeout = new TimeSpan(1, 0, 0) };
                blobReference.UploadFromStream(packageStream, options);
            }
        }

        public string GetStoragePrimaryKey(X509Certificate2 managementCert, string subscriptionId, string storageName)
        {
            string primaryKey = null;

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            var req = (HttpWebRequest)WebRequest.Create(
                    string.Format("https://management.core.windows.net/{0}/services/storageservices/{1}/keys",
                    subscriptionId, storageName));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(managementCert);

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                XDocument xResponse = XDocument.Load(response.GetResponseStream());
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new ApplicationException(
                        string.Format("Unable to retrieve the primary key for the storage account {0}. Status = {1}",
                        storageName, response.StatusCode));

                primaryKey = xResponse.Descendants(xmlns.GetName("Primary")).First().Value;
            }

            return primaryKey;
        }

        private string CreateHostedService(X509Certificate2 managementCert, string subscriptionId, string hostedServiceName)
        {
            // documentation: http://msdn.microsoft.com/en-us/library/gg441304.aspx

            var req = (HttpWebRequest)WebRequest.Create(
                string.Format("https://management.core.windows.net/{0}/services/hostedservices",
                subscriptionId));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();
            xreq.Add(
                new XElement(xmlns.GetName("CreateHostedService"),
                    new XElement(xmlns.GetName("ServiceName"), hostedServiceName),
                    new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(hostedServiceName))),
                    new XElement(xmlns.GetName("Description"), hostedServiceName + " pour RayTracer"),
                    new XElement(xmlns.GetName("Location"), WINDOWS_AZURE_LOCATION)));

            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                Console.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new ApplicationException(
                        string.Format("Error creating a hosted service. status ={0}", response.StatusCode));
                }

                return response.Headers["x-ms-request-id"];
            }
        }

        public void WaitForOperationCompletion(X509Certificate2 managementCert, string subscriptionId, string storageName, string requestId)
        {
            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";
            Console.WriteLine("pending request {0}", requestId);

            while (true)
            {
                var req = (HttpWebRequest)WebRequest.Create(
                        string.Format("https://management.core.windows.net/{0}/operations/{1}",
                        subscriptionId, requestId));

                req.Headers["x-ms-version"] = "2011-10-01";
                req.ClientCertificates.Add(managementCert);

                using (var response = req.GetResponse() as HttpWebResponse)
                {
                    XDocument xResponse = XDocument.Load(response.GetResponseStream());
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException(
                            string.Format("Could not get request status for request ID {0}: status={1}",
                            requestId, response.StatusCode));

                    string asyncStatus = xResponse.Descendants(xmlns.GetName("Status")).First().Value;
                    if (asyncStatus == "InProgress")
                    {
                        Thread.Sleep(3000);
                        Console.Write(".");
                        continue;
                    }
                    Console.WriteLine();

                    if (asyncStatus == "Failed")
                    {
                        throw new ApplicationException(
                            string.Format("Request with ID {0} failed: {1}",
                            requestId, xResponse.ToString()));
                    }

                    if (asyncStatus == "Succeeded")
                        break;

                    throw new ApplicationException(
                        string.Format("Unexpected async status for Request with ID {0}: '{1}'",
                        requestId, asyncStatus));

                }
            }
        }

        private string CreateStorageAccount(X509Certificate2 managementCert, string subscriptionId, string storageName)
        {
            // create storage account
            // http://msdn.microsoft.com/en-us/library/hh264518.aspx

            var req = (HttpWebRequest)WebRequest.Create(
                string.Format("https://management.core.windows.net/{0}/services/storageservices",
                subscriptionId));

            req.Headers["x-ms-version"] = "2011-10-01";
            req.ClientCertificates.Add(managementCert);
            req.Method = "POST";
            req.ContentType = "application/xml";

            XNamespace xmlns = "http://schemas.microsoft.com/windowsazure";

            XDocument xreq = new XDocument();
            xreq.Add(
                new XElement(xmlns.GetName("CreateStorageServiceInput"),
                    new XElement(xmlns.GetName("ServiceName"), storageName),
                    new XElement(xmlns.GetName("Description"), "Stockage pour RayTracer"),
                    new XElement(xmlns.GetName("Label"), Convert.ToBase64String(Encoding.UTF8.GetBytes(storageName))),
                    new XElement(xmlns.GetName("Location"), WINDOWS_AZURE_LOCATION)));

            using (Stream reqBodyStream = req.GetRequestStream())
            {
                xreq.Save(reqBodyStream);
            }

            using (var response = req.GetResponse() as HttpWebResponse)
            {
                Console.WriteLine(response.StatusCode);

                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new ApplicationException(
                        string.Format("Error creating storage account. status ={0}", response.StatusCode));
                }

                return response.Headers["x-ms-request-id"];
            }
        }
    }
}
