using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    public class Schema
    {
        public string CreatedBy { get; set; }

        public string Name { get; set; }

        public List<Property> Properties { get; set; }

        public string Description { get; set; }
    }
}