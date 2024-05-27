namespace OOP.LW13_ISerializer
{
    public interface ISerializer
    {
        void Serialize(object obj, string filePath);
        T? Deserialize<T>(string filePath) where T : class;
    }
}