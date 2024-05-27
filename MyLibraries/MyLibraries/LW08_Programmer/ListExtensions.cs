using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.LW08_Programmer
{
    public static class ListExtensions
    {
        public static void PopFirst<T>(this List<T> l)
        {
            if (l.Any()) l.RemoveAt(0);
        }

        public static void PopLast<T>(this List<T> l)
        {
            if (l.Any()) l.RemoveAt(l.Count - 1);
        }

        public static void Shuffle<T>(this List<T> l)
        {
            Random rnd = new();
            int rndIdx;
            for (int i = l.Count; i > 1; i--)
            {
                rndIdx = rnd.Next(i);
                (l[rndIdx], l[i - 1]) = (l[i - 1], l[rndIdx]);
            }
        }
    }
}
