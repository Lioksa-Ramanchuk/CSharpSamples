using System;

namespace OOP
{
    using LW04_Documents;
    using LW05_AccountingDepartment;

    static class LaboratoryWork05
    {
        static void Main()
        {
            AccountingDepartment accDept = new();

            Cheque ch1 = new("Стефания Латыпова", "Павлина Погосская", "Белаколбанк", 1000.0m, new(27, 7, 2022), 9712262);
            ch1 = ch1.DoClone();
            ch1.Sign();

            Product tableSet1 = new("Раздвижной стол", 800.50m, 20);
            Product chairSet1 = new("Деревянный стул", 158.50m, 40);
            Product bedSet1 = new("Комод", 250.10m, 2);
            PackingList pl1 = new("IEKA", "GoodOrg Inc.", new Product[] { tableSet1, chairSet1, bedSet1 }, new(15, 7, 2022), 7264571);
            pl1.Sign();

            Cheque ch2 = new("Ян Оробейко", "Константин Будько", "Банк М", 2300m, new(8, 9, 2022), 651234);
            ch2.Sign();

            Product chairSet2 = new("Деревянный стул", 158.50m, 15);
            Product microwaveSet2 = new("Микроволновая печь", 1200m, 1);
            Product fridgeSet2 = new("Холодильник", 1300m, 2);
            PackingList pl2 = new("ООО \"Уютно.\"", "ООО \"Понятно\"", new Product[] { chairSet2, microwaveSet2, fridgeSet2 }, new(25, 3, 2022), 1039004);
            pl2.Sign();

            Cheque ch3 = new("Дмитрий Каленик", "Виктория Канецкая", "Банк М", 23034m, new(25, 8, 2022), 9867053);

            accDept.Archive.Add(ch1, pl1, ch2, pl2, ch3);

            accDept.Archive.Show();

            // ========================================
            Console.WriteLine($"{new string('=', 40)}\n");

            Console.Write("Суммарная стоимость товара \"Деревянный стул\" по всем накладным: ");
            Console.WriteLine($"{accDept.GetProductCost("Деревянный стул"):C2}");

            // ========================================
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Console.WriteLine($"Количество чеков в бухгалтерии: {accDept.GetNumberOfCheques()}");

            // ========================================
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Console.WriteLine("Список документов с 01.07.2022 по 31.12.2022:\n");
            Archive.ShowDocuments(
                accDept.GetDocumentsForTimePeriod(new Date(1, 7, 2022), new Date(31, 12, 2022))
            );

            // ========================================
            Console.WriteLine($"{new string('=', 40)}\n");

            accDept.SaveToFile("AccDept.txt");
            accDept.Archive.Clear();
            accDept.GetFromFile("AccDept.txt");
            accDept.Archive.Show();

            // ========================================
            Console.WriteLine($"{new string('=', 40)}\n");

            accDept.SaveToJson("AccDept.json");
            accDept.Archive.Clear();
            accDept.GetFromJson("AccDept.json");
            accDept.Archive.Show();
        }
    }
}