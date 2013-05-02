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

    /// <summary>
    /// Class that holds all constant values re-used through out application
    /// </summary>
    public class Constants
    {
        // To stop intializing
        private Constants()
        {
        }

        /// <summary>
        /// Azure package extension
        /// </summary>
        public const string AzurePackageExtension = "cspkg";

        /// <summary>
        /// Service config files extension
        /// </summary>
        public const string ServiceConfigurationExtension = "cscfg";

        /// <summary>
        /// File extensions for log files in general
        /// </summary>
        public const string LogExtension = ".log";

        /// <summary>
        /// Application file first name
        /// </summary>
        public const string AppLogFileName = "DTlog";

        /// <summary>
        /// publishSettings file extensions
        /// </summary>
        public const string PublishSettingsExtension = "publishsettings";
        
        /// <summary>
        /// Azure pacakages folder name
        /// </summary>
        public const string AzurePackagesFolderName = "AzurePackages";
        
        /// <summary>
        /// Deployment code foldername
        /// </summary>
        public const string DeploymentCodeFolderName = "DeploymentCode";
        
        /// <summary>
        /// Logs folder extension
        /// </summary>
        public const string LogsFolderExtension = "Logs";
        
        /// <summary>
        /// tfs log stem
        /// </summary>
        public const string TFSLogIdentifier = "-TFS";
        
        /// <summary>
        /// msbuild log stem
        /// </summary>
        public const string MSBuildLogIdentifier = "-MSBUILD";
        
        /// <summary>
        /// Azure log stem
        /// </summary>
        public const string AzureLogIdentifier = "-CLOUD";
        
        /// <summary>
        /// Service certificate extension
        /// </summary>
        public const string ServiceCertificateExtension = "pfx";

        /// <summary>
        /// Used in case of combo boxes
        /// </summary>
        public const string AllItemsFilterValue = "--All--";

        /// <summary>
        /// Represents TFS label for local deployment
        /// </summary>
        public const string EmptyTFSLabel = "-";

        /// <summary>
        /// In case of rollback folder creation has this extension
        /// </summary>
        public const string RollbackFolderExtension = "_Rollback";
    }
}
