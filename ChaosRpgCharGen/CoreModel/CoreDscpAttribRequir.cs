using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED ATTRIB-SCHEMA TO MEASURE THE STUDY DIFFICULTY OF DSCP
    /// IN USE IT ALWAYS CONNECTED TO AN ATTRIBUTE OF DSCP-ATTRIB-POOL -> MODIFIER TO ALTER JP VALUE
    /// </summary>
    public class CoreDscpAttribRequir
    {
        public byte theAttribId { get; }
        public short theAttribStarterSchema { get; }
        /// <summary>
        /// CONSTRUCTOR OF A DSCP ATTRIPB REQUIREMENT ELEMENT
        /// </summary>
        /// <param name="attribId">attribId</param>
        /// <param name="attribSchema">starter schema</param>
        public CoreDscpAttribRequir(byte attribId, short attribSchema)
        {
            theAttribId = attribId;
            theAttribStarterSchema = attribSchema;
        }
    }
}
