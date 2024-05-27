using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP
{
    using LW02_Product;
    using LW10_QueryExtensions;

    static class LaboratoryWork10
    {
        static void Main()
        {
            string[] monthNames = { "June", "July", "May", "December", "January", "August", "February", "September", "March", "October", "April", "November" };

            Console.WriteLine($"Названия месяцев длиной 5: {string.Join(", ", monthNames.WhereLength(5))}");
            Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", monthNames.WhereSummerWinterMonth())}");
            Console.Write("Месяцы в алфавитном порядке: "); monthNames.PrintOrdered();
            Console.WriteLine($"Количество месяцев с буквой 'u' длиной не менее 4: {string.Join(", ", monthNames.CountWithLetterAndMinLength('u', 4))}");

            //=====================
            Console.WriteLine();
            Console.WriteLine(new string('=', 40));

            List<Product> warehouse = new()
            {
                new("Молоко", "Минский молочный завод №1", 1.70m, TimeSpan.FromDays(14), 40),
                new("Шоколад", "Коммунарка", 3.50m, TimeSpan.FromDays(180), 15),
                new("Шоколад", "Nestlé S. A.", 3.25m, TimeSpan.FromDays(180), 20),
                new("Шоколад", "Alpen Gold", 3.40m, TimeSpan.FromDays(180), 30),
                new("Конфеты", "Слодыч", 10m, TimeSpan.FromDays(120), 500),
                new("Икра красная", "Юкра", 260m, TimeSpan.FromDays(90), 5),
                new("Креветки", "Grupo Newsan", 105m, TimeSpan.FromDays(720), 7),
                new("Крупа гречневая", "Гомельский комбинат хлебопродуктов", 3.50m, TimeSpan.FromDays(2), 365),
                new("Печенье", "Слодыч", 1m, TimeSpan.FromDays(20), 40),
                new("Яблоки", "Агрофилдс", 3.40m, TimeSpan.FromDays(60), 50),
            };

            Console.WriteLine("\nВесь список товаров:");
            Array.ForEach(warehouse.ToArray(), Console.WriteLine);

            //=======================

            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nСписок товаров \"Шоколад\":\n");
            Array.ForEach(warehouse.WhereName("Шоколад").ToArray(), Console.WriteLine);

            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nСписок товаров \"Шоколад\" ценой до 3.45:\n");
            Array.ForEach(warehouse.WhereNameWithPriceLimit("Шоколад", 3.45m).ToArray(), Console.WriteLine);

            Console.WriteLine(new string('=', 40));

            Console.WriteLine($"\nКоличество товаров ценой больше 100: {warehouse.CountIfPriceExceeds(100)}\n");

            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nТовар с максимальной стоимостью:\n");
            Console.WriteLine(warehouse.MaxByCost());

            Console.WriteLine(new string('=', 40));

            Console.WriteLine("\nСписок товаров, упорядоченных по производителю и количеству:\n");
            Array.ForEach(warehouse.OrderByProducerAndAmount().ToArray(), Console.WriteLine);

            //======================
            Console.WriteLine(new string('=', 40));

            var cart = new Product[]
            {
                new("Молоко", "Минский молочный завод №1", null, null, 40),
                new("Шоколад", "Alpen Gold", null, null, 30),
                new("Конфеты", "Слодыч", null, null, 100),
                new("Печенье", "Слодыч", null, null, 40),
            };

            Console.WriteLine("\nКорзина товаров:\n");
            Array.ForEach(cart, Console.WriteLine);

            decimal totalCost = cart.Join(warehouse,
                                          pCart => new { pCart.Name, pCart.Producer },
                                          pWh => new { pWh.Name, pWh.Producer },
                                          (pCart, pWh) => (pCart.Amount ?? 0) * (pWh.Price ?? 0))
                                    .Sum();

            Console.WriteLine(new string('=', 40));

            Console.WriteLine($"\nСтоимость корзины: {totalCost:C2}");
        }
    }
}