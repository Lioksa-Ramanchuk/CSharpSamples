using System.Text.Json.Serialization;

namespace OOP.LW04_Documents
{
    public class Product
    {
        decimal _price = 0;
        uint _amount = 0;

        [JsonConstructor]
        public Product(string name, decimal price, uint amount)
        {
            Name = name;
            Price = price;
            Amount = amount;
        }

        public string Name { get; set; }
        public decimal Price
        {
            get => _price;
            set
            {
                _price = (value >= 0) ? value : 0;
                Cost = _price * _amount;
            }
        }
        public uint Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                Cost = _price * _amount;
            }
        }
        public decimal Cost { get; private set; }

        public override string ToString()
        {
            return $"Название товара: {Name}\n" +
                   $"Цена:            {Price:C2}\n" +
                   $"Количество:      {Amount}\n" +
                   $"Общая стоимость: {Cost:C2}\n";
        }
    }
}