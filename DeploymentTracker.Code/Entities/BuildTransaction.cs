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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DeploymentTracker.Services.Utilities;
   
    /// <summary>
    /// Build item to log in database. For reporting purpose
    /// </summary>
    public class BuildTransactionEntry
    {
        /// <summary>
        /// Gets or sets the name of the solution.
        /// </summary>
        /// <value>
        /// The name of the solution.
        /// </value>
        public string SolutionName { get; set; }

        /// <summary>
        /// Gets or sets the submitted by.
        /// </summary>
        /// <value>
        /// The submitted by.
        /// </value>
        public string PerformedBy { get; set; }

        /// <summary>
        /// Gets or sets the deployment date time.
        /// </summary>
        /// <value>
        /// The deployment date time.
        /// </value>
        public DateTime DeploymentDateTime { get; set; }

        /// <summary>
        /// Gets or sets the tfslabel used.
        /// </summary>
        /// <value>
        /// The tfslabel used.
        /// </value>
        public string Tfslabelused { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the deployment notes.
        /// </summary>
        /// <value>
        /// The deployment notes.
        /// </value>
        public string DeploymentNotes { get; set; }

        /// <summary>
        /// Gets or sets the environment.
        /// </summary>
        /// <value>
        /// The environment.
        /// </value>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public DeploymentStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate()
        {
            this.SolutionName.VerifyNotNull("Solution Name", "Solution Name can not be null/empty");
            this.PerformedBy.VerifyNotNull("Submitted By", "Submitted by can not be null/empty");
            this.Tfslabelused.VerifyNotNull("TFS Label Used", "TFS Label Used can not null/empty");
            //this.DeploymentNotes.VerifyNotNull("Deployment Notes", "Deployment Notes can not be null/empty");
            this.Environment.VerifyNotNull("Environment", "Environment cannot be null");
            this.Version.VerifyNotNull("Version", "Version cannot be null");
        }
    }
}
