using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED DISCIPLINE TYPE DESCRIPTION
    /// </summary>
    public class CoreOneDscpType
    {
        public byte theTypeId { get; }
        public string theTypeName { get; }
        public bool theTypeIsOriginallyBenefic { get; }
        /// <summary>
        /// CONSTRUCTOR OF DISCIPLINE TYPE DEFINITION
        /// </summary>
        /// <param name="typeId">type Id</param>
        /// <param name="typeName">type name</param>
        public CoreOneDscpType(byte typeId, string typeName, bool originallyBenefic)
        {
            theTypeId = typeId;
            theTypeName = typeName;
            theTypeIsOriginallyBenefic = originallyBenefic;
        }
    }
}
