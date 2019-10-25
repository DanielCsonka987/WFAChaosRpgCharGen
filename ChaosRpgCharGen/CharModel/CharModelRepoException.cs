using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.CharModel
{
    [Serializable]
    internal class CharModelRepoException : Exception
    {
        public CharModelRepoException()
        {
        }

        public CharModelRepoException(string message) : base(message)
        {
        }

        public CharModelRepoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CharModelRepoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}