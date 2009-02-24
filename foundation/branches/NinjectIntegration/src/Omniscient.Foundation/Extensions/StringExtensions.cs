using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omniscient.Foundation.Extensions
{
    /// <summary>
    /// Defines extension methods on the String class.
    /// </summary>
    public static class StringExtensions
    {
        public static string FormatEx(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
