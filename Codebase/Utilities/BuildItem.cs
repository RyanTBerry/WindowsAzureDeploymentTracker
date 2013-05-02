// -----------------------------------------------------------------------
// <copyright file="BuildItem.cs" company="Microsoft IT">
//     Copyright 2012 Microsoft Corporation.  All rights reserved.
//     THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//     OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//     LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//     FITNESS FOR A PARTICULAR PURPOSE.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
   
    /// <summary>
    /// Build item to log in database. For reporting purpose
    /// </summary>
    public class BuildItem
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
        public string SubmittedBy { get; set; }

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
        /// Gets or sets the drop location path.
        /// </summary>
        /// <value>
        /// The drop location path.
        /// </value>
        public string DropLocationPath { get; set; }

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

        internal void Validate()
        {
            //implement this
        }
    }
}
