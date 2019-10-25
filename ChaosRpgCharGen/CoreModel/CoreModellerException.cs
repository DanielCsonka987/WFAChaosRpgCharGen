using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.CoreModel
{
    [Serializable]
    public class CoreModelException : Exception
    {
        public CoreModelException()
        {
        }

        public CoreModelException(string message) : base(message)
        {
        }

        public CoreModelException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CoreModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}