using ChaosRpgCharGen.CharModel;
using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.CharRepository1;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.Service
{
    public class ManageCharactService : IManageCharactService
    {
        private CharAttributes theCharAttribRepo;
        private CharDisciplines theCharDscpRepo;
        private CharJPGaining theCharJpGainRepo;

        private CoreAttribsRacesSizesRepo theCoreAttribSizesRepo;
        private CoreDisciplinesTypesRepo theCoreDscpTypeRepo;
        private CoreJPLevelsSchemasRepo theCoreJpLevelsSchemaRepo;

        private int theCharId_ThatUnderHandle;
        /// <summary>
        /// CONSTRUCTOR OF MANAGER SERVICE
        /// </summary>
        /// <param name="charId">charId that is under manage</param>
        public ManageCharactService(int charId)
        {
            try
            {
                theCharAttribRepo = new CharAttributes(charId);
                theCharDscpRepo = new CharDisciplines(charId);
                theCharJpGainRepo = new CharJPGaining(charId);

                theCoreAttribSizesRepo = new CoreAttribsRacesSizesRepo();
                theCoreDscpTypeRepo = new CoreDisciplinesTypesRepo();
                theCoreJpLevelsSchemaRepo = new CoreJPLevelsSchemasRepo();

                theCharId_ThatUnderHandle = charId;
            }
            catch (CharRepositoryException e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #region MainManagerWindow presenter

        /// <summary>
        /// GETTER OF ATTRIBUTES FULL VALUE COLLECTION
        /// </summary>
        /// <returns>attrib values</returns>
        public short[] ManagerWindow_getTheCharFullAttributes()
        {
            try
            {
                return theCharAttribRepo.getTheAttributesCollectively_ToShowFinal();
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// SHOWS ALL THE DSCP'S DATAS IN sHORT AND TOP LEVEL
        /// </summary>
        /// <returns>DataTable of dscpSurface</returns>
        public DataTable ManagerWindow_collectAllTheDscpSurface()
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn dscpInd = new DataColumn("Sorsz", typeof(int));
                DataColumn dscpName = new DataColumn("Jártasság", typeof(string));
                DataColumn dscpLevel = new DataColumn("Szint", typeof(byte));
                DataColumn dscpJP = new DataColumn("JP", typeof(int));
                DataColumn dscpNote = new DataColumn("Leírás", typeof(string));
                DataColumn dscpSpec = new DataColumn("Spec", typeof(string));
                dt.Columns.AddRange(new DataColumn[] { dscpInd, dscpName, dscpLevel, dscpJP, dscpNote, dscpSpec });

                foreach (CharDscpSurfaceAboutLevels entity in theCharDscpRepo.collectAllDisciplSurface())
                {
                    dt.Rows.Add(new object[] { entity.theDscpIndex,
                    theCoreDscpTypeRepo.findTheDscpName(entity.theDscpId), entity.theDscpTopLevel,
                    entity.theDscpJp, entity.theDscpNote, entity.theDscpHasSpec?"Van":"Nincs"}
                    );
                }

                return dt;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscpHandle - General
        /// <summary>
        /// ANALYZE THE AVAILABLE JP POOL AND DEFINE THE IS THERE ENOUGH TO GAIN LEVEL
        /// IT IS FOR TO SHOW THE USER THE RESULT AS WELL
        /// </summary>
        /// <param name="starterJpOfCharacter">starterJP of character</param>
        /// <param name="neededJPForDscpLevel">JP of level</param>
        /// <returns></returns>
        public bool DscpGeneralInfo_isThereEnoughJPToGetLevel(int starterJpOfCharacter, int neededJPForDscpLevel)
        {
            try
            {
                if (JPGeneralInfo_countTheSumAvailableJP(starterJpOfCharacter) >= neededJPForDscpLevel)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REVISE THE DSCP AT PROCESS IS BENEFITIAL - ORIGINALLY OR CHAR CHOOSE
        /// </summary>
        /// <param name="selectedDscpType">dscpId of dscp under process</param>
        /// <param name="charPrefBenefDscpType">chosen char-benefDscpType</param>
        /// <returns></returns>
        public bool DscpGeneralInfo_defineIfNewDscpIsBeneficial_AtNewAndGainUpLvl(byte selectedDscpType, byte charPrefBenefDscpType)
        {
            try
            {
                if (selectedDscpType == charPrefBenefDscpType || theCoreDscpTypeRepo.isThisDscpStudyIsOriginallyBeneficial(selectedDscpType))
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REMOVES SOME LEVEL FROM A DSCP - NORM AND SPEC AND WHOLE DSCP
        /// </summary>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="levelFrom">level from need to remove</param>
        /// <param name="isItSpec">is it a specialisation</param>
        /// <param name="specIndex">specIndex</param>
        public void DscpGeneralProcess_RemoveSelectedLevel(int dscpIndex, byte levelFrom, byte specIndex)
        {
            try
            {
                //COLLECT THE ALL ATTRIB ENCH-S THAT NEED TO REMOVE
                int[] attribEnchantsNeedToDelet = theCharDscpRepo.collectTheAttribEnchOfThisDscpIndexFromLevel_UnderDirectDeletion(dscpIndex, levelFrom, specIndex);

                //COLLECT DSCP-INDEXES ATTRIB-ENCHANT-INDEXES -> TO MODIFY AT SOURCE SIDE AS WELL
                List<int[]> overlappings = theCharAttribRepo.collectAttribEnchThatOverlapsWithThese(attribEnchantsNeedToDelet, dscpIndex);

                //REMOVE THE ATTRIB ENCH OF CHOSEN DSCP
                foreach (int ench in attribEnchantsNeedToDelet)
                {
                    theCharAttribRepo.removeOneAttribEnchElement(ench);
                }

                //CORRECT THE DSCP REPO OF (INDIRECT CONNECTED) REMOVED ENCHANTES AT TYPE 1 CASE WITH OVERLAPPING DSCP INDEX
                //NO MEAN CORRECT AT DIRECT CONNECTED DSCP -> THESE WILL BE DELETED
                foreach (int[] overlap in overlappings)
                {
                    theCharDscpRepo.addUnspentAttribPoint_resetAttribSpending_UnknownType(overlap[0], overlap[1]);
                }

                //MODIFY DSCP REPO - DELETE DSCP AND/OR LEVELS
                if (specIndex > 0)
                    theCharDscpRepo.removeThisDsciplSpecFrom(dscpIndex, specIndex, levelFrom);
                else
                {
                    if (levelFrom == 1)
                        theCharDscpRepo.removeThisWholeDiscipl(dscpIndex);
                    else
                        theCharDscpRepo.removeDisciplLevelFrom(dscpIndex, levelFrom);
                }
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscpHandle - brandNew Presenters
        /// <summary>
        /// SHOWS THE CHOSEN DSCP'S JP DIFFICULTY TO STUDY
        /// </summary>
        /// <param name="selectedDscpType">selected dscpType</param>
        /// <param name="charPrefBenefDscpType">char basic beneficDscp</param>
        /// <returns>costing in string</returns>
        public string DscpNewWindow_getTheDscpBasicCosting_ToText(byte selectedDscpType, byte charPrefBenefDscpType)
        {
            try
            {
                if (DscpGeneralInfo_defineIfNewDscpIsBeneficial_AtNewAndGainUpLvl(selectedDscpType, charPrefBenefDscpType))
                    return "Könnyített";
                else
                    return "Normál nehézség";
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SHOWS THE CHOSEN DSCP STUDIABILITY
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>string of studiability</returns>
        public string DscpNewWindow_getTheDscpStudiability_ToText(short dscpId)
        {
            try
            {
                if (theCoreDscpTypeRepo.mustBeMentorAtStrudyThisDscp(dscpId))
                    return "Mentorálás kell";
                else
                    return "Gyakorolható";
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SHOWS THE USER THE DSCP'S JP-SCHEMA OF CHOSEN ATTRIB
        /// </summary>
        /// <param name="dscpId">chosen dscpId</param>
        /// <param name="attribId">chosen attribId</param>
        /// <returns>schema in string</returns>
        public string DscpNewWindow_getTheDscpChosenJPAttribSchemaModifLevelsText_ToShow(short dscpId, byte attribId)
        {
            try
            {
                short temp = theCoreDscpTypeRepo.findTheDscpAttribSchemValue(dscpId, attribId);
                string res = temp.ToString() + "/";
                for (byte i = 1; i < 4; i++)
                {
                    res += temp + (10 * i);
                    if (i != 3)
                        res += "/";
                }
                return res;
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE CHOSEN DSCP'S THE MODIFIER THAT COMES FROM ATTRIBUTE AND JP SCHEMA
        /// </summary>
        /// <param name="levelMark">needed level</param>
        /// <param name="dscpTypeCharChosen">is that beneficial by character?</param>
        /// <param name="isItSpec">is that a specialisation?</param>
        /// <param name="dscpJPSchemaId">schemaId that modifies the dscp study</param>
        /// <param name="connAttribValue">attrib value to modifiy the dscp study</param>
        /// <returns>JP that needs or not need for study</returns>
        public int DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec,
                byte dscpAttribId)
        {
            try
            {
                int theBasicJP = DscpNewWindow_getTheBasicJPForThisDscp(dscpId, levelMark, beneficialDscpIdByCharProp, isItSpec);
                short dscpJPSchemaIdFromDscp = theCoreDscpTypeRepo.findTheDscpAttribSchemValue(dscpId, dscpAttribId);
                short charConnAttribValue = theCharAttribRepo.getTheFullValueOfThisAttribute(dscpAttribId);
                if (!isItSpec)
                    dscpJPSchemaIdFromDscp += theCoreJpLevelsSchemaRepo.findTheSchemaRiserForThisNormalLevel(levelMark);
                short attribSchemaModif = theCoreJpLevelsSchemaRepo.findSchemaModifForThisJPSchema(dscpJPSchemaIdFromDscp, charConnAttribValue);
                long finalJPValue = (theBasicJP * attribSchemaModif) / 100;   //THE MODIFIER COMES - IF DECREASE, + IF INCREASE THE VALUE
                return (int)finalJPValue;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE CHOSEN DSCP'S TRAINING MODIFIER JP VALUE - TO SHOW IT
        /// </summary>
        /// <param name="dscpId">analyzes dscpId</param>
        /// <param name="levelMark">need level</param>
        /// <param name="isThatBeneficialByCharProp">is that beneficial by character?</param>
        /// <param name="isItSpec">is that a specialisation?</param>
        /// <param name="isItMantorated">is that want toy study alone:</param>
        /// <returns>JP that needs for the train alone</returns>
        public int DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp,
            bool isItSpec, bool isItMantorated)
        {
            try
            {
                if (!analyzeOneDscp_getTheDscpStudiability_ToBoolean(dscpId) && !isItSpec)
                {
                    if (!isItMantorated && !analyzeOneDscp_getTheDscpStudiability_ToBoolean(dscpId)) //DSCP MUST BE PRACTICABLE TO CHARGE TRAINING DIFF
                    {
                        int theBasicJP = DscpNewWindow_getTheBasicJPForThisDscp(dscpId, levelMark, beneficialDscpIdByCharProp, isItSpec);
                        short practiceModif = theCoreJpLevelsSchemaRepo.findPracticeModifForThisLevel(levelMark);
                        return (int)(theBasicJP * practiceModif) / 100;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// SHOWS THE ALL DSCP TYPES IN STRING
        /// </summary>
        /// <returns>list of dscpType names</returns>
        public List<string> DscpNewWindow_collectDscpTyes_TextList()
        {
            try
            {
                return theCoreDscpTypeRepo.collectTheAllTypesOfDscps_InString();
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SHOWS THE ALL DSCP NAME FROM THAT TYPE
        /// </summary>
        /// <param name="typeId">seeked typeID</param>
        /// <returns>list of dscpNames</returns>
        public List<string> DscpNewWindow_collectDscpNames_TextList(byte typeId)
        {
            try
            {
                return theCoreDscpTypeRepo.collectTheseTypesOfDscps(typeId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// USER HAS CHOSEN DSCP -> THE REQUIREMENT IS NEEDED TO DEFINE
        /// THE USERTO DEFINE CHOOSES FROM A LIST - THIS CREATES THAT
        /// </summary>
        /// <param name="dscpId">chosen dscpId</param>
        /// <returns>list of string combinations, that describe possibilities</returns>
        public List<string> DscpNewWindow_collectDscpRequir(short dscpId)
        {
            try
            {
                return theCoreDscpTypeRepo.collectThePossibRequirs_OfThisDscpNormalLevels_InStringCombin(dscpId); ;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SHOWS THE ATTRIB NAMES TO USER THAT CONNECTED THIS DSCP
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>list of attrib names</returns>
        public List<string> DscpNewWindow_collectDscpAttirb(short dscpId)
        {
            try
            {
                List<CoreDscpAttribRequir> depend = theCoreDscpTypeRepo.collectTheAllAttribsOfThisDscp(dscpId);
                List<string> temp = new List<string>();
                foreach (CoreDscpAttribRequir d in depend)
                {
                    temp.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(d.theAttribId));
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscpHandle - brandNew processes
        /// <summary>
        /// FINDS IF A DSCP NEEDS A MENTOR TO STUDY
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>true=mustHave / false=notNeeded</returns>
        private bool analyzeOneDscp_getTheDscpStudiability_ToBoolean(short dscpId)
        {
            return theCoreDscpTypeRepo.mustBeMentorAtStrudyThisDscp(dscpId);
            
        }
        /// <summary>
        /// SEEKS BACK THE CHOSEN DSCPTYPE ID BY USER - TO COLELCT THE DSCP IN THAT TYPE
        /// </summary>
        /// <param name="typeName">typeName</param>
        /// <returns>typeId</returns>
        public byte DscpNewWindow_getTheDscpTypeId(string typeName)
        {
            try
            {
                return theCoreDscpTypeRepo.findTheDscpTypeIdOfThisTypeText(typeName);
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS BACK THE CHOSEN DSCPID BY USER - TO READ IN DETAIILS AND SAVES THE DSCP-GIVING UP
        /// </summary>
        /// <param name="dscpName">seeked dscpName</param>
        /// <returns>dscpId</returns>
        public short DscpNewWindow_getTheDscpId(string dscpName)
        {
            try
            {
                return theCoreDscpTypeRepo.findTheDscpIdOfThisDscpText(dscpName);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS THE REQUIR GROUP ID OF A CHOSEN DSCP'S CHOSEN REQUIR
        /// </summary>
        /// <param name="dscpId">chosen dscpId</param>
        /// <param name="dscpCombText">text of chosen requirDescr</param>
        /// <returns>requirGroupId</returns>
        public byte DscpNewWindow_findTheDscpChosenRequirGroupId(short dscpId, string dscpCombText)
        {
            try
            {
                return theCoreDscpTypeRepo.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(dscpId, dscpCombText);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS THE CHOSEN ATTIB SCHEMA CONNECTED THE CHOSEN DSCP
        /// </summary>
        /// <param name="attribName">chosen attribName</param>
        /// <returns>attribSchema connected the spec dscp and chosen attrib</returns>
        public byte DscpNewWindow_getTheDscpAttibId(string attribName)
        {
            try
            {
                return theCoreAttribSizesRepo.findTheAttriIdThisAttribName(attribName);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// ADDS BRAND NEW SKILL TO THE CHARACTER
        /// </summary>
        /// <param name="dscpId">dscpId</param>
        /// <param name="chosenRequirGroup">chosen requiremGroup</param>
        /// <param name="attribId">chosen attribId</param>
        /// <param name="descr">given description</param>
        /// <param name="jpCost">starter JPcost</param>
        /// <param name="trainMentorOrPract">trained by mentor or alone</param>
        public void DscpNewWindow_GainUpBrandNewDscp(short dscpId, byte chosenRequirGroup, byte attribId,
             string descr, short jpCost, bool trainMentorOrPract)
        {
            try
            {
                theCharDscpRepo.addNewDiscipl(dscpId, theCoreDscpTypeRepo.findTheTypeIdOfDscp(dscpId),
                    attribId, descr, trainMentorOrPract, jpCost, chosenRequirGroup);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// IT DEFINES THE BASIC COST OF A DSCP LEVEL - SHOW THAT THE USER
        /// IT IS FOR TO SHOW THE USER THE RESULT AS WELL
        /// </summary>
        /// <param name="levelMark">the level</param>
        /// <param name="isThatSummariselyBeneficial">is that benefitial</param>
        /// <param name="isItSpec">is that specialisation</param>
        /// <returns>valus basic JP</returns>
        public int DscpNewWindow_getTheBasicJPForThisDscp(short dscpId, byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec)
        {
            try
            {
                byte typeOfSelectedDscp = theCoreDscpTypeRepo.findTheTypeIdOfDscp(dscpId);
                bool isThatSummariselyBeneficial = DscpGeneralInfo_defineIfNewDscpIsBeneficial_AtNewAndGainUpLvl(
                    typeOfSelectedDscp, beneficialDscpIdByCharProp);
                if (isItSpec)
                {
                    if (isThatSummariselyBeneficial)
                        return theCoreJpLevelsSchemaRepo.findTheSpecLevelBeneficJP(levelMark);
                    else
                        return theCoreJpLevelsSchemaRepo.findTheSpecLevelAverageJP(levelMark);
                }
                else
                {
                    if (isThatSummariselyBeneficial)
                        return theCoreJpLevelsSchemaRepo.findTheNormalLevelBeneficJP(levelMark);
                    else
                        return theCoreJpLevelsSchemaRepo.findTheNormalLevelAverageJP(levelMark);
                }
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// IT DEFINES THE FINAL COST OF A DSCP LEVEL - SHOw THE USER
        /// IT IS FOR TO SHOW THE USER THE RESULT AS WELL
        /// </summary>
        /// <param name="dscpId">dscpId - define if it is practicable</param>
        /// <param name="levelMark">the level</param>
        /// <param name="beneficialDscpIdByCharProp">is that benefitial</param>
        /// <param name="isItSpec">is that specialisation</param>
        /// <param name="chosenAttribId">attribId that dscp uses</param>
        /// <param name="charConnAttribValue">attrib value of character that is connected to dscp</param>
        /// <param name="isItMentorated">is that practiced</param>
        /// <returns>value final JP</returns>
        public int DscpNewWindow_calculateTheFinalJPForThisDscp(short dscpId, byte levelMark,
            byte beneficialDscpIdByCharProp, bool isItSpec, byte chosenAttribId, bool isItMentorated)
        {
            try
            {
                //DATA-ERROR TESTING
                if (isItSpec && levelMark > 2)
                    throw new ManageCharactServException("A jártasságspecializációnak nincs ilyen magas szintje!");
                if (levelMark < 1)
                    throw new ManageCharactServException("A jártasság szintje nem lehet negítív értékű!");

                //DEFINE IF STUDY IS PERMITTED
                bool neededMentorToStudy = analyzeOneDscp_getTheDscpStudiability_ToBoolean(dscpId);
                if (neededMentorToStudy && !isItMentorated && !isItSpec)
                    return 0;
                //throw new ManagerServiceException("A jártasság felvételéhez mentor segítsége szükséges!");

                //GETTING BASIC JP AND JPSCEMA
                int theBasicJP = DscpNewWindow_getTheBasicJPForThisDscp(dscpId, levelMark, beneficialDscpIdByCharProp, isItSpec);
                short dscpJPSchemaIdFromDscp = theCoreDscpTypeRepo.findTheDscpAttribSchemValue(dscpId, chosenAttribId);
                short charConnAttribValue = theCharAttribRepo.getTheFullValueOfThisAttribute(chosenAttribId);

                //GETTING THE BASIC MODIFIERS IN PERCENTAGE - IT IS NORMAL
                short theModifiers = 100;
                //DSCP MUST BE PRACTICABLE TO CHARGE TRAINING DIFF - IF IT IS NOT SPECIALISATION, THAT HAS NO CHANGE
                if (!isItMentorated && !neededMentorToStudy && !isItSpec)
                    theModifiers += theCoreJpLevelsSchemaRepo.findPracticeModifForThisLevel(levelMark);
                //RISES THE ATTRIB SCHEMA BY THE LEVEL - IF IT IS NOT SPECIALISATION, THAT HAS NO CHANGE
                if (!isItSpec)
                    dscpJPSchemaIdFromDscp += theCoreJpLevelsSchemaRepo.findTheSchemaRiserForThisNormalLevel(levelMark);
                //GETTING THE JPSCHEMA-ATTRIBUTE MODIFIER
                theModifiers += theCoreJpLevelsSchemaRepo.findSchemaModifForThisJPSchema(dscpJPSchemaIdFromDscp, charConnAttribValue);
                //throw new Exception("Here " + theBasicJP + " " + theModifiers + " " + typeOfSelectedDscp + " " + dscpJPSchemaIdFromDscp + " " + charConnAttribValue);

                //CALUCLATE
                int theFinalJP = (theBasicJP * theModifiers) / 100;
                return theFinalJP;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// ANALYZE WHEATHER THE REQUIREMNETS OF DSCP-ID ARE THERE ACCORDING TO THE CHOSEN REQUIR-GROUP
        /// IT IS FOR TO SHOW THE USER THE RESULT AS WELL
        /// </summary>
        /// <param name="dscpIndex">dscpId, that would need</param>
        /// <param name="dscpReqGroupToAnalyzeBy">requirGroup from all, with that define the requir</param>
        /// <param name="dscpNote">dscp description for strinct identif</param>
        /// <param name="neededDscpLevel">needed level to gain</param>
        /// <param name="dscpIndexToSpec">is this a specialisation? if yes dscpIndex needed</param>
        /// <returns>0=noResult / 1=allrigth / 2=noAllRequir / 3=topLevel</returns>
        public byte DscpNewWindow_areThereAllRequirement(short dscpIdThatWouldNeed, byte dscpReqGroupToAnalyzeBy,
            byte neededDscpLevel)
        {
            try
            {
                if (neededDscpLevel > 10)   //IT IS AT MAX LEVEL
                    return 3;
                if (dscpReqGroupToAnalyzeBy == 0)   //NO REQUIREMENT GROUP THERE, IT IS 0 -> NO REQUIREMENT
                    return 1;
                //EXAMINE THE DSCP REALLY HAS REQUIREMENTS
                if (theCoreDscpTypeRepo.isThereRequirementofThisDscp(dscpIdThatWouldNeed))
                {
                    return analyzeForThisNormDscpRequirLevelRequirAreThere(dscpIdThatWouldNeed, dscpReqGroupToAnalyzeBy, neededDscpLevel);
                }
                return 0;   //NO REAL RESULT
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD FOR NORMAL LEVEL REQUIREMENT REVISION
        /// ANALYZE ARE THERE ALL THE DSCP REQUIREMENT
        /// </summary>
        /// <param name="dscpId">dscpId under process</param>
        /// <param name="dscpReqGroup">needed requireGroupId</param>
        /// <param name="levelNeeded">level under process</param>
        /// <param name="isSevere">the analyte is needed severe</param>
        /// <returns>1=allrigth / 2=noAllRequir </returns>
        private byte analyzeForThisNormDscpRequirLevelRequirAreThere(short dscpId, byte dscpReqGroup, byte levelNeeded)
        {
            try
            {
                byte requirParts = 0;
                CoreDscpNormalRequir demandRequir = theCoreDscpTypeRepo.findTheDscpRequirementOfThisDscp_Entity(dscpId, dscpReqGroup);
                requirParts = (byte)demandRequir.theDisciplRequirElementId.Length;
                for (byte i = 0; i < demandRequir.theDisciplRequirElementId.Length; i++)
                {
                    if (demandRequir.theRequirIsADscpGroup[i])  //A DSCP TYPE IS THE ACT REQUIR
                    {
                        if (theCharDscpRepo.findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(
                            demandRequir.theDisciplRequirElementId[i], levelNeeded))
                        {
                            requirParts--;
                        }
                    }
                    else
                    {           //AN INDIVIDUAL DSCP IS THE ACT REQUIR
                        if (theCharDscpRepo.findThisDscpRequir_IndividualHaveTheLevel_NormalCase(
                            demandRequir.theDisciplRequirElementId[i], levelNeeded,
                            theCoreDscpTypeRepo.mustTreathSevereTheRequirForThisDscp(dscpId)))
                        {
                            requirParts--;
                        }
                    }
                }
                if (requirParts == 0)
                    return 1;
                else
                    return 2;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscpHandle - gainLevel presenter
        /// <summary>
        /// SHOWS A DSCP'S ALL NORMAL LEVEL LAYERS - AT DETAILED WIONDOW
        /// </summary>
        /// <param name="dscpIndex">seeked dscpIndex</param>
        /// <returns>list of layers</returns>
        public DataTable DscpRiseSpec_collectThisDscpNormalLevelsDeep(int dscpIndex)
        {
            try
            {
                List<CharDscpLevel> dscpNorm = theCharDscpRepo.collectThisDisciplLayers(dscpIndex);

                DataTable res = new DataTable();
                DataColumn level = new DataColumn("Szint", typeof(int));
                DataColumn jp = new DataColumn("Jp", typeof(int));
                DataColumn mentor = new DataColumn("Mentor", typeof(string));
                DataColumn attribRise = new DataColumn("Növelt főértéket?", typeof(string));

                res.Columns.AddRange(new DataColumn[] { level, jp, mentor, attribRise });
                foreach (CharDscpLevel row in dscpNorm)
                {
                    res.Rows.Add(new object[] { row.theLevelMark, row.theLevelJP, row.theWayOfStudy ? "Volt" : "Nélkül",
                    row.theAttribRiseIsSpent == 0 ? "Nem" : "Igen" });
                }

                return res;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        public DataTable DscpRiseSpec_collectThisDscpSpecLevelsDeep(int dscpIndex)
        {
            try
            {
                List<CharDscpSpecLevel> dscpSpec = theCharDscpRepo.collectThisDisciplSpecAllLayers(dscpIndex);

                DataTable res = new DataTable();
                DataColumn specIndex = new DataColumn("Sorsz", typeof(int));
                DataColumn level = new DataColumn("Szint", typeof(int));
                DataColumn jp = new DataColumn("Jp", typeof(int));
                DataColumn area = new DataColumn("Terület", typeof(string));
                DataColumn descr = new DataColumn("Szűken", typeof(string));
                DataColumn attribRise = new DataColumn("Növelt főértéket?", typeof(string));

                res.Columns.AddRange(new DataColumn[] { specIndex, level, jp, area, descr, attribRise });

                if (dscpSpec.Count > 0)
                {
                    foreach (CharDscpSpecLevel row in dscpSpec)
                    {
                        res.Rows.Add(new object[] { row.theSpecIndex, row.theLevelMark, row.theLevelJP,
                        screenAndGetTheDscpSpecAreaText(dscpIndex, row.theSpecArea),
                        row.theSpecDescr, row.theSpecLevelAttribPointSpent == 0? "Nem":"Igen" });
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// HELPER METHOD TO DSCPInDeep - SHOW THE USER THE SPEC AREA IN TEXT
        /// </summary>
        /// <param name="dscpIndes">dscpIndex of that area is seeked</param>
        /// <param name="areaId">the areaId, that is needed</param>
        /// <returns>area in text</returns>
        private string screenAndGetTheDscpSpecAreaText(int dscpIndes, byte areaId)
        {
            short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndes);
            return theCoreDscpTypeRepo.findTheSpecAreaTextForThisDscp(dscpId, areaId);

        }

        /// <summary>
        /// COLLECTS THE DATAS TO SHOW THE USER ON DSCPRISESPEC WINDOW
        /// </summary>
        /// <param name="dscpIndex">chosen dscpIndex</param>
        /// <returns></returns>
        public string[] DscpRiseSpec_collectThisDscpSeveralDetails(int dscpIndex)
        {
            try
            {
                int[] details = theCharDscpRepo.collectDisciplDetails_ToShow(dscpIndex);
                string[] coll = new string[11];
                coll[0] = theCoreDscpTypeRepo.findTheDscpName((short)details[0]);   //NAME FROM DSCP ID
                coll[1] = theCoreDscpTypeRepo.findTheDscpTypeName((short)details[0]);   //TYPE FROM DSCP ID
                coll[2] = details[3].ToString();                                    //ACT LEVEL
                coll[3] = theCoreAttribSizesRepo.findTheNameOfThisAttrib((byte)details[1]);     //ATTRIB NAME FROM DSCP ATTRIB ID
                coll[4] = theCharAttribRepo.getTheFullValueOfThisAttribute((byte)details[1]).ToString();    //CHAR ATTRIB VALUE FROM DSCP ATTRIB ID

                coll[5] = DscpNewWindow_getTheDscpChosenJPAttribSchemaModifLevelsText_ToShow((short)details[0], (byte)details[1]);    //DSCP'S SCHEMA BY CHOSEN ATTRIB ID

                coll[6] = theCoreDscpTypeRepo.mustBeMentorAtStrudyThisDscp((short)details[0])
                    ? "Csak mentorral" : "Önállóan is gyakrolható";                             //MENTOR IS ESSENTIAL  
                coll[7] = theCoreDscpTypeRepo.mustTreathSevereTheRequirForThisDscp((short)details[0])
                    ? "Szigorúan vett" : "Nem szigorú";                                     //DSCP STUDY SEVERITY
                CoreDscpNormalRequir temp = theCoreDscpTypeRepo.findTheDscpRequirementOfThisDscp_Entity((short)details[0], (byte)details[2]);
                string requir = "";
                for (byte i = 0; i < temp.theDisciplRequirElementId.Length; i++)
                {
                    if (temp.theDisciplRequirElementId[i] == 0)
                        requir += "Nincs";
                    else
                    {
                        if (temp.theRequirIsADscpGroup[i])
                            requir += theCoreDscpTypeRepo.findTheDscpTypeName(temp.theDisciplRequirElementId[i]);
                        else
                            requir += theCoreDscpTypeRepo.findTheDscpName(temp.theDisciplRequirElementId[i]);
                        if (temp.theDisciplRequirElementId.Length > 1)
                            requir += " + ";
                    }
                }
                coll[8] = requir;   //DSCP REQUIREMENT IN TEXT
                coll[9] = details[4].ToString();    //SUM JP IN THE DSCP
                coll[10] = theCharDscpRepo.findThisDscpNoteOfThisDscp(dscpIndex);   //DSCP NOTE
                return coll;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECT THE POSSIBLE SPEC AREAS THAT CONNECTED TO THAT
        /// IN CASE LACK OF CONTENT IF GIVES NULL ARRAY
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <returns></returns>
        public string[] DscpRiseSpec_collectAllProperSpecAreasToGainNew(int dscpIndex)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return theCoreDscpTypeRepo.collectThePossibleSpecAreasForThisDscp_ToShowTheUser(dscpId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE NEXT LEVEL OF A DSCP AT NORMAL OR SPEC FIELD
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public byte DscpRiseSpec_getTheNextDscpNormOrSpecLevel(int dscpIndex, byte specIndex)
        {
            try
            {
                return theCharDscpRepo.getTheNextLevelMark(dscpIndex, specIndex);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE DSCP IF IT IS STUDAIBLE ONLY WITH THE HELP OF MENTOR
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <returns></returns>
        public bool DscpRiseSpec_helpOfMentorIsNeeded(int dscpIndex)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return theCoreDscpTypeRepo.mustBeMentorAtStrudyThisDscp(dscpId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECT THE REQUIREMENTS OF A SPEC BY ITS SPEC INDEX
        /// IT IS NEEDED TO DESCRIBE HERE BECAUSE IT HAS ATTRIBUTE DETAILS AS WELL
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="dscpSpecIndex"></param>
        /// <returns></returns>
        public string[] DscpRiseSpec_getTheSelectedSpecPossibleRequirs(int dscpIndex)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                List<CoreDscpSpec> specAndRequirem = theCoreDscpTypeRepo.collectTheChosenSpecRequirementsForThisDscp(dscpId);

                string[] tempRes = new string[specAndRequirem.Count];
                string row = "";

                for (byte i = 0; i < specAndRequirem.Count; i++)
                {
                    row = (i + 1).ToString() + ") ";
                    for (byte j = 0; j < specAndRequirem[i].theDisciplSpecRequirId.Length; j++)
                    {
                        if (specAndRequirem[i].theDisciplRequirType[j] == 0)   //ITS A DSCP ENTITY
                        {
                            row += theCoreDscpTypeRepo.findTheDscpName(specAndRequirem[i].theDisciplSpecRequirId[j]);
                            row += " " + specAndRequirem[i].theDisciplReuirLevel[j].ToString();
                            row += " sz.";
                        }
                        else if (specAndRequirem[i].theDisciplRequirType[j] == 1) //ITS AN ATTRIB
                        {
                            row += theCoreAttribSizesRepo.findTheNameOfThisAttrib(
                                (byte)specAndRequirem[i].theDisciplSpecRequirId[j]);
                            row += " tulajdonság";
                            row += " " + specAndRequirem[i].theDisciplReuirLevel[j].ToString();
                            row += " érték";
                        }
                        else if (specAndRequirem[i].theDisciplRequirType[j] == 2) //ITS A DSCP TYPTE GROUP
                        {
                            row += theCoreDscpTypeRepo.findTheTypeNameByThisTypId(
                                (byte)specAndRequirem[i].theDisciplSpecRequirId[j]);
                            row += " jártasságok";
                            row += " " + specAndRequirem[i].theDisciplReuirLevel[j].ToString();
                        }
                        if (j < (specAndRequirem[i].theDisciplSpecRequirId.Length - 1))
                            row += "+";
                    }
                    tempRes[i] = row;
                }

                return tempRes;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscpHandle - gainLevel processes
        /// <summary>
        /// FINDS THE VALUE OF FINAL ATTRIB-SCHEMA MODIFIER IN JP
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="nextLevel"></param>
        /// <param name="charBenefDscpType"></param>
        /// <param name="isItSpec"></param>
        /// <param name="dscpAttribType"></param>
        /// <returns></returns>
        public int DscpRiseSpec_getTheAttribSchemaJPModif(int dscpIndex, byte nextLevel,
           byte charBenefDscpType, bool isItSpec, byte dscpAttribType)
        {
            try
            {
                short managedDscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(managedDscpId,
                    nextLevel, charBenefDscpType, isItSpec, dscpAttribType);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE VALUE OF TRAINING MODIFIER IN JP 
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="nextLevel"></param>
        /// <param name="charBenefDscpType"></param>
        /// <param name="isItSpec"></param>
        /// <param name="isItPracticed"></param>
        /// <returns></returns>
        public int DscpRiseSpec_getTheTrainJPModif(int dscpIndex, byte nextLevel,
            byte charBenefDscpType, bool isItSpec, bool isItPracticed)
        {
            try
            {
                short managedDscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(managedDscpId,
                    nextLevel, charBenefDscpType, isItSpec, isItPracticed);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE DSCP-ID OF A SPECIFIC DSCP BY ITS DSCP-INDEX
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <returns></returns>
        public short DscpRiseSpec_getTheDscpIdOfThisDscp(int dscpIndex)
        {
            try
            {
                return theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE ATTRIB-ID THAT CONNECTED TO DSCP, BY ITS DSCP-INDEX
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <returns></returns>
        public byte DscpRiseSpec_getTheAttribIdOfThisDscp(int dscpIndex)
        {
            try
            {
                return theCharDscpRepo.findTheDscpAttribForThisDscp(dscpIndex);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REVISE THE DSCP IF IT HAS SPECIALISATION BY ITS DSCP-INDEX
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <returns></returns>
        public bool DscpRiseSpec_isThereSpecialization(int dscpIndex)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return theCoreDscpTypeRepo.isThereSpecialisationForThisDscp(dscpId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE BASIC JP VALUE OF THE NEXT LEVEL OF THE EXISTIng DSCP
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="levelMark"></param>
        /// <param name="beneficialDscpIdByCharProp"></param>
        /// <param name="isItSpec"></param>
        /// <returns></returns>
        public int DscpRiseSpec_getTheBasicJPForThisDscp(int dscpIndex, byte levelMark, byte beneficialDscpIdByCharProp, bool isItSpec)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return DscpNewWindow_getTheBasicJPForThisDscp(dscpId, levelMark, beneficialDscpIdByCharProp, isItSpec);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// CALCULATE THE FINAL JP OF THE NEXT LEVEL OF THE EXISTING DSCP
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="levelMark"></param>
        /// <param name="beneficialDscpIdByCharProp"></param>
        /// <param name="isItSpec"></param>
        /// <param name="chosenAttribId"></param>
        /// <param name="isItMentorated"></param>
        /// <returns></returns>
        public int DscpRiseSpec_calculateTheFinalJPForThisDscp(int dscpIndex, byte levelMark,
            byte beneficialDscpIdByCharProp, bool isItSpec, byte chosenAttribId, bool isItMentorated)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                return DscpNewWindow_calculateTheFinalJPForThisDscp(dscpId, levelMark, beneficialDscpIdByCharProp,
                    isItSpec, chosenAttribId, isItMentorated);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        
        /// <summary>
        /// EXAMINE THE CHAR DSCP-POOL IF THERE ARE THE NEEDED REQUIREMENT OF THAT ONE
        /// </summary>
        /// <param name="managedDscpIndex"></param>
        /// <param name="neededDscpLevel"></param>
        /// <returns></returns>
        public byte DscpRiseSpec_areThereAllRequirement_GainMoreNormalLvl(int managedDscpIndex, byte neededDscpLevel)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(managedDscpIndex);
                byte dscpReqGroupToAnalyzeBy = theCharDscpRepo.findTheDscpRequirGroupIfOdThisDscp(managedDscpIndex);
                return DscpNewWindow_areThereAllRequirement(dscpId, dscpReqGroupToAnalyzeBy, neededDscpLevel);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        
        /// <summary>
        /// ANALYZE WHEATHER THE REQUIREMNETS OF DSCP-ID ARE THERE ACCORDING TO THE CHOSEN REQUIR-GROUP
        /// IT IS FOR TO SHOW THE USER THE RESULT AS WELL
        /// </summary>
        /// <param name="dscpIndexThatManagedToSpec"></param>
        /// <param name="neededDscpLevel"></param>
        /// <param name="dscpReqGroupToAnalyzeBy"></param>
        /// <param name="areaText"></param>
        /// <param name="dscpSpecNote"></param>
        /// <returns>0=noResult / 1=allrigth / 2=noAllRequir / 3=topLevel</returns>
        public byte DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(int dscpIndexThatManagedToSpec, byte neededDscpLevel,
            byte dscpReqGroupToAnalyzeBy)
        {
            try
            {
                short dscpIdThatWouldNeed = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndexThatManagedToSpec);
                //EXAMINE THE DSCP ITSELF IF IT HAS THE PREVIOUS LVL 1 AT CASE LVL 2 IS NEEDED
                if (neededDscpLevel > 1)  //IT IS ALREADY AT MAX SPEC LEVEL
                    return 3;
                if (theCoreDscpTypeRepo.isThereSpecialisationForThisDscp(dscpIdThatWouldNeed))
                    return analyzeForThisSpecTheLevelRequirAreThere(dscpIdThatWouldNeed, dscpReqGroupToAnalyzeBy, neededDscpLevel);
                return 0;   //NO SPECIALISATION TO MASTER
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + " " + e.Message);
            }
        }
        /// <summary>
        /// REVISIES THE CHAR DSCP REPO IF IT HAS THE REQUIREMENTS OF MORE LEVELS OF A SPECIFIC SPECIALIZATION
        /// </summary>
        /// <param name="dscpIndexThatManagedToSpec"></param>
        /// <param name="neededDscpLevel"></param>
        /// <param name="specIndexToManage"></param>
        /// <returns></returns>
        public byte DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(int dscpIndexThatManagedToSpec, byte neededDscpLevel,
            byte specIndexToManage)
        {
            try
            {
                //EXAMINE THE DSCP ITSELF IF IT HAS THE PREVIOUS LVL 1 AT CASE LVL 2 IS NEEDED
                if (neededDscpLevel == 2)
                {
                    short dscpIdThatWouldNeed = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndexThatManagedToSpec);
                    byte tempActSpecLevel = theCharDscpRepo.findTheMaxSpecLevelOfThisDscp_ToDefineIfSpecPermitted(
                        dscpIndexThatManagedToSpec, specIndexToManage);
                    if (tempActSpecLevel == 0)    //IT STILL DONT HAVE SPEC LEVEL 1 - IT IS HERE JUST IN CASE
                        return 2;
                    byte dscpReqGroupToAnalyzeBy = theCharDscpRepo.findTheSpecRequirIdOfThisDscpSpec_ToDefineIfSpecPermitted(
                        dscpIndexThatManagedToSpec, specIndexToManage);
                    return analyzeForThisSpecTheLevelRequirAreThere(dscpIdThatWouldNeed, dscpReqGroupToAnalyzeBy, neededDscpLevel);
                }
                if (neededDscpLevel > 2 || neededDscpLevel < 2)  //IT IS ALREADY AT MAX SPEC LEVEL OR LESS THAN PREFERRED HERE
                    return 3;
                return 0;   //NO SPECIALISATION TO MASTER
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// SEEKS BACK THE SPEC-REQUIR GROUP-ID TO HAVE HE NEW SPEC
        /// </summary>
        /// <param name="dscpIndex">the dscpIndex that is under process</param>
        /// <param name="specRequirText"></param>
        /// <returns></returns>
        public byte DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(int dscpIndex, string specRequirText)
        {
            try
            {
                short dscpIdUnderManage = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                specRequirText = specRequirText.Remove(0, 3);
                string[] tempAnalyze = null;
                if (specRequirText.Contains("+"))
                    tempAnalyze = specRequirText.Split('+');
                else
                {
                    tempAnalyze = new string[1];
                    tempAnalyze[0] = specRequirText;
                }
                short[] tempResId = new short[tempAnalyze.Length];
                byte[] tempResType = new byte[tempAnalyze.Length];

                for (byte i = 0; i < tempAnalyze.Length; i++)
                {
                    if (tempAnalyze[i].Contains("jártasságok"))     //TYPE 2
                    {
                        string[] tempParts = tempAnalyze[i].Split(' ');
                        tempResId[i] = theCoreDscpTypeRepo.findTheDscpTypeIdOfThisTypeText(tempParts[0]);
                        tempResType[i] = 2;
                    }
                    else if (tempAnalyze[i].Contains("tulajdonság"))    //TYPE 1
                    {
                        string[] tempParts = tempAnalyze[i].Split(' ');
                        tempResId[i] = theCoreAttribSizesRepo.findTheAttriIdThisAttribName(tempParts[0]);
                        tempResType[i] = 1;
                    }
                    else
                    {   //ITS A SPCEIFIC DSCP TYPE 0
                        int indexLastSace = tempAnalyze[i].LastIndexOf(' ');
                        tempAnalyze[i] = tempAnalyze[i].Remove(indexLastSace);  //THE DSCP-NAME AND LEVEL
                        indexLastSace = tempAnalyze[i].LastIndexOf(' ');
                        tempAnalyze[i] = tempAnalyze[i].Remove(indexLastSace);  //THE DSCP-NAME ONLY
                        tempResId[i] = theCoreDscpTypeRepo.findTheDscpIdOfThisDscpText(tempAnalyze[i]);
                        tempResType[i] = 0;
                    }
                }
                //throw new Exception("Here " + specRequirText);
                return theCoreDscpTypeRepo.findTheSeekedSpecRequir_RequirGroupId(
                    dscpIdUnderManage, tempResId, tempResType);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// SAVES THE NEW LEVEL ABOUT A SPECIFIC DSCP
        /// </summary>
        /// <param name="dscpIndex">dscpId under process</param>
        /// <param name="jp">JP for that level</param>
        /// <param name="isWithMentor">is this with mentor or not</param>
        public void DscpRiseSpec_GainNormalLevel(int dscpIndex, short jp, bool isWithMentor)
        {
            try
            {
                theCharDscpRepo.riseThisDiscipl(dscpIndex, isWithMentor, jp);
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SAVES THE NEW SPEC OR SPEC LEVEL ABOUT A SPECIFIC DSCP
        /// </summary>
        /// <param name="dscpIndex">dscpIf under process</param>
        /// <param name="jp">JP for that level</param>
        /// <param name="specRequirGroup">specRequirGroup</param>
        /// <param name="specAreaText">chosen specArea</param>
        /// <param name="specDescr">chosen specDescr</param>
        /// <param name="thisSpecGivesAttrib">is that gives attrib</param>
        public void DscpRiseSpec_GainNewSpecialisation(int dscpIndex, short jp, byte specRequirGroup, string specAreaText,
            string specDescr)
        {
            try
            {
                short dscpId = theCharDscpRepo.findTheDscpIdForThisDscp(dscpIndex);
                byte specArea = theCoreDscpTypeRepo.findTheChosenSpecAreaIdByItsText(dscpId, specAreaText);
                theCharDscpRepo.addNewDsciplSpecialLevel(dscpIndex, jp, specRequirGroup, specArea, specDescr);
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        public void DscpRiseSpec_GainNewSpecLevel(int dscpIndex, short jp, byte specIndex)
        {
            try
            {
                byte[] tempNumbers = theCharDscpRepo.findSpecRequirGroupAndAreaByItsSpecIndex_AtSpecLvlRise(dscpIndex, specIndex);
                byte specRequirGroup = tempNumbers[0];
                byte specArea = tempNumbers[1];
                string specDescr = theCharDscpRepo.findSpecDescrByItsSpecIndex_AtSpecLvlRise(dscpIndex,specIndex);
                theCharDscpRepo.addNewDsciplSpecialLevel(dscpIndex, jp, specRequirGroup, specArea, specDescr);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD FOR SPEC LEVEL REQUIREMENT REVISION
        /// ANALYZE WHISCH TYIPE OF REQUIREMENT IS NEEDED TO REVISE
        /// </summary>
        /// <param name="dscpId">analyzed dscpId</param>
        /// <param name="specReqGroup">needed requirGroup from all</param>
        /// <param name="levelThatNeeded">needed level</param>
        /// <returns>1=allrigth / 2=noAllRequir </returns>
        private byte analyzeForThisSpecTheLevelRequirAreThere(short dscpId, byte specReqGroup, byte levelThatNeeded)
        {
            try
            {
                //EXAMINE THE PREDEFINED REQUIREMENTS FROM CORE
                CoreDscpSpec requirement = theCoreDscpTypeRepo.findTheSeekedSpecRequir_Entity(dscpId, specReqGroup);
                byte requirParts = (byte)requirement.theDisciplSpecRequirId.Length;
                for (byte i = 0; i < requirement.theDisciplSpecRequirId.Length; i++)
                {

                    if (requirement.theDisciplRequirType[i] == 0)    //AN ONYL DSCP IS THE ACT REQUIRE PART
                    {
                        byte levelThatReallyNeeded = requirement.theDisciplReuirLevel[i];
                        if (requirement.theDisciplSpecNeedToRise[i])
                            levelThatReallyNeeded += theCoreJpLevelsSchemaRepo.findTheRequirementRiserForThisSpecLevel(levelThatNeeded);
                        if (theCharDscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(
                            requirement.theDisciplSpecRequirId[i], levelThatReallyNeeded))
                            requirParts--;
                    }
                    else if (requirement.theDisciplRequirType[i] == 1)   //AN ATTRIB IS THE ACT REQURE PART
                    {
                        short levelRised = requirement.theDisciplReuirLevel[i];
                        if (requirement.theDisciplSpecNeedToRise[i])
                            levelRised += theCoreJpLevelsSchemaRepo.findTheAttributeRiserForThisSpec(levelThatNeeded);
                        if (theCharAttribRepo.getTheFullValueOfThisAttribute((byte)requirement.theDisciplSpecRequirId[i]) >= levelRised)
                            requirParts--;
                    }
                    else if (requirement.theDisciplRequirType[i] == 2)   //A DSCP TYPE IS THE ACT REQUIRE PART
                    {
                        byte levelRised = requirement.theDisciplReuirLevel[i];
                        if (requirement.theDisciplSpecNeedToRise[i])
                            levelRised += theCoreJpLevelsSchemaRepo.findTheRequirementRiserForThisSpecLevel(levelThatNeeded);
                        if (theCharDscpRepo.findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(
                            requirement.theDisciplSpecRequirId[i], levelRised))
                            requirParts--;
                    }
                }
                if (requirParts > 0)
                    return 2;   //code 2 noAllRequir
                else
                    return 1;   //code 1 allRight
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region attributeHandle - general
        /// <summary>
        /// FINDS THE ATTRIB ID BY ITS ATTRIB NAME
        /// </summary>
        /// <param name="attribName">seeked attribName</param>
        /// <returns>attribId</returns>
        public byte AttribGeneral_getTheAttribIdOfThisAttribName(string attribName)
        {
            try
            {
                return theCoreAttribSizesRepo.findTheAttriIdThisAttribName(attribName);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THA ATTRIB NAME BY ITS ATTRIB ID
        /// </summary>
        /// <param name="attribId">seeked attribId</param>
        /// <returns>attribName</returns>
        public string AttribGeneral_getTheAttribNameOfThisAttribId(byte attribId)
        {
            try
            {
                return theCoreAttribSizesRepo.findTheNameOfThisAttrib(attribId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS AN ATTRIB FULL VALUE
        /// </summary>
        /// <param name="attribId">attribId</param>
        /// <returns>attribValue</returns>
        public short AttribGeneral_getTheThisAttribFullValue(byte attribId)
        {
            try
            {
                return theCharAttribRepo.getTheFullValueOfThisAttribute(attribId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region attributeHandle - enchanceWindow presenter
        /// <summary>
        /// COLLECTS THE ATTRIB POINTS IN DETAIL - DISTINCT THE BASE AND RISED VALUES
        /// </summary>
        /// <returns>list of values in details</returns>
        public string[] AttribEnchWindow_collectTheCharDetailedAttributes()
        {
            try
            {
                short[,] temp = theCharAttribRepo.getTheAttrubutesCollectively_ToShowInDetails();
                string[] final = new string[12];
                for (byte i = 0; i < final.Length; i++)
                {
                    final[i] = temp[i, 0].ToString() + " + " + temp[i, 1].ToString();
                }
                return final;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS THE UNSPENT ATTRIB POINTS
        /// </summary>
        /// <returns>table of unspentPoints</returns>
        public DataTable AttribEnchWindow_collectTheCharUnspentAttribEnchances()
        {
            try
            {
                List<CharDscpUnspentAttrib> possibleAttribs = theCharDscpRepo.collectPossiblyUnspentAttribPoints();
                DataTable dt = new DataTable();

                DataColumn index = new DataColumn("Sorsz", typeof(int));
                DataColumn dscp = new DataColumn("Jártasság", typeof(string));
                DataColumn attrib = new DataColumn("Tulajdonsága", typeof(string));
                DataColumn level = new DataColumn("Szintje", typeof(int));
                DataColumn spec = new DataColumn("Specialitás?", typeof(int));
                DataColumn type = new DataColumn("Típusa", typeof(string));
                dt.Columns.AddRange(new DataColumn[] { index, dscp, attrib, level, spec, type });

                foreach (CharDscpUnspentAttrib act in possibleAttribs)
                {
                    dt.Rows.Add(
                        new object[] {  act.theDscpIndex,
                        theCoreDscpTypeRepo.findTheDscpName(theCharDscpRepo.findTheDscpIdForThisDscp(act.theDscpIndex)),
                        theCoreAttribSizesRepo.findTheNameOfThisAttrib(act.theAttribThatConnectedWith),
                        act.theDscpLevel,
                        act.theDscpSpecIndex,
                        convertAttribPointType_ToLevelChategories_UnspentCase(act.thePointType)
                            }
                        );
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// COLLECTS THE DETAILS OF ATTRIB POINT THAT ALREADY SPENT
        /// </summary>
        /// <returns>table of attribEnchList</returns>
        public DataTable AttribEnchWindow_collectTheCharSpentAttribEnchances()
        {
            try
            {
                List<CharOneAttribEnch> tempEnchantList = theCharAttribRepo.collectTheAttributeEnchList_ToShow();
                DataTable dt = new DataTable();

                DataColumn attribEnchId = new DataColumn("Sorsz", typeof(int));
                DataColumn attribName = new DataColumn("Tulajdonság", typeof(string));
                DataColumn type = new DataColumn("Típusa", typeof(string));
                DataColumn dscpInd1 = new DataColumn("Sorsz-1", typeof(int));
                DataColumn dscpName1 = new DataColumn("Jártasság-1", typeof(string));
                DataColumn level1 = new DataColumn("Szint-1", typeof(string));
                DataColumn spec1 = new DataColumn("Specializáció-1?", typeof(string));
                DataColumn dscpInd2 = new DataColumn("Sorsz-2", typeof(int));
                DataColumn dscpName2 = new DataColumn("Jártasság-2", typeof(string));
                DataColumn level2 = new DataColumn("Szint-2", typeof(string));
                DataColumn spec2 = new DataColumn("Specializáció-2?", typeof(string));

                dt.Columns.AddRange(new DataColumn[] { attribEnchId, attribName, type, dscpInd1, dscpName1, level1, spec1,
                dscpInd2, dscpName2, level2, spec2});

                int dscp1Index = 0;
                string dscp1Name = "";
                string dscp1Level = "";
                string dscp1Spec = "";
                int dscp2Index = 0;
                string dscp2Name = "";
                string dscp2Level = "";
                string dscp2Spec = "";
                foreach (CharOneAttribEnch act in tempEnchantList)
                {
                    dscp1Index = 0;
                    dscp1Name = "";
                    dscp1Level = "";
                    dscp1Spec = "";
                    dscp2Index = 0;
                    dscp2Name = "";
                    dscp2Level = "";
                    dscp2Spec = "";

                    //0.->dscpIndex1; 1.->dscpId1; 2.->levelMark1; 3.->norm/spec
                    //4.->dscpIndex2; 5.->dscpId2; 6.->levelMark2; 7.->norm/spec
                    int[] tempDatasFromSkill = theCharDscpRepo.findDetailsSpentAttribPoint(
                        act.theDisciplIndex_First, act.theDisciplIndex_Second, act.theAttribEnchanceIndex);
                    dscp1Index = tempDatasFromSkill[0];
                    dscp1Name += theCoreDscpTypeRepo.findTheDscpName((short)tempDatasFromSkill[1]);
                    dscp1Level += tempDatasFromSkill[2].ToString();
                    dscp1Spec += tempDatasFromSkill[3] == 1 ? "Igen" : "Nem";
                    if (tempDatasFromSkill.Length == 8)
                    {
                        dscp2Index = tempDatasFromSkill[4];
                        dscp2Name += theCoreDscpTypeRepo.findTheDscpName((short)tempDatasFromSkill[5]);
                        dscp2Level += tempDatasFromSkill[6].ToString();
                        dscp2Spec += tempDatasFromSkill[7] == 1 ? "Igen" : "Nem";
                    }

                    string typeName = convertAttribPointType_ToLevelChategories_SpentCase(act);

                    dt.Rows.Add(new Object[] {
                    act.theAttribEnchanceIndex,
                    theCoreAttribSizesRepo.findTheNameOfThisAttrib(act.theAttributeId),typeName,
                    dscp1Index, dscp1Name, dscp1Level, dscp1Spec, dscp2Index, dscp2Name, dscp2Level, dscp2Spec

                });
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// HELPER METHOD TO SHOW THE SPENT ATTRIB RISIES
        /// CONVERTS THE TYPE DEFINITION OF ATTRIB ENCHANCE
        /// ALREADY ENCHANCED TYPES TO SHOW
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        private string convertAttribPointType_ToLevelChategories_SpentCase(CharOneAttribEnch act)
        {
            try
            {
                string point = "";
                if (act.thePointType == 1 && act.theDisciplIndex_Second == 0)
                    point = "4-5-6 sz.(adottra)";
                else if (act.thePointType == 1 && act.theDisciplIndex_Second != 0)
                    point = "4-5-6 sz.(csoportra)";
                else if (act.thePointType == 2)
                    point = "7-8-9 sz.";
                else
                    point = "10.sz";
                return point;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD TO SHOW THE UNSPENT ATTRIB POINTS
        /// CONVERTS THE TYPE DEFINITION OF ATTRIB ENCHANCE
        /// POSSIBLE ENCH TO SHOW
        /// </summary>
        /// <param name="actPoint">pointType</param>
        /// <returns>text of level</returns>
        private string convertAttribPointType_ToLevelChategories_UnspentCase(byte actPoint)
        {
            try
            {
                string point = "";
                if (actPoint == 1)
                    point = "4-5-6 sz.";
                else if (actPoint == 2)
                    point = "7-8-9 sz.";
                else
                    point = "10.sz";
                return point;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region attributeHandle - enchanceWindow processes

        /// <summary>
        /// SAVES AND BOOK DOWN THE ATTRIB SPENDING
        /// </summary>
        /// <param name="attribNameTitle">managed attribName - from combobox</param>
        /// <param name="dscpIndex1">chosen dscpIndex of attribEnch</param>
        /// <param name="dscpLevel1">chosen dscpLevelTexting of attribEnch</param>
        /// <param name="specIndex1">chosen dscpSpecIndex if it is spec</param>
        /// <param name="dscpIndex2">in case type1 dscpIndex</param>
        /// <param name="dscpLevel2">in case type1 dscpLevelText</param>
        /// <param name="specIndex2">in case type1 dscpSpecIndex</param>
        public void AttribEnchWindow_saveTheNewAttribEnchancement(string attribNameTitle, string typeChategoryText, 
            int dscpIndex1, int dscpLevel1, int specIndex1,
            int dscpIndex2, int dscpLevel2, int specIndex2)
        {
            try
            {
                //DATA INTERPRETING
                byte chosenAttribId_toEnchante = theCoreAttribSizesRepo.findTheAttriIdThisAttribName(attribNameTitle);
                byte chosenTypeOfEnchanting = reverseUnspentAttribPointLevelChategories_ToTypeId(typeChategoryText);

                //SAVE TO DB AT APPLIANCE SIDE - GETTING ENCHANT-INDEX
                int enchIndex = theCharAttribRepo.addOnePointRiseToSpecifAttribute(chosenAttribId_toEnchante, dscpIndex1, dscpIndex2, chosenTypeOfEnchanting);
                if (enchIndex == 0)
                    throw new ManageCharactServException("A tulajdonság növekedés adminisztrálásában hiba történt!");

                //SAVE TO DB AT SOURCE SIDE
                theCharDscpRepo.removeUnpentAttribPoint_itIsSpent(dscpIndex1, (byte)dscpLevel1, (byte)specIndex1, enchIndex);
                if (dscpIndex2 != 0)
                    theCharDscpRepo.removeUnpentAttribPoint_itIsSpent(dscpIndex2, (byte)dscpLevel2, (byte)specIndex2, enchIndex);

            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REMOVES THE SPENT ATTRIB POINT - NO VIEW REFRESH
        /// </summary>
        /// <param name="dscpIndex1"></param>
        /// <param name="isItSpec1"></param>
        /// <param name="dscpIndex2"></param>
        /// <param name="isItSpec2"></param>
        /// <param name="attribEnchIndex">attribId from list</param>
        public void AttribEncWindow_removeTheAttribEnchancement(int dscpIndex1, bool isItSpec1, int dscpIndex2, bool isItSpec2, int attribEnchIndex)
        {
            try
            {
                //RESETING THE DSCP-S TO BASE STATE
                theCharDscpRepo.addUnspentAttribPoint_resetAttribSpending(dscpIndex1, isItSpec1, attribEnchIndex);
                if (dscpIndex1 == dscpIndex2 && isItSpec1 != isItSpec2)
                    theCharDscpRepo.addUnspentAttribPoint_resetAttribSpending(dscpIndex1, !isItSpec1, attribEnchIndex);
                if (dscpIndex1 != dscpIndex2 && dscpIndex2 != 0)
                    theCharDscpRepo.addUnspentAttribPoint_resetAttribSpending(dscpIndex2, isItSpec2, attribEnchIndex);

                //REMOVE FROM APPLIANCE SIDE
                theCharAttribRepo.removeOneAttribEnchElement(attribEnchIndex);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// COLLECTS THE ATTRIBUTE NAMES THAT POSSIBLE TO RISE BY SOME DSCP LEVEL
        /// THE USER NEEDS TO CHOOSE ONE OR TWO UNSPENT LIST ELEMENT TO MAINTAIN THIS
        /// IT GOES TO COMBOBOX FOR THE USER TO CHOOSE DESTINATION
        /// </summary>
        /// <param name="attribNameFromPoint1">attribId from list</param>
        /// <param name="attribNameFromPoint2">attribId from list - possible</param>
        /// <param name="typeText1">type, that depends on level</param>
        /// <param name="typeText2">type, that depends in level - possible</param>
        /// <returnsthe>list of string to give the user to choose new attrib ench</returns>
        public List<string> AttribEnchWindow_collectAttribListToUpgrade(string attribNameFromPoint1, string attribNameFromPoint2,
            string typeText1, string typeText2)
        {
            try
            {
                byte realPoint1 = reverseUnspentAttribPointLevelChategories_ToTypeId(typeText1);
                byte realPoint2 = reverseUnspentAttribPointLevelChategories_ToTypeId(typeText2);    //IF IS EMPTY GIVES 0
                byte realAttribId1 = theCoreAttribSizesRepo.findTheAttriIdThisAttribName(attribNameFromPoint1);
                byte realAttribId2 = theCoreAttribSizesRepo.findTheAttriIdThisAttribName(attribNameFromPoint2);     //IF IS EMPTY GIVES 0
                List<string> temp = adjustChosableAttrib_OneAndGroups(realAttribId1, realAttribId2, realPoint1, realPoint2);
                return temp;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// REVERSE THE TYPE DEFINITION OF ATTRIB ENCHACNCE
        /// POSSIBLE ENCH TO PROCESS OF NEW ENCH
        /// </summary>
        /// <param name="text">text of level</param>
        /// <returns>pointType</returns>
        private byte reverseUnspentAttribPointLevelChategories_ToTypeId(string text)
        {
            try
            {
                if (text == "10.sz")
                    return 3;
                else if (text == "7-8-9 sz.")
                    return 2;
                else if (text == "4-5-6 sz.")
                    return 1;
                else
                    return 0;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPRE METHOD TO ADD CHOSABLE ATTRIB ID TO USER-PALETTE
        /// </summary>
        /// <param name="donor">attribEnch entity</param>
        /// <param name="layer">to where put</param>
        /// <param name="conatiner">collection of attribId-s</param>
        private List<string> adjustChosableAttrib_OneAndGroups(byte attrib1, byte attrib2, byte type1, byte type2)
        {
            try
            {
                List<string> attribNamesToChoose = new List<string>();
                if (type1 == 1 && type2 == 0)
                    attribNamesToChoose.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(attrib1));
                else if (type1 == 1 && type2 == 1)
                {
                    if ((attrib1 == 1 || attrib1 == 2 || attrib1 == 3) && (attrib2 == 1 || attrib2 == 2 || attrib2 == 3))
                    {
                        return adjustChoosableAttribGroup(attribNamesToChoose, 1);
                    }
                    else if ((attrib1 == 4 || attrib1 == 5 || attrib1 == 6) && (attrib2 == 4 || attrib2 == 5 || attrib2 == 6))
                        return adjustChoosableAttribGroup(attribNamesToChoose, 4);
                    else if ((attrib1 == 7 || attrib1 == 8 || attrib1 == 9) && (attrib2 == 7 || attrib2 == 8 || attrib2 == 9))
                        return adjustChoosableAttribGroup(attribNamesToChoose, 7);
                    else if ((attrib1 == 10 || attrib1 == 11 || attrib1 == 12) && (attrib2 == 10 || attrib2 == 11 || attrib2 == 12))
                        return adjustChoosableAttribGroup(attribNamesToChoose, 10);
                }
                else if (type1 == 2)
                {
                    if (attrib1 == 1 || attrib1 == 2 || attrib1 == 3)
                        return adjustChoosableAttribGroup(attribNamesToChoose, 1);
                    else if (attrib1 == 4 || attrib1 == 5 || attrib1 == 6)
                        return adjustChoosableAttribGroup(attribNamesToChoose, 4);
                    else if (attrib1 == 7 || attrib1 == 8 || attrib1 == 9)
                        return adjustChoosableAttribGroup(attribNamesToChoose, 7);
                    else if (attrib1 == 10 || attrib1 == 11 || attrib1 == 12)
                        return adjustChoosableAttribGroup(attribNamesToChoose, 10);
                }
                else if (type1 == 3)
                    return adjustChosableAttribAll(attribNamesToChoose);
                return attribNamesToChoose;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD TO ADD ALL ATTRIB ID USER-PALETTE
        /// </summary>
        /// <param name="contain">container, that needs to fill up</param>
        /// <param name="groupStarterId">starter attribId</param>
        /// <returns>filled up container</returns>
        private List<string> adjustChoosableAttribGroup(List<string> contain, byte groupStarterId1)
        {
            try
            {
                if (groupStarterId1 == 1)
                {
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(1));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(2));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(3));
                }
                else if (groupStarterId1 == 4)
                {
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(4));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(5));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(6));
                }
                else if (groupStarterId1 == 7)
                {
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(7));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(8));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(9));
                }
                else if (groupStarterId1 == 10)
                {
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(10));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(11));
                    contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(12));
                }
                return contain;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// HELPER METHOD TO ADD ALL ATTRIB ID USER-PALETTE
        /// </summary>
        /// <param name="contain">container, that needs to fill up</param>
        /// <returns>filled up container</returns>
        private List<string> adjustChosableAttribAll(List<string> contain)
        {
            try
            {
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(1));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(2));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(3));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(4));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(5));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(6));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(7));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(8));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(9));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(10));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(11));
                contain.Add(theCoreAttribSizesRepo.findTheNameOfThisAttrib(12));
                return contain;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        
        #endregion

        #region general JP presenters

        /// <summary>
        /// COLLECTS AND SUMS THE ALL AVAILABLE JP
        /// </summary>
        /// <param name="starterJPFromTrunk">baseJP</param>
        /// <returns>balanceJp</returns>
        public int JPGeneralInfo_countTheSumAvailableJP(int starterJPFromTrunk)
        {
            try
            {
                int balance = starterJPFromTrunk;
                balance += theCharJpGainRepo.sumAllCollectedJP();
                balance -= theCharDscpRepo.sumAllJPOfDscp();
                return balance;
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS AND SUMS THE ALL GATHERED JP
        /// </summary>
        /// <param name="starterJPFromTrunk">baseJP</param>
        /// <returns>sumJP</returns>
        public int JPGeneralInfo_countTheSumCollectedJP(int starterJPFromTrunk)
        {
            try
            {
                int sum = starterJPFromTrunk;
                sum += theCharJpGainRepo.sumAllCollectedJP();
                return sum;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS AND SUMMS THE ALL SPENT JP
        /// </summary>
        /// <returns></returns>
        public int JPGeneralInfo_countTheSumSpentJP()
        {
            try
            {
                return theCharDscpRepo.sumAllJPOfDscp();
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }


        /// <summary>
        /// COLLECTS THE AQUIRED JP-S
        /// </summary>
        /// <returns></returns>
        public int JPGeneralInfo_countTheCollectedJP()
        {
            try
            {
                return theCharJpGainRepo.sumAllCollectedJP();
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region jpManager - information presenters

        /// <summary>
        /// SAVES NEW JP AMOUNT
        /// </summary>
        /// <param name="gainedJP">earned JP</param>
        public void JPManagerWinodw_saveTheNewJPPortion(int gainedJP)
        {
            try
            {
                theCharJpGainRepo.addNewJPGain(gainedJP);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REMOVES ADDED JP PORTION
        /// </summary>
        /// <param name="jpGainId">not needed jp index</param>
        public void JPManagerWindow_removeTheExistingJPPortion(int jpGainId)
        {
            try
            {
                theCharJpGainRepo.removeThisJPGain(jpGainId);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS THE GAINED JP COLLECTION TO SHOW THE USER
        /// </summary>
        /// <returns>table of JP Gains</returns>
        public DataTable JPManagerWindow_collectTheJPGains()
        {
            try
            {
                DataTable dt = new DataTable();
                DataColumn index = new DataColumn("Sorsz", typeof(int));
                DataColumn jp = new DataColumn("Jártasságpont", typeof(int));
                dt.Columns.AddRange(new DataColumn[] { index, jp });
                foreach (CharOneJPGain one in theCharJpGainRepo.getTheJPGainCollection())
                {
                    dt.Rows.Add(new object[] { one.theGainingId, one.theJPAmount });
                }
                return dt;
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

    }
}
