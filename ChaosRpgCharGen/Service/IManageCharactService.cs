using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.Service
{
    /// <summary>
    /// INTERFACE OF SERVICE MANAGE CHARACTER
    /// </summary>
    interface IManageCharactService
    {
        //MAIN MANAGER WINDOW
        DataTable ManagerWindow_collectAllTheDscpSurface();    //DSCP NAME FROM CORE-REPO, OTHERS FROM CHAR-DSCP REPO
        short[] ManagerWindow_getTheCharFullAttributes();   //FROM CHAR-ATTRIBURES REPO

        #region DSCP GENERALS
        //DSCP GENERAL INFORMATION - PRESENTER AT DECISSION MAKING
        bool DscpGeneralInfo_isThereEnoughJPToGetLevel(int starterJpOfCharacter, int neededJPForDscpLevel);   //ANSWER IS NEEDED TO SHOW THE USER
        bool DscpGeneralInfo_defineIfNewDscpIsBeneficial_AtNewAndGainUpLvl(byte selectedDscpType, byte charPrefBenefDscpType);

        //ALL CASES DELET DSCP - PROCESS
        void DscpGeneralProcess_RemoveSelectedLevel(int dscpIndex, byte levelFrom, byte specIndex);
        #endregion

        #region DSCP BRAND NEW WINDOW

        //DSCP BRANDNEW - PRESENTERS
        string DscpNewWindow_getTheDscpBasicCosting_ToText(byte selectedType, byte charPrefBenefDscpType);
        string DscpNewWindow_getTheDscpStudiability_ToText(short dscpId);
        string DscpNewWindow_getTheDscpChosenJPAttribSchemaModifLevelsText_ToShow(short dscpId, byte attribId);
        int DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec,
            byte attribId);
        int DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp,
            bool isItSpec, bool isItPracticed);
        List<string> DscpNewWindow_collectDscpTyes_TextList();
        List<string> DscpNewWindow_collectDscpNames_TextList(byte typeId);
        List<string> DscpNewWindow_collectDscpRequir(short dscpId);
        List<string> DscpNewWindow_collectDscpAttirb(short dscpId);

        //DSCP BRANDNEW - PROCESSES
            //analyzeOneDscp_getTheDscpStudiability_ToBoolean(short dscpId)
        byte DscpNewWindow_getTheDscpTypeId(string typeName);
        short DscpNewWindow_getTheDscpId(string dscpName);
        byte DscpNewWindow_findTheDscpChosenRequirGroupId(short dscpId, string dscpCombText);
        byte DscpNewWindow_getTheDscpAttibId(string attribName);
        void DscpNewWindow_GainUpBrandNewDscp(short dscpId, byte chosenRequirGroup, byte attribId,
             string descr, short jpCost, bool trainMentorOrPract);
        int DscpNewWindow_getTheBasicJPForThisDscp(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec);   //SHOW ORIGINAL VALUE
        int DscpNewWindow_calculateTheFinalJPForThisDscp(short dscpId, byte levelMark, byte benefDscpTypeOfChar, bool isItSpec,
            byte dscpChosenAttribId, bool isItPracticed);  //SHOW FINAL VALUE
        byte DscpNewWindow_areThereAllRequirement(short dscpIdThatWouldNeed, byte dscpReqGroup,
             byte neededDscpLevel);       //ANSWER IS NEEDED TO SHOW THE USER
             //bool analyzeNormLevelRequir(short dscpId, byte dscpReqGroup, string dscpNote, byte level);
             //bool analyzeSpecLevelRequir(short dscpId, byte specReqGroup, string dscpNote, byte level);
             //bool analyzeOneDscp_getTheDscpStudiability_ToBoolean(short dscpId);
        #endregion

        #region DSCP GAIN NEW LEVEL WINDOW

        //DSCP NEW LEVEL AND SPEC PRESENTER
        DataTable DscpRiseSpec_collectThisDscpNormalLevelsDeep(int dscpIndex);  //FROM CHAR-DSCP REPO
        DataTable DscpRiseSpec_collectThisDscpSpecLevelsDeep(int dscpIndex);
            //string screenAndGetTheDscpSpecAreaText(int dscpIndes, byte areaId)
        string[] DscpRiseSpec_collectAllProperSpecAreasToGainNew(int dscpIndex);
        string[] DscpRiseSpec_collectThisDscpSeveralDetails(int dscpIndex);   //TO LABEL-TEXTS FROM CORE-DSCP REPO
        byte DscpRiseSpec_getTheNextDscpNormOrSpecLevel(int dscpIndex, byte specIndex);
        bool DscpRiseSpec_helpOfMentorIsNeeded(int dscpIndex);
        string[] DscpRiseSpec_getTheSelectedSpecPossibleRequirs(int dscpIndex);

        //DSCP NEW LEVEL AND SPEC PROCESSES
        int DscpRiseSpec_getTheAttribSchemaJPModif(int dscpIndex, byte nextLevel,
           byte charBenefDscpType, bool isItSpec, byte dscpAttribType);
        int DscpRiseSpec_getTheTrainJPModif(int dscpIndex, byte nextLevel,
            byte charBenefDscpType, bool isItSpec, bool isItPracticed);
        short DscpRiseSpec_getTheDscpIdOfThisDscp(int dscpIndex);
        byte DscpRiseSpec_getTheAttribIdOfThisDscp(int dscpIndex);
        bool DscpRiseSpec_isThereSpecialization(int dscpIndex);
        int DscpRiseSpec_getTheBasicJPForThisDscp(int dscpIndex,
            byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec);
        int DscpRiseSpec_calculateTheFinalJPForThisDscp(int dscpIndex, byte levelMark,
            byte beneficialDscpIdByCharProp, bool isItSpec, byte chosenAttribId, bool isItMentorated);
        byte DscpRiseSpec_areThereAllRequirement_GainMoreNormalLvl(int managedDscpIndex, byte neededDscpLevel);   //FOR NORMAL LEVELS
        byte DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(int dscpIndexThatManagedTospec, byte neededDscpLevel,
            byte dscpReqGroupToAnalyzeBy);//FOR SPEC LEVELS
        byte DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(int dscpIndex, string specRequirText);
        void DscpRiseSpec_GainNormalLevel(int dscpIndex, short jp, bool isWithMentor);
        void DscpRiseSpec_GainNewSpecialisation(int dscpIndex, short jp, byte specRequirGroup, string specAreaText,
            string specDescr);
        void DscpRiseSpec_GainNewSpecLevel(int dscpIndex, short jp, byte specIndex);
            //byte analyzeForThisSpecTheLevelRequirAreThere(short dscpId, byte specReqGroup, byte levelThatNeeded)

        #endregion

        #region ATTRIBUTE HANDLE

        //GENERAL ATTRIB HANDLE
        byte AttribGeneral_getTheAttribIdOfThisAttribName(string attribName);
        string AttribGeneral_getTheAttribNameOfThisAttribId(byte attribId);
        short AttribGeneral_getTheThisAttribFullValue(byte attribId);

        //ATTRIB WINDOW PRESENTERS
        string[] AttribEnchWindow_collectTheCharDetailedAttributes(); //ONLY FROM ATTRIB DATAS
        DataTable AttribEnchWindow_collectTheCharUnspentAttribEnchances(); //ONLY FROM ATTRIB ENCHANCES
        DataTable AttribEnchWindow_collectTheCharSpentAttribEnchances();  //ONYL FROM UNSPENT POINTS
            //string convertAttribPointType_ToLevelChategories_SpentCase(CharOneAttribEnch act);
            //string convertAttribPointType_ToLevelChategories_UnspentCase(byte actPoint)

        //ATTRIB WINDOW PROCESSES
        void AttribEnchWindow_saveTheNewAttribEnchancement(string attribNameTitle, string typeChategoryText,
            int dscpIndex1, int dscpLevel1, int fromSpecIndex1,
            int dscpIndex2, int dscpLevel2, int specIndex2);
        void AttribEncWindow_removeTheAttribEnchancement(int dscpIndex1, bool isItSpec1,
            int dscpIndex2, bool isItSpec2, int attribEnchIndex);
        List<string> AttribEnchWindow_collectAttribListToUpgrade(string attribNameFromPoint1, string attribNameFromPoint2,
            string typeText1, string typeText2);

            //byte reverseUnspentAttribPointLevelChategories_ToTypeId(string text);
            //List<string> adjustChosableAttrib_OneAndGroups(byte attrib1, byte attrib2, byte type1, byte type2);
            //List<string> adjustChoosableAttribGroup(List<string> contain, byte groupStarterId1);
            //List<string> adjustChosableAttribAll(List<string> contain);
        #endregion

        #region JP HANDLE

        //WITH GENERAL PURPOSE - JPs
        int JPGeneralInfo_countTheSumCollectedJP(int starterJPFromTrunk);    //CHAR-DSCP REPO
        int JPGeneralInfo_countTheSumSpentJP();        //CHAR-DSCP REPO
        int JPGeneralInfo_countTheSumAvailableJP(int starterJPFromTrunk);    //CHAR-DSCP REPO

        //JP_MANAGER WINDOW - PRESENTERS
        DataTable JPManagerWindow_collectTheJPGains();      //FROM CHAR-JPGAIN REPO
        void JPManagerWinodw_saveTheNewJPPortion(int gainedJP);     //FROM CHAR-JPFAIN REPO
        void JPManagerWindow_removeTheExistingJPPortion(int jpGainId);      //FROM CHAR-JPGAIN REPO

        #endregion
    }
}
