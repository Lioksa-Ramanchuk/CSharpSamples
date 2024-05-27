namespace OOP.LW04_Documents
{
    public interface ISignable
    {
        bool IsSigned { get; }

        void Sign();
    }
}