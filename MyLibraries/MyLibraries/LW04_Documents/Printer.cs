using System;

namespace OOP.LW04_Documents
{
    public class Printer
    {
        public void IAmPrinting(Document doc)
        {
            Console.WriteLine($"IAmPrinting: тип объекта - {doc.GetType().FullName}");
            Console.WriteLine(doc);
        }
    }
}