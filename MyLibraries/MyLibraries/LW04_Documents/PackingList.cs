using Newtonsoft.Json;
using System.Linq;

namespace OOP.LW04_Documents
{
    public partial class PackingList : Document, IClonable
    {
        [JsonConstructor]
        public PackingList(string vendor, string customer, Product[] products, Date date, uint number)
        {
            IsOriginal = true;
            Vendor = vendor;
            Customer = customer;
            Products = products;
            Date.Set(date);
            Number = number;
        }

        IClonable IClonable.DoClone()
        {
            return DoClone();
        }
        public override PackingList DoClone()
        {
            return new PackingList(new(Vendor), new(Customer), Products, Date, Number) { IsOriginal = false, IsSigned = IsSigned };
        }

        public string Vendor { get; set; }
        public string Customer { get; set; }
        public Product[] Products { get; set; }
        public decimal Cost
        {
            get => Products.Select(p => p.Cost).Sum();
        }

        public override string GetTypeName()
        {
            return "Накладная";
        }

        public override string ToString()
        {
            uint productNumber = 1;
            return $"=== {GetTypeName()} ===\n" +
                   $"Поставщик:       {Vendor}\n" +
                   $"Получатель:      {Customer}\n" +
                   $"Список товаров:\n" +
                   $"|  №  |        Товар         | Количество |      Цена       |      Сумма      |\n" +
                   string.Concat(Products.Select(p => $"| {productNumber++,3} | {p.Name,-20} | {p.Amount,10} | {p.Price,15:C2} | {p.Cost,15:C2} |\n")) +
                   $"| {"",3} | {"",20} | {"",10} | {"",15} | {Products.Select(p => p.Cost).Sum(),15:C2} |\n" +
                   $"Дата:            {Date}\n" +
                   $"Номер документа: {Number}\n" +
                   $"Оригинал:        {(IsOriginal ? "Да" : "Нет")}\n" +
                   $"Подпись:         {(IsSigned ? "Есть" : "Нет")}\n";
        }
    }
}