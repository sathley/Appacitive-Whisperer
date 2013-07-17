using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Appacitive.Tools.DBImport
{
    public static class StringValidation
    {
        private static readonly Regex AphanumericRegex = new Regex(@"^[a-z]+[a-z0-9]*([_]*[a-z0-9])*[a-z0-9]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value) == true)
                return false;
            long number;
            return long.TryParse(value, out number);
        }

        public static bool IsAlphanumeric(string value)
        {
            if (string.IsNullOrWhiteSpace(value) == true)
                return false;
            return AphanumericRegex.IsMatch(value);
        }
    }
}
