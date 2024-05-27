using System;
using System.Linq;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteLocalFunctionTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" ЛОКАЛЬНАЯ ФУНКЦИЯ");
            Console.WriteLine("===================================================\n");

            int[] arr = { 5, 2, 4 };
            string str = "Hello";
            (int? maxElem, int? minElem, int? arrSum, char? firstLetter) = GetArrayStatsAnd1stLetter(arr, str);

            Console.WriteLine($"iArr = {{ {string.Join(", ", arr)} }}");
            Console.WriteLine($"str = {str}");
            Console.WriteLine($"Максимальный элемент массива: {maxElem}");
            Console.WriteLine($"Минимальный элемент массива: {minElem}");
            Console.WriteLine($"Сумма элементов массива: {arrSum}");
            Console.WriteLine($"Первая буква строки: {firstLetter}");

            Console.ReadLine();

            static (int?, int?, int?, char?) GetArrayStatsAnd1stLetter(int[] arr, string str)
            {
                (int? maxElem, int? minElem, int? arrSum, char? firstLetter) result;

                if (arr.Length == 0)
                {
                    result.maxElem = result.minElem = result.arrSum = null;
                }
                else
                {
                    result.maxElem = result.minElem = result.arrSum = arr[0];

                    for (int i = 1; i < arr.Length; i++)
                    {
                        if (arr[i] > result.maxElem)
                        {
                            result.maxElem = arr[i];
                        }
                        else if (arr[i] < result.minElem)
                        {
                            result.minElem = arr[i];
                        }

                        result.arrSum += arr[i];
                    }
                }

                result.firstLetter = null;

                if (!string.IsNullOrEmpty(str) && str.Any(c => char.IsLetter(c)))
                {
                    result.firstLetter = str.First(c => char.IsLetter(c));
                }

                return result;
            }
        }
    }
}