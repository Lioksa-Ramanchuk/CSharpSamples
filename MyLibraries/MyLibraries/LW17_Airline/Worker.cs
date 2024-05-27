using System;
using System.Linq;

namespace OOP.LW17_Airline
{
    public interface IClonable
    {
        IClonable Clone();
    }

    public abstract class Worker : IClonable
    {
        public string Name { get; set; } = string.Empty;
        public Company? Company { get; set; } = null;
        public override string ToString() => Name;
        public abstract IClonable Clone();
    }
    public class Administrator : Worker
    {
        public Administrator(string name)
        {
            Name = name;
        }
        public void AddFlight(Flight f)
        {
            if (Company is Airline a)
            {
                Console.ForegroundColor = Controls.GetInstance().Color;
                if (a.AddFlight(f))
                {
                    Console.WriteLine($"{Name}: добавлен рейс №{f.Id}. Авиакомпания: {Company} Самолёт: {f.Airplane}");
                    Console.WriteLine($"     Состав лётной бригады: {
                        string.Join(", ", f.Crew.Pilots)}; {
                        string.Join(", ", f.Crew.Navigators)}; {
                        string.Join(", ", f.Crew.RadioOperators)}; {
                        string.Join(", ", f.Crew.FlightAttendants)}");
                }
                else
                {
                    Console.WriteLine($"{Name}: рейс №{f.Id} не был добавлен. Похоже, сейчас не лётная погода.");
                }
                Console.ResetColor();
            }
        }
        public void RemoveFlight(Flight f)
        {
            if (Company is Airline a)
            {
                Console.ForegroundColor = Controls.GetInstance().Color;
                if (a.RemoveFlight(f))
                {
                    Console.WriteLine($"{Name}: удалён рейс №{f.Id}. Авиакомпания: {Company}");
                }
                else
                {
                    Console.WriteLine($"{Name}: рейс №{f.Id} не был запланирован");
                }
                Console.ResetColor();
            }
        }
        public override IClonable Clone()
        {
            return new Administrator(new string(Name)) { Company = Company };
        }
    }
    public interface ICrewBuilder
    {
        ICrewBuilder Reset();
        ICrewBuilder AddFlightAttendants(params FlightAttendant[] flightAttendants);
        ICrewBuilder AddNavigators(params Navigator[] navigators);
        ICrewBuilder AddPilots(params Pilot[] pilots);
        ICrewBuilder AddRadioOperators(params RadioOperator[] radioOperators);
        Crew GetCrew();
    }
    public class Dispatcher : Worker, ICrewBuilder
    {
        Crew _crew = new();
        public Dispatcher(string name)
        {
            Name = name;
        }
        public ICrewBuilder Reset()
        {
            _crew = new Crew();
            return this;
        }
        public ICrewBuilder AddFlightAttendants(params FlightAttendant[] flightAttendants)
        {
            _crew.FlightAttendants.AddRange(flightAttendants);
            Console.ForegroundColor = Controls.GetInstance().Color;
            Console.WriteLine($"{Name}: в лётную бригаду добавлен(ы) бортпроводник(и) {string.Join(", ", flightAttendants.AsEnumerable())}");
            Console.ResetColor();
            return this;
        }
        public ICrewBuilder AddNavigators(params Navigator[] navigators)
        {
            _crew.Navigators.AddRange(navigators);
            Console.ForegroundColor = Controls.GetInstance().Color;
            Console.WriteLine($"{Name}: в лётную бригаду добавлен(ы) штурман(ы) {string.Join(", ", navigators.AsEnumerable())}");
            Console.ResetColor();
            return this;
        }
        public ICrewBuilder AddPilots(params Pilot[] pilots)
        {
            _crew.Pilots.AddRange(pilots);
            Console.ForegroundColor = Controls.GetInstance().Color;
            Console.WriteLine($"{Name}: в лётную бригаду добавлен(ы) пилот(ы) {string.Join(", ", pilots.AsEnumerable())}");
            Console.ResetColor();
            return this;
        }
        public ICrewBuilder AddRadioOperators(params RadioOperator[] radioOperators)
        {
            _crew.RadioOperators.AddRange(radioOperators);
            Console.ForegroundColor = Controls.GetInstance().Color;
            Console.WriteLine($"{Name}: в лётную бригаду добавлен(ы) радист(ы) {string.Join(", ", radioOperators.AsEnumerable())}");
            Console.ResetColor();
            return this;
        }
        public Crew GetCrew()
        {
            return _crew;
        }
        public override IClonable Clone()
        {
            return new Dispatcher(new string(Name)) { Company = Company };
        }
    }
    public class Pilot : Worker
    {
        public Pilot(string name)
        {
            Name = name;
        }
        public override IClonable Clone()
        {
            return new Pilot(new string(Name)) { Company = Company };
        }
    }
    public class RadioOperator : Worker
    {
        public RadioOperator(string name)
        {
            Name = name;
        }
        public override IClonable Clone()
        {
            return new RadioOperator(new string(Name)) { Company = Company };
        }
    }
    public class FlightAttendant : Worker
    {
        public FlightAttendant(string name)
        {
            Name = name;
        }
        public override IClonable Clone()
        {
            return new FlightAttendant(new string(Name)) { Company = Company };
        }
    }
    public class Navigator : Worker
    {
        public Navigator(string name)
        {
            Name = name;
        }
        public override IClonable Clone()
        {
            return new Navigator(new string(Name)) { Company = Company };
        }
    }
}
