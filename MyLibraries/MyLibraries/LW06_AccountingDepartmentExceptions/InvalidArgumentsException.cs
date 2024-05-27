using System;
using System.Runtime.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    [Serializable]
    public class InvalidArgumentsException : AccountingDepartmentException
    {
        public InvalidArgumentsException()
        {
        }
        public InvalidArgumentsException(string message) : base(message)
        {
        }
        public InvalidArgumentsException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected InvalidArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}