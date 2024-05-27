using System;
using System.Runtime.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    [Serializable]
    public class DeserializationException : AccountingDepartmentException
    {
        public DeserializationException()
        {
        }
        public DeserializationException(string message) : base(message)
        {
        }
        public DeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected DeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}