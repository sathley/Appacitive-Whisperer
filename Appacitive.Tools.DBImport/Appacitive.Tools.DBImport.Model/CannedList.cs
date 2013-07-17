using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class CannedList
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<ListItem> Items { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }
    }

    [Serializable]
    public class ListItem
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "position")]
        public long Position { get; set; }
    }
}