using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace OOP
{
    using LW04_Documents;
    using LW13_ISerializer;

    static class LaboratoryWork13
    {
        static void Main()
        {
            Cheque chq = new("Оксана", "Михаил", "Банк Трим", 500m, new Date(23, 11, 2022), 123);
            chq = chq.DoClone();
            chq.Sign();

            void TestSerializer(ISerializer serializer, string filePath)
            {
                serializer.Serialize(chq, filePath);
                Console.WriteLine(serializer.Deserialize<Cheque>(filePath));
            }

            ISerializer serializer;

            // 1, 2
            try
            {
                Console.WriteLine("Двоичная (де-)сериализация:");
                serializer = new MyBinarySerializer();
                TestSerializer(serializer, Path.Combine("D:", "Temp", "binCh.bin"));

                Console.WriteLine("Soap (де-)сериализация:");
                serializer = new MySoapSerializer();
                TestSerializer(serializer, Path.Combine("D:", "Temp", "soapCh.xml"));

                Console.WriteLine("JSON (де-)сериализация:");
                serializer = new MyJsonSerializer();
                TestSerializer(serializer, Path.Combine("D:", "Temp", "jsonCh.json"));

                Console.WriteLine("XML (де-)сериализация:");
                serializer = new MyXmlSerializer();
                TestSerializer(serializer, Path.Combine("D:", "Temp", "xmlCh.xml"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message} ({e.TargetSite})");
                return;
            }

            // 3
            ArrayList? objList = new() { 1, "s2", 3.3, 4 };
            Console.WriteLine($"ArrayList до десериализации:    {string.Join("; ", objList.ToArray())}");
            serializer = new MyJsonSerializer();
            serializer.Serialize(objList, Path.Combine("D:", "Temp", "list.json"));
            objList.Clear();
            objList = serializer.Deserialize<ArrayList>(Path.Combine("D:", "Temp", "list.json"));
            Console.WriteLine($"ArrayList после десериализации: {string.Join("; ", objList?.ToArray() ?? Array.Empty<object>())}\n");

            // 4
            XmlDocument xmlDoc = new();
            xmlDoc.Load(Path.Combine("D:", "Temp", "xmlCh.xml"));
            XmlNode? node = xmlDoc.DocumentElement?.SelectSingleNode("Payee");
            if (node is not null)
            {
                Console.WriteLine($"Чекодержатель: {node.InnerText}");
            }
            node = xmlDoc.DocumentElement?.SelectSingleNode("Date/Year");
            if (node is not null)
            {
                Console.WriteLine($"Год: {node.InnerText}");
            }
            Console.WriteLine();

            // 5
            XDocument xdoc = new(
                new XElement("Documents",
                new XElement("Cheque",
                    new XAttribute("Date", "24.11.2022"),
                    new XAttribute("Number", "12345"),
                    new XElement("Parties",
                        new XComment("Чекодатель"),
                        new XElement("Drawer", "Андрей"),
                        new XComment("Чекодержатель"),
                        new XElement("Payee", "Марта"),
                        new XComment("Плательщик"),
                        new XElement("Drawee", "Банк Трим")),
                    new XElement("Amount", "300")),
                new XElement("Cheque",
                    new XAttribute("Date", "24.11.2022"),
                    new XAttribute("Number", "54321"),
                    new XElement("Parties",
                        new XComment("Чекодатель"),
                        new XElement("Drawer", "Валерия"),
                        new XComment("Чекодержатель"),
                        new XElement("Payee", "Марк"),
                        new XComment("Плательщик"),
                        new XElement("Drawee", "Банк Трим")),
                    new XElement("Amount", "150"))));
            xdoc.Save(Path.Combine("D:", "Temp", "xpath.xml"));

            Console.WriteLine($"Чекодержатель чека с номером 12345: {xdoc.Root?.Elements("Cheque")
                                                                               .Single(e => e.Attribute("Number")?.Value == "12345")
                                                                               .Descendants("Payee").Single().Value}");
            Console.WriteLine($"Сумма второго чека: {decimal.Parse(xdoc.Root?.Elements("Cheque")
                                                                             .Skip(1).First()
                                                                             .Element("Amount")?.Value ?? "0"):C2}");
        }
    }
}