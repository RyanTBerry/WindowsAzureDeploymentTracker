// -----------------------------------------------------------------------
// <copyright file="TFSConnectionString.cs" company="MSIT">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TFSConnectionString
    {
        public string ServerName { get; set; }
        public int PortNumber { get; set; }
        public string DefaultCollection { get; set; }

        internal void Validate()
        {
            if (string.IsNullOrEmpty(this.ServerName)
                || this.PortNumber <= 0 || string.IsNullOrEmpty(this.DefaultCollection))
            {
                throw new ArgumentOutOfRangeException("Invalid TFS Connection string parameters");
            }
        }
    }
}
