using System.Collections.Generic;
using System;

namespace OOP
{
    using LW08_Programmer;
    static class LaboratoryWork08
    {
        static void Main()
        {
            List<int> il;
            List<double> dl;
            List<string> sl;

            initLists();
            showLists();
            testEvents();

            initLists();
            testEvents();

            initLists();
            testEvents();

            void initLists()
            {
                il = new() { 1, 2, 3, 4, 5 };
                dl = new() { 1.1, 2.2, 3.3, 4.4, 5.5 };
                sl = new() { "s1", "s2", "s3", "s4", "s5" };
            }

            void showLists()
            {
                Console.WriteLine($"il = [{string.Join(", ", il)}]");
                Console.WriteLine($"dl = [{string.Join(", ", dl)}]");
                Console.WriteLine($"sl = [{string.Join(", ", sl)}]\n");
            }

            void testEvents()
            {
                Programmer progr = new();

                progr.Delete += (sender, e) => il.PopLast();
                progr.Delete += (sender, e) => dl.PopLast();
                progr.Delete += (sender, e) => il.PopLast();
                progr.Delete += (sender, e) => dl.PopFirst();
                progr.DoDelete();

                progr.Mutate += (sender, e) => il.Shuffle();
                progr.Mutate += (sender, e) => sl.Shuffle();
                progr.DoMutate();

                showLists();
            }

            //=====================================
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();

            string str = "hello,   world! Hello, universe!";

            Func<string, IEnumerable<string>> SomeTransformations = (s) =>
            {
                return s.RemoveCommas()
                        .InsertStringAtMid("<!>")
                        .InverseCharCase()
                        .RemoveExtraSpaces()
                        .GetUpperCaseWords();
            };

            Console.WriteLine($"Слова в верхнем регистре после преобразований строки: {string.Join(" ", SomeTransformations(str))}");
        }
    }
}