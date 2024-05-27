using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP.LW17_Airline
{
    public interface IWeatherDependant
    {
        void Update(bool weatherIsGood);
    }

    public class Airline : Company, IWeatherDependant
    {
        AirlineState _state;
        public List<Flight> Flights { get; init; } = new();
        public List<Airplane> Airplanes { get; init; } = new();
        public Airline(string name)
        {
            Name = name;
            ChangeState(new NormalAirlineState());
        }

        public void Update(bool weatherIsGood)
        {
            ChangeState(weatherIsGood ? new NormalAirlineState() : new BadAirlineState());
        }

        public void ChangeState(AirlineState airlineState)
        {
            _state = airlineState;
            airlineState.SetAirline(this);
        }

        public override void AddStuff(params Worker[] workers)
        {
            Array.ForEach(workers, w => w.Company = this);
            Stuff.AddRange(workers);

            Console.ForegroundColor = Controls.GetInstance().Color;
            Console.WriteLine($"В авиакомпанию {Name} наняты:");
            foreach (Worker worker in workers)
            {
                Console.WriteLine($" - {worker switch
                {
                    Administrator => "администратор ",
                    Dispatcher => "диспетчер ",
                    Pilot => "пилот ",
                    Navigator => "штурман ",
                    RadioOperator => "радист ",
                    FlightAttendant => "бортпроводник ",
                    _ => string.Empty
                }}{worker}");
            }
            Console.ResetColor();
        }
        public bool AddFlight(Flight f)
        {
           return _state.AddFlight(f);
        }
        public bool RemoveFlight(Flight f)
        {
            return Flights.Remove(f);
        }
        public void AddAirplanes(List<Airplane> airplanes)
        {
            Array.ForEach(airplanes.ToArray(), AddAirplane);
        }
        public void RemoveAirplanes(params Airplane[] airplanes)
        {
            foreach (var a in airplanes.Where(Airplanes.Contains))
            {
                Airplanes.Remove(a);
            }
        }
        public void AddAirplane(Airplane airplane)
        {
            if (!Airplanes.Contains(airplane))
            {
                Airplanes.Add(airplane);
            }
        }

        public class FlightsSnapshot
        {
            public FlightsSnapshot(List<Flight> flights)
            {
                Flights = flights;
            }
            public List<Flight> Flights { get; init; }
        }
        public FlightsSnapshot SaveFlights()
        {
            return new FlightsSnapshot(Flights);
        }
        public void RestoreFlights(FlightsSnapshot fs)
        {
            Flights.Clear();
            fs.Flights.CopyTo(Flights.ToArray());
        }
    }
}