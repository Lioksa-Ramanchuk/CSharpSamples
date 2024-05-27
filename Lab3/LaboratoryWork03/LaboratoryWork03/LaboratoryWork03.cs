using System;

namespace OOP
{
    using LW03_List;

    static class LaboratoryWork03
    {
        static void Main()
        {
            List<int> list1 = new(1, 2, 3);
            list1.Owner.Name = "Aliaksiej R.";
            list1.Owner.Organization = "BSTU";
            Console.WriteLine($"list1 = {list1}");

            list1 = 4 + list1;
            Console.WriteLine($"list1 = 4 + list1 = {list1}");

            list1--;
            Console.WriteLine($"list1-- = {list1}");

            List<int> list2 = new(3, 4, 5);
            Console.WriteLine($"list2 = {list2}");
            Console.WriteLine($"list1 {(list1 != list2 ? "!=" : "==")} list2");
            List<int> list3 = list1 * list2;
            Console.WriteLine($"list3 = list1 * list2 = {list3}");
            Console.WriteLine($"list1 {(list1.ContainsDuplicates() ? "" : "не ")}содержит повторяющиеся элементы.");
            Console.WriteLine($"list3 {(list3.ContainsDuplicates() ? "" : "не ")}содержит повторяющиеся элементы.");

            list2 = new(1, 2, 3);
            Console.WriteLine($"list2 = {list2}");
            Console.WriteLine($"list1 {(list1 == list2 ? "==" : "!=")} list2");
            Console.WriteLine($"list1 * list2 = {list1 * list2}");

            Console.WriteLine($"\nСоздатель list1 - {list1.Owner}");
            Console.WriteLine($"Дата создания list1: {list1.CreationDate}");

            Console.WriteLine($"\nСумма значений элементов list1: {list1.Sum()}");
            Console.WriteLine($"Разница между максимальным и минимальным значениями элементов list1: {list1.MaxMinDiff()}");
            Console.WriteLine($"Количество элементов list1: {list1.Count()}");

            List<string> list4 = new("Синтаксис языка Ада ", "унаследован от языков", " типа Algol или Паскаль.");
            Console.WriteLine($"\nlist4 = {list4}");
            Console.WriteLine($"Количество слов с заглавной буквы: {list4.CountCapitalWords()}");

            string str = "Синтаксис языка Ада унаследован от языков типа Algol или Паскаль.";
            Console.WriteLine($"\nstr = {str}");
            Console.WriteLine($"Количество слов с заглавной буквы: {str.CountCapitalWords()}");
            Console.WriteLine($"str {(str.ContainsDuplicates() ? "" : "не ")}содержит повторяющиеся элементы.");
            str = "Компьютер";
            Console.WriteLine($"str = {str}");
            Console.WriteLine($"str {(str.ContainsDuplicates() ? "" : "не ")}содержит повторяющиеся элементы.");
        }
    }
}