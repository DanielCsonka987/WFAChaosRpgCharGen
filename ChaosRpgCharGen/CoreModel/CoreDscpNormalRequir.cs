using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED ATTRIBUTE REQUIREMENT OF A DISCIPLINE
    /// </summary>
    public class CoreDscpNormalRequir
    {
        public byte theDisciplReqGroup { get; }        //WHICH VARIANT OF DSCP REQIREMENT OF A DISCIPLINE
        public short[] theDisciplRequirElementId { get; }  //SPECIFIC DISCIPLINE ID OR TYPE ID - ZERO MEANS NONE REQUIREMENT
        public bool[] theRequirIsADscpGroup { get; }    //IT DEFINES WHETHER A SPECIFIC DISCIPLINE OR GROUP OF DISCIPLINES
        /// <summary>
        /// CONSTRUCTOR OF A DISCIPLINE ATTRIB REQUIREMENT ELEMENT
        /// </summary>
        /// <param name="requirGroup">which version from all</param>
        /// <param name="requirDscpId">which dscp is requir</param>
        /// <param name="isADscpGroup">is a dscpGroup?</param>
        public CoreDscpNormalRequir(byte requirGroup, short[] requirDscpId, bool[] isADscpGroup)
        {
            theDisciplReqGroup = requirGroup;
            theDisciplRequirElementId = requirDscpId;
            theRequirIsADscpGroup = isADscpGroup;
        }
    }
}
