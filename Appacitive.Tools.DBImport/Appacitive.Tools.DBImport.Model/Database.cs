using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Appacitive.Tools.DBImport.Model
{

    public class Database
    {
        public Database()
        {
            this.Tables = new List<Table>();
        }
        public List<Table> Tables { get; set; } 
    }

    public class Table
    {
        public Table()
        {
            this.Columns = new List<Column>();
        }

        public string Name { get; set; }

        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        public Column()
        {
            this.Indexes = new List<Index>();
            this.Constraints = new List<Constraint>();
        }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DbDataType Type { get; set; }

        public List<Index> Indexes { get; set; }

        public List<Constraint> Constraints { get; set; } 

    }

    public enum DbDataType
    {
        BigInt,
        Binary,
        Bit,
        Char,
        DateTime,
        Decimal,
        Float,
        Image,
        Int,
        Money,
        NChar,
        NText,
        NVarChar,
        Real,
        UniqueIdentifier,
        SmallDateTime,
        SmallInt,
        SmallMoney,
        Text,
        Timestamp,
        TinyInt,
        VarBinary,
        VarChar,
        Variant,
        Xml,
        Udt,
        Structured,
        Date,
        Time,
        DateTime2,
        DateTimeOffset,
        Geography
    }

#region Index

    [JsonConverter(typeof(StringEnumConverter))]
    public enum IndexTypeEnum
    {
        Clustered,
        NonClustered,
        Unique,
        FullText,
        Spatial,
        Filtered,
        XML
    }

    public abstract class Index
    {
        public string Type { get; set; }

        public string Name { get; set; }
    }

    public class ClusteredIndex : Index
    {
        public ClusteredIndex()
        {
            this.Type = "clustered";
        }
    }

    public class NonClusteredIndex : Index
    {
        public NonClusteredIndex()
        {
            this.Type = "nonclustered";
        }

    }

    public class UniqueIndex : Index
    {
        public UniqueIndex()
        {
            this.Type = "unique";
        }
        
        public long SequenceInIndex { get; set; }
    }

    public class PrimaryIndex : Index
    {
        public PrimaryIndex()
        {
            this.Type = "primary";
            this.Name = "PRIMARY";
        }
        public long SequenceInIndex { get; set; }
    }

    public class ForeignIndex : Index
    {
        public ForeignIndex()
        {
            this.Type = "foreign";
        }

        public string ReferenceTableName { get; set; }

        public string ReferenceColumnName { get; set; }
    }

    public class FullTextIndex : Index
    {
        public FullTextIndex()
        {
            this.Type = "fulltext";
        }
    }

    public class XMLIndex : Index
    {
        public XMLIndex()
        {
            this.Type = "xml";
        }
    }

    public class FilteredIndex : Index
    {
        public FilteredIndex()
        {
            this.Type = "filtered";
        }
    }

    public class SpatialIndex : Index
    {
        public SpatialIndex()
        {
            this.Type = "spatial";
        }
    }

    #endregion

#region Constraints

    public enum ConstraintEnum
    {
        NotNull,
        Check,
        Default,
        Unique,
        Primary,
        Foreign
    }

    public abstract class Constraint
    {
        public string Type { get; set; }
    }

    public class CheckConstraint : Constraint
    {
        public CheckConstraint()
        {
            this.Type = "check";

        }
        
        public string MinValue { get; set; }

        public string MaxValue { get; set; }

        public long MinLength { get; set; }

        public long MaxLength { get; set; }

        public string Regex { get; set; }
    }

    public class DefaultConstraint : Constraint
    {
        public DefaultConstraint()
        {
            this.Type = "default";
        }
        public string DefaultValue { get; set; }
    }

    public class NotNullConstraint : Constraint
    {
        public NotNullConstraint()
        {
            this.Type = "notnull";
        }
    }

    #endregion
}
