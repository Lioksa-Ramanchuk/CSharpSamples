using System.Collections.Generic;
using System.Linq;

namespace OOP.LW18_AirlineAdd
{
    using LW17_Airline;

    public class FlightsHistorian
    {
        readonly List<Airline.FlightsSnapshot> _history = new();
        public FlightsHistorian(Airline airline)
        {
            _airline = airline;
        }
        readonly Airline _airline;
        public void Backup()
        {
            _history.Add(_airline.SaveFlights());
        }
        public void Undo()
        {
            if (!_history.Any())
            {
                return;
            }

            var lastSnapshot = _history.Last();
            _history.RemoveAt(_history.Count - 1);
            _airline.RestoreFlights(lastSnapshot);
        }
    }
}