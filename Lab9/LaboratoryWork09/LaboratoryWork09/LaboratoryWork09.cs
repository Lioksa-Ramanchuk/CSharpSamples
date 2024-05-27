using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace OOP
{
    using LW09_Game;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    static class LaboratoryWork09
    {
        static void Main()
        {
            using BlockingCollection<Game> games = new();

            object locker = new();

            // Добавление игр
            Task.Run(async () =>
            {
                Game[] gamesToAdd =
                {
                    new("Шашки", "Марта", "Павел"),
                    new("Шахматы", "Алесь", "Виктория"),
                    new("Монополия", "Виктория", "Василий", "Змитер", "Анна")
                };

                foreach (Game game in gamesToAdd)
                {
                    lock (locker)
                    {
                        games.Add(game);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Добавлена игра: {game.Name}\n");
                        Console.ResetColor();
                    }

                    await Task.Delay(1000);
                }

                games.CompleteAdding();
            });

            // Текущие игры
            Task.Run(async () =>
            {
                await Task.Delay(250);
                while (true)
                {
                    lock (locker)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Текущие игры:");
                        foreach (Game game in games)
                        {
                            Console.WriteLine(game);
                        }
                        Console.WriteLine();
                        Console.ResetColor();
                    }

                    await Task.Delay(1000);
                }
            });

            // Поиск игр, в которые играет Виктория
            Task.Run(async () =>
            {
                await Task.Delay(500);
                while (true)
                {
                    lock (locker)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Виктория играет в: {string.Join(", ", games.Where(g => g.Contains("Виктория")).Select(g => g.Name))}\n");
                        Console.ResetColor();
                    }

                    await Task.Delay(1000);
                }
            });

            // Удаление игр
            Task takeAll = Task.Run(async () =>
            {
                await Task.Delay(1750);
                foreach (Game game in games.GetConsumingEnumerable())
                {
                    lock (locker)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Удалена игра: {game.Name}\n");
                        Console.ResetColor();
                    }

                    await Task.Delay(1000);
                }
            });

            takeAll.Wait();

            //===============================================
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();

            using BlockingCollection<int> bcInts = new() { 1, 2, 3, 4, 5 };

            //a
            Console.WriteLine($"bcInts: {string.Join(", ", bcInts)}");
            //b
            int[] takenValues = new int[3];
            bcInts.TryTake(out takenValues[0]);
            bcInts.TryTake(out takenValues[1]);
            bcInts.TryTake(out takenValues[2]);
            Console.WriteLine($"Взятые элементы: {string.Join(", ", takenValues)}");
            Console.WriteLine($"bcInts: {string.Join(", ", bcInts)}");
            //c
            bcInts.Add(11);
            bcInts.TryAdd(22);
            Console.WriteLine($"bcInts: {string.Join(", ", bcInts)}");
            //d
            List<int> listInts = bcInts.ToList();
            //e
            Console.WriteLine($"listInts: {string.Join(", ", listInts)}");
            //f
            Console.WriteLine($"Индекс 22 в listInts: {listInts.IndexOf(22)}\n");

            //===============================================
            Console.WriteLine(new string('=', 40));
            Console.WriteLine();

            ObservableCollection<Game> ocInts = new();
            ocInts.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        {
                            if (e.NewItems?[0] is Game newGame)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Добавлена игра: {newGame.Name}");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        {
                            if (e.OldItems?[0] is Game oldGame)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Удалена игра: {oldGame.Name}");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        {
                            if ((e.NewItems?[0] is Game replacingGame) &&
                                (e.OldItems?[0] is Game replacedGame))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"Игра {replacedGame.Name} заменена на игру {replacingGame.Name}");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Move:
                        {
                            if (e.NewItems?[0] is Game movedGame)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Игра {movedGame.Name} перемещена с позиции {e.OldStartingIndex} на позицию {e.NewStartingIndex}");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Коллекция очищена");
                            Console.ResetColor();
                        }
                        break;
                }
            };

            ocInts.Add(new("Шашки", "Марта", "Павел"));
            ocInts.Add(new("Шахматы", "Алесь", "Виктория"));
            ocInts[0] = new("Монополия", "Виктория", "Василий", "Змитер", "Анна");
            ocInts.Move(1, 0);
            ocInts.RemoveAt(1);
            ocInts.Clear();
        }
    }
}