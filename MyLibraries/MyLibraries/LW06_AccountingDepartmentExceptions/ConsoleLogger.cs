using System;
using System.IO;
using System.Text.Json.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    public sealed class ConsoleLogger : Logger
    {
        [JsonConstructor]
        public ConsoleLogger()
        {
            _writer = new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding) { AutoFlush = true };
        }

        public override void WriteLine(RecordType recordType, string msg)
        {
            if (_writer is null)
            {
                return;
            }

            Console.ForegroundColor = recordType switch
            {
                RecordType.ERROR => ConsoleColor.Red,
                RecordType.WARNING => ConsoleColor.Yellow,
                RecordType.INFO => ConsoleColor.Green,
                _ => Console.ForegroundColor
            };

            base.WriteLine(recordType, msg);

            Console.ResetColor();
        }
    }
}