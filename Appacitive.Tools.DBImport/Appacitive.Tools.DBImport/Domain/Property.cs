namespace Appacitive.Tools.DBImport
{
    public class Property
    {
        public string CreatedBy { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public Range Range { get; set; }

        public string RegexValidator { get; set; }

        public bool IsMandatory { get; set; }

        public bool AreValuesFromCannedList { get; set; }

        public long CannedListId { get; set; }

        public string CannedListName { get; set; }

        public bool IsUnique { get; set; }

        public bool IsImmutable { get; set; }

        public long MinLength { get; set; }

        public long MaxLength { get; set; }

        public string Description { get; set; }

        public string DefaultValue { get; set; }

        public bool HasDefaultValue { get; set; }

        public bool IsHashed { get; set; }

        public PropertyFormatter Formatter { get; set; }

        public bool IsMultiValued { get; set; }
    }

    public class Range
    {
        public string MinValue { get; set; }

        public string MaxValue { get; set; }
    }

    public class PropertyFormatter
    {
        public string Name { get; set; }

        public KeyValue[] FormatOptions { get; set; }
    }

    public class KeyValue
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}