using System.Text;

namespace OOP
{
    using LW17_Airline;
    
    static class LaboratoryWork17
    {
        static void Main()
        {
            static string GenName()
            {
                Random rand = new();
                StringBuilder name = new(5);
                char[][] letters =
                {
                    new char[] { 'a', 'e', 'i', 'o', 'u', 'y' },
                    new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' }
                };
                int letterFlag = rand.Next(2);
                name.Append(char.ToUpper(letters[letterFlag][rand.Next(letters[letterFlag].Length)]));
                for (int i = 1, n = rand.Next(4,7); i < n; i++)
                {
                    letterFlag = (letterFlag + 1) % 2;
                    name.Append(letters[letterFlag][rand.Next(letters[letterFlag].Length)]);
                }
                return $"{name}";
            }

            Controls.GetInstance().Color = ConsoleColor.Green;
            Airline airline = new("Sealand Airlines");

            airline.AddAirplane(new BoeingFactory().CreateAirplane(1));
            airline.AddAirplane(new AirbusFactory().CreateAirplane(2));

            Administrator admin = new(GenName());
            Dispatcher dispatcher = new(GenName());
            RadioOperator radioOperator1 = new(GenName());
            RadioOperator radioOperator2 = new(GenName());
            Navigator navigator1 = new(GenName());
            Navigator navigator2 = new(GenName());
            Pilot pilot1 = new(GenName());
            Pilot pilot2 = new(GenName());
            Pilot pilot3 = new(GenName());
            Pilot pilot4 = new(GenName());
            FlightAttendant flightAttendant1 = new(GenName());
            FlightAttendant flightAttendant2 = (FlightAttendant)flightAttendant1.Clone();
            FlightAttendant flightAttendant3 = new(GenName());
            FlightAttendant flightAttendant4 = new(GenName());
            airline.AddStuff(admin,
                             dispatcher,
                             radioOperator1, radioOperator2,
                             navigator1, navigator2,
                             pilot1, pilot2, pilot3, pilot4,
                             flightAttendant1, flightAttendant2, flightAttendant3, flightAttendant4);
            Controls.GetInstance().Color = ConsoleColor.Yellow;

            Flight flight1 = new(1, airline.Airplanes[0])
            {
                Crew = dispatcher
                    .Reset()
                    .AddPilots(airline.Stuff.OfType<Pilot>().Take(2).ToArray())
                    .AddFlightAttendants(airline.Stuff.OfType<FlightAttendant>().Take(2).ToArray())
                    .AddNavigators(airline.Stuff.OfType<Navigator>().First())
                    .AddRadioOperators(airline.Stuff.OfType<RadioOperator>().First())
                    .GetCrew(),
            };
            admin.AddFlight(flight1);

            Flight flight2 = new(2, airline.Airplanes[1])
            {
                Crew = dispatcher
                    .Reset()
                    .AddPilots(airline.Stuff.OfType<Pilot>().Take(2).ToArray())
                    .AddFlightAttendants(airline.Stuff.OfType<FlightAttendant>().Take(2).ToArray())
                    .AddNavigators(airline.Stuff.OfType<Navigator>().First())
                    .AddRadioOperators(airline.Stuff.OfType<RadioOperator>().First())
                    .GetCrew(),
            };
            admin.AddFlight(flight2);

            admin.RemoveFlight(flight1);
            admin.RemoveFlight(flight2);
        }
    }
}