using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungLib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string s)
        {
            return long.TryParse(s, out long result);
        }

        public static bool IsDateTime(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return false;

            return DateTime.TryParse(s, out DateTime result);
        }
    }
}
