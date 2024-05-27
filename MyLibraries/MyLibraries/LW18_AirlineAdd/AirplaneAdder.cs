using System.Linq;

namespace OOP.LW18_AirlineAdd
{
    using LW17_Airline;

    public interface IAirplaneAdder
    {
        void AddNewAirplanes(params Airplane[] airplanes);
    }
    public class AirplaneAdder : IAirplaneAdder
    {
        readonly Airline _airline;
        public AirplaneAdder(Airline airline)
        {
            _airline = airline;
        }

        public void AddNewAirplanes(params Airplane[] airplanes)
        {
            _airline.AddAirplanes(airplanes.ToList());
        }
    }
}