using System;

namespace OOP.LW17_Airline
{
    public sealed class Controls
    {
        static readonly Lazy<Controls> Lazy = new(() => new Controls());
        Controls() { }
        public static Controls GetInstance() => Lazy.Value;
        public ConsoleColor Color { get; set; } = default;
    }
}
