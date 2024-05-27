#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.IO;

namespace OOP.LW12_FileHandling
{
    public static class RAMDiskInfo
    {
        public static bool PrintTotalFreeSpace(DriveInfo drive)
        {
            if (!drive.IsReady)
            {
                Console.WriteLine($"Диск {drive.Name} не готов.");
                return false;
            }
            Console.WriteLine($"Свободное место на диске {drive.Name}: {drive.TotalFreeSpace}Б");
            RAMLog.WriteLine($"Вывод информации о диске: свободное место. Диск: {drive.Name}");
            return true;
        }

        public static bool PrintFileSystem(DriveInfo drive)
        {
            if (!drive.IsReady)
            {
                Console.WriteLine($"Диск {drive.Name} не готов.");
                return false;
            }
            Console.WriteLine($"Файловая система диска {drive.Name}: {drive.DriveFormat}");
            RAMLog.WriteLine($"Вывод информации о диске: файловая система. Диск: {drive.Name}");
            return true;
        }

        public static void PrintDrivesInfo()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Console.WriteLine();
                if (drive.IsReady)
                {
                    Console.WriteLine($"Диск: {drive.Name}");
                    Console.WriteLine($"Объём: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Доступный объём: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                }
                else
                {
                    Console.WriteLine($"Диск {drive.Name} не готов.");
                }
            }

            RAMLog.WriteLine($"Вывод информации о всех дисках: имя, объём, доступный объём, метка тома.");
        }
    }
}