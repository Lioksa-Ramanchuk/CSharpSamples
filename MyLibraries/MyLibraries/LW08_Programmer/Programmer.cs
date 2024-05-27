using System;

namespace OOP.LW08_Programmer
{
    public class Programmer
    {
        public event EventHandler Delete = delegate { };
        public event EventHandler Mutate = delegate { };

        public void DoDelete() => Delete(this, EventArgs.Empty);
        public void DoMutate() => Mutate(this, EventArgs.Empty);
    }
}