using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);
    }
}
