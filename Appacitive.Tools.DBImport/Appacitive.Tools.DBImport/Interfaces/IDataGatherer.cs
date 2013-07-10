using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public interface IDataDefinitionGatherer
    {
        Database GatherData(string connectionString, string database);
    }
}
