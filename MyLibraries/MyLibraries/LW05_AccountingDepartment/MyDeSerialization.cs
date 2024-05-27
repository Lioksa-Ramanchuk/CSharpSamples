using System;
using System.Collections.Generic;
using System.IO;

namespace OOP.LW05_AccountingDepartment
{
    using LW04_Documents;

    public static class MyDeSerialization
    {
        public static void SaveStringToFile(this string? str, StreamWriter sw)
        {
            if (str is null)
            {
                sw.WriteLine();
            }
            else
            {
                sw.Write("!");
                string[] strParts = str.Split('\n');
                foreach (string strPart in strParts)
                {
                    sw.WriteLine($">{strPart}");
                }
            }
        }
        public static string? GetStringFromFile(StreamReader sr)
        {
            string? strPart = sr.ReadLine();
            if (string.IsNullOrEmpty(strPart))
            {
                return null;
            }

            List<string> strParts = new() { strPart[2..] };

            while (sr.Peek() == '>')
            {
                strParts.Add(sr.ReadLine()![1..]);
            }

            return string.Join('\n', strParts);
        }

        public static bool SaveToFile(this Document doc, StreamWriter sw)
        {
            sw.WriteLine(doc.GetType().Name);

            switch (doc)
            {
                case PaymentReceipt pr:
                    {
                        pr.Organization.Name.SaveStringToFile(sw);
                        pr.Organization.Address.SaveStringToFile(sw);
                        pr.Organization.Description.SaveStringToFile(sw);
                        pr.Payer.SaveStringToFile(sw);
                        pr.Service.SaveStringToFile(sw);
                        sw.WriteLine(pr.Cost);
                        break;
                    }
                case PackingList pl:
                    {
                        pl.Vendor.SaveStringToFile(sw);
                        pl.Customer.SaveStringToFile(sw);
                        foreach (Product p in pl.Products)
                        {
                            p.Name.SaveStringToFile(sw);
                            sw.WriteLine(p.Price);
                            sw.WriteLine(p.Amount);
                        }
                        sw.WriteLine('<');
                        break;
                    }
                case Cheque ch:
                    {
                        ch.Drawer.SaveStringToFile(sw);
                        ch.Payee.SaveStringToFile(sw);
                        ch.Drawee.SaveStringToFile(sw);
                        sw.WriteLine(ch.Amount);
                        break;
                    }
                default:
                    return false;
            }

            sw.WriteLine($"{doc.Date.Day:D2}{doc.Date.Month:D2}{doc.Date.Year}");
            sw.WriteLine(doc.Number);
            sw.WriteLine(doc.IsOriginal);
            sw.WriteLine(doc.IsSigned);

            return true;
        }
        public static Document? GetFromFile(DocumentType docType, StreamReader sr)
        {
            Document doc;

            switch (docType)
            {
                case DocumentType.PaymentReceipt:
                    {
                        doc = new PaymentReceipt(
                            new Organization(GetStringFromFile(sr)!, GetStringFromFile(sr), GetStringFromFile(sr)),
                            GetStringFromFile(sr)!,
                            GetStringFromFile(sr)!,
                            Convert.ToDecimal(sr.ReadLine()),
                            new Date(Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToUInt32(sr.ReadLine())),
                            Convert.ToUInt32(sr.ReadLine())
                        );
                        break;
                    }
                case DocumentType.PackingList:
                    {
                        string vendor = GetStringFromFile(sr)!;
                        string customer = GetStringFromFile(sr)!;
                        List<Product> products = new();
                        while (sr.Peek() != '<')
                        {
                            products.Add(new Product(
                                GetStringFromFile(sr)!,
                                Convert.ToDecimal(sr.ReadLine()),
                                Convert.ToUInt32(sr.ReadLine())
                            ));
                        }
                        sr.ReadLine();

                        doc = new PackingList(
                            vendor,
                            customer,
                            products.ToArray(),
                            new Date(Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToUInt32(sr.ReadLine())),
                            Convert.ToUInt32(sr.ReadLine())
                        );
                        break;
                    }
                case DocumentType.Cheque:
                    {
                        doc = new Cheque(
                            GetStringFromFile(sr)!,
                            GetStringFromFile(sr)!,
                            GetStringFromFile(sr)!,
                            Convert.ToDecimal(sr.ReadLine()),
                            new Date(Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToByte(sr.Read() + sr.Read()),
                                     Convert.ToUInt32(sr.ReadLine())),
                            Convert.ToUInt32(sr.ReadLine())
                        );
                        break;
                    }
                default:
                    return null;
            }

            if (!Convert.ToBoolean(sr.ReadLine()))
            {
                doc = (Document)doc.DoClone();
            }

            if (Convert.ToBoolean(sr.ReadLine()))
            {
                doc.Sign();
            }

            return doc;
        }
    }
}
