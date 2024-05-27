using Newtonsoft.Json;
using System;

namespace OOP.LW03_List
{
    [JsonObject(MemberSerialization.Fields)]
    public class Date
    {
        public byte Day { get; }
        public byte Month { get; }
        public uint Year { get; }

        public Date()
        {
            Day = (byte)DateTime.Now.Day;
            Month = (byte)DateTime.Now.Month;
            Year = (uint)DateTime.Now.Year;
        }

        public override bool Equals(object? obj)
        {
            return obj is Date d &&
                   Day == d.Day &&
                   Month == d.Month &&
                   Year == d.Year;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Day, Month, Year);
        }
        public override string ToString() => $"{Day:D2}.{Month:D2}.{Year}";
    }
}