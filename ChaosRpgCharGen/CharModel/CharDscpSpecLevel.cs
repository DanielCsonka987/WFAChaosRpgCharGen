using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// SPECIALISATION LEVEL DESCRIPTIVE MODEL OF THE MANAGED CHARACTER
    /// </summary>
    public class CharDscpSpecLevel
    {
        public byte theSpecIndex { get; }
        public byte theLevelMark { get; }
        public short theLevelJP { get; }
        public byte theLevelRequirGroup { get; }
        public byte theSpecArea { get; }
        public string theSpecDescr { get; }
        public int theSpecLevelAttribPointSpent { get; set; }
        /// <summary>
        /// CONSTRUCTOR OF A SPECIALISATION LEVEL OF A SPECIFIC DISCIPLINE
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="reqgroup">requir group</param>
        /// <param name="area">area of spec</param>
        /// <param name="descr">description</param>
        /// <param name="jp">jp cost</param>
        /// <param name="comesAttribPointFromThis">this level gives attrb</param>
        public CharDscpSpecLevel(byte specIndex, byte level, short jp, byte reqgroup,
            byte area, string descr, int attribPointSpent)
        {
            theSpecIndex = specIndex;
            theLevelMark = level;
            theLevelJP = jp;
            theLevelRequirGroup = reqgroup;
            theSpecArea = area;
            theSpecDescr = descr;
            theSpecLevelAttribPointSpent = attribPointSpent;
        }
    }
}
