using System;
using System.Diagnostics;

namespace OOP.LW14_Threads
{
    public static partial class Lab14
    {
        public static void PrintProcesses()
        {
            foreach (Process p in Process.GetProcesses())
            {
                Console.WriteLine($"Имя процесса: {p.ProcessName} (id: {p.Id})");
                Console.Write("Приоритет: ");
                try { Console.WriteLine(p.PriorityClass); } catch (Exception e) { Console.WriteLine(e.Message); }
                Console.Write("Время запуска: ");
                try { Console.WriteLine(p.StartTime); } catch (Exception e) { Console.WriteLine(e.Message); }
                Console.WriteLine($"Текущее состояние: {(p.Responding ? "Running" : "Not Responding")}");
                Console.Write("Времени использовано: ");
                try { Console.WriteLine(p.TotalProcessorTime); } catch (Exception e) { Console.WriteLine(e.Message); }
                Console.WriteLine();
            }
        }
    }
}