using System;
using System.Text;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteStringTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" СТРОКИ");
            Console.WriteLine("===================================================\n");

            // Задание a
            Console.WriteLine("== Задание a ==\n");

            string firstString = "Hello", secondString = "World";
            CompareStrings(firstString, secondString);
            CompareStrings(secondString, firstString);
            CompareStrings(firstString, firstString);

            static void CompareStrings(string str1, string str2)
            {
                Console.WriteLine(str1 +
                                  str1.CompareTo(str2) switch
                                  {
                                      < 0 => " < ",
                                      0 => " = ",
                                      > 0 => " > "
                                  } +
                                  str2);
            }


            // Задание b
            Console.WriteLine("\n== Задание b ==\n");

            string s1 = "Hello, world!", s2 = "ABC", s3 = "123";
            Console.WriteLine($"s1 = {s1}, s2 = {s2}, s3 = {s3}");
            Console.WriteLine($"s1 + s2 + s3 = {string.Concat(s1, s2, s3)}");
            s3 = new string(s2);
            Console.WriteLine($"s3 = s2 --> s3 = {s3}");
            Console.WriteLine($"s1[1..5] = {s1.Substring(1, 4)}");
            Console.Write("Слова строки s1: ");
            Console.WriteLine(string.Join(" ; ", s1.Split(new[] { ' ', ',', '!' }, StringSplitOptions.RemoveEmptyEntries)));
            Console.WriteLine("Вставка строки s2 в позицию 3 строки s1:");
            Console.WriteLine($" s1 = {s1}");
            Console.WriteLine($" s2 = {s2}");
            s1 = s1.Insert(3, s2);
            Console.WriteLine($" --> s1 = {s1}");
            Console.WriteLine($"Удаление подстроки s2 из строки s1:");
            Console.WriteLine($" s1 = {s1}");
            Console.WriteLine($" s2 = {s2}");
            s1 = s1.Replace(s2, string.Empty);
            Console.WriteLine($" --> s1 = {s1}");


            // Задание с
            Console.WriteLine("\n== Задание c ==\n");

            string? emptyString = string.Empty, nullString = null, strHello = "Hello";

            static void printStringIsNullOrEmpty(string? str, string strName)
            {
                if (string.IsNullOrEmpty(str))
                    Console.WriteLine($"Строка {strName} пуста либо содержит null");
                else
                    Console.WriteLine($"Строка {strName} не пуста и не содержит null");
            }

            printStringIsNullOrEmpty(emptyString, nameof(emptyString));
            printStringIsNullOrEmpty(nullString, nameof(nullString));
            printStringIsNullOrEmpty(strHello, nameof(strHello));

            Console.WriteLine($"Длина строки emptyString: {emptyString?.Length}");
            Console.WriteLine($"Длина строки nullString: {nullString?.Length}");


            // Задание d
            Console.WriteLine("\n== Задание d ==\n");

            StringBuilder sb = new("Hello, world!");
            Console.WriteLine($"Строка: {sb}");

            sb.Remove(5, 2);
            sb.Remove(10, 1);
            Console.WriteLine($"Строка, содержащая только буквы: {sb}");

            sb.Insert(0, "123");
            sb.Append("123");
            Console.WriteLine($"Строка, начинающаяся и заканчивающаяся на 123: {sb}");

            Console.ReadLine();
        }
    }
}