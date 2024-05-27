#pragma warning disable S3010 // Static fields should not be updated in constructors
#pragma warning disable S3963 // "static" fields should be initialized inline

using System;

namespace OOP.LW02_Product
{
    public partial class Product
    {
        const string _className = "Product";
        static uint _count;
        readonly uint _id;
        string? _producer;
        decimal? _price;
        TimeSpan? _shelfLife;

        static Product()
        {
            _count = 0;
        }

        public Product(string name, string? producer = null, decimal? price = null, TimeSpan? shelfLife = null, uint? amount = null)
            : this()
        {
            Name = name;
            Producer = producer;
            Price = price;
            ShelfLife = shelfLife;
            Amount = amount;
        }

        public Product(Product product) : this()
        {
            Name = product.Name;
            Upc = product.Upc;
            _producer = product._producer;
            _price = product._price;
            _shelfLife = product._shelfLife;
            Amount = product.Amount;
        }

        Product()
        {
            _count++;
            _id = (uint)HashCode.Combine(_count);
            Name = string.Empty;
            Upc = 0;
            _producer = null;
            _price = null;
            _shelfLife = null;
            Amount = null;
        }

        public string Name { get; init; }

        public uint Upc { get; private set; }

        public string? Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                Upc = (uint)HashCode.Combine(Name, _producer, _price, _shelfLife);
            }
        }

        public decimal? Price
        {
            get => _price;
            set
            {
                _price = (value is null || value >= 0 ? value : null);
                Upc = (uint)HashCode.Combine(Name, _producer, _price, _shelfLife);
            }
        }

        public TimeSpan? ShelfLife
        {
            get => _shelfLife;
            set
            {
                _shelfLife = value;
                Upc = (uint)HashCode.Combine(Name, _producer, _price, _shelfLife);
            }
        }

        public uint? Amount { get; set; }
    }
}