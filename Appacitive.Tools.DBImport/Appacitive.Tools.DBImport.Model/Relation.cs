using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class Relation
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public List<Property> Properties { get; set; }

        [JsonProperty(PropertyName = "endpointa")]
        public EndPoint EndPointA { get; set; }

        [JsonProperty(PropertyName = "endpointb")]
        public EndPoint EndPointB { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }

    [Serializable]
    public class EndPoint
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "schemaid")]
        public long SchemaId { get; set; }

        [JsonProperty(PropertyName = "schemaname")]
        public string SchemaName { get; set; }

        [JsonProperty(PropertyName = "multiplicity")]
        public int Multiplicity { get; set; }
    }
}