using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED SPECIALISATION VERSION OF A DISCIPLINE
    /// </summary>
    public class CoreDscpSpec
    {
        public byte theDisciplSpecRequirGroup { get; }  //DEFINES WHICH SPEC REQUIR GROUP HAS THIS
        public short[] theDisciplSpecRequirId { get; }    //NEEDED THIS IDENTIF
        public byte[] theDisciplReuirLevel { get; }       //NEEDED LEVEL FROM THAT
        public byte[] theDisciplRequirType { get; }       //DEFINES WHAT IS THAT THING
        public bool[] theDisciplSpecNeedToRise { get; }   //THIS GPOUP PART IS NEEDED TO RISE AT LEVEL 2?
        /// <summary>
        /// CONSTRUCTOR OF A SPECIALISATION WAY OF A DISCIPLINE
        /// </summary>
        /// <param name="specRequirGroup">specRequirGroup</param>
        /// <param name="requirId">requirement identif</param>
        /// <param name="neededLevel">needed level</param>
        /// <param name="typeOfRequir">type 0=DSCP, 1=attrib, 2=DSCPGroup</param>
        /// <param name="isNeedToRise">this requir. part rises</param>
        public CoreDscpSpec(byte specRequirGroup, short[] requirId, byte[] neededLevel,
            byte[] typeOfRequir, bool[] isNeedToRise)
        {
            theDisciplSpecRequirGroup = specRequirGroup;
            theDisciplSpecRequirId = requirId;
            theDisciplReuirLevel = neededLevel;
            theDisciplSpecNeedToRise = isNeedToRise;
            theDisciplRequirType = typeOfRequir;
        }
    }
}
