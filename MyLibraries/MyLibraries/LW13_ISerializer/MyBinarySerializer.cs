#pragma warning disable SYSLIB0011 // Тип или член устарел

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OOP.LW13_ISerializer
{
    public class MyBinarySerializer : ISerializer
    {
        public void Serialize(object obj, string filePath)
        {
            BinaryFormatter bf = new();
            using FileStream fs = File.Create(filePath);
            bf.Serialize(fs, obj);
        }

        public T? Deserialize<T>(string filePath) where T : class
        {
            BinaryFormatter bf = new();
            using FileStream fs = File.OpenRead(filePath);
            return bf.Deserialize(fs) as T;
        }
    }
}
