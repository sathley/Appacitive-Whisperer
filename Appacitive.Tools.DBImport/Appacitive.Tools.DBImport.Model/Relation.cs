using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    public class Relation
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public List<Property> Properties { get; set; }

        public EndPoint EndPointA { get; set; }

        public EndPoint EndPointB { get; set; }

        public string Description { get; set; }
    }

    public class EndPoint
    {
        public string Label { get; set; }

        public long SchemaId { get; set; }

        public string SchemaName { get; set; }

        public int Multiplicity { get; set; }
    }
}