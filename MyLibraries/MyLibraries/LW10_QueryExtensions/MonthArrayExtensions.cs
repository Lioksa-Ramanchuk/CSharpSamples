using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.LW10_QueryExtensions
{
    public static class MonthArrayExtensions
    {
        public static IEnumerable<string> WhereLength(this IEnumerable<string> months, int n)
        {
            return months.Where(m => m.Length == n);
        }

        public static IEnumerable<string> WhereSummerWinterMonth(this IEnumerable<string> months)
        {
            string[] summerWinterMonths = {
                "June", "July", "August",
                "December", "January", "February"
            };

            return months.Where(m => summerWinterMonths.Contains(m));
        }

        public static void PrintOrdered(this IEnumerable<string> months)
        {
            Console.WriteLine(string.Join(", ", months.OrderBy(m => m)));
        }

        public static int CountWithLetterAndMinLength(this IEnumerable<string> months, char letter, int minLength)
        {
            return months.Count(m => m.Length >= minLength && m.Contains(letter, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}