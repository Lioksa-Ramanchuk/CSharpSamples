using System;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteUnCheckedTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" CHECKED / UNCHECKED");
            Console.WriteLine("===================================================\n");

            Console.Write("Unchecked: ");
            try
            {
                Console.WriteLine($"int.MaxValue + 1 = {GetMaxIntPlus1Unchecked()}");
            }
            catch (OverflowException err)
            {
                Console.WriteLine(err.Message);
            }

            Console.Write("Checked: ");
            try
            {
                Console.WriteLine($"int.MaxValue + 1 = {GetMaxIntPlus1Checked()}");
            }
            catch (OverflowException err)
            {
                Console.WriteLine(err.Message);
            }

            Console.ReadLine();

            static int GetMaxIntPlus1Checked()
            {
                int result = int.MaxValue;
                checked { result++; }

                return result;
            }

            static int GetMaxIntPlus1Unchecked()
            {
                int result = int.MaxValue;
                unchecked { result++; }

                return result;
            }
        }
    }
}