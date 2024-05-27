#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.IO;

namespace OOP.LW12_FileHandling
{
    public static class RAMDirInfo
    {
        public static bool PrintFilesNumber(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Директории {dir.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Количество файлов в директории {dir.Name}: {dir.GetFiles().Length}");
            RAMLog.WriteLine($"Вывод информации о директории: количество файлов. Директория: {dir.Name} Путь: {dir.FullName}");
            return true;
        }

        public static bool PrintCreationTime(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Директории {dir.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Время создания директории {dir.Name}: {dir.CreationTime:t}");
            RAMLog.WriteLine($"Вывод информации о директории: время создания. Директория: {dir.Name} Путь: {dir.FullName}");
            return true;
        }

        public static bool PrintSubdirNumber(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Директории {dir.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Количество поддиректорий директории {dir.Name}: {dir.GetDirectories().Length}");
            RAMLog.WriteLine($"Вывод информации о директории: количество поддиректорий. Директория: {dir.Name} Путь: {dir.FullName}");
            return true;
        }

        public static bool PrintParentDir(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Директории {dir.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Родительская директория директории {dir.Name}: {dir.Parent?.Name ?? "-"}");
            RAMLog.WriteLine($"Вывод информации о директории: родительская директория. Директория: {dir.Name} Путь: {dir.FullName}");
            return true;
        }
    }
}