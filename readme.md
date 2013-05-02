Introduction

The Deployment Tracker tool simplifies the deployment and management of cloud-based applications. It automates the process of compiling and deploying an application to Windows Azure.

Deployment Tracker provides the following capabilities:
•	Build and deploy applications to Windows Azure with a single click.
•	Audit which application was deployed, by whom, and when.
•	Perform deployments without deep developer knowledge of the underlying application.
•	Receive alerts about the status of the deployment and any errors that occur.
•	Perform multiple, simultaneous deployments.
•	Roll back to a previous version of the application with a single click.
•	View Windows Azure application deployment results, and manage filters.
This document provides an overview of the Deployment Tracker sample application by describing the tool and explaining different usage scenarios.

System requirements
To run Deployment Tracker, you must have the following:
•	Windows 7 or Windows Server 2008, 64-bit operating system
•	Visual Studio 2010 (any edition) and Team Foundation Server (TFS)
•	Windows Azure Software Deployment Kit (SDK) 1.6
•	MSBuild
•	A Microsoft (formerly Windows Live) account, to log in to the Windows Azure Management Portal
•	At least one Windows Azure subscription, to deploy the application code to the cloud
•	Administrator privileges to run Deployment Tracker


Tool overview
Deployment Tracker makes it easy for you to automatically deploy application code to the Windows Azure Management Portal. 
You can use Deployment Tracker to do the following:
•	Specify the TFS instances that you want to use during the deployment process.
•	Download the labeled solution for a specific TFS instance.
•	Configure the Windows Azure settings, such as the subscriptions, hosted service, storage account, and environment that you want to deploy the application code to.
•	Build, publish, and deploy the downloaded application code to the Windows Azure Management Portal with a specific Windows Azure configuration.
•	Manage application-level error logs (if any) for each deployment that you attempt.
•	View reports that provide the status of each deployment.
•	Roll back currently running application code in the Windows Azure Management Portal to the last successfully deployed version (if any).


Copyright (c) Microsoft Corporation 
All rights reserved. 



Microsoft Platform and Azure License

 

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions
The terms “reproduce,” “reproduction,” “derivative works,” and “distribution” have the same meaning here as under U.S. copyright law.
A “contribution” is the original software, or any additions or changes to the software.
A “contributor” is any person that distributes its contribution under this license.
“Licensed patents” are a contributor’s patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) No Trademark License- This license does not grant you rights to use any contributors’ name, logo, or trademarks.
(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(E) The software is licensed “as-is.” You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
(F) Platform Limitation- The licenses granted in sections 2(A) & 2(B) extend only to the software or derivative works that (1) runs on a Microsoft Windows operating system product, and (2) operates with Microsoft Windows Azure.
