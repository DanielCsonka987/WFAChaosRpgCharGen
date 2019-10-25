using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// DEFINES A MODEL OF AN ATTRIB-ENCHANTMENT
    /// THAT DB CONTAINS AND IT IS THE APPLIANCE-SIDE OF ATTRIB-ENCHANTMENTS
    /// </summary>
    public class CharOneAttribEnch
    {
        public int theAttribEnchanceIndex { get; }     //primary key in DB
        public byte theAttributeId { get; }        //where it goes
        public int theDisciplIndex_First { get; }
        public int theDisciplIndex_Second { get; }  //case collected
        public byte thePointType { get; }          //1=level 4-5-6, 2=level 7-8-9, 3=level 10<
        /// <summary>
        /// CONSTRUCTOR OF AN ATTRIBUTE ENCHANCING RECORD - SAME CHAR_ID
        /// </summary>
        /// <param name="enchIndex">the identifier</param>
        /// <param name="attribId">the atrribute where to spent</param>
        /// <param name="dscpIndex1">what dscp is comming from</param>
        /// <param name="dscpIndex2">what dscp is comming from</param>
        /// <param name="pointType">type of point</param>
        public CharOneAttribEnch(int enchIndex, byte attribId, int dscpIndex1,
            int dscpIndex2,  byte pointType)
        {
            theAttribEnchanceIndex = enchIndex;
            theAttributeId = attribId;
            theDisciplIndex_First = dscpIndex1;
            theDisciplIndex_Second = dscpIndex2;
            thePointType = pointType;
        }
    }
}
