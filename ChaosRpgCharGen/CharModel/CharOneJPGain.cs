using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// MODEL OF A JP AMOUNT OF THE MANAGED CHARACTER
    /// THAT COMES WITH CHARACTER DEVELOPMeNT
    /// </summary>
    public class CharOneJPGain
    {
        public int theGainingId { get; }
        public int theJPAmount { get; }
        /// <summary>
        /// CONSTRUCTOR OF JPGAIN ELEMENT
        /// </summary>
        /// <param name="gainId">gainId</param>
        /// <param name="jpamount">jp amount</param>
        public CharOneJPGain(int gainId, int jpamount)
        {
            theGainingId = gainId;
            theJPAmount = jpamount;
        }
    }
}
