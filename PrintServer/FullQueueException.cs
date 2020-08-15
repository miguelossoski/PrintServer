using System;
using System.Runtime.Serialization;

namespace PrintServer
{
    public class FullQueueException : Exception
    {
        public FullQueueException()
        {
        }

        public FullQueueException(string message) : base(message)
        {
        }

        public FullQueueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FullQueueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
