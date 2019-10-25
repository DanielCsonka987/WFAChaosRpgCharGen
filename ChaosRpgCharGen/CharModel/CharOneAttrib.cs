using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// MODEL OF AN ATTRIBUTE VALUE OF THE MANAGED CHARACTER
    /// ALL DIFFERENT ATTRIBUTE HAS ONE FROM 1->12
    /// SELF COMPARABLE - TO SHOW IT GOOD ORDER VIA ARRAY MEDIATOR AND DEFINE WELL THE ATTRIB-ENCHANTMENT
    /// </summary>
    public class CharOneAttrib :IComparable<CharOneAttrib>
    {
        public byte theAttribId { get; }         //IT IS FROM STAT DB PRIMARY KEY
        public short theAttributeBaseValue { get; }          //IT IS FROM STAT DB

        public short theAttributeRisingValue { get; set; }   //IT IS FROM ATTRIBENCH DB

        /// <summary>
        /// ONE ATTRIBUTE OF A CHARACTER
        /// </summary>
        /// <param name="attribID"></param>
        /// <param name="attribBase"></param>
        public CharOneAttrib(byte attribID, short attribBase)
        {
            theAttribId = attribID;
            theAttributeBaseValue = attribBase; 
        }
        /// <summary>
        /// ONE ATTRIBUTES TRUE VALUE
        /// </summary>
        /// <returns>TRUE VALUE OF THIS ATTRIBUTE</returns>
        public short getTheFullAttribValue()
        {
            return (short) (theAttributeBaseValue + theAttributeRisingValue);
        }

        public int CompareTo(CharOneAttrib other)
        {
            if (this.theAttribId < other.theAttribId)
                return -1;
            else
                return 1;
        }
    }
}
