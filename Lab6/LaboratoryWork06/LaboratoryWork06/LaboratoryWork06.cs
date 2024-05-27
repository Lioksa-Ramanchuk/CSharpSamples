using System;
using System.IO;

namespace OOP
{
    using LW04_Documents;
    using LW05_AccountingDepartment;
    using LW06_AccountingDepartmentExceptions;

    static class LaboratoryWork06
    {
        static void Main()
        {
            Cheque ch = new("drawer", "payee", "drawee", 300m, new(21, 10, 2022), 1234567);
            ch.Sign();

            AccountingDepartment accDept = new(ch);
            accDept.SaveToFile("accDept.txt");

            static void ShowException(Exception e)
            {
                Console.WriteLine($"Тип исключения: {e.GetType().Name}");
                Console.WriteLine($"Сообщение: {e.Message}");
                Console.WriteLine($"Сборка: {e.Source}");
                Console.WriteLine($"Метод: {e.TargetSite?.Name}");
                Console.WriteLine($"Стек:\n{e.StackTrace}");
            }

            using FileLogger fl = new();
            accDept.Logger = fl;
            accDept.Logger.WriteLine(Logger.RecordType.INFO, "Started logging...");
            accDept.Logger.WriteLine(Logger.RecordType.WARNING, "Testing warning...");

            using ConsoleLogger cl = new();
            accDept.Logger = cl;
            accDept.Logger.WriteLine(Logger.RecordType.INFO, "Started logging...");
            accDept.Logger.WriteLine(Logger.RecordType.WARNING, "Testing warning...");

            FileStream? fs = null;

            for (int testNumber = 0; testNumber < 5; testNumber++)
            {
                try
                {
                    if (testNumber == 2)
                    {
                        fs = File.OpenWrite("accDept.txt");
                        fs.Position = 0;
                        fs.WriteByte(0);
                        accDept.Logger = fl;
                    }

                    try
                    {
                        Console.WriteLine($"\nТест №{testNumber + 1}");

                        switch (testNumber)
                        {
                            case 0:
                                Archive.ShowDocuments(accDept.GetDocumentsForTimePeriod(new(21, 10, 2022), new(20, 10, 2022)));
                                break;
                            case 1:
                                accDept.GetFromFile("wrong path");
                                break;
                            case 2:
                                accDept.GetFromFile("accDept.txt");
                                break;
                            case 3:
                                accDept.GetFromFile("accDept.txt");
                                break;
                            case 4:
                                accDept.Archive.Add(null!);
                                break;
                        }
                    }
                    catch (InvalidArgumentsException e)
                    {
                        Console.WriteLine("Ошибка при получении документов за период времени.");
                        accDept.Logger?.WriteLine(Logger.RecordType.ERROR, e.Message);
                        ShowException(e);
                    }
                    catch (FileOpeningException e)
                    {
                        Console.WriteLine($"Ошибка при открытии файла.");
                        accDept.Logger?.WriteLine(Logger.RecordType.ERROR, e.Message);
                        ShowException(e);
                    }
                    catch (DeserializationException e)
                    {
                        Console.WriteLine($"Ошибка при десериализации бухгалтерии.");
                        accDept.Logger?.WriteLine(Logger.RecordType.ERROR, e.Message);
                        ShowException(e);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Ошибка:");
                        throw;
                    }
                }
                catch (Exception e)
                {
                    accDept.Logger?.WriteLine(Logger.RecordType.ERROR, e.Message);
                    ShowException(e);
                }
                finally
                {
                    fs?.Close();
                    fs = null;
                }
            }
        }
    }
}