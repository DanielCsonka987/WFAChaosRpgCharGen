using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.GeneralRepository
{
    [Serializable]
    internal class GeneralRepositoryException : Exception
    {
        public GeneralRepositoryException()
        {
        }

        public GeneralRepositoryException(string message) : base(message)
        {
        }

        public GeneralRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GeneralRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}