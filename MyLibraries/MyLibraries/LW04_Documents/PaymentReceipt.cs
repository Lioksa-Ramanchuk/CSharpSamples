using Newtonsoft.Json;

namespace OOP.LW04_Documents
{
    public partial class PaymentReceipt : Document, IClonable
    {
        decimal _cost;

        [JsonConstructor]
        public PaymentReceipt(Organization organization, string payer, string service, decimal cost, Date date, uint number)
        {
            IsOriginal = true;
            Organization = organization;
            Payer = payer;
            Service = service;
            Cost = cost;
            Date.Set(date);
            Number = number;
        }

        IClonable IClonable.DoClone()
        {
            return DoClone();
        }
        public override PaymentReceipt DoClone()
        {
            return new(new(Organization), new(Payer), new(Service), Cost, Date, Number) { IsOriginal = false, IsSigned = IsSigned };
        }
        
        public Organization Organization { get; set; }
        public string Payer { get; set; }
        public string Service { get; set; }
        public decimal Cost
        {
            get => _cost;
            set => _cost = (value >= 0m) ? value : 0m;
        }

        public override string GetTypeName()
        {
            return "Квитанция";
        }

        public override string ToString()
        {
            return $"=== {GetTypeName()} ===\n" +
                   $"Организация:     {Organization.Name}\n" +
                   $"Плательщик:      {Payer}\n" +
                   $"Услуга:          {Service}\n" +
                   $"Стоимость:       {Cost:C2}\n" +
                   $"Дата:            {Date}\n" +
                   $"Номер документа: {Number}\n" +
                   $"Оригинал:        {(IsOriginal ? "Да" : "Нет")}\n" +
                   $"Подпись:         {(IsSigned ? "Есть" : "Нет")}\n";
        }
    }
}