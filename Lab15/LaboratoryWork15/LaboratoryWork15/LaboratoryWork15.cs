using System.Diagnostics;
using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace OOP
{
    static class LaboratoryWork15
    {
        static async Task Main()
        {
            CancellationTokenSource cancelTokenSource = new();
            CancellationToken token = cancelTokenSource.Token;

            // 1
            void TaskFunc(int taskId)
            {
                Stopwatch sw = Stopwatch.StartNew();
                Random rand = new();
                int[] vector = Enumerable.Repeat(0, 10_000_000)
                                         .Select(i => rand.Next(int.MaxValue))
                                         .ToArray();
                for (int i = 0; i < vector.Length; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine($"Задача {taskId} прервана. Итерация: {i}");
                        return;
                    }
                    vector[i] *= rand.Next();
                }
                sw.Stop();
                Console.WriteLine($"Задача {taskId} выполнилась за {sw.ElapsedMilliseconds} мс");
            }
            Task longTask = Task.Run(() => TaskFunc(Task.CurrentId ?? 0));

            Console.WriteLine($"ID задачи: {longTask.Id}");
            Console.WriteLine($"Задача {(longTask.IsCompleted ? "" : "не ")}завершена");
            Console.WriteLine($"Статус задачи: {longTask.Status}");
            longTask.Wait();

            Console.WriteLine("\nПараллельное выполнение:");
            Stopwatch sw = Stopwatch.StartNew();
            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => TaskFunc(Task.CurrentId ?? 0));
            }
            Task.WaitAll(tasks);
            sw.Stop();
            Console.WriteLine($"Времени прошло: {sw.ElapsedMilliseconds} мс");

            Console.WriteLine("\nПоследовательное выполнение:");
            sw.Restart();
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new(() => TaskFunc(Task.CurrentId ?? 0));
                tasks[i].RunSynchronously();
            }
            sw.Stop();
            Console.WriteLine($"Времени прошло: {sw.ElapsedMilliseconds} мс");

            // 2
            Console.WriteLine();
            longTask = Task.Run(() => TaskFunc(Task.CurrentId ?? 0));
            Thread.Sleep(200);
            cancelTokenSource.Cancel();
            longTask.Wait();

            // 3
            Console.WriteLine($"\n{new string('=', 40)}\n");

            var calcCurrent = Task.Run(async () => { await Task.Delay(100); return 20; });
            var calcResistance = Task.Run(async () => { await Task.Delay(150); return 5; });
            var calcTime = Task.Run(async () => { await Task.Delay(200); return 10; });

            var calcHeat = Task.Run(() => calcCurrent.Result * calcCurrent.Result * calcResistance.Result * calcTime.Result);
            Console.WriteLine($"Теплота, выделяющаяся на проводнике с R = {calcResistance.Result} Ом," +
                              $" по которому t = {calcTime.Result} с проходит ток I = {calcCurrent.Result} А: {calcHeat.Result} Дж");

            // 4
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Console.Write("ContinueWith | Десять чисел, начиная с 20:");
            Task printNums1 = Task.Run(() => { });
            for (int i = 0; i < 10; i++)
            {
                int numToAdd = i;
                printNums1 = printNums1.ContinueWith(t => Console.Write($" {20 + numToAdd}"));
            }
            printNums1.Wait();
            Console.WriteLine();

            Console.Write("GetAwaiter   | Десять чисел, начиная с 20:");
            bool printed = false;
            var printNums2 = Task.Run(() => { return Enumerable.Range(20, 10); });
            var awaiter = printNums2.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Array.ForEach(awaiter.GetResult().ToArray(), i => Console.Write($" {i}"));
                Console.WriteLine();
                printed = true;
            });
            while (!printed)
            {
                await Task.Delay(100);
            }

            // 5
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Random rand = new();

            int[] fibNumNumbers = Enumerable.Repeat(0, 50).Select(l => rand.Next(1, 10)).ToArray();
            Console.WriteLine("Массив номеров чисел Фибоначчи:");
            Array.ForEach(fibNumNumbers, i => Console.Write($" {i,3}"));
            Console.WriteLine();

            long[] fibNums1 = new long[fibNumNumbers.Length];
            Console.WriteLine("Числа Фибоначчи (Parallel.For):");
            sw.Restart();
            Parallel.For(0, fibNumNumbers.Length, i =>
            {
                long n1 = 1;
                long n2 = 1;
                long n3 = 2;
                for (int j = 1; j < fibNumNumbers[i]; j++)
                {
                    (n1, n2, n3) = (n2, n3, n2 + n3);
                }
                fibNums1[i] = n1;
            });
            sw.Stop();
            Array.ForEach(fibNums1, i => Console.Write($" {i,3}"));
            Console.WriteLine($" (время: {sw.ElapsedMilliseconds} мс)");

            long[] fibNums2 = new long[fibNumNumbers.Length];
            Console.WriteLine("Числа Фибоначчи (Parallel.ForEach):");
            sw.Restart();
            Parallel.ForEach(fibNumNumbers, (fibNumNumber, _, i) =>
            {
                long n1 = 1;
                long n2 = 1;
                long n3 = 2;
                for (int j = 1; j < fibNumNumber; j++)
                {
                    (n1, n2, n3) = (n2, n3, n2 + n3);
                }
                fibNums2[i] = n1;
            });
            sw.Stop();
            Array.ForEach(fibNums2, i => Console.Write($" {i,3}"));
            Console.WriteLine($" (время: {sw.ElapsedMilliseconds} мс)");

            long[] fibNums3 = new long[fibNumNumbers.Length];
            Console.WriteLine("Числа Фибоначчи (for):");
            sw.Restart();
            for (int i = 0; i < fibNumNumbers.Length; i++)
            {
                long n1 = 1;
                long n2 = 1;
                long n3 = 2;
                for (int j = 1; j < fibNumNumbers[i]; j++)
                {
                    (n1, n2, n3) = (n2, n3, n2 + n3);
                }
                fibNums3[i] = n1;
            }
            sw.Stop();
            Array.ForEach(fibNums3, i => Console.Write($" {i,3}"));
            Console.WriteLine($" (время: {sw.ElapsedMilliseconds} мс)");

            long[] fibNums4 = new long[fibNumNumbers.Length];
            Console.WriteLine("Числа Фибоначчи (foreach):");
            sw.Restart();
            int fibNums4Index = 0;
            foreach (int fibNumNumber in fibNumNumbers)
            {
                long n1 = 1;
                long n2 = 1;
                long n3 = 2;
                for (int j = 1; j < fibNumNumber; j++)
                {
                    (n1, n2, n3) = (n2, n3, n2 + n3);
                }
                fibNums4[fibNums4Index++] = n1;
            }
            sw.Stop();
            Array.ForEach(fibNums4, i => Console.Write($" {i,3}"));
            Console.WriteLine($" (время: {sw.ElapsedMilliseconds} мс)");

            // 6
            Console.WriteLine($"\n{new string('=', 40)}\n");

            Console.Write("Числа от 1 до 7 в случайном порядке:");
            List<Action> parallelInvokeMethods = new();
            for (int i = 0; i < 7; i++)
            {
                int num = i + 1;
                parallelInvokeMethods.Add(() => Console.Write($" {num}"));
            }
            Parallel.Invoke(parallelInvokeMethods.ToArray());
            Console.WriteLine();

            // 7
            Console.WriteLine($"\n{new string('=', 40)}\n");

            BlockingCollection<string> warehouse = new();
            string[] itemNames = { "холодильник", "пылесос", "плита", "миксер", "тостер" };
            Task[] producers = new Task[5];
            Task[] consumers = new Task[10];
            Mutex m = new();
            for (int i = 0; i < itemNames.Length; i++)
            {
                int iProducer = i;
                producers[i] = Task.Run(async () =>
                {
                    await Task.Delay(rand.Next(100, 5000));
                    m.WaitOne();
                    warehouse.Add(itemNames[iProducer]);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Поставщик {iProducer + 1} завёз товар {itemNames[iProducer]}");
                    Console.ResetColor();
                    Console.WriteLine($"Товары на складе: {string.Join(", ", warehouse)}");
                    m.ReleaseMutex();
                });
            }
            for (int i = 0; i < 10; i++)
            {
                int iConsumer = i;
                consumers[i] = Task.Run(async () =>
                {
                    await Task.Delay(rand.Next(100, 5000));
                    m.WaitOne();
                    if (warehouse.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"Потребитель {iConsumer + 1} купил товары: {warehouse.Take()}");
                        while (warehouse.TryTake(out string? item))
                        {
                            Console.Write($", {item}");
                        }
                        Console.ResetColor();
                        Console.WriteLine("\nСклад пуст.");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Потребитель {iConsumer + 1} ничего не купил.");
                        Console.ResetColor();
                    }
                    m.ReleaseMutex();
                });
            }
            Task.WaitAll(producers.Concat(consumers).ToArray());

            // 8
            Console.WriteLine($"\n{new string('=', 40)}\n");

            async Task printHello()
            {
                await Task.Delay(1000);
                Console.Write("Hello, ");
            }
            async Task printWorld()
            {
                await Task.Delay(500);
                Console.WriteLine("World!");
            }
            await printHello();
            await printWorld();
        }
    }
}