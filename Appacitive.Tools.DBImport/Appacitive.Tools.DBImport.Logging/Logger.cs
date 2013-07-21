using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Logging
{
    public static class Logger
    {
        public static void Log(string message)
        {
            //  Basic logger
            // TODO - MAke better
            var loggingEnable = bool.Parse(ConfigurationManager.AppSettings["loggingenabled"]);
            if (loggingEnable)
            {
                File.AppendAllLines("D:/log.txt", new string[] { message });
            }
        }
    }
}
