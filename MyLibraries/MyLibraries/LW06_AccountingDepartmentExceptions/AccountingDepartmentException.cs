using System;
using System.Runtime.Serialization;

namespace OOP.LW06_AccountingDepartmentExceptions
{
    [Serializable]
    public class AccountingDepartmentException : Exception
    {
        public AccountingDepartmentException() : base()
        {
        }
        public AccountingDepartmentException(string message) : base(message)
        {
        }
        public AccountingDepartmentException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected AccountingDepartmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}