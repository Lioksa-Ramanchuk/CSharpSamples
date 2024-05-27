using System;
using System.Linq;

namespace OOP
{
    using LW03_List;
    using LW04_Documents;
    using LW07_IInteractive;

    static class LaboratoryWork07
    {
        static void Main()
        {
            List<int> iList = new(1, 2, 3);
            Console.WriteLine($"iList: {iList}");
            iList.Serialize("iList.json");
            IInteractive<int> InterI = iList;
            InterI.Add(4);
            InterI.Delete(2);
            iList = new(InterI.Where(i => i > 1).ToArray());
            Console.WriteLine($"(+4) (-2) (>1): {iList}");
            iList.Deserialize("iList.json");
            Console.WriteLine($"Десериализация: {iList}\n");

            List<double> dList = new(1.1, 2.2, 3.3);
            Console.WriteLine($"dList: {dList}");
            dList.Serialize("dList.json");
            IInteractive<double> InterD = dList;
            InterD.Add(4.4);
            InterD.Delete(2.2);
            dList = new(InterD.Where(d => d > 1.5).ToArray());
            Console.WriteLine($"(+4.4) (-2.2) (>1.5): {dList}");
            dList.Deserialize("dList.json");
            Console.WriteLine($"Десериализация: {dList}\n");

            List<string> sList = new("s1", "s2", "s3");
            Console.WriteLine($"sList: {sList}");
            sList.Serialize("sList.json");
            IInteractive<string> InterS = sList;
            InterS.Add("s4");
            InterS.Delete("s2");
            sList = new(InterS.Where(s => s.CompareTo("s1") > 0).ToArray());
            Console.WriteLine($"(+s4) (-s2) (>s1): {sList}");
            sList.Deserialize("sList.json");
            Console.WriteLine($"Десериализация: {sList}\n");

            List<Cheque> chqList = new(new Cheque("dr1", "p1", "de1", 100m, new(), 1),
                                       new Cheque("dr2", "p2", "de2", 200m, new(), 2),
                                       new Cheque("dr3", "p3", "de3", 300m, new(), 3));
            Console.WriteLine($"chqList:(№) [{string.Join(", ", chqList.Select(chq => chq.Number))}]");
            chqList.Serialize("chqList.json");
            IInteractive<Cheque> InterChq = chqList;
            InterChq.Add(new Cheque("dr4", "p4", "de4", 400m, new(), 4));
            InterChq.Delete(new Cheque("dr2", "p2", "de2", 200m, new(), 2));
            chqList = new(InterChq.Where(chq => chq.Number > 1).ToArray());
            Console.WriteLine($"(+№4) (-№2) (№>1):(№) [{string.Join(", ", chqList.Select(chq => chq.Number))}]");
            chqList.Deserialize("chqList.json");
            Console.WriteLine($"Десериализация:(№) [{string.Join(", ", chqList.Select(chq => chq.Number))}]\n");
        }
    }
}