using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace OOP.LW13_ISerializer
{
    public class MyXmlSerializer : ISerializer
    {
        public void Serialize(object obj, string filePath)
        {
            DataContractSerializer dcxs = new(obj.GetType());
            using XmlWriter xw = XmlWriter.Create(filePath, new XmlWriterSettings() { Indent = true });
            dcxs.WriteObject(xw, obj);
        }

        public T? Deserialize<T>(string filePath) where T : class
        {
            DataContractSerializer dcxs = new(typeof(T));
            using FileStream fs = File.OpenRead(filePath);
            return dcxs.ReadObject(fs) as T;
        }
    }
}
