using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Extensions
{
    public static class StringExtensions
    {
        public static string EncloseInDoubleQuotes(this string sourceStr)
        {
            return sourceStr.EncloseInTags("\"", "\"");
        }
        public static string EncloseInSingleQuotes(this string sourceStr)
        {
            return sourceStr.EncloseInTags("'", "'");
        }
        public static string EncloseInParenthesis(this string sourceStr)
        {
            return sourceStr.EncloseInTags("(", ")");
        }
        public static string EncloseInBrackets(this string sourceStr)
        {
            return sourceStr.EncloseInTags("[", "]");
        }

        public static string EncloseInTags(this string sourceStr, string openTag, string closeTag)
        {
            string string2Enclose = sourceStr;
            string tag1 = openTag ?? "";
            string tag2 = closeTag ?? "";

            if (string2Enclose != null)
            {
                string2Enclose = tag1 + string2Enclose + tag2;
            }

            return string2Enclose;
        }
    }
}
