using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.LW03_List
{
    public static class StatisticOperation
    {
        public static int Sum(this List<int> list) => list.Aggregate((total, nextElem) => total + nextElem);

        public static int MaxMinDiff(this List<int> list) => list.Max() - list.Min();

        public static int Count<T>(this List<T> list) where T : IEquatable<T> => ((IEnumerable<T>)list).Count();

        public static int CountCapitalWords(this string str)
        {
            return str.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                      .Count(word => char.IsUpper(word[0]));
        }
        public static int CountCapitalWords(this List<string> list)
        {
            return list.Select(str => str.CountCapitalWords())
                       .Aggregate((total, nextStringWordsNumber) => total + nextStringWordsNumber);
        }

        public static bool ContainsDuplicates(this string str)
        {
            return str.Length != str.Distinct().Count();
        }
        public static bool ContainsDuplicates<T>(this List<T> list) where T : IEquatable<T>
        {
            return list.Count() != list.Distinct().Count();
        }
    }
}