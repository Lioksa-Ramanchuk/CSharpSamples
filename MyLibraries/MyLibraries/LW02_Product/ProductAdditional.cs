using System;

namespace OOP.LW02_Product
{
    public partial class Product
    {
        public static void PrintClassInfo()
        {
            Console.WriteLine($"Класс: {_className}. Количество созданных объектов: {_count}.");
        }

        public override bool Equals(object? obj)
        {
            return obj is Product p &&
                   Upc == p.Upc &&
                   Amount == p.Amount;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Upc, Amount);
        }

        public override string ToString()
        {
            return $"ID:            {_id}\n" +
                   $"Наименование:  {Name}\n" +
                   $"UPC:           {Upc}\n" +
                   $"Производитель: {Producer ?? "-"}\n" +
                   $"Цена:          {(_price is not null ? $"{_price:C2}" : "-")}\n" +
                   $"Срок хранения: {(_shelfLife is not null ? _shelfLife?.Days + " дней" : "-")}\n" +
                   $"Количество:    {(Amount is not null ? Amount : "-")}\n";
        }

        public void AddProduct(ref Product? product, out bool isSuccess)
        {
            if (product is not null && Upc == product.Upc)
            {
                Amount += product.Amount;
                product = null;
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
        }

        public void PrintSum()
        {
            Console.WriteLine($"Общая сумма продукта \"{Name}\": {_price * Amount}.");
        }
    }
}