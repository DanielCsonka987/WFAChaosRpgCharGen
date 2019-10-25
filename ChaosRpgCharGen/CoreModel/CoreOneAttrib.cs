using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED ATTRIBUTE THAT CONTAINS THE NAME AND ID OF AN ATTRIBUTE THAT MANAGED IN SYSTEM
    /// </summary>
    public class CoreOneAttrib
    {
        public byte theAttribId { get; }    //DB IDENTIFIER (from 1-12)
        public string theAttribName { get; }    //NAME OF THAT
        /// <summary>
        /// CONSTRUCTOR OF AN ATTRIBUTE CORE DEFINITION
        /// </summary>
        /// <param name="attrId">the attrId</param>
        /// <param name="name">the name</param>
        public CoreOneAttrib(byte attrId, string name)
        {
            theAttribId = attrId;
            theAttribName = name;
        }
    }
}
