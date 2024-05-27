namespace OOP.LW17_Airline
{
    public abstract class AirlineState
    {
        protected Airline? _airline;

        public void SetAirline(Airline airline)
        {
            _airline = airline;
        }
        public abstract bool AddFlight(Flight f);
    }
    public class NormalAirlineState : AirlineState
    {
        public override bool AddFlight(Flight f)
        {
            _airline.Flights.Add(f);
            return true;
        }
    }
    public class BadAirlineState : AirlineState
    {
        public override bool AddFlight(Flight f)
        {
            return false;
        }
    }
}