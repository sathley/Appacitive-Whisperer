using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class Schema
    {
        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public List<Property> Properties { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}