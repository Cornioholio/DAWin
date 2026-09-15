using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAWin.Core
{

    internal class Logger
    {
        enum LogLevel
        {
            Info,
            Warning,
            Error
        }
        struct LogEntry 
        {
            LogLevel logLevel_;
            string logMessage_;
            int logCode_;
        }

        public static event Action<string>? OnLog;

        public static void LogInfo(string message, int code) 
        {
            WriteLog(LogLevel.Info, message, code);
        }
        public static void LogWarning(string message, int code) 
        {
            WriteLog(LogLevel.Warning, message, code);
        }
        public static void LogError(string message, int code) 
        {
            WriteLog(LogLevel.Error, message, code);
        }
        private static void WriteLog(LogLevel level, string message, int code) 
        {
            string logMessage = $"[{DateTime.Now}] [{level}] Code: {code} - Message: {message}";

            Debug.WriteLine(logMessage);

            OnLog?.Invoke(logMessage);
        }
    }
}
