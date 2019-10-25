using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// NORMAL LEVEL DESCRIPTIVE MODEL OF THE MANAGED CHARACTER
    /// </summary>
    public class CharDscpLevel
    {
        public byte theLevelMark { get; }
        public bool theWayOfStudy { get; }
        public short theLevelJP { get; }
        public bool theLevelHasAttribRise { get; }

        public int theAttribRiseIsSpent { get; set; }  //ABOUT SPENDING THE USER DECIDES

        /// <summary>
        /// CONSTRUCTOR OF A LEVEL OF A SPECIFIC DISCIPLINE
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="mentor_practice">mentorated or not</param>
        /// <param name="jp">jp cost</param>
        /// <param name="isThereAttribRise">is there attrib rising</param>
        /// <param name="attribRiseIsSpent">is needed to spend this</param>
        public CharDscpLevel(byte level, bool mentor_practice, short jp,
            bool isThereAttribRise, int attribRiseIsSpent)
        {
            theLevelMark = level;
            theWayOfStudy = mentor_practice;
            theLevelJP = jp;
            theLevelHasAttribRise = isThereAttribRise;
            theAttribRiseIsSpent = attribRiseIsSpent;
        }
    }
}
