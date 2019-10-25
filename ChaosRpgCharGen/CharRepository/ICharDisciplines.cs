using ChaosRpgCharGen.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1
{
    /// <summary>
    /// INTERFACE OF CHAR-DISCIPLINES REPOSITORY
    /// </summary>
    interface ICharDisciplines
    {
        //DSCP NORMAL LEVEL MANAGEMENT
        void addNewDiscipl(short dscpId, byte type, byte attribId, string dscpDescr, bool mentor_practice,
            short jpCost, byte dscpRequir);
        void riseThisDiscipl(int dscpIndex, bool mentor_practice, short jpCost);
        void removeDisciplLevelFrom(int dscpIndex, byte levelFrom);
        void removeThisWholeDiscipl(int dscpIndex);

        List<CharDscpSurfaceAboutLevels> collectAllDisciplSurface();              //TO REVIEW LIST AT MAIN MANAGER
        List<CharDscpLevel> collectThisDisciplLayers(int dscpIndex);    //TO DETAIL LIST AT DSCP TREATER
        int[] collectDisciplDetails_ToShow(int dscpIndex);      //FINDS THE DATAS FO DSCP TREATER

        //DSCP SPECIALISATION
        void addNewDsciplSpecialLevel(int dscpIndex, short jp, byte reqgroup, byte area, string descr);
        void removeThisDsciplSpecFrom(int dscpIndex, byte specIndex, byte levelFrom);
        bool hasThisDisciplSpecialis(int dscpIndex);
        bool hasThisDisciplSpecWithThisArea(int dscpIndex, byte reqgroup, byte area, string descr);

        //List<CharDscpSpecLevel> collectAllSpecSurface();             //TO REVIEW LIST AT MAIN MANAGER - NOT USED
        List<CharDscpSpecLevel> collectThisDisciplSpecAllLayers(int dscpIndex);     //TO DETAIL LIST AT DSCP TREATER

        //DSCP GENERAL MANAGEMENT
        int sumAllJPOfDscp();       //TO SHOW THE REVIEW WINDOW
        int sumTheJPOfThisDscp(int dscpIndex);      //TO sHOW THE DSCP DETAILED WINDOW
        short findTheDscpIdForThisDscp(int dscpIndex);
        byte findTheDscpAttribForThisDscp(int dscpIndex);
        string findThisDscpNoteOfThisDscp(int dscpIndex);   //TO sHOW THE DSCP DETAILED WINDOW
        int[] collectTheAttribEnchOfThisDscpIndexFromLevel_UnderDirectDeletion(int dscpIndex, byte levelFrom, byte specIndex);     //AT DELETION THE ATTRIB ENCH NEEDS TO DELETE
        byte getTheNextLevelMark(int dscpIndex, byte specIndex);

        //REQUIREMENT ANALYZERS
        //bool findThisDscpRequir_NormWithTrunkDetails(short seekedDscpId,byte seekedDscpReqGroup, string dscpDescr,    //THESE 3 IS FOR THE PROPER IDENTIFY
          //  byte levelNeed, bool isSevere);     //THESE 2 DEFINE THE NEEDS - NOT USED
        bool findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(short seekedDscpId, byte levelNeed);
            //THIS REMOVED FROM IDES    //byte findTheMaxNormalLevelOfThisDscp_ToDefineIfSpecPermitted(int dscpIndexToSpec);
        byte findTheMaxSpecLevelOfThisDscp_ToDefineIfSpecPermitted(int dscpIndexToSpec, byte dscpSpecRequirGroup, byte areaId, string dscpSpecNote);
        byte findTheMaxSpecLevelOfThisDscp_ToDefineIfSpecPermitted(int dscpIndexToSpec, byte specIndex);
        byte findTheSpecRequirIdOfThisDscpSpec_ToDefineIfSpecPermitted(int dscpIndexToSpec, byte specIndex);
        bool findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(short seekedDscpType, byte levelNeed);
        bool findThisDscpRequir_IndividualHaveTheLevel_NormalCase(short seekedDscpId, byte level, bool isSevere);
        byte[] findSpecRequirGroupAndAreaByItsSpecIndex_AtSpecLvlRise(int dscpIndex, byte specIndex);
        string findSpecDescrByItsSpecIndex_AtSpecLvlRise(int dscpIndex, byte specIndex);

        //DSCP ATTRIB RISING MANAGEMENT - IT IS THE SOURCE PART
        void removeUnpentAttribPoint_itIsSpent(int dscpIndex, byte disciplLevel, byte isSpec, int attribEnchIndex);  //DEFINE THE ATTRIB POINTS - THAT SPENT FOR SURE
        void addUnspentAttribPoint_resetAttribSpending(int dscpIndex, bool isItSpec, int attribEnchIndex);
        void addUnspentAttribPoint_resetAttribSpending_UnknownType(int dscpIndex, int attribEnchIndex);
        List<CharDscpUnspentAttrib> collectPossiblyUnspentAttribPoints();   //IT DEFINES WHICH DSCP LEVEL COULD BE ATTRIB RISEN - THAT IS NOT SPENT

        int[] findDetailsSpentAttribPoint(int dscpIndex1, int dscpIndex2, int attribEnchIndex);
        //byte findSpecIndexBySpentAttribPoint_HelperRemovingAttribEnch(int dscpIndex, int attribEnchIndex);    //NOT USED
    }
}
