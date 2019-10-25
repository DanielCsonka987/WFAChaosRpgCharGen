using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL OF CORE DEFINED RACES IN THE RPG AND WHAT LIMITS IT HAS AT CHARACTER CREATION
    /// </summary>
    public class CoreOneRaceDetails
    {
        public byte theRaceId { get; }
        public string theRaceName { get; }
        public byte theRaceSize { get; }

        public short theAttrPhysuqueMax { get; }
        public short theAttrEffucuencyMax { get; }
        public short theAttrConscienceMax { get; }
        public short theAttrEssencyMax { get; }
        /// <summary>
        /// CONSTRUCTIOR OF A RACE BASIC DEFINITION
        /// </summary>
        /// <param name="raceId">raceId</param>
        /// <param name="raceName">name</param>
        /// <param name="size"></param>
        /// <param name="phisciq">max of phisique</param>
        /// <param name="efficiency">max of efficency</param>
        /// <param name="conscience">max of coscience</param>
        /// <param name="essense">max of essence</param>
        public CoreOneRaceDetails(byte raceId, string raceName, byte size,
            short phisciq, short efficiency, short conscience, short essense )
        {
            theRaceId = raceId;
            theRaceName = raceName;
            theRaceSize = size;

            theAttrPhysuqueMax = phisciq;
            theAttrEffucuencyMax = efficiency;
            theAttrConscienceMax = conscience;
            theAttrEssencyMax = essense;
        }
    }
}
