namespace OOP.LW04_Documents
{
    public partial class Date
    {
        public override bool Equals(object? obj)
        {
            return obj is Date d &&
                   Day == d.Day &&
                   Month == d.Month &&
                   Year == d.Year;
        }
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Day, Month, Year);
        }
        public override string ToString()
        {
            return $"{Day:D2}.{Month:D2}.{Year:D4}";
        }
    }
}