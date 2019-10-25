using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.CoreRepository
{
    [Serializable]
    public class CoreRepositoryException : Exception
    {
        public CoreRepositoryException()
        {
        }

        public CoreRepositoryException(string message) : base(message)
        {
        }

        public CoreRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CoreRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}