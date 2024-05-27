using System.Linq;

namespace OOP.LW18_AirlineAdd
{
    using LW17_Airline;

    public class StuffAdder : IStuffAdder
    {
        protected readonly Company _company;
        public StuffAdder(Company company)
        {
            _company = company;
        }

        public virtual void AddStuff(params Worker[] workers)
        {
            _company.AddStuff(workers);
        }
    }
    public class SafeStuffAdder : StuffAdder
    {
        public SafeStuffAdder(Company company) : base(company)
        {
        }
        public override void AddStuff(params Worker[] workers)
        {
            _company.AddStuff(workers.Where(w => w.Company is null).ToArray());
        }
    }
}