using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public class MappingConfig
    {
        public string BlueprintId { get; set; }

        public string APIKey { get; set; }

        public string ConnectionString { get; set; }

        public string DBProvider { get; set; }  //sql, msql etc.

        public List<TableMapping> TableMappings { get; set; } 
    }

    public class TableMapping
    {
        public string TableName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveName { get; set; }

        public bool MakeCannedList { get; set; }

        public List<string> IgnoreColumns { get; set; }

        public List<Property> AddProperties { get; set; }

        public List<PropertyMapping> PropertyMappings { get; set; }
    }

    public class PropertyMapping
    {
        public string ColumnName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveName { get; set; }
        
    }
}
