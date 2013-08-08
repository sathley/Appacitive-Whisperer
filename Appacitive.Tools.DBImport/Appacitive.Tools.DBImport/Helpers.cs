using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public static class NameValidationHelper
    {
        private static readonly Regex AphanumericRegex = new Regex(@"^[a-z]+[a-z0-9]*([_]*[a-z0-9])*[a-z0-9]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private  static readonly List<string> BlacklistedNames=new List<string>()
            {
                "schema","relation","cannedlist","appacitive","gossamer","article","connection"
            }; 
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value) == true)
                return false;
            long number;
            return long.TryParse(value, out number);
        }

        public static bool IsValidName(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) == true)
                return false;
            if (AphanumericRegex.IsMatch(value) == false)
                return false;
            if (BlacklistedNames.Contains(value.ToLower()) == true)
                return false;
            return true;
        }
    }

    public static class ConstraintsHelper
    {
        public static void Process(Constraint constraint, ref Property property)
        {
            switch (constraint.Type)
            {
                case "check":
                    var checkConstraint = constraint as CheckConstraint;
                    if (checkConstraint != null && checkConstraint.MinValue != null)
                        property.Range.MinValue = checkConstraint.MinValue;
                    if (checkConstraint != null && checkConstraint.MaxValue != null)
                        property.Range.MaxValue = checkConstraint.MaxValue;
                    if (checkConstraint != null)
                    {
                        property.MinLength = checkConstraint.MinLength;
                        property.MaxLength = checkConstraint.MaxLength;
                        if (string.IsNullOrEmpty(checkConstraint.Regex) == false)
                            property.RegexValidator = checkConstraint.Regex;
                    }
                    break;
                case "default":
                    var defaultConstraint = constraint as DefaultConstraint;
                    if (defaultConstraint != null && string.IsNullOrEmpty(defaultConstraint.DefaultValue) == false)
                    {
                        property.HasDefaultValue = true;
                        property.DefaultValue = defaultConstraint.DefaultValue;
                    }
                    break;
                case "notnull":
                    var notNullConstraint = constraint as NotNullConstraint;
                    property.IsMandatory = true;
                    break;
            }
        }
    }

    public static class DataTypeHelper
    {
        public static string FigureDataType(Column column)
        {
            //  Figure out data type
            if (column.Type == DbDataType.NVarChar || column.Type == DbDataType.NChar
                || column.Type == DbDataType.VarChar || column.Type == DbDataType.Char)
                return "string";
            if (column.Type == DbDataType.Bit)
                return "bool";
            if (column.Type == DbDataType.SmallInt || column.Type == DbDataType.BigInt || column.Type == DbDataType.Int || column.Type == DbDataType.TinyInt)
                return "long";
            if (column.Type == DbDataType.Timestamp || column.Type == DbDataType.SmallDateTime || column.Type == DbDataType.DateTime || column.Type == DbDataType.DateTime2)
                return "datetime";
            if (column.Type == DbDataType.Date)
                return "date";
            if (column.Type == DbDataType.Time)
                return "time";
            if (column.Type == DbDataType.Geography)
                return "geography";
            if (column.Type == DbDataType.Text || column.Type == DbDataType.NText)
                return "text";
            if (column.Type == DbDataType.Float || column.Type == DbDataType.Decimal)
                return "decimal";
            if (column.Type == DbDataType.VarBinary || column.Type == DbDataType.Binary || column.Type == DbDataType.Image)
                return "blob";

            //  default to string
            return "string";
        }
    }
}
