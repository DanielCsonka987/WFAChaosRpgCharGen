using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// INTERFACE OF CHAR-ONE-DSCP MODEL-REPOSITORY
    /// </summary>
    interface ICharOneDscp
    {
        //NORMAL LEVEL MANAGEMENT
        void gainNewLevel(int charId, bool mentor_Practice, short jpCost);
        void removeTheseLevels(int charId, byte levelFrom);
        //CharDscpLevel getTheHighestLevel();         //CASE REVIEW DSCP NOT USED
        List<CharDscpLevel> getTheNormalLayers();           //CASE DETAILS DSCP
        byte getTheHighestLevelMark();
        int[] findTheAttribEnchantOfNormalLevels_UnderDeletion(byte levelFrom);

        //SPECIALISATION MANAGEMENT
        void gainNewSpecLevel(int charId, short jp, byte reqgroup, byte area, string descr);
        void removeTheseSpecLevels(int charId, byte specIndex, byte levelFrom);
        void removeAllSpecLevels(int charId);
        bool isThereSpecAtAll();
        byte getTheHighestSpecLevelMark(byte specRequirGroup, byte areaId, string specNote);
        byte getTheHighestSpecLevelMark(byte specIndex);
        byte getTheNextSpecLevelMarkBySpecInxed(byte specIndex);
        int findTheAttribEnchantOfThisSpecIndex_UnderDeletion(byte specIndex);
        byte getTheChosenSpecRequirGroupIdOfThis(byte specIndex);

        bool isThereAlreadyThisSpecType(byte specGroup, byte area, string descr);   //CASE OF CREATION
        bool isThereThisSpecLevels(byte specIndex, byte levelToDelet);      //CASE OF DELETION
        //List<CharDscpSpecLevel> getTheHighestSpecLevels();  //CASE REVIEW DSCP - NOT USED
        List<CharDscpSpecLevel> getTheSpecLayers();         //CASE DETAILS DSCP
        byte[] findTheSpecRequirGroupAndArea_AtSpecLvlRise(byte specIndex);
        string findTheSpecDescr_AtSpecLvlRise(byte specIndex);

        //JP COUNTER
        int sumFullJP_ThisDscp();
        int[] findAllAttribEnchantOfThisDscp_UnderDeletion();

        //ATTRIB ENCHANCE MANAGEMENT - SOURCE PART
        bool isThereUnspentAttribs();
        List<CharDscpUnspentAttrib> collectTheUnspentAttribs();
        void setSpentTheAttribPoint_FromNormalLevel(int charId, byte level, int attribEnchIndex);
        void setSpentTheAttribPoint_FromSpecLevel(int charId, byte specIndex, int attribEnchIndex);
        void setUnspentTheAttribPoint_FromNormalLevel(int charId, int attribEnchIndex);
        void setUnspentTheAttribPoint_FromSpecLevel(int charId, int attribEnchIndex);

        int[] findDetailsSpentAttribPoint(int attribEnchIndex, byte modeOfSearch);
        byte helperUnspentTheAttribPoint_UnknownTypeDetection(int theManagedCharId, int attribEnchIndex);
    }
}
