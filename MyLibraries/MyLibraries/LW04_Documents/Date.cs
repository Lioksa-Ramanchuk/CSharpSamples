using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace OOP.LW04_Documents
{
    [Serializable]
    [DataContract(Namespace = "")]
    public partial class Date : IComparable<Date>
    {
        [NonSerialized]
        readonly byte[] _maxDaysNumberInMonths = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public Date(Date d)
        {
            Set(d);
        }

        [JsonConstructor]
        public Date(byte day = 1, byte month = 1, uint year = 0)
        {
            Set(day, month, year);
        }

        int IComparable<Date>.CompareTo(Date? other)
        {
            if (other is null || this > other)
            {
                return 1;
            }
            else if (this == other)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        [DataMember]
        public byte Day { get; private set; }
        [DataMember]
        public byte Month { get; private set; }
        [DataMember]
        public uint Year { get; private set; }

        public static bool operator ==(Date d1, Date d2)
        {
            return d1.Equals(d2);
        }
        public static bool operator !=(Date d1, Date d2)
        {
            return !d1.Equals(d2);
        }
        public static bool operator >(Date d1, Date d2)
        {
            return d1.Year > d2.Year
                   ||
                   d1.Year == d2.Year && (d1.Month > d2.Month
                                          ||
                                          d1.Month == d2.Month && d1.Day > d2.Day);
        }
        public static bool operator <(Date d1, Date d2)
        {
            return !(d1.Equals(d2) || d1 > d2);
        }
        public static bool operator >=(Date d1, Date d2)
        {
            return d1.Equals(d2) || d1 > d2;
        }
        public static bool operator <=(Date d1, Date d2)
        {
            return !(d1 > d2);
        }

        public static bool IsLeapYear(uint year)
        {
            return ((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0);
        }

        public void Set(Date d)
        {
            (Day, Month, Year) = (d.Day, d.Month, d.Year);
        }
        public void Set(byte day, byte month, uint year)
        {
            Year = year;
            Month = month >= 1 && month <= 12 ? month : (byte)1;
            _maxDaysNumberInMonths[1] = (byte)(IsLeapYear(Year) ? 29 : 28);
            Day = (day >= 1 && day <= _maxDaysNumberInMonths[Month - 1]) ? day : (byte)1;
        }
    }
}