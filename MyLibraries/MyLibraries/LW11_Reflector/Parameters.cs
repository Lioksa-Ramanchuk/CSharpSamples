namespace OOP.LW11_Reflector
{
    public class Parameters
    {
        public object?[]? Pars { get; set; }
        public Parameters()
        {
            Pars = null;
        }
        public Parameters(object?[]? pars)
        {
            Pars = pars;
        }
    }
}