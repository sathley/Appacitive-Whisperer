﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport.PostgreSQL
{
    public class PostgreSQLGatherer : IDataDefinitionGatherer
    {
        public Database GatherData(string connectionString, string database)
        {
            throw new NotImplementedException();
        }
    }
}
