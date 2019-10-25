using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// DEFINES A MODEL FOR UNSPENT ATTRIB POINTS OF DSCP LEVELS
    /// IT GOEAS TO ATTRIB VIEW-FORM POSSIBLY-SPENDABLE DTGRVW
    /// COMES FROM THE DSCP-REPO AND IT REPRESET UNUSED ELEMENTS ON THE SOURCE SIDE
    /// </summary>
    public class CharDscpUnspentAttrib
    {
        public byte theAttribThatConnectedWith { get; }    //THE ATTRIB WITH WHICH IT IS CONNECTED
        public int theDscpIndex { get; }
        public byte theDscpSpecIndex { get; }
        public byte theDscpLevel { get; }
        public byte thePointType { get; }   //DEFINES THE POINT TYPE - IT IS ADJUSTED AUTOMATICLY

        /// <summary>
        /// CONSTRUCTOR OF AN UNSPENT ATTRIB POINT ELEMENT
        /// </summary>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="chosAttrib">attribId connected dscp</param>
        /// <param name="dscpLevel">levelSign</param>
        /// <param name="pointType">pointType</param>
        public CharDscpUnspentAttrib(int dscpIndex, byte chosAttrib, byte dscpLevel, byte specIndex, byte type)
        {
            theDscpIndex = dscpIndex;
            theAttribThatConnectedWith = chosAttrib;
            theDscpLevel = dscpLevel;
            theDscpSpecIndex = specIndex;
            thePointType = type;
        }
    }
}
