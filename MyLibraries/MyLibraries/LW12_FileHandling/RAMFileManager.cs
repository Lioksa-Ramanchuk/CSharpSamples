#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace OOP.LW12_FileHandling
{
    public static class RAMFileManager
    {
        public static string GetFilesAndFoldersNames(DriveInfo drive)
        {
            StringBuilder result = new();
            result.Append($"Файлы: {string.Join(", ", drive.RootDirectory.GetFiles().Select(f => f.Name))}\n");
            result.Append($"Папки: {string.Join(", ", drive.RootDirectory.GetDirectories().Select(d => d.Name))}");
            RAMLog.WriteLine($"Вывод информации о диске: список файлов, папок. Диск: {drive.Name}");
            return $"{result}";
        }

        public static DirectoryInfo CreateDir(string pathToParent, string dirName)
        {
            string newPath = Path.Combine(pathToParent, dirName);
            DirectoryInfo newDir = Directory.CreateDirectory(newPath);
            RAMLog.WriteLine($"Создание директории. Директория: {newDir.Name} Путь: {newDir.FullName}");
            return newDir;
        }

        public static FileInfo? CreateFileInDir(string fileName, DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Создание файла в директории не выполнено: директории {dir.FullName} не существует.");
                return null;
            }
            string filePath = Path.Combine(dir.FullName, fileName);
            using FileStream fs = File.Create(filePath);
            RAMLog.WriteLine($"Создание файла в директории. Файл: {fileName} Путь: {filePath} Директория: {dir.Name}");
            return new FileInfo(filePath);
        }

        public static bool WriteInFile(FileInfo file, string text)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"Запись текста в файл не выполнена: файла {file.FullName} не существует.");
                return false;
            }
            using StreamWriter sw = File.AppendText(file.FullName);
            sw.Write(text);
            RAMLog.WriteLine($"Запись текста в файл. Файл: {file.Name} Путь: {file.FullName}");
            return true;
        }

        public static bool CopyFileToDir(FileInfo srcFile, DirectoryInfo destDir)
        {
            if (!srcFile.Exists)
            {
                Console.WriteLine($"Копирование файла не выполнено: файла {srcFile.FullName} не существует.");
                return false;
            }
            string newFilePath = Path.Combine(destDir.FullName, srcFile.Name);
            if (File.Exists(newFilePath))
            {
                Console.WriteLine($"Копирование файла не выполнено: в директории {destDir.FullName} уже есть файл с именем {srcFile.Name}.");
                return false;
            }
            File.Copy(srcFile.FullName, newFilePath);
            RAMLog.WriteLine($"Создание копии файла в директории. Файл: {srcFile.Name} Путь: {srcFile.FullName} Путь к копии: {newFilePath} Директория: {destDir.Name}");
            return true;
        }

        public static bool RenameFile(ref FileInfo file, string newFileName)
        {
            string oldName = file.Name;
            string oldPath = file.FullName;
            string newPath = Path.Combine(file.DirectoryName ?? "", newFileName);
            if (File.Exists(newPath))
            {
                Console.WriteLine($"Переименование файла не выполнено: файл {newPath} уже существует.");
                return false;
            }
            File.Move(file.FullName, newPath);
            file = new FileInfo(newPath);

            RAMLog.WriteLine($"Переименование файла. Прежнее имя: {oldName} Путь: {oldPath} Новое имя: {Path.GetFileName(newPath)} Путь: {newPath}");
            return true;
        }

        public static bool DeleteFile(ref FileInfo? file)
        {
            if (file is null || !file.Exists)
            {
                Console.WriteLine($"Удаление файла не выполнено: файла {file?.FullName ?? "\b"} не существует.");
                return false;
            }
            string fileName = file.Name;
            string fileFullName = file.FullName;
            File.Delete(fileFullName);
            RAMLog.WriteLine($"Удаление файла. Файл: {fileName} Путь: {fileFullName}");
            file = null;
            return true;
        }

        public static bool CopyFilesWithExt(DirectoryInfo srcDir, DirectoryInfo destDir, string fileExt)
        {
            if (!srcDir.Exists)
            {
                Console.WriteLine($"Копирование файлов между директориями не выполнено: директории {srcDir.FullName} не существует.");
                return false;
            }
            if (!destDir.Exists)
            {
                Console.WriteLine($"Копирование файлов между директориями не выполнено: директории {destDir.FullName} не существует.");
                return false;
            }

            foreach (FileInfo file in srcDir.GetFiles($"*{fileExt}"))
            {
                file.CopyTo(Path.Combine(destDir.FullName, file.Name));
            }
            RAMLog.WriteLine($"Копирование файлов с заданным расширением между деректориями. Расширение: {fileExt} Директория-источник: {srcDir.Name} Путь: {srcDir.FullName} Директория-приёмник: {destDir.Name} Путь: {srcDir.FullName}");
            return true;
        }

        public static bool MoveDirToDir(ref DirectoryInfo dir, DirectoryInfo destDir)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Перемещение директории в другую директорию не выполнено: директории {dir.FullName} не существует.");
                return false;
            }
            if (!destDir.Exists)
            {
                Console.WriteLine($"Перемещение директории в другую директорию не выполнено: директории {destDir.FullName} не существует.");
                return false;
            }
            string newDirPath = Path.Combine(destDir.FullName, dir.Name);
            if (Directory.Exists(newDirPath))
            {
                Console.WriteLine($"Перемещение директории в другую директорию не выполнено: директория {newDirPath} уже существует.");
                return false;
            }
            dir.MoveTo(newDirPath);
            dir = new DirectoryInfo(newDirPath);
            RAMLog.WriteLine($"Перемещение директории в другую директорию. Директория: {dir.Name} Прежний путь: {dir.FullName} Новый путь: {newDirPath} Директория-приёмник: {destDir.Name}");
            return true;
        }

        public static bool ZipDir(DirectoryInfo dir, string zipPath)
        {
            if (!dir.Exists)
            {
                Console.WriteLine($"Создание архива из директории не выполнено: директории {dir.FullName} не существует.");
                return false;
            }
            if (File.Exists(zipPath))
            {
                Console.WriteLine($"Создание архива из директории не выполнено: архив {zipPath} уже существует.");
                return false;
            }
            ZipFile.CreateFromDirectory(dir.FullName, zipPath);
            RAMLog.WriteLine($"Создание архива из директории. Директория: {dir.Name} Путь: {dir.FullName} Архив: {Path.GetFileName(zipPath)} Путь: {Path.GetFullPath(zipPath)}");
            return true;
        }

        public static bool UnzipToDir(string zipPath, DirectoryInfo destDir)
        {
            if (!File.Exists(zipPath))
            {
                Console.WriteLine($"Распаковка архива в директорию не выполнена: архива {zipPath} не существует.");
                return false;
            }
            if (!destDir.Exists)
            {
                Console.WriteLine($"Распаковка архива в директорию не выполнена: директории {destDir.FullName} не существует.");
                return false;
            }
            ZipFile.ExtractToDirectory(zipPath, destDir.FullName);
            RAMLog.WriteLine($"Распаковка архива в директорию. Архив: {Path.GetFileName(zipPath)} Путь: {Path.GetFullPath(zipPath)} Директория: {destDir.Name} Путь: {destDir.FullName}");
            return true;
        }
    }
}