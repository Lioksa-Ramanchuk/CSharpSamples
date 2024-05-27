using System.IO;
using System.Text.Json.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    public sealed class FileLogger : Logger
    {
        string _path = "Log.txt";

        [JsonConstructor]
        public FileLogger(string path = "Log.txt")
        {
            Path = path;
        }

        public string Path
        {
            get => _path;
            set
            {
                _writer?.Close();
                _writer = new StreamWriter(value, true) { AutoFlush = true };
                _path = value;
            }
        }
    }
}