using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public interface IDataDefinitionGatherer
    {
        Database GatherData(string connectionString, string database);
    }
}
