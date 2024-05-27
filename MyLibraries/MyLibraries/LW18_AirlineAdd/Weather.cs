using System.Collections.Generic;

namespace OOP.LW18_AirlineAdd
{
    using LW17_Airline;

    public static class Weather
    {
        static bool _isGood = true;
        static readonly List<IWeatherDependant> _companiesToNotify = new();
        public static bool IsGood
        {
            get => _isGood;
            set
            {
                _isGood = value;
                Notify();
            }
        }
        public static void Subscribe(IWeatherDependant company)
        {
            if (!_companiesToNotify.Contains(company))
            {
                _companiesToNotify.Add(company);
            }
        }
        public static void Unsubscribe(IWeatherDependant company)
        {
            _companiesToNotify.Remove(company);
        }
        public static void Notify()
        {
            _companiesToNotify.ForEach(c => c.Update(_isGood));
        }
    }
}