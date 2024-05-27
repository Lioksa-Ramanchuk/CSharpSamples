#pragma warning disable S2583 // Conditionally executed code should be reachable

using System;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteTupleTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" КОРТЕЖИ");
            Console.WriteLine("===================================================\n");

            //Задание a
            Console.WriteLine("== Задание a ==\n");

            var t1 = (-1, "два", 'з', "четыре", 5ul);
            Console.WriteLine($"Кортеж: {t1}");


            // Задание b
            Console.WriteLine("\n== Задание b ==\n");

            Console.WriteLine($"Элементы 1, 2, 4 кортежа: {t1.Item1}, {t1.Item2}, {t1.Item4}");


            // Задание c
            Console.WriteLine("\n== Задание c ==\n");

            Console.WriteLine("Распаковки кортежа:");

            (int i1, string str11, char c1, string str12, ulong ul1) = t1;
            Console.WriteLine($"1: ({i1}, {str11}, {c1}, {str12}, {ul1})");

            int i2;
            string str21, str22;
            char c2;
            ulong ul2;

            var t2 = (i2, str21, c2, str22, ul2) = t1;
            Console.WriteLine($"2: ({i2}, {str21}, {c2}, {str22}, {ul2})");

            var (i3, str31, c3, _, _) = t1;
            var (_, _, _, str32, ul3) = t1;
            Console.WriteLine($"3: ({i3}, {str31}, {c3}, {str32}, {ul3})");


            // Задание d
            Console.WriteLine("\n== Задание d ==\n");

            Console.WriteLine($"t1 = {t1}\nt2 = {t2}");
            Console.WriteLine(t2.Equals(t1) ? "t1 == t2" : "t1 != t2");
            t2.i2++;
            Console.WriteLine($"t2 = {t2}");
            Console.WriteLine(t2.Equals(t1) ? "t1 == t2" : "t1 != t2");

            Console.ReadLine();
        }
    }
}