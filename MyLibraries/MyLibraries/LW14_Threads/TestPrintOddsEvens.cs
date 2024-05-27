using System;
using System.IO;
using System.Threading;

namespace OOP.LW14_Threads
{
    public static partial class Lab14
    {
        enum PrintNumsFuncMode
        {
            Default,
            OddsFirst,
            Alternate,
        }

        public static void TestPrintOddsEvens()
        {
            Console.WriteLine("Введите n, до которого выводить чётные и нечётные числа:");
            ulong n;
            while (!ulong.TryParse(Console.ReadLine()!, out n))
            {
                Console.WriteLine("Некорректный ввод. Введите ещё раз:");
            }

            Mutex m = new();
            Thread thOdd, thEven;

            using (StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "numbers.txt")))
            {
                Console.WriteLine("\nЧётные и нечётные числа:");
                thOdd = new(() => PrintOdds(n, sw, PrintNumsFuncMode.Default));
                thEven = new(() => PrintEvens(n, sw, PrintNumsFuncMode.Default));
                thOdd.Start();
                thEven.Start();
                thOdd.Join();
                thEven.Join();
            }

            using (StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "numbers_priority.txt")))
            {
                Console.WriteLine("\n\nЧётные и нечётные числа (у потока нечётных приоритет выше):");
                thOdd = new(() => PrintOdds(n, sw, PrintNumsFuncMode.Default)) { Priority = ThreadPriority.Lowest };
                thEven = new(() => PrintEvens(n, sw, PrintNumsFuncMode.Default)) { Priority = ThreadPriority.AboveNormal };
                thOdd.Start();
                thEven.Start();
                thOdd.Join();
                thEven.Join();
            }

            using (StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "numbers_odds_first.txt")))
            {
                Console.WriteLine("\n\nСначала все чётные, затем нечётные числа:");
                thOdd = new(() => PrintOdds(n, sw, PrintNumsFuncMode.OddsFirst));
                thOdd.Start();
                thEven = new(() => PrintEvens(n, sw, PrintNumsFuncMode.OddsFirst));
                thEven.Start();
                thOdd.Join();
                thEven.Join();
            }

            using (StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "numbers_alternate.txt")))
            {
                Console.WriteLine("\n\nЧётные числа чередуются с нечётными:");
                thOdd = new(() => PrintOdds(n, sw, PrintNumsFuncMode.Alternate));
                thOdd.Start();
                thEven = new(() => PrintEvens(n, sw, PrintNumsFuncMode.Alternate));
                thEven.Start();
                thOdd.Join();
                thEven.Join();
            }

            void PrintOdds(ulong n, StreamWriter sw, PrintNumsFuncMode mode)
            {
                switch (mode)
                {
                    case PrintNumsFuncMode.Default:
                        for (ulong i = 0; i <= n; i += 2)
                        {
                            m.WaitOne();
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            m.ReleaseMutex();
                            Thread.Sleep(50);
                        }
                        break;

                    case PrintNumsFuncMode.OddsFirst:
                        m.WaitOne();
                        for (ulong i = 0; i <= n; i += 2)
                        {
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            Thread.Sleep(50);
                        }
                        m.ReleaseMutex();
                        break;

                    case PrintNumsFuncMode.Alternate:
                        for (ulong i = 0; i <= n; i += 2)
                        {
                            m.WaitOne();
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            Thread.Sleep(50);
                            m.ReleaseMutex();
                        }
                        break;
                }
            }
            void PrintEvens(ulong n, StreamWriter sw, PrintNumsFuncMode mode)
            {
                switch (mode)
                {
                    case PrintNumsFuncMode.Default:
                        for (ulong i = 1; i <= n; i += 2)
                        {
                            m.WaitOne();
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            m.ReleaseMutex();
                            Thread.Sleep(150);
                        }
                        break;

                    case PrintNumsFuncMode.OddsFirst:
                        Thread.Sleep(10);
                        m.WaitOne();
                        for (ulong i = 1; i <= n; i += 2)
                        {
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            Thread.Sleep(150);
                        }
                        m.ReleaseMutex();
                        break;

                    case PrintNumsFuncMode.Alternate:
                        Thread.Sleep(10);
                        for (ulong i = 1; i <= n; i += 2)
                        {
                            m.WaitOne();
                            Console.Write($"{i} ");
                            sw.Write($"{i} ");
                            Thread.Sleep(150);
                            m.ReleaseMutex();
                        }
                        break;
                }
            }
        }
    }
}
