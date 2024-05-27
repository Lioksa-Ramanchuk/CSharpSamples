#pragma warning disable S101 // Types should be named in PascalCase

using System;
using System.IO;
using System.Linq;

namespace OOP.LW12_FileHandling
{
    public static class RAMLog
    {
        public static string LogPath { get; set; } = Path.Combine("E:", "ramlogfile.txt");

        public static void WriteLine(string s)
        {
            using StreamWriter sw = File.AppendText(LogPath);
            sw.WriteLine($"{DateTime.Now:G} {s}");
        }

        public static void PrintRecordsByDate(DateTime date)
        {
            using StreamReader sr = File.OpenText(RAMLog.LogPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string logRecord in File.ReadLines(LogPath).Where(s => s[0..8].Equals($"{date:d}")))
            {
                Console.WriteLine(logRecord);
            }
            Console.ResetColor();
        }

        public static void PrintRecordsInTimeInterval(DateTime dateStart, DateTime dateEnd)
        {
            using StreamReader sr = File.OpenText(RAMLog.LogPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string logRecord in File.ReadLines(LogPath).Where(s => {
                DateTime dt = DateTime.ParseExact(s[0..17], "G", null);
                return dateStart <= dt && dt <= dateEnd;
            }))
            {
                Console.WriteLine(logRecord);
            }
            Console.ResetColor();
        }

        public static void PrintRecordsByPhrase(string phrase)
        {
            using StreamReader sr = File.OpenText(RAMLog.LogPath);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string logRecord in File.ReadLines(LogPath).Where(s => s.Contains(phrase, StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.WriteLine(logRecord);
            }
            Console.ResetColor();
        }
    }
}