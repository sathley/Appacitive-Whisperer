using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public interface IRelationalDatabase
    {
        string ConnectionString { get; set; }

        // Execute a query
        DataSet ExecuteQuery(DataCommand command);

        // Execute a non query
        void ExecuteNonQuery(DataCommand command);
    }

    public class DataCommand
    {
        public DataCommand()
        {
            Parameters=new Dictionary<string, object>();
        }
        public string CommandText { get; set; }

        public Dictionary<string, object> Parameters { get; set; }
    }
}
