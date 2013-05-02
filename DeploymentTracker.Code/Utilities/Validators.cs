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
namespace DeploymentTracker.Services.Utilities
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Validators class. Contains extenders
    /// </summary>
    public static class Validators
    {
        /// <summary>
        /// Verifies the not null.
        /// </summary>
        /// <param name="stringTargetObj">The string target obj.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public static void VerifyNotNull(this string stringTargetObj, string parameterName, string message)
        {
            if (string.IsNullOrEmpty(stringTargetObj))
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }

        /// <summary>
        /// Verifies the URI.
        /// </summary>
        /// <param name="stringTargetObj">The string target obj.</param>
        /// <param name="message">The message.</param>
        public static void VerifyUri(this string stringTargetObj, string message)
        {
            if (!Uri.IsWellFormedUriString(stringTargetObj, UriKind.Absolute))
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Verifies the non zero.
        /// </summary>
        /// <param name="intTargetObj">The int target obj.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public static void VerifyNonZero(this int intTargetObj, string parameterName, string message)
        {
            if (intTargetObj == 0)
            {
                throw new ArgumentException(parameterName, message);
            }
        }

        /// <summary>
        /// Verifies the physical path.
        /// </summary>
        /// <param name="intTargetObj">The int target obj.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        public static void VerifyPhysicalPath(this string intTargetObj, string parameterName, string message)
        {
            if (!File.Exists(intTargetObj))
            {
                throw new FileNotFoundException(parameterName, message);
            }
        }

        /// <summary>
        /// Verifies the integer.
        /// </summary>
        /// <param name="intTargetObj">The int target obj.</param>
        /// <param name="message">The message.</param>
        public static void VerifyInteger(this string intTargetObj, string message)
        {
            try
            {
                int x = int.Parse(intTargetObj);
            }
            catch
            {
                throw new FormatException(message);
            }
        }

        /// <summary>
        /// Verifies the integer is greater than zero or not
        /// </summary>
        /// <param name="intTargetObj">The int target obj.</param>
        /// <param name="message">The message.</param>
        public static void VerifyGreaterThanZero(this string intTargetObj, string message)
        {
            try
            {
                int x = int.Parse(intTargetObj);
                if (x <= 0)
                {
                    throw new ArgumentException(message);
                }
            }
            catch
            {
                throw new FormatException(message);
            }
        }

        /// <summary>
        /// Verifies the path.
        /// </summary>
        /// <param name="stringPath">The string path.</param>
        /// <param name="message">The message.</param>
        public static void VerifyPath(this string stringPath, string message)
        {
            if (stringPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException(message);
            }

            if (stringPath.Split(':').Length - 1 > 1)
            {
                throw new ArgumentException(message);
            }

            foreach (var item in stringPath.Split(new char[]{'\\', ':'}))
            {
                if (item.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                {
                    throw new ArgumentException(message);
                }
            }
        }
    } 
}
