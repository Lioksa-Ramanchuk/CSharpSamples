using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace OOP.LW13_ISerializer
{
    public class MyJsonSerializer : ISerializer
    {
        public void Serialize(object obj, string filePath)
        {
            DataContractJsonSerializer dcjs = new(obj.GetType());
            using XmlDictionaryWriter xdw = JsonReaderWriterFactory.CreateJsonWriter(
                File.Create(filePath), System.Text.Encoding.UTF8, true, true);
            dcjs.WriteObject(xdw, obj);
        }

        public T? Deserialize<T>(string filePath) where T : class
        {
            DataContractJsonSerializer dcjs = new(typeof(T));
            using FileStream fs = File.OpenRead(filePath);
            return dcjs.ReadObject(fs) as T;
        }
    }
}
