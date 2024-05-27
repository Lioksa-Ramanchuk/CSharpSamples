using System;

namespace OOP
{
    using LW14_Threads;

    static class LaboratoryWork14
    {
        static void Main()
        {
            // 1
            Lab14.PrintProcesses();

            // 2
            Console.WriteLine($"{new string('=', 40)}\n");

            Lab14.PrintCurrentDomain();
            //AppDomain newDomain = AppDomain.CreateDomain("New Domain");
            //newDomain.Load("LW04_Documents");
            //AppDomain.Unload(newDomain);

            // 3
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Lab14.TestCalcPrimes();
            
            // 4
            Console.WriteLine($"\n\n{new string('=', 40)}\n");

            Lab14.TestPrintOddsEvens();
            Console.WriteLine();
        }
    }
}