using System;
using Newtonsoft.Json;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class Property
    {
        [JsonProperty(PropertyName = "createdby")]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "datatype")]
        public string DataType { get; set; }

        [JsonProperty(PropertyName = "range")]
        public Range Range { get; set; }

        [JsonProperty(PropertyName = "regexvalidator")]
        public string RegexValidator { get; set; }

        [JsonProperty(PropertyName = "ismandatory")]
        public bool IsMandatory { get; set; }

        [JsonProperty(PropertyName = "arevaluesfromcannedlist")]
        public bool AreValuesFromCannedList { get; set; }

        [JsonProperty(PropertyName = "cannedlistid")]
        public long CannedListId { get; set; }

        [JsonProperty(PropertyName = "cannedlistname")]
        public string CannedListName { get; set; }

        [JsonProperty(PropertyName = "isunique")]
        public bool IsUnique { get; set; }

        [JsonProperty(PropertyName = "isimmutable")]
        public bool IsImmutable { get; set; }

        [JsonProperty(PropertyName = "minlength")]
        public long MinLength { get; set; }

        [JsonProperty(PropertyName = "maxlength")]
        public long MaxLength { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "defaultvalue")]
        public string DefaultValue { get; set; }

        [JsonProperty(PropertyName = "hasdefaultvalue")]
        public bool HasDefaultValue { get; set; }

        [JsonProperty(PropertyName = "ishashed")]
        public bool IsHashed { get; set; }

        [JsonProperty(PropertyName = "formatter")]
        public PropertyFormatter Formatter { get; set; }

        [JsonProperty(PropertyName = "ismultivalued")]
        public bool IsMultiValued { get; set; }
    }

    [Serializable]
    public class Range
    {
        [JsonProperty(PropertyName = "minvalue")]
        public string MinValue { get; set; }

        [JsonProperty(PropertyName = "maxvalue")]
        public string MaxValue { get; set; }
    }

    [Serializable]
    public class PropertyFormatter
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "formatoptions")]
        public KeyValue[] FormatOptions { get; set; }
    }

    [Serializable]
    public class KeyValue
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}