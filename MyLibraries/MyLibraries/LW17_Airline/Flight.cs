using System.Collections.Generic;

namespace OOP.LW17_Airline
{
    public class Flight
    {
        public Flight(int id, Airplane airplane)
        {
            Id = id;
            Airplane = airplane;
        }
        public int Id { get; init; }
        public Airplane Airplane { get; init; }
        public Crew Crew { get; set; } = new();
    }
    public class Crew
    {
        public List<Navigator> Navigators { get; set; } = new();
        public List<Pilot> Pilots { get; set; } = new();
        public List<RadioOperator> RadioOperators { get; set; } = new();
        public List<FlightAttendant> FlightAttendants { get; set; } = new();
    }
}
