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
    using System;
    using DeploymentTracker.Services.Utilities;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System.Net;
    using System.Globalization;

    /// <summary>
    /// Represents TFS connection string
    /// </summary>
    public class TFSConnectionString
    {
        /// <summary>
        /// Invalid chars in TFS servername
        /// </summary>
        private const string INVALIDCHARS = "\"/:<>\\|*?@";

        /// <summary>
        /// Determines whether the connection string is http or https
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is HTTPS; otherwise, <c>false</c>.
        /// </value>
        public bool IsHttps { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>
        /// The port number.
        /// </value>
        public int PortNumber { get; set; }

        /// <summary>
        /// Gets or sets the default collection.
        /// </summary>
        /// <value>
        /// The default collection.
        /// </value>
        public string DefaultCollection { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            this.ServerName.VerifyNotNull("Server Name", "Servername cannot be left empty");
            this.DefaultCollection.VerifyNotNull("Default collection", "Default collection cannot be left empty");
            this.PortNumber.VerifyNonZero("Port Number", "Port Number cannot be less than or equal to zero");
            if (this.ServerName.Contains(" "))
            {
                throw new ArgumentException("Server name cannot contain white spaces");
            }

            if (this.DefaultCollection.Contains(" "))
            {
                throw new ArgumentException("Default collection cannot contain white spaces");
            }

            foreach (var item in this.ServerName.ToCharArray())
            {
                if (INVALIDCHARS.Contains(item.ToString()))
                {
                    throw new ArgumentException("Invalid TFS server name.");
                }
            }
        }
    }
}
