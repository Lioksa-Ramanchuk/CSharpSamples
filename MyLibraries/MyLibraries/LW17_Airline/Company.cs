using System;
using System.Collections.Generic;

namespace OOP.LW17_Airline
{
    public interface IStuffAdder
    {
        public void AddStuff(params Worker[] workers);
    }

    public abstract class Company : IStuffAdder
    {
        public string Name { get; set; } = string.Empty;
        public List<Worker> Stuff { get; init; } = new();

        public virtual void AddStuff(params Worker[] workers)
        {
            Array.ForEach(workers, w => w.Company = this);
            Stuff.AddRange(workers);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
