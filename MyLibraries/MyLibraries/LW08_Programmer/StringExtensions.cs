using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OOP.LW08_Programmer
{
    public static class StringExtensions
    {
        public static string RemoveCommas(this string s)
        {
            return s.Replace(",", string.Empty);
        }

        public static string InsertStringAtMid(this string s, string strToInsert)
        {
            return s.Insert(s.Length / 2, strToInsert);
        }

        public static string InverseCharCase(this string s)
        {
            return string.Concat(s.Select(c => char.IsLower(c) ? char.ToUpper(c) : char.ToLower(c)));
        }

        public static string RemoveExtraSpaces(this string s)
        {
            return new Regex(@"\s{2,}").Replace(s, " ");
        }

        public static IEnumerable<string> GetUpperCaseWords(this string s)
        {
            string[] words = s.Split(new char[] { ' ', '.', ',', ':', ';', '!', '?', '-', '\"' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Where(w => w.All(c => char.IsUpper(c)));
        }
    }
}
