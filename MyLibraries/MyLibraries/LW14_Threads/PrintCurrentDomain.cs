using System;
using System.Linq;

namespace OOP.LW14_Threads
{
    public static partial class Lab14
    {
        public static void PrintCurrentDomain()
        {
            Console.WriteLine($"Текущий домен: {AppDomain.CurrentDomain.FriendlyName} (id: {AppDomain.CurrentDomain.Id})");
            Console.WriteLine($"Базовая директория приложения: {AppDomain.CurrentDomain.SetupInformation.ApplicationBase}");
            Console.WriteLine($"Целевая платформа: {AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName}");
            Console.WriteLine($"Сборки: {string.Join(", ", AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetName().Name))}");
        }
    }
}