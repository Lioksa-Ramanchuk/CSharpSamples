using System;
using System.IO;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    public abstract class Logger : IDisposable
    {
        bool _disposed = false;
        protected StreamWriter? _writer = null;

        public enum RecordType
        {
            ERROR,
            WARNING,
            INFO,
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writer?.Dispose();
                    _writer = null;
                }
                _disposed = true;
            }
        }

        public virtual void WriteLine(RecordType recordType, string msg)
        {
            if (_writer is null)
            {
                return;
            }

            _writer.Write($"{DateTime.Now:dd.MM.yyyy HH:mm}, ");
            _writer.Write(recordType switch
            {
                RecordType.ERROR => "ERROR: ",
                RecordType.WARNING => "WARNING: ",
                RecordType.INFO => "INFO: ",
                _ => string.Empty
            });
            _writer.WriteLine(msg);
        }
    }
}