// -----------------------------------------------------------------------
// <copyright file="LogEntry.cs" company="MSIT">
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
    public class LogEntry
    {
        public string Message { get; set; }

        public string UserName
        { 
            get
            {
                return string.Concat(Environment.UserDomainName, "\\", Environment.UserName);
            } 
        }

        public LogType Logtype
        {
            get;
            set;
        }
        
        public string StackTrace
        {
            
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public string LogTime
        {
            get
            {
                return DateTime.Now.ToString();
            }
        }
    }
}
