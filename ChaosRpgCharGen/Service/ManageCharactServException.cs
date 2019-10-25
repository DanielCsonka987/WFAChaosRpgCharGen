using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.Service
{
    [Serializable]
    public class ManageCharactServException : Exception
    {
        public ManageCharactServException()
        {
        }

        public ManageCharactServException(string message) : base(message)
        {
        }

        public ManageCharactServException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManageCharactServException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}