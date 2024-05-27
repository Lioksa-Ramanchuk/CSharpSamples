using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace OOP.LW05_AccountingDepartment
{
    using LW04_Documents;
    using LW06_AccountingDepartmentExceptions;

    public class AccountingDepartment
    {
        [JsonConstructor]
        public AccountingDepartment(Archive archive)
        {
            Archive = archive;
        }

        public AccountingDepartment(params Document[] documents)
        {
            Archive = new Archive(documents);
        }

        public Archive Archive { get; private set; }
        public Logger? Logger { get; set; } = null;

        public decimal GetProductCost(string productName)
        {
            return Archive.Where(doc => doc is PackingList)
                          .Select(pl => (pl as PackingList)!.Products.Where(p => p.Name.Equals(productName))
                                                                     .Select(p => p.Cost)
                                                                     .Sum())
                          .Sum();
        }
        public int GetNumberOfCheques()
        {
            return Archive.OfType<Cheque>().Count();
        }
        public Document[] GetDocumentsForTimePeriod(Date start, Date end)
        {
            if (start > end)
            {
                throw new InvalidArgumentsException($"Промежуток времени задан некорректно: {nameof(start)} > {nameof(end)}");
            }

            return Archive.Where(doc => (start <= doc.Date) && (doc.Date <= end))
                          .ToArray();
        }

        public void SaveToFile(string path)
        {
            StreamWriter sw;
            try
            {
                sw = File.CreateText(path);
            }
            catch (Exception e)
            {
                throw new FileOpeningException(e.Message);
            }

            foreach (Document doc in Archive)
            {
                doc.SaveToFile(sw);
            }
            sw.Close();
        }
        public void GetFromFile(string path)
        {
            Archive newArchive = new();
            StreamReader sr;
            try
            {
                sr = File.OpenText(path);
            }
            catch (Exception e)
            {
                throw new FileOpeningException(e.Message);
            }

            try
            {
                while (!sr.EndOfStream)
                {
                    ((System.Collections.Generic.ICollection<Document>)newArchive).Add(sr.ReadLine() switch
                    {
                        "PaymentReceipt" => MyDeSerialization.GetFromFile(DocumentType.PaymentReceipt, sr) ??
                                            throw new DeserializationException($"Не удалось десериализовать квитанцию (путь к файлу: {path})."),
                        "PackingList" => MyDeSerialization.GetFromFile(DocumentType.PackingList, sr) ??
                                         throw new DeserializationException($"Не получилось десериализовать накладную (путь к файлу: {path})."),
                        "Cheque" => MyDeSerialization.GetFromFile(DocumentType.Cheque, sr) ??
                                    throw new DeserializationException($"Не получилось десериализовать чек (путь к файлу: {path})."),
                        _ => throw new DeserializationException($"Ожидалось \"PaymentReceipt\", \"PackingList\" или \"Cheque\" (путь к файлу: {path}).")
                    });
                }
            }
            finally
            {
                sr.Close();
            }

            Archive = newArchive;
        }
        public void SaveToJson(string path)
        {
            StreamWriter sw;
            try
            {
                sw = File.CreateText(path);
            }
            catch (Exception e)
            {
                throw new FileOpeningException(e.Message);
            }

            sw.WriteLine(JsonConvert.SerializeObject(this,
                                                     new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented }));
            sw.Close();
        }
        public void GetFromJson(string path)
        {
            StreamReader sr;
            try
            {
                sr = File.OpenText(path);
            }
            catch (Exception e)
            {
                throw new FileOpeningException(e.Message);
            }

            try
            {
                AccountingDepartment newAD = JsonConvert.DeserializeObject<AccountingDepartment>
                    (
                        sr.ReadToEnd(),
                        new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented }
                    ) ??
                    throw new DeserializationException($"Не удалось десериализовать бухгалтерию (путь к файлу: {path}).");

                (Archive, Logger) = (newAD.Archive, newAD.Logger);
            }
            catch (DeserializationException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DeserializationException(e.Message);
            }
            finally
            {
                sr.Close();
            }
        }
    }
}