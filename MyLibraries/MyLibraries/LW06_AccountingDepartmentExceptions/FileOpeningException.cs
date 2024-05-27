using System;
using System.Runtime.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    [Serializable]
    public class FileOpeningException : AccountingDepartmentException
    {
        public FileOpeningException()
        {
        }
        public FileOpeningException(string message) : base(message)
        {
        }
        public FileOpeningException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected FileOpeningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}