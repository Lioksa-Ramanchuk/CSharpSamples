using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace OOP
{
    using LW04_Documents;
    using LW11_Reflector;

    static class NumberPrinter
    {
        public static void PrintNumber(int i) => Console.WriteLine(i);
    }

    static class LaboratoryWork11
    {
        static void Main()
        {
            static void PrintClassInfo(string className, string filePath, string paramTypeName)
            {
                Reflector.FilePath = filePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Console.WriteLine($"{className}:");
                Console.WriteLine($"Имя сборки: {Reflector.GetAssemblyName(className)}");
                Console.WriteLine($"Публичные конструкторы: {Reflector.HasPublicConstructors(className) switch
                {
                    true => "есть",
                    false => "нет",
                    _ => "?"
                }}");
                Console.WriteLine($"Публичные методы: {string.Join(", ", Reflector.GetPublicMethods(className)?.Select(m => m.Name) ?? new string[] { "?" })}");
                Console.WriteLine($"Поля и свойства: {string.Join(", ", Reflector.GetAllFieldsAndProperties(className)?.Select(m => m.Name) ?? new string[] { "?" })}");
                Console.WriteLine($"Интерфейсы: {string.Join(", ", Reflector.GetInterfaces(className)?.Select(t => t.Name) ?? new string[] { "?" })}");
                Reflector.PrintMethodNamesWithParamType(className, paramTypeName);
            }

            PrintClassInfo("OOP.LW04_Documents.Date, LW04_Documents", Path.Combine("D:", "Temp", "date.txt"), "System.UInt32");
            Console.WriteLine();
            PrintClassInfo("OOP.LW04_Documents.Organization, LW04_Documents", Path.Combine("D:", "Temp", "organization.txt"), "System.String");
            Console.WriteLine();

            PrintClassInfo("System.Boolean", Path.Combine("D:", "Temp", "bool.txt"), "System.Boolean");
            Console.WriteLine();
            PrintClassInfo("System.Object", Path.Combine("D:", "Temp", "object.txt"), "System.Object");
            Console.WriteLine();

            using (StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "params.xml")))
            {
                XmlSerializer serializer = (Reflector.Create(typeof(XmlSerializer), new object[] { typeof(Parameters) }) as XmlSerializer)!;
                Reflector.Invoke(serializer, "Serialize", new object[] { sw, new Parameters(new object[] { (byte)10, (byte)11, (uint)2022 }) });
            }

            Date d1 = (Reflector.Create(typeof(Date)!, (byte)18, (byte)11, (uint)2022) as Date)!;
            Date d2 = (Reflector.Create(typeof(Date)!, (byte)18, (byte)11, (uint)2022) as Date)!;

            Reflector.Invoke(Console.Out, "WriteLine", new object[] { $"{d1} == {d2}: {d1 == d2}" });
            Reflector.Invoke(d1, "Set", Path.Combine("D:", "Temp", "params.xml"));
            Reflector.Invoke(Console.Out, "WriteLine", new object[] { $"{d1} == {d2}: {d1 == d2}" });
            Reflector.Invoke(d1, "Set", new object[] { (byte)18, (byte)11, (uint)2022 });
            Reflector.Invoke(Console.Out, "WriteLine", new object[] { $"{d1} == {d2}: {d1 == d2}" });
            Reflector.Invoke(Console.Out, "Write", new object[] { $"Значение по умолчанию для int: " });
            Reflector.Invoke(typeof(NumberPrinter), "PrintNumber");
            
            Reflector.Invoke(
                typeof(Reflector),
                "Invoke",
                new object?[] {
                    typeof(Reflector),
                    "Invoke",
                    new object?[] {
                        Console.Out,
                        "WriteLine",
                        new object?[] { "Рекурсия" }
                    }
                });
        }
    }
}