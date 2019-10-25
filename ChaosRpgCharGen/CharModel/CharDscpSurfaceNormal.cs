using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// DSCP DEFINIITIVE MODEL TO SHOW THE USER ESSENTIAL DATAS ABOUT A DSCP IN SORT
    /// IT GOES TO REVIEW DSCP DTGRVW AT VIEW-FORM
    /// </summary>
    public class CharDscpSurfaceAboutLevels
    {
        public int theDscpIndex { get; }
        public short theDscpId { get; }
        public byte theDscpTopLevel { get; }
        public int theDscpJp { get; }
        public string theDscpNote { get; }
        public bool theDscpHasSpec { get; }

        /// <summary>
        /// CONSTRUCTOR OF A DSCP LAYER
        /// </summary>
        /// <param name="dscpIndex">char dscpIndex</param>
        /// <param name="dscpId">dscpId</param>
        /// <param name="level">top level</param>
        /// <param name="jp">dscp fullJP</param>
        /// <param name="note">dscpNote</param>
        /// <param name="isTherSpec">dscp has spec</param>
        public CharDscpSurfaceAboutLevels(int dscpIndex, short dscpId,
            byte level, int jp, string note, bool isTherSpec)
        {
            theDscpIndex = dscpIndex;
            theDscpId = dscpId;
            theDscpTopLevel = level;
            theDscpJp = jp;
            theDscpNote = note;
            theDscpHasSpec = isTherSpec;
        }
    }
}
