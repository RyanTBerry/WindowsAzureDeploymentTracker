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

using System.Xml;

namespace DeploymentTracker.App.Utilities
{
    using System;
    using System.Data.SqlServerCe;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using DeploymentTracker.App.Dialogs;
    using DeploymentTracker.Properties;
    using DeploymentTracker.Services.Entities;
    using DeploymentTracker.Services.Utilities;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using System.Net;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class UtilityLibrary
    {
        /// <summary>
        /// TFS url pattern
        /// </summary>
        private const string TFSUrlFormat = @"{0}://{1}:{2}/tfs/{3}";

        /// <summary>
        /// To access today's log path
        /// </summary>
        private static XDocument Xdoc;

        /// <summary>
        /// To access today's log path with out race condition
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// Gets the today's log path.
        /// </summary>
        public static string TodayLogPath
        {
            get
            {
                return Path.Combine(
                    Settings.Default.ApplicationLogPath,
                    string.Concat(Constants.AppLogFileName, "-", DateTime.Now.ToString("MMddyyyy"), ".Xml"));
            }
        }

        /// <summary>
        /// Shows the generic exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="customMessage">The custom message.</param>
        public static void ShowGenericException(this Exception ex, string customMessage = "")
        {
            ex.WriteToLog();
            string msgToshow = string.IsNullOrEmpty(customMessage) ? "Unexpected error occurred." : customMessage;
            using (AdvancedMessageBox advBox = new AdvancedMessageBox(string.Concat(msgToshow, "\n Please refer Application log for further details."), ReportStatus.Fail))
            {
                advBox.StartPosition = FormStartPosition.CenterScreen;
                advBox.TopMost = true;
                advBox.ShowDialog();
            }
        }

        /// <summary>
        /// Shows the UI exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void ShowUIException(this Exception ex)
        {
            ex.WriteToLog();
            using (AdvancedMessageBox advBox = new AdvancedMessageBox(ex.Message, ReportStatus.Fail))
            {
                advBox.StartPosition = FormStartPosition.CenterScreen;
                advBox.TopMost = true;
                advBox.ShowDialog();
            }
        }

        /// <summary>
        /// Shows the UI information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="startpos">The startpos.</param>
        public static void ShowUIInformation(this string message, FormStartPosition startpos = FormStartPosition.CenterParent)
        {
            using (AdvancedMessageBox advBox = new AdvancedMessageBox(message, ReportStatus.Information))
            {
                advBox.StartPosition = startpos;
                advBox.TopMost = true;
                advBox.ShowDialog();
            }
        }


        /// <summary>
        /// Shows the UI information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="header">The header.</param>
        public static void PopInformationDialog(this string message, string header = "Information Details")
        {
            using (InformationBox advBox = new InformationBox(message, ReportStatus.Information, header))
            {
                advBox.ShowDialog();
            }
        }

        /// <summary>
        /// Writes to log.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void WriteToLog(this Exception ex)
        {
            LogEntry logentry = new LogEntry 
            { 
                Source = ex.Source,
                Logtype = LogType.Error,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };

            logentry.StoreLogEntry();
        }

        /// <summary>
        /// Stores the log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        public static void StoreLogEntry(this LogEntry logEntry)
        {
             string logLine = string.Format(
                 "{0} - {1} - {2} - {3} - {4}", 
                 logEntry.Logtype,
                 logEntry.Message, 
                 logEntry.Source, 
                 logEntry.StackTrace,
                 logEntry.UserName);

            XElement xelement = new XElement(
                "LogEntry",
                    new XElement("LogType", logEntry.Logtype.ToString()),
                    new XElement("Message", logEntry.Message),
                    new XElement("Source", logEntry.Source),
                    new XElement("StackTrace", logEntry.StackTrace),
                    new XElement("UserName", logEntry.UserName),
                    new XElement("DateTime", logEntry.LogTime));

            // if day changed. Need to have this check
            if (!File.Exists(TodayLogPath))
            {
                File.Create(TodayLogPath);
            }

            if (Xdoc == null && File.Exists(TodayLogPath))
            {
                try
                {
                    Xdoc = XDocument.Load(TodayLogPath);
                }
                catch (XmlException ex)
                {
                    ex.WriteToSystemLog("Cause may be manual deletion of xml schema in application log file.");
                    "Corrupted/ Invalid application log file. Schema corrupted".ShowUIInformation(FormStartPosition.CenterScreen);
                }
            }

            // thread safety
             lock (SyncObject) 
             {
                 try
                 {
                     if (Xdoc != null)
                     {
                         if (Xdoc.Root != null) 
                             Xdoc.Root.Add(xelement);
                         Xdoc.Save(TodayLogPath);
                     }
                 }
                 catch (Exception ex)
                 {
                     // Write to system log
                    ex.WriteToSystemLog(logLine);
                 }
             }
        }

        /// <summary>
        /// Writes to system log.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void WriteToSystemLog(this Exception ex, string logLine)
        {
            const string sSource = "Deployment Tracker App";
            const string sLog = "Application";
            string sEvent = string.Format("Logger failed to write. Problem: {0} \n App Error: {1}", ex.Message,
                                          logLine);

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent,
                EventLogEntryType.Error, 123);
        }

        /// <summary>
        /// Writes to log.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteToLog(this string message)
        {
            LogEntry logentry = new LogEntry
            {
                Source = string.Empty,
                Logtype = LogType.Information,
                Message = message,
                StackTrace = string.Empty,
            };

            logentry.StoreLogEntry();
        }

        /// <summary>
        /// Updates the solution flags to null.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <param name="currentVersion">The current version.</param>
        internal static void UpdateSolutionFlagsToNull(string solutionName, string currentVersion)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            if (string.IsNullOrEmpty(currentVersion))
            {
                throw new ArgumentNullException("Current Version cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;
            
            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("update BuildsRecordTable set IsLatestVersion = '' where (Version < @currentversion or Version = '1.0') and SolutionName = @solutionName", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    com.Parameters.AddWithValue("@currentversion", currentVersion);
                    com.ExecuteNonQuery();
                    using (SqlCeCommand com1 = new SqlCeCommand(@"update BuildsRecordTable 
set IsLatestVersion = '' where Version = @currentversion and SolutionName = @solutionName and 
not exists 
( 
select count(*) from BuildsRecordTable where solutionName = @solutionName and version < @currentversion and status in  ('deployed', 'rollback')
group by solutionname having count(*) >= 1 
)", con))
                    {
                        com1.Parameters.AddWithValue("@solutionName", solutionName);
                        com1.Parameters.AddWithValue("@currentversion", currentVersion);
                        com1.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Updates the solution flags to null.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        internal static void UpdateAllSolutionFlagsToNull(string solutionName)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("update BuildsRecordTable set IsLatestVersion = '' where SolutionName = @solutionName", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    com.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Determines whether this instance can rollback the specified solution name.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <returns>
        ///   <c>true</c> if this instance can rollback the specified solution name; otherwise, <c>false</c>.
        /// </returns>
        internal static bool CanRollback(string solutionName)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

             // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("select count(*) from BuildsRecordTable where status='Deploying' and SolutionName = @solutionName", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    object returnValue = com.ExecuteScalar();
                    if (returnValue != null)
                    {
                        if (int.Parse(returnValue.ToString()) > 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is rollback version] [the specified solution name].
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <param name="version">The version.</param>
        /// <returns>
        ///   <c>true</c> if [is rollback version] [the specified solution name]; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsRollbackVersion(string solutionName, string version)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("select count(*) from BuildsRecordTable where status='Deployed' and version = @version and SolutionName = @solutionName", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    com.Parameters.AddWithValue("@version", version);
                    object returnValue = com.ExecuteScalar();
                    if (returnValue != null)
                    {
                        // If there is a entry with same solution name in the database then return that it is a rollback version
                        if (int.Parse(returnValue.ToString()) > 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the last successful version for solution.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <param name="currentVersion">The current version.</param>
        /// <returns></returns>
        internal static string GetLastSuccessfulVersionForSolution(string solutionName, string currentVersion)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand(
                    @"SELECT version, status FROM BuildsRecordTable WHERE solutionName= @solutionName and status 
in ('Deployed','Rollback') and lastupdate in 
(SELECT MAX(lastupdate) FROM BuildsRecordTable where solutionName= @solutionName and status in 
('Deployed','Rollback') and lastupdate not in (select max(lastupdate) from BuildsRecordTable where solutionName= @solutionName and status in ('Deployed','Rollback')))", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    com.Parameters.AddWithValue("@currentversion", currentVersion);
                    using (SqlCeDataReader reader = com.ExecuteReader())
                    {
                        if (reader == null)
                        {
                            return "-1";
                        }

                        if (reader.Read())
                        {
                            string version = reader.GetString(0);
                            string status = reader.GetString(1);
                            if (version != null && status != null && status.Equals(DeploymentStatus.Rollback.ToString()))
                            {
                                return string.Concat(version, Constants.RollbackFolderExtension);
                            }

                            if (version != null)
                            {
                                return version;
                            }

                            return "-1";
                        }

                        return "-1";
                    }
                }
            }
        }


        /// <summary>
        /// Gets the unique version for solution.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <returns></returns>
        internal static string GetUniqueVersionForSolution(string solutionName)
        {
            if(string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("SELECT max(version) from BuildsRecordTable where solutionName = @solutionName", con))
                {
                    com.Parameters.AddWithValue("@solutionName", solutionName);
                    object version = com.ExecuteScalar();
                    if (version == null || version == System.DBNull.Value)
                    {
                        return "1.0";
                    }

                    double v = double.Parse(version.ToString());
                    return (v+0.1).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Updates the deployment log status.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <param name="version">The version.</param>
        /// <param name="deploymentStatus">The deployment status.</param>
        internal static void UpdateDeploymentLogStatus(string solutionName, string version, DeploymentStatus deploymentStatus)
        {
            if (string.IsNullOrEmpty(solutionName))
            {
                throw new ArgumentNullException("Solution Name cannot be null");
            }

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            try
            {
                UpdateAllSolutionFlagsToNull(solutionName);
             
                // Open the connection using the connection string.
                using (SqlCeConnection con = new SqlCeConnection(conString))
                {
                    con.Open();
                    using (SqlCeCommand com = new SqlCeCommand("Update BuildsRecordTable set status = @status, islatestversion = @islatestversion  where solutionName = @solutionName and version = @version and lastupdate in (select max(lastupdate) from BuildsRecordTable where solutionName = @solutionName and version = @version)", con))
                    {
                        com.Parameters.AddWithValue("@solutionName", solutionName);
                        com.Parameters.AddWithValue("@status", deploymentStatus);
                        com.Parameters.AddWithValue("@version", version);
                        com.Parameters.AddWithValue("@islatestversion", deploymentStatus == DeploymentStatus.Rollback ? string.Empty : (deploymentStatus == DeploymentStatus.Deployed ? DeploymentStatus.Rollback.ToString() : string.Empty));
                        int rowsupdated = com.ExecuteNonQuery();

                        UpdateSolutionFlagsToNull(solutionName, version);
                    }
                }
            }
            catch (SqlCeException ex)
            {
                ex.WriteToLog();
            }
        }

        /// <summary>
        /// Saves the this in DB.
        /// </summary>
        /// <param name="buildItem">The build item.</param>
        public static void SaveThisInDB(this BuildTransactionEntry buildItem)
        {
            // Validate build Item
            buildItem.Validate();

            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("INSERT INTO BuildsRecordTable VALUES(@solutionName, @submittedBy, @tfsLabelUsed, @isSucess, @deploymentNotes, @status, @version, @environment, @islatestversion, @deploymentDateTime)", con))
                {
                    com.Parameters.AddWithValue("@solutionName", buildItem.SolutionName);
                    com.Parameters.AddWithValue("@submittedBy", buildItem.PerformedBy);
                    com.Parameters.AddWithValue("@tfsLabelUsed", buildItem.Tfslabelused);
                    com.Parameters.AddWithValue("@isSucess", buildItem.IsSuccess);
                    com.Parameters.AddWithValue("@deploymentDateTime", buildItem.DeploymentDateTime);
                    com.Parameters.AddWithValue("@deploymentNotes", buildItem.DeploymentNotes);
                    com.Parameters.AddWithValue("@status", buildItem.Status);
                    com.Parameters.AddWithValue("@version", buildItem.Version);
                    com.Parameters.AddWithValue("@environment", buildItem.Environment);
                    com.Parameters.AddWithValue("@islatestversion",buildItem.Status == DeploymentStatus.Deployed ? DeploymentStatus.Rollback.ToString() : string.Empty);
                    
                    com.ExecuteNonQuery();

                    // Now disable previous versions for rollback
                    if (buildItem.IsSuccess)
                    {
                        UpdateSolutionFlagsToNull(buildItem.SolutionName, buildItem.Version);
                    }
                }
            }
        }

        /// <summary>
        /// Generates the TFS URL.
        /// </summary>
        /// <param name="tfsConnectionString">The TFS connection string.</param>
        /// <returns>TFS connection string</returns>
        public static string GenerateTFSUrl(this TFSConnectionString tfsConnectionString)
        {
            tfsConnectionString.Validate();
            return string.Format(CultureInfo.CurrentUICulture, TFSUrlFormat, tfsConnectionString.IsHttps ? "https" : "http", tfsConnectionString.ServerName, tfsConnectionString.PortNumber, tfsConnectionString.DefaultCollection);
        }

        /// <summary>
        /// Gets the TFS connection string.
        /// </summary>
        /// <param name="tfsServerName">Name of the TFS server.</param>
        /// <returns>Looks up and returns TFS connectionstring from database</returns>
        public static TFSConnectionString GetTFSConnectionString(this string tfsServerName)
        {
            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;

            TFSConnectionString connectionString = null;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();

                using (SqlCeCommand com = new SqlCeCommand("SELECT servername, portnumber, defaultcollection, ishttps from TFSConnections where servername = @servername", con))
                {
                    com.Parameters.AddWithValue("@servername", tfsServerName);

                    using (SqlCeDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            connectionString = new TFSConnectionString
                            {
                                ServerName = reader.GetString(0),
                                PortNumber = reader.GetInt32(1),
                                DefaultCollection = reader.GetString(2),
                                IsHttps = bool.Parse(reader.GetString(3))
                            };
                        }
                    }
                }

                return connectionString;
            }
        }

        /// <summary>
        /// Converts to TFS connection string.
        /// </summary>
        /// <param name="tfsConnectionString">The TFS connection parameters.</param>
        public static void SaveOrUpdateTFSConnectionStringToDB(this TFSConnectionString tfsConnectionString, bool raiseErrorIfExists = false)
        {
            tfsConnectionString.Validate();
           
            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;
            bool recordAlreadyExist = false;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();

                using (SqlCeCommand com = new SqlCeCommand("SELECT servername from TFSConnections where servername = @servername", con))
                {
                    com.Parameters.AddWithValue("@servername", tfsConnectionString.ServerName);

                    if (com.ExecuteScalar() != null)
                    {
                        recordAlreadyExist = true;
                    }
                }

                // Raise error if we need to in case of duplicates
                if (recordAlreadyExist && raiseErrorIfExists)
                {
                    throw new InvalidOperationException("TFS connection with same servername already exists. Delete it and try again.");
                }

                string sqlCommand = recordAlreadyExist ? "Update TFSConnections set portnumber = @portnumber, defaultcollection= @defaultcollection, IsHttps = @ishttps where servername = @servername"
                    : "INSERT INTO TFSConnections VALUES(@servername, @portnumber, @defaultcollection, @ishttps)";
                
                using (SqlCeCommand com = new SqlCeCommand(sqlCommand, con))
                {
                    com.Parameters.AddWithValue("@servername", tfsConnectionString.ServerName);
                    com.Parameters.AddWithValue("@portnumber", tfsConnectionString.PortNumber);
                    com.Parameters.AddWithValue("@defaultcollection", tfsConnectionString.DefaultCollection);
                    com.Parameters.AddWithValue("@ishttps", tfsConnectionString.IsHttps);
                    com.ExecuteNonQuery();
                }
            }   
        }
        
        /// <summary>
        /// Deletes the TFS connection string to DB.
        /// </summary>
        /// <param name="tfsConnectionString">The TFS connection string.</param>
        public static void DeleteTFSConnectionStringToDB(this TFSConnectionString tfsConnectionString)
        {
            //tfsConnectionString.Validate();
           
            // Retrieve the connection string from the settings file.
            string conString = Properties.Settings.Default.DeploymentTrackerLocalDBConnectionString;
            bool recordAlreadyExist = false;

            // Open the connection using the connection string.
            using (SqlCeConnection con = new SqlCeConnection(conString))
            {
                con.Open();

                using (SqlCeCommand com = new SqlCeCommand("SELECT servername from TFSConnections where servername = @servername", con))
                {
                    com.Parameters.AddWithValue("@servername", tfsConnectionString.ServerName);

                    if (com.ExecuteScalar() != null)
                    {
                        recordAlreadyExist = true;
                    }
                }

                if (!recordAlreadyExist)
                {
                    throw new InvalidOperationException("Record doesnot exist");
                }

                string sqlCommand = "Delete from TFSConnections where servername = @servername";

                using (SqlCeCommand com = new SqlCeCommand(sqlCommand, con))
                {
                    com.Parameters.AddWithValue("@servername", tfsConnectionString.ServerName);
                    if (com.ExecuteNonQuery() == 0)
                    {
                        throw new InvalidOperationException("Unable to delete row");
                    }
                }
            }
        }

        /// <summary>
        /// Validates the app settings.
        /// </summary>
        internal static void ValidateAppSettings()
        {
            string dbpath = Path.Combine("Data", "DeploymentTrackerLocalDB.sdf");
            dbpath.VerifyPhysicalPath("Database file", "Cannot find local database file");
            FileInfo dbFileInfo = new FileInfo(dbpath);
            if (dbFileInfo.IsReadOnly)
            {
                throw new InvalidOperationException("Cannot edit database file. Make it writable and relaunch app");
            }

            Settings.Default.ApplicationLogPath.VerifyNotNull("Application Log directory value", "Application Log directory value cannot be empty");
            Settings.Default.DeploymentLogPath.VerifyNotNull("Deployment Log directory value", "Deployment Log directory value cannot be empty");
            Settings.Default.TFSWorkingPath.VerifyNotNull("TFS Working path value", "TFS Working path value cannot be empty");
            Settings.Default.DeploymentTrackerLocalDBConnectionString.VerifyNotNull("Local DB connection", "Local Database connection value cannot be empty");
            Settings.Default.MaxSessionsPermitted.VerifyInteger(string.Format("Invalid MaxSesion permitted value : {0}", Settings.Default.MaxSessionsPermitted));
            Settings.Default.MaxSessionsPermitted.VerifyGreaterThanZero("Number of parallel sessions should be greater than zero");
            Settings.Default.NumberOfTries.VerifyInteger("Invalid Number of tries value");
            Settings.Default.NumberOfTries.VerifyGreaterThanZero("Number of tries should be greater than zero");
            Settings.Default.AzurePublishSettingsDownloadLink.VerifyNotNull("Azure publish Settings.", "Azure publishSettings Url cannot be empty");
            Settings.Default.AzurePublishSettingsDownloadLink.VerifyUri(string.Format("Invalid Azure publishSettings Url : {0}", Settings.Default.AzurePublishSettingsDownloadLink));
            Settings.Default.ApplicationLogPath.VerifyPath("Invalid path value Application Log directory");
            Settings.Default.DeploymentLogPath.VerifyPath("Invalid path value Deployment Log Path");
            Settings.Default.TFSWorkingPath.VerifyPath("Invalid path value TFS Working Path");
        }
    }
}
