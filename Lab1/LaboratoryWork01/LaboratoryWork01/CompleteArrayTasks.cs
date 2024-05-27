#pragma warning disable CS0219 // Переменная назначена, но ее значение не используется
#pragma warning disable IDE0059 // Ненужное присваивание значения
#pragma warning disable S1481 // Unused local variables should be removed

using System;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteArrayTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" МАССИВЫ");
            Console.WriteLine("===================================================\n");

            // Задание a
            Console.WriteLine("== Задание a ==\n");

            int[,] iArr = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            for (int i = 0; i < iArr.GetLength(0); i++)
            {
                for (int j = 0; j < iArr.GetLength(1); j++)
                {
                    Console.Write(" " + iArr[i, j]);
                }

                Console.WriteLine();
            }


            // Задание b
            Console.WriteLine("\n== Задание b ==\n");

            string[] strArr = { "Hello,", "world", "!" };

            Console.WriteLine("Содержимое массива strArr:");
            foreach (string s in strArr)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine($"Длина массива strArr: {strArr.Length}");

            int pos;
            Console.Write($"Введите позицию в массиве strArr для замены элемента (от 1 до {strArr.Length}): ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out pos) &&
                    pos > 0 &&
                    pos <= strArr.Length)
                {
                    break;
                }
                else
                {
                    Console.Write($"Позиция должна быть целым числом от 1 до {strArr.Length}. Введите ещё раз: ");
                }
            }

            Console.Write("Введите строку: ");
            strArr[pos - 1] = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\nСодержимое массива strArr:");
            foreach (string s in strArr)
                Console.WriteLine(s);


            // Задание c
            Console.WriteLine("\n== Задание c ==\n");

            int[][] intJaggedArray = new int[][]
            {
                new int[2],
                new int[3],
                new int[4]
            };

            for (int i = 0; i < intJaggedArray.Length; i++)
            {
                for (int j = 0; j < intJaggedArray[i].Length; j++)
                {
                    Console.Write($"Введите значение элемента A({i}, {j}): ");
                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out intJaggedArray[i][j]))
                        {
                            break;
                        }
                        else
                        {
                            Console.Write($"Некорректный ввод. Введите целое число: ");
                        }
                    }
                }
            }

            Console.WriteLine("\nСодержимое массива intJaggedArray:");
            for (int i = 0; i < intJaggedArray.Length; i++)
            {
                for (int j = 0; j < intJaggedArray[i].Length; j++)
                {
                    Console.Write(" " + intJaggedArray[i][j]);
                }

                Console.WriteLine();
            }


            // Задание d

            var vArr = new[] { 1L, 2L, 3L };
            var vStr = "Hello";

            Console.ReadLine();
        }
    }
}