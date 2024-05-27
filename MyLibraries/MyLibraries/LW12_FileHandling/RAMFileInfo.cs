#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.IO;

namespace OOP.LW12_FileHandling
{
    public static class RAMFileInfo
    {
        public static bool PrintFullPath(FileInfo file)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"Файла {file.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Полный путь к файлу {file.Name}: {file.FullName}");
            RAMLog.WriteLine($"Вывод информации о файле: полный путь. Файл: {file.Name} Путь: {file.FullName}");
            return true;
        }

        public static bool PrintFileNameSizeExt(FileInfo file)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"Файла {file.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Имя файла: {file.Name}");
            Console.WriteLine($"Размер: {file.Length}Б");
            Console.WriteLine($"Расширение: {file.Extension}");
            RAMLog.WriteLine($"Вывод информации о файле: имя, размер, расширение. Файл: {file.Name} Путь: {file.FullName}");
            return true;
        }

        public static bool PrintCreationAndEditDate(FileInfo file)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"Файла {file.FullName} не существует.");
                return false;
            }
            Console.WriteLine($"Дата создания файла {file.Name}: {file.CreationTime:d}");
            Console.WriteLine($"Дата последнего изменения файла: {file.LastWriteTime:d}");
            RAMLog.WriteLine($"Вывод информации о файле: дата создания, дата изменения. Файл: {file.Name} Путь: {file.FullName}");
            return true;
        }
    }
}