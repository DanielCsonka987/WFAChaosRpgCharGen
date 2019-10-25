using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.GeneralModel
{
    /// <summary>
    /// MODEL OF CHARACTERS MAIN INFORMATION - TO SHOW THEM THE USER AT REVISE WINDOW
    /// </summary>
    public class GeneralOneTrunkEntity
    {
        public int theCharId { get; set; } //IT IS DEFINED LATER IN NEW CHAR CREATION
        public byte theRaceId { get; }
        public string theRaceName { get; }
        public string theCharName { get;}
        public string theCharDescr { get; }
        public byte theBeneficDscp { get; }
        public byte theRealSize { get; }
        public string theSizeText { get; }
        public byte theRealSpeed { get; }
        public int theStarterJPValue { get; }
        /// <summary>
        /// CONSTRUCTOR OF A CHARACTER TRUNK TO REVISE WINDOW AND OPEN THAT - IT IS USED AT OPENING
        /// </summary>
        /// <param name="charIdentif">charId</param>
        /// <param name="raceId">raceId</param>
        /// <param name="raceName">raceName</param>
        /// <param name="name">charName</param>
        /// <param name="descr">charDescr</param>
        /// <param name="benefDscp">benefDscp</param>
        /// <param name="realSize">size</param>
        /// <param name="sizeText">sizeText</param>
        /// <param name="realSpeed">quickness</param>
        /// <param name="starterJP">starterJP</param>
        public GeneralOneTrunkEntity(int charIdentif, byte raceId,
            string raceName, string name, string descr, byte benefDscp,
            byte realSize, string sizeText, byte realSpeed, int starterJP)
        {
            theCharId = charIdentif;
            theRaceId = raceId;
            theRaceName = raceName;
            theCharName = name;
            theCharDescr = descr;
            theBeneficDscp = benefDscp;
            theRealSize = realSize;
            theSizeText = sizeText;
            theRealSpeed = realSpeed;
            theStarterJPValue = starterJP;
        }
        /// <summary>
        /// CONSTRUCTOR OF A CHARACTER TRUNK TO REVISE WINDOW AND OPEN THAT - IT IS USED AT CHARACTER CREATION
        /// </summary>
        /// <param name="raceId">raceId</param>
        /// <param name="raceName">raceName</param>
        /// <param name="name">charName</param>
        /// <param name="descr">charDescr</param>
        /// <param name="benefDscp">benefDscp</param>
        /// <param name="realSize">size</param>
        /// <param name="realSpeed">quickness</param>
        /// <param name="starterJP">starterJP</param>
        public GeneralOneTrunkEntity(byte raceId, string raceName, string name, string descr, byte benefDscp,
            byte realSize, string sizeText, byte realSpeed, int starterJP)
        {
            theRaceId = raceId;
            theRaceName = raceName;
            theCharName = name;
            theCharDescr = descr;
            theBeneficDscp = benefDscp;
            theRealSize = realSize;
            theSizeText = sizeText;
            theRealSpeed = realSpeed;
            theStarterJPValue = starterJP;
        }
    }
}
