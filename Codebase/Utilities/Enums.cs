// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="MSIT">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DeploymentTracker.Utilities
{
    public enum OperationType
    {
        Add = 0,
        Edit,
        Delete
    }

    public enum TaskStatus
    {
        Success = 0,
        Fail = 1
    }
    
    public enum LogType
    {
        Warning = 0,
        Error,
        Information
    }
   

}
