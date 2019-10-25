using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.CharRepository1
{
    [Serializable]
    public class CharRepositoryException : Exception
    {
        public CharRepositoryException()
        {
        }

        public CharRepositoryException(string message) : base(message)
        {
        }

        public CharRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CharRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}