using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace OOP.LW04_Documents
{
    [Serializable]
    [DataContract(Namespace = "")]
    [JsonObject(MemberSerialization.Fields)]
    public sealed partial class Cheque : Document, IClonable, IEquatable<Cheque>
    {
        decimal _amount = 0;

        public Cheque(string drawer, string payee, string drawee, decimal amount, Date date, uint number)
        {
            Drawer = drawer;
            Payee = payee;
            Drawee = drawee;
            Amount = amount;
            Date.Set(date);
            Number = number;
        }

        IClonable IClonable.DoClone()
        {
            return DoClone();
        }
        public override Cheque DoClone()
        {
            return new Cheque(new(Drawer), new(Payee), new(Drawee), Amount, Date, Number) { IsOriginal = false, IsSigned = IsSigned };
        }

        bool IEquatable<Cheque>.Equals(Cheque? other)
        {
            return Equals(other);
        }

        [DataMember]
        public string Drawer { get; set; }
        [DataMember]
        public string Payee { get; set; }
        [field: NonSerialized]
        public string Drawee { get; set; }
        [DataMember]
        public decimal Amount
        {
            get => _amount;
            set => _amount = (value >= 0) ? value : 0;
        }

        public override string GetTypeName()
        {
            return "Чек";
        }

        public override string ToString()
        {
            return $"=== {GetTypeName()} ===\n" +
                   $"Чекодатель:      {Drawer}\n" +
                   $"Чекодержатель:   {Payee}\n" +
                   $"Плательщик:      {Drawee}\n" +
                   $"Сумма:           {Amount:C2}\n" +
                   $"Дата:            {Date}\n" +
                   $"Номер документа: {Number}\n" +
                   $"Оригинал:        {(IsOriginal ? "Да" : "Нет")}\n" +
                   $"Подпись:         {(IsSigned ? "Есть" : "Нет")}\n";
        }

        public override bool Equals(object? obj)
        {
            return obj is Cheque other &&
                   Drawer.Equals(other.Drawer) &&
                   Payee.Equals(other.Payee) &&
                   Drawee.Equals(other.Drawee) &&
                   Amount == other.Amount &&
                   Date.Equals(other.Date) &&
                   Number == other.Number &&
                   IsOriginal == other.IsOriginal &&
                   IsSigned == other.IsSigned;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Drawer, Payee, Drawee, Amount, Date, Number, IsOriginal, IsSigned);
        }
    }
}