namespace OOP.LW17_Airline
{
    public abstract class Airplane
    {
        public int Id { get; init; } = 0;
    }
    public class Boeing : Airplane
    {
        public Boeing(int id)
        {
            Id = id;
        }
        public override string ToString() => $"Боинг #{Id}";
    }
    public class Airbus : Airplane
    {
        public Airbus(int id)
        {
            Id = id;
        }
        public override string ToString() => $"Аэробус #{Id}";
    }

    public abstract class AirplaneFactory
    {
        public abstract Airplane CreateAirplane(int id);
    }
    public class BoeingFactory : AirplaneFactory
    {
        public override Boeing CreateAirplane(int id) => new(id);
    }
    public class AirbusFactory : AirplaneFactory
    {
        public override Airbus CreateAirplane(int id) => new(id);
    }
}
