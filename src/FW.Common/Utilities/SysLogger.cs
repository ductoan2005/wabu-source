using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FW.Common.Utilities
{
    public static class SysLogger
    {
        /// <summary>
        /// The log object
        /// </summary>
        private static readonly log4net.ILog TheLog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly log4net.ILog TheDbMaintenanceLog = log4net.LogManager.GetLogger("DbMaintenance");

        private static readonly log4net.ILog TheActionLog = log4net.LogManager.GetLogger("ActionLog");
        /// <summary>
        /// Logs a message object with the INFO level including the stack trace of the
        /// System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void DbMtInfo(string message, Exception exception = null)
        {
            TheDbMaintenanceLog.Info(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Debug level including the
        /// stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void DbMtDebug(string message, Exception exception = null)
        {
            TheDbMaintenanceLog.Debug(message, exception);
        }

        /// <summary>
        /// Logs a message object with the INFO level including the stack trace of the
        /// System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void DbMtWarn(string message, Exception exception = null)
        {
            TheDbMaintenanceLog.Warn(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Error level including the
        /// stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void DbMtError(string message, Exception exception = null)
        {
            TheDbMaintenanceLog.Error(message, exception);
        }

        /// <summary>
        /// Logs a message object with the INFO level including the stack trace of the
        /// System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Info(string message, Exception exception = null)
        {
            TheLog.Info(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Debug level including the
        /// stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Debug(string message, Exception exception = null)
        {
            TheLog.Debug(message, exception);
        }

        /// <summary>
        /// Logs a message object with the INFO level including the stack trace of the
        /// System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Warn(string message, Exception exception = null)
        {
            TheLog.Warn(message, exception);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Error level including the
        /// stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Error(string message, Exception exception = null)
        {
            TheLog.Error(message, exception);
        }

        /// <summary>
        /// Writes the process time span.
        /// </summary>
        /// <param name="utcStartProcessTime">The the start process time (shoud be gotten by DateTime.UtcNow) to calculate the process time span.</param>
        public static void WriteProcessTimeSpan(DateTime utcStartProcessTime)
        {
            TimeSpan timeSpan = DateTime.UtcNow - utcStartProcessTime;
            TheLog.Warn("Process time: " + timeSpan, null);
        }

        /// <summary>
        /// Log a message object with the log4net.Core.Level.Fatal level including the
        /// stack trace of the System.Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log.</param>
        /// <param name="exception">The exception to log, including its stack trace.</param>
        public static void Fatal(string message, Exception exception = null)
        {
            TheLog.Fatal(message, exception);
        }

        /// <summary>
        /// Get caller information
        /// </summary>
        /// <param name="obj">The object that use this method.</param>
        /// <returns>The method full name.</returns>
        public static string GetMethodFullName(this object obj)
        {
            string callerInfo = null;
            try
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                callerInfo = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            }
            catch { }
            return callerInfo;
        }

        /// <summary>
        /// Get caller information
        /// </summary>
        /// <returns>The method full name.</returns>
        public static string GetMethodFullName()
        {
            string callerInfo = null;
            try
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                callerInfo = methodBase.DeclaringType.FullName + "." + methodBase.Name;
            }
            catch
            {

            }
            return callerInfo;
        }
        //public static void TheActionLogInfo(string message, Exception exception = null)
        //{
        //    TheActionLog.Info(message, exception);
        //}
        //public static void addTbActionLog(string companyCd, string userId, string method, string table, string info = "", string error = "", Exception exception = null)
        //{
        //    TheActionLog.Info(companyCd.ToString() + "," + userId.ToString() + "," + method + "," + table + "," + info + "," + error, exception);
        //}

        public static void addTbActionLog(string username, string method, string table, string info = "", string error = "", Exception exception = null)
        {
            TheActionLog.Info(username.ToString() + "," + method + "," + table + "," + info + "," + error, exception);
        }
    }
}
