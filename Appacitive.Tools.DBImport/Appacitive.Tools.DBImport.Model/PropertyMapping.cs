using System;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class PropertyMapping
    {
        public string ColumnName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitivePropertyName { get; set; }

        public string Description { get; set; }

        //  Add option for changing datatype
    }
}