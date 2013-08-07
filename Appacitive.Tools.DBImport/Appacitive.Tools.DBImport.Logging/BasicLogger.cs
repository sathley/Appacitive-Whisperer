﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Logging
{
    public class BasicLogger : ILogger
    {
        public BasicLogger()
        {
            _loggingEnable = bool.Parse(ConfigurationManager.AppSettings["loggingenabled"]);
            _path = "D:/log.txt";
        }

        private static bool _loggingEnable;
        private static string _path;
        public static void Log(string message)
        {
            if (_loggingEnable)
            {
                File.AppendAllLines(_path, new string[] { message });
            }
        }

        public void LogInfo(string message)
        {
            if (_loggingEnable)
            {
                File.AppendAllLines(_path, new string[] { message });
            }
        }

        public void LogWarning(string message)
        {
            if (_loggingEnable)
            {
                File.AppendAllLines(_path, new string[] { message });
            }
        }

        public void LogError(string message)
        {
            if (_loggingEnable)
            {
                File.AppendAllLines(_path, new string[] { message });
            }
        }
    }
}
