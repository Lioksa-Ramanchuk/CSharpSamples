using System;
using System.Linq;

namespace OOP
{
    using LW02_Product;

    static class LaboratoryWork02
    {
        static void Main()
        {
            Product.PrintClassInfo();
            Product p1 = new("Молоко", "ОАО \"Минский молочный завод №1\"", 1.70m, TimeSpan.FromDays(14), 40);
            Console.WriteLine($"p1:\n{p1}");
            p1.PrintSum();

            Product p2 = new("Шоколад", "СОАО \"Коммунарка\"", 3.50m);
            Console.WriteLine($"\np2:\n{p2}");

            Product p3 = new(p1);
            Console.WriteLine($"p3:\n{p3}");

            Product p4 = new("Шоколад", "Nestlé S. A.", 3.25m);
            Console.WriteLine($"p4:\n{p4}");

            Product p5 = new("Шоколад", "Alpen Gold", 3.40m);
            Console.WriteLine($"p5:\n{p5}");

            Product p6 = new("Молоко", "ОАО \"Минский молочный завод №1\"", 1.70m, TimeSpan.FromDays(14), 20);
            Console.WriteLine($"p6:\n{p6}");
            Product.PrintClassInfo();

            Console.WriteLine($"\nТип объекта p1: {p1.GetType().Name} ({p1.GetType().FullName})");

            Console.WriteLine($"\np1.GetHashCode(): {p1.GetHashCode()}");
            Console.WriteLine($"p3.GetHashCode(): {p3.GetHashCode()}");
            Console.WriteLine($"p1 {(p1.Equals(p3) ? "==" : "!=")} p3");
            p3.Amount++;
            Console.WriteLine($"\np1.GetHashCode(): {p1.GetHashCode()}");
            Console.WriteLine($"p3.GetHashCode(): {p3.GetHashCode()}");
            Console.WriteLine($"p1 {(p1.Equals(p3) ? "==" : "!=")} p3");

            p1.AddProduct(ref p6!, out bool isSuccess);
            if (isSuccess)
            {
                Console.WriteLine("\nК товару p1 был добавлен товар p6.");
                Console.WriteLine($"p1:\n{p1}");
                Console.WriteLine($"p6: {(p6 is null ? "null\n" : $"\n{p6}")}");
            }
            else
            {
                Console.WriteLine("\nНе удалось добавить товар p6 к товару p1.");
            }

            p1.Producer = "ОАО \"Савушкин продукт\"";
            p1.Price = 4.11m;
            p1.ShelfLife = TimeSpan.FromDays(120);
            p1.Amount = 34;
            Console.WriteLine($"p1:\n{p1}");

            p1.Price = -5m;
            Console.WriteLine($"p1:\n{p1}");

            Product?[] shop = new[] { p1, p2, p3, p4, p5, p6 };
            Console.WriteLine("Введите наименование товара для поиска:");
            string productNameToSearch = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("\nНайденные товары:");
            shop.Where(p => p?.Name == productNameToSearch)
                .ToList()
                .ForEach(Console.WriteLine);

            Console.Write($"Введите максимальную цену товара {productNameToSearch}: ");
            decimal productMaxPrice = decimal.Parse(Console.ReadLine() ?? string.Empty);

            Console.WriteLine("\nНайденные товары:");
            shop.Where(p => p?.Name == productNameToSearch && p?.Price <= productMaxPrice)
                .ToList()
                .ForEach(Console.WriteLine);

            var anonProduct = new
            {
                Id = 12345678u,
                Name = "Торт \"Наполеон\"",
                Upc = 87654321u,
                Producer = "КУП \"Минскхлебпром\"",
                Price = 50m,
                ShelfLife = TimeSpan.FromDays(7),
                Amount = 5u
            };

            Console.WriteLine("\nТовар анонимного типа:\n" +
                              $"ID:            {anonProduct.Id}\n" +
                              $"Наименование:  {anonProduct.Name}\n" +
                              $"UPC:           {anonProduct.Upc}\n" +
                              $"Производитель: {anonProduct.Producer}\n" +
                              $"Цена:          {anonProduct.Price:C2}\n" +
                              $"Срок хранения: {anonProduct.ShelfLife.Days} дней\n" +
                              $"Количество:    {anonProduct.Amount}\n");
        }
    }
}