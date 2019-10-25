using System;
using System.Runtime.Serialization;

namespace ChaosRpgCharGen.GeneralRepository
{
    [Serializable]
    internal class GeneralBenefException : Exception
    {
        public GeneralBenefException()
        {
        }

        public GeneralBenefException(string message) : base(message)
        {
        }

        public GeneralBenefException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GeneralBenefException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}