using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
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

    }

    public class ClusteredIndex : Index
    {

    }

    public class NonClusteredIndex : Index
    {

    }

    public class UniqueIndex : Index
    {

    }

    public class FullTextIndex : Index
    {

    }

    public class XMLIndex : Index
    {

    }

    public class FilteredIndex : Index
    {

    }

    public class SpatialIndex : Index
    {

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

    }

    public class CheckConstraint : Constraint
    {
        public object MinValue { get; set; }

        public object MaxValue { get; set; }

        public string Regex { get; set; }
    }

    public class DefaultConstraint : Constraint
    {
        public object DefaultValue { get; set; }
    }

    public class UniqueConstraint : Constraint
    {

    }

    public class PrimaryConstraint : Constraint
    {

    }

    public class ForeignConstraint : Constraint
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }
    }

    public class NotNullConstraint : Constraint
    {

    }

    #endregion
}
