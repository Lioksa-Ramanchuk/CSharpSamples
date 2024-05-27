using System;
using System.IO;
using System.Threading;

namespace OOP.LW14_Threads
{
    public static partial class Lab14
    {
        public static void TestCalcPrimes()
        {
            Console.WriteLine("Введите число, до которого вывести простые числа:");
            ulong n;
            while (!ulong.TryParse(Console.ReadLine()!, out n))
            {
                Console.WriteLine("Некорректный ввод. Введите ещё раз:");
            }

            AutoResetEvent waitHandler = new(true);
            Thread thCurrent = Thread.CurrentThread;

            PrintThreadState(thCurrent);

            Thread thCalcPrimes = new(() => CalcPrimes(n));
            thCalcPrimes.Start();
            Thread.Sleep(2000);

            waitHandler.WaitOne();
            Thread.Sleep(2000);
            waitHandler.Set();

            thCalcPrimes.Join();

            static void PrintThreadState(object? oThread)
            {
                Thread th = (oThread as Thread)!;
                Console.WriteLine($"""


                    Поток: {th.Name} (id: {th.ManagedThreadId})
                    Статус: {th.ThreadState}
                    Приоритет: {th.Priority}

                    """);
            }
            void CalcPrimes(ulong n)
            {
                using Timer t = new(new TimerCallback(PrintThreadState), Thread.CurrentThread, 0, 1000);

                using StreamWriter sw = File.CreateText(Path.Combine("D:", "Temp", "primes.txt"));
                Console.WriteLine($"Простые числа от 1 до {n}:");
                for (ulong i = 2; i <= n; i++)
                {
                    waitHandler.WaitOne();
                    waitHandler.Set();

                    if (IsPrime(i))
                    {
                        Console.Write($"{i} ");
                        sw.Write($"{i} ");
                        Thread.SpinWait(1000000);
                    }
                }

                static bool IsPrime(ulong n)
                {
                    if (n <= 1) return false;
                    if (n == 2 || n == 3 || n == 5) return true;
                    if (n % 2 == 0 || n % 3 == 0 || n % 5 == 0) return false;

                    ulong bound = (ulong)Math.Floor(Math.Sqrt(n));
                    ulong i = 6;
                    while (i <= bound)
                    {
                        if (n % (i + 1) == 0 || n % (i + 5) == 0) return false;
                        i += 6;
                    }
                    return true;
                }
            }
        }
    }
}
