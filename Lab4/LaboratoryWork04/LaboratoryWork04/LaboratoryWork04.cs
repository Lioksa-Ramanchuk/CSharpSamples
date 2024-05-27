using System;

namespace OOP
{
    using LW04_Documents;

    static class LaboratoryWork04
    {
        static void Main()
        {
            Organization preply = new("Doreply", "г. Минск, ул. Стародорожская, 7", "Платформа для поиска репетиторов");
            Date pr1Date = new(9, 10, 2022);
            PaymentReceipt pr1 = new(preply, "Дмитрий Тарасевич", "15-часовой курс по эсперанто", 30.45m, pr1Date, 284891);

            Product tableSet = new("Раздвижной стол", 800.50m, 20);
            Product chairSet = new("Деревянный стул", 158.50m, 40);
            Product dresserSet = new("Комод", 250.10m, 2);
            PackingList pl1 = new("IEKA", "GoodOrg Inc.", new Product[] { tableSet, chairSet, dresserSet }, new(10, 4, 2022), 1276273);
            pl1.Sign();

            Cheque ch1 = new("Алина Климук", "Дарья Тышкевич", "Банк Приколофф", 1000.0m, new(2, 5, 2022), 1939318);
            ch1.Sign();

            Printer printer = new();

            Document doc1 = ch1;
            if (doc1 is Cheque)
            {
                Console.Write("Чек ");
            }
            Console.WriteLine(nameof(doc1));
            printer.IAmPrinting(doc1);

            BaseClone bc1 = ch1;
            Document? doc2 = bc1.DoClone() as Document;
            if (doc2 is not null)
            {
                if (doc2 is Cheque)
                {
                    Console.Write("Чек ");
                }
                Console.WriteLine(nameof(doc2));
                printer.IAmPrinting(doc2);
            }

            IClonable idoc1 = ch1;
            Document? doc3 = idoc1.DoClone() as Document;
            if (doc3 is not null)
            {
                if (doc3 is Cheque)
                {
                    Console.Write("Чек ");
                }
                Console.WriteLine(nameof(doc3));
                printer.IAmPrinting(doc3);
            }

            Array.ForEach(new Document[] { pr1, pl1, ch1 }, printer.IAmPrinting);
        }
    }
}