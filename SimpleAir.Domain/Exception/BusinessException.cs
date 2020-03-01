using System;
using System.Runtime.Serialization;

namespace SimpleAir.Domain.Service.Exception
{
    public class BusinessException : SystemException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}