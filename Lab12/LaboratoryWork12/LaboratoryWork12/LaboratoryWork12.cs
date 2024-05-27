namespace OOP
{
    using LW12_FileHandling;

    static class LaboratoryWork12
    {
        static void Main()
        {
            try
            {
                // 3
                DriveInfo driveE = new("E:");
                RAMDiskInfo.PrintTotalFreeSpace(driveE);
                RAMDiskInfo.PrintFileSystem(driveE);
                RAMDiskInfo.PrintDrivesInfo();
                Console.WriteLine();

                // 4
                FileInfo file = new(Path.Combine("E:", "Download", "Chrome", "11_Рефлексия.pdf"));
                RAMFileInfo.PrintFullPath(file);
                RAMFileInfo.PrintFileNameSizeExt(file);
                RAMFileInfo.PrintCreationAndEditDate(file);
                Console.WriteLine();

                // 5
                DirectoryInfo dir = new(Path.Combine("E:", "Install"));
                RAMDirInfo.PrintFilesNumber(dir);
                RAMDirInfo.PrintCreationTime(dir);
                RAMDirInfo.PrintSubdirNumber(dir);
                RAMDirInfo.PrintParentDir(dir);
                Console.WriteLine();

                // 6
                string filesAndFoldersNames = RAMFileManager.GetFilesAndFoldersNames(driveE);
                DirectoryInfo inspectDir = RAMFileManager.CreateDir("E:", "RAMInspect");
                string dirInfoFileName = "ramdirinfo.txt";
                FileInfo? dirInfoFile = RAMFileManager.CreateFileInDir(dirInfoFileName, inspectDir);
                if (dirInfoFile is null) return;
                using (StreamWriter sw = new(dirInfoFile.FullName))
                {
                    sw.WriteLine(filesAndFoldersNames);
                }

                if (RAMFileManager.CopyFileToDir(dirInfoFile, driveE.RootDirectory))
                {
                    FileInfo dirInfoCopyFile = new(Path.Combine("E:", dirInfoFile.Name));
                    RAMFileManager.RenameFile(ref dirInfoCopyFile, "something.txt");
                    RAMFileManager.DeleteFile(ref dirInfoFile);
                }

                DirectoryInfo filesDir = RAMFileManager.CreateDir("E:", "RAMFiles");
                Console.WriteLine("Введите путь к директории с файлами для копирования:");
                string srcDirPath = Console.ReadLine()!;
                Console.WriteLine("Введите расширение файлов для копирования (начиная с '.'):");
                string fileExt = Console.ReadLine()!;
                RAMFileManager.CopyFilesWithExt(new DirectoryInfo(srcDirPath), filesDir, fileExt);
                RAMFileManager.MoveDirToDir(ref filesDir, inspectDir);

                string zipPath = Path.Combine("E:", "RAMFiles.zip");
                if (RAMFileManager.ZipDir(filesDir, zipPath))
                {
                    DirectoryInfo unzipDir = RAMFileManager.CreateDir("E:", "Unzipped RAMFiles");
                    RAMFileManager.UnzipToDir(zipPath, unzipDir);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Ошибка ввода-вывода {e.GetType()}: {e.Message} ({e.TargetSite})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка {e.GetType()}: {e.Message} ({e.TargetSite})");
            }

            // 7
            int choice;
            do
            {
                Console.WriteLine("\nВыберите:");
                Console.WriteLine(" 1 - вывести информацию лога за определённый день");
                Console.WriteLine(" 2 - вывести информацию лога за диапазон времени");
                Console.WriteLine(" 3 - вывести информацию лога по ключевой фразе");
                Console.WriteLine(" 4 - выйти из программы");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.Write("Некорректный ввод. Попробуйте ещё раз: ");
                }
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Введите дату: ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                            {
                                Console.WriteLine("Дата введена некорректно.");
                                break;
                            }
                            
                            RAMLog.PrintRecordsByDate(date);
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Введите начальную дату (ДД.ММ.ГГ ЧЧ:ММ:СС): ");
                            if (!DateTime.TryParseExact(Console.ReadLine(), "G", null, System.Globalization.DateTimeStyles.None, out DateTime dateStart))
                            {
                                Console.WriteLine("Дата введена некорректно.");
                                break;
                            }
                            Console.WriteLine("Введите конечную дату (ДД.ММ.ГГ ЧЧ:ММ:СС): ");
                            if (!DateTime.TryParseExact(Console.ReadLine(), "G", null, System.Globalization.DateTimeStyles.None, out DateTime dateEnd))
                            {
                                Console.WriteLine("Дата введена некорректно.");
                                break;
                            }
                            if (dateStart > dateEnd)
                            {
                                Console.WriteLine("Диапазон задан некорректно.");
                                break;
                            }

                            RAMLog.PrintRecordsInTimeInterval(dateStart, dateEnd);
                        }
                        break;
                    case 3:
                        {
                            Console.WriteLine("Введите ключевую фразу: ");
                            string keyPhrase = Console.ReadLine()!;
                            RAMLog.PrintRecordsByPhrase(keyPhrase);
                        }
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            } while (choice != 4);
        }
    }
}