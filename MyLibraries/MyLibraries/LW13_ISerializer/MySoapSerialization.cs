using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace OOP.LW13_ISerializer
{
    public class MySoapSerializer : ISerializer
    {
        public void Serialize(object obj, string filePath)
        {
            SoapFormatter sf = new();
            using FileStream fs = File.Create(filePath);
            sf.Serialize(fs, obj);
        }

        public T? Deserialize<T>(string filePath) where T : class
        {
            SoapFormatter sf = new();
            using FileStream fs = File.OpenRead(filePath);
            return sf.Deserialize(fs) as T;
        }
    }
}
