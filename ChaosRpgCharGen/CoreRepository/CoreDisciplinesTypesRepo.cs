using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.Databese;

namespace ChaosRpgCharGen.CoreRepository
{
    /// <summary>
    /// REPOSITROY OF CORE DISCIPLINES AND THE CORE DSCP-TYPES
    /// </summary>
    public class CoreDisciplinesTypesRepo : ICoreDisciplinesTypes
    {
        private List<CoreOneDscp> theDscpCollection;
        private List<CoreOneDscpType> theDscpTypes;
        /// <summary>
        /// CONSTRUCTOR OF DSCPIPLINES AND TYPES REPOSITORY
        /// </summary>
        public CoreDisciplinesTypesRepo()
        {
            try
            {
                theDscpCollection = new List<CoreOneDscp>();
                extractAllDscpFromDB();
                theDscpTypes = new List<CoreOneDscpType>();
                extreactAllDscpTypesFromDB();
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #region here used helper methods
        private string queryToLoadInDscps = "SELECT discipl_id,  discipl_name, discipl_type, " +
            "discipl_studyReqImprt, discipl_studiab FROM chaos_disciplines;";
        /// <summary>
        /// LOADS IN THE DSCPS FROM DB
        /// </summary>
        private void extractAllDscpFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInDscps);
                foreach (object[] entity in temp)
                {
                    byte severity = byte.Parse(entity[3].ToString());
                    byte mentorNeed = byte.Parse(entity[4].ToString());
                    bool sev = severity == 1 ? true : false;
                    bool men = mentorNeed == 1 ? true : false;
                    string name = entity[1].ToString();
                    CoreOneDscp newOne = new CoreOneDscp(
                        short.Parse(entity[0].ToString()), byte.Parse(entity[2].ToString()),
                        name,
                        sev, men
                        );
                    theDscpCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        private string queryToLoadInTypes = "SELECT type_id, type_name, type_inheritylBeneficial" +
            " FROM chaos_discipline_type;";
        private void extreactAllDscpTypesFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInTypes);
                foreach (object[] entity in temp)
                {
                    CoreOneDscpType newOne = new CoreOneDscpType(
                        byte.Parse(entity[0].ToString()), entity[1].ToString(),
                        byte.Parse(entity[2].ToString()) == 1 ? true : false
                        );
                    theDscpTypes.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// GENERALLY FINDS A SEEKED DSCP FROM DSCP_COLLECTION
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>result DscpContainer</returns>
        private CoreOneDscp seekThisDiscipline(short dscpId)
        {
            try
            {
                CoreOneDscp temp = theDscpCollection.Find(x => x.theDisciplId == dscpId);
                if (temp == null)
                    throw new CoreRepositoryException("Nincs jártasság ilyen törzs-azonosítóval!");
                else
                    return temp;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// GENERALLY FINDS A SEEKED DSCP FROM DSCP_COLLECTION
        /// </summary>
        /// <param name="dscpId">seeked dscpName</param>
        /// <returns>result DscpContainer</returns>
        private CoreOneDscp seekThisDiscipline(string dscpName)
        {
            try
            {
                CoreOneDscp temp = theDscpCollection.Find(x => x.theDisciplName == dscpName);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs jártasság ilyen névvel!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// GENERALLY FINDS A SEEKED DSCP TYPE FROM DSCPTYPE_COLLECTION
        /// </summary>
        /// <param name="typeId">seeked typeId</param>
        /// <returns>dscpTypeContainer</returns>
        private CoreOneDscpType seekThisDscpType(byte typeId)
        {
            try
            {
                CoreOneDscpType temp = theDscpTypes.Find(x => x.theTypeId == typeId);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs jártasságtípus ilyen azonosítóval!");
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// GENERALLY FINDS A SEEKED DSCP TYPE FROM DSCPTYPE_COLLECTION
        /// </summary>
        /// <param name="typeName">seeked typeName</param>
        /// <returns>dscpTypeContainer</returns>
        private CoreOneDscpType seekThisDscpType(string typeName)
        {
            try
            {
                CoreOneDscpType temp = theDscpTypes.Find(x => x.theTypeName == typeName);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs jártasságtípus ilyen névvel!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region to NewCharacter creation

        /// <summary>
        /// COLLECTS THE POSSIBLE DSCP TYPES TO BENEFICIAL
        /// </summary>
        /// <returns>list of dscpTypeRec to create stringList</returns>
        public List<CoreOneDscpType> collectTheAllTypesOfDscps_InColl()
        {
            return theDscpTypes;
        }

        #endregion

        #region to NewDscpWindow
        /// <summary>
        /// COLLECTS THE DIFFERENT DSCP TYPES
        /// MAINLY TO SHOW THEM TO THE USER
        /// </summary>
        /// <returns>collection of types</returns>
        public List<string> collectTheAllTypesOfDscps_InString()
        {
            try
            {
                List<string> temp = new List<string>();
                foreach (CoreOneDscpType t in theDscpTypes)
                    temp.Add(t.theTypeName);
                return temp;
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// COLLECTS THE DSCPS THAT HAS THE SAME TYPE
        /// MAINLY TO SHOW THEM TO THE USER
        /// </summary>
        /// <param name="typeId">seeked typeId</param>
        /// <returns>list of dscp names</returns>
        public List<string> collectTheseTypesOfDscps(byte typeId)
        {
            try
            {
                List<string> temp = new List<string>();
                foreach (CoreOneDscp d in theDscpCollection)
                {
                    if (d.theDisciplType == typeId)
                        temp.Add(d.theDisciplName);
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP ID BY ITS DSCP NAME
        /// </summary>
        /// <param name="dscpName">dscpName</param>
        /// <returns>dscpId</returns>
        public short findTheDscpIdOfThisDscpText(string dscpName)
        {
            try
            {
                CoreOneDscp temp = theDscpCollection.Find(x => x.theDisciplName == dscpName);
                if (temp != null)
                    return temp.theDisciplId;
                else
                    throw new CoreRepositoryException("Nincs ilyen jártasságnév!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED TYPE ID BY ITS NAME
        /// </summary>
        /// <param name="typeName">typeText</param>
        /// <returns>typeId</returns>
        public byte findTheDscpTypeIdOfThisTypeText(string typeName)
        {
            try
            {
                return seekThisDscpType(typeName).theTypeId;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED TYPE OF THIS DSCP BY ITS DSCPID
        /// </summary>
        /// <param name="dscpId">dscpId</param>
        /// <returns>typeId</returns>
        public byte findTheTypeIdOfDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theDisciplType;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S TYPE
        /// </summary>
        /// <param name="dscpId"></param>
        /// <returns>the typeName</returns>
        public string findTheDscpTypeName(short dscpId)
        {
            try
            {
                byte typeIndex = seekThisDiscipline(dscpId).theDisciplType;
                return theDscpTypes.Find(x => x.theTypeId == typeIndex).theTypeName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S ATTRIBUIE REQUIREMENTS
        /// MAINLY TO SHOW THEM TO THE USER
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>attribute requir</returns>
        public List<CoreDscpAttribRequir> collectTheAllAttribsOfThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theAttribRequirCollecton;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS BACK THE DSCPID BY ITS NAME
        /// </summary>
        /// <param name="dscpName">seeked dscpName</param>
        /// <returns>dscpId</returns>
        private short findTheDscpOfThisDscpName(string dscpName)
        {
            try
            {
                return seekThisDiscipline(dscpName).theDisciplId;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED REQUIR GROUP ID OF A CHOSEN REQUIR COMBINATION
        /// </summary>
        /// <param name="dscpId">chosen dscpId</param>
        /// <param name="normReqirElementIdCombin">chosen dscpIdcombination</param>
        /// <returns>requirGroupId</returns>
        private byte findTheChosenNormDscpRequirCombinId(short dscpId, short[] normReqirElementIdCombin)
        {
            try
            {
                return seekThisDiscipline(dscpId).findSeekedDscpRequirId(normReqirElementIdCombin);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP'S NAME
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>the dscpName</returns>
        public string findTheDscpName(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theDisciplName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region showing spec dscp details to user

        /// <summary>
        /// FINDS THE SEEKED DSCP'S ONE SPECIAL'S CONCRETE REQUIREMENT FROM SEVERAL
        /// MAINLY TO SHOW THE USER THE POSSIBILITIES
        /// </summary>
        /// <param name="dscpId">seeked dscp</param>
        /// <param name="specGroup">seeked spec requir-pack</param>
        /// <returns></returns>
        public List<CoreDscpSpec> collectTheChosenSpecRequirementsForThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theSpecCollection;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP'S SEVERAL AREAS
        /// </summary>
        /// <param name="dscpId"></param>
        /// <returns></returns>
        public string[] collectThePossibleSpecAreasForThisDscp_ToShowTheUser(short dscpId)
        {
            try
            {
                string[] temp = null;
                List<CoreDscpSpecArea> dscpTemp = seekThisDiscipline(dscpId).collectAllSpecialisationArea();
                if (dscpTemp.Count == 0)
                    return temp;
                temp = new string[dscpTemp.Count];
                for (byte i = 0; i < dscpTemp.Count; i++)
                {
                    temp[i] = dscpTemp[i].theSpecAreaDescr;
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        public byte findTheChosenSpecAreaIdByItsText(short dscpId, string specAreaText)
        {
            try
            {
                foreach (CoreDscpSpecArea area in seekThisDiscipline(dscpId).collectAllSpecialisationArea())
                {
                    if (area.theSpecAreaDescr == specAreaText)
                        return area.theSpecAreaGroup;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP'S AREA TEXT - AT SHOW SPEC DETAILS IN TABLE
        /// </summary>
        /// <param name="dscpId"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public string findTheSpecAreaTextForThisDscp(short dscpId, byte areaId)
        {
            try
            {
                CoreOneDscp temp = seekThisDiscipline(dscpId);
                if (temp == null)
                    return "";
                return temp.findSeekedSpecAreaText(areaId);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region about normal requirement text code-decode

        public string findTheTypeNameByThisTypId(byte typeId)
        {
            try
            {
                return seekThisDscpType(typeId).theTypeName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP'S CONCRETE NORM REQUIREMENT
        /// MAINLY TO ANALYZE THE REQUIREMENT FULLFILLNESS AND SHOW AT DSCPGAIN_SPEC WINDOW
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <param name="requirGroup">seeked dscpNormRequir</param>
        /// <returns>requirement(s)</returns>
        public CoreDscpNormalRequir findTheDscpRequirementOfThisDscp_Entity(short dscpId, byte requirGroup)
        {
            try
            {
                return seekThisDiscipline(dscpId).findSeekedDscpRequir(requirGroup);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        
        /// <summary>
        /// FINDS THE SEEKED DSCP'S CONCRETE SPEC REQUIREMENTS
        /// </summary>
        /// <param name="dscpId"></param>
        /// <param name="specReqGroup"></param>
        /// <returns></returns>
        public CoreDscpSpec findTheSeekedSpecRequir_Entity(short dscpId, byte specReqGroup)
        {
            try
            {
                return seekThisDiscipline(dscpId).findSeekedSpecRequirement_Entity(specReqGroup);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE PROPER SPEC-REQUIR-ID DURRING REQUIREMENT ANALYTE
        /// </summary>
        /// <param name="dscpId"></param>
        /// <param name="elementId"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public byte findTheSeekedSpecRequir_RequirGroupId(short dscpId, short[] elementId, byte[] types)
        {
            try
            {
                return seekThisDiscipline(dscpId).findSeekedSpecRequirement_GroupId(elementId, types);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// COLLECTS THE SEEKED DSCP'S ALL DSCP NORMAL REQUIREMENTS
        /// MAINLY TO SHOW THEM TO THE USER
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <param name="requirNormGroup">seeked requir</param>
        /// <returns>collection of dscpRequir</returns>
        public List<string> collectThePossibRequirs_OfThisDscpNormalLevels_InStringCombin(short dscpId)
        {
            try
            {
                List<string> temp = new List<string>();
                if (isThereRequirementofThisDscp(dscpId))
                {
                    List<CoreDscpNormalRequir> requirsOfChosenDscp = seekThisDiscipline(dscpId).theRequirCollection;
                    string element = "";
                    for (byte i = 0; i < requirsOfChosenDscp.Count; i++)
                    {
                        element = (i + 1).ToString() + ") ";
                        for (byte j = 0; j < requirsOfChosenDscp[i].theDisciplRequirElementId.Length; j++)
                        {
                            if (requirsOfChosenDscp[i].theRequirIsADscpGroup[j])     //it has group of Dscp-s
                            {
                                if (requirsOfChosenDscp[i].theDisciplRequirElementId[j] == 999)
                                    element += "Akármi";
                                else
                                    element += findTheTypeNameByThisTypId((byte)requirsOfChosenDscp[i].theDisciplRequirElementId[j]) + " jártasságok";
                            }
                            else
                            {
                                if (requirsOfChosenDscp[i].theDisciplRequirElementId[j] == 0)
                                    element += "Nincs feltétele";
                                else
                                    element += findTheDscpName(requirsOfChosenDscp[i].theDisciplRequirElementId[j]);
                            }
                            if (j != requirsOfChosenDscp[i].theDisciplRequirElementId.Length - 1)
                                element += "+";
                        }
                        temp.Add(element);
                    }
                }
                else
                {
                    temp.Add("Nincs feltétele");
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// SEEKS BACK THE USER WHICH DSCP NORMAL LEVEL REQUIR COMBINATION HAD BEEN CHOSEN
        /// FROM A STRING COMBINATION FINDS THE SPECIFIC REQUIR GROUPID
        /// </summary>
        /// <param name="dscpCombText">combobox requir combin text</param>
        /// <returns>group id</returns>
        public byte findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(short dscpId, string dscpCombText)
        {
            try
            {
                bool[] dscpRequirIsAGroup = null;
                short[] dscpRequirElementId = null;
                if (dscpCombText.Contains("Nincs feltétele")) //no dscp requir is chosen
                {
                    dscpRequirIsAGroup = new bool[1];
                    dscpRequirElementId = new short[1];
                    dscpRequirIsAGroup[0] = false;
                    dscpRequirElementId[0] = 0;
                }
                else
                {
                    dscpCombText = dscpCombText.Remove(0, 3);
                    if (dscpCombText.Contains(" jártasságok"))  //it is a dscp group
                    {
                        dscpRequirIsAGroup = new bool[1];
                        dscpRequirElementId = new short[1];
                        int indexOfParts = dscpCombText.LastIndexOf(' ');
                        string temp = dscpCombText.Remove(indexOfParts);
                        dscpRequirIsAGroup[0] = true;
                        dscpRequirElementId[0] = findTheDscpTypeIdOfThisTypeText(temp);
                    }
                    else if (dscpCombText == "Akármi")
                    {
                        dscpRequirIsAGroup = new bool[1];
                        dscpRequirElementId = new short[1];
                        dscpRequirIsAGroup[0] = true;
                        dscpRequirElementId[0] = 999;
                    }
                    else
                    {
                        //a specific dscp or combination is chosen
                        if (dscpCombText.Contains('+'))
                        {
                            string[] temp = dscpCombText.Split('+');
                            dscpRequirElementId = new short[temp.Length];
                            dscpRequirIsAGroup = new bool[temp.Length];
                            for (byte i = 0; i < temp.Length; i++)
                            {
                                dscpRequirElementId[i] = findTheDscpOfThisDscpName(temp[i]);
                                dscpRequirIsAGroup[i] = false;
                            }
                        }
                        else
                        {
                            dscpRequirIsAGroup = new bool[1];
                            dscpRequirElementId = new short[1];
                            dscpRequirIsAGroup[0] = false;
                            dscpRequirElementId[0] = findTheDscpOfThisDscpName(dscpCombText);
                        }
                    }
                }
                return findTheChosenNormDscpRequirCombinId(dscpId, dscpRequirElementId);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region general methods
        /// <summary>
        /// FINDS THE SEEKED DSCP AND ANALYZE IS THERE SPECIALISATION
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>true=there is / false=no specialisation</returns>
        public bool isThereSpecialisationForThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).isThereSpecialisation();
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP AND ANALYZE IS THERE DSCP REQUIREMENT
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>true=there is / fales=no, only one zero</returns>
        public bool isThereRequirementofThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).isThereANormRequirement();
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP AND ANALYZE TO STUDY IS ORIGINALLY BENEFICIAL IN JP PRICE
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>ture=it is benefic from start / false=not benefic alone</returns>
        public bool isThisDscpStudyIsOriginallyBeneficial(byte typeId)
        {
            try
            {
                return seekThisDscpType(typeId).theTypeIsOriginallyBenefic;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP AND ANALYZE IS NEEDED MENTOR FOR STUDY 
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <returns>true=yes, it must have / false=no, practicable</returns>
        public bool mustBeMentorAtStrudyThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theDisciplMentorOrPracticable;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S REQUIREMENT IS NEEDED TO TREATH SEVERE
        /// </summary>
        /// <param name="dscpId">dscpId</param>
        /// <returns>true=needTreathSevere = false=noNeedSoSevere</returns>
        public bool mustTreathSevereTheRequirForThisDscp(short dscpId)
        {
            try
            {
                return seekThisDiscipline(dscpId).theDisciplStrongConnectedRequir;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S STRICT ATTRIB'S JP SCHEMA MODIFIER BASIC VALUE
        /// </summary>
        /// <param name="dscpId">seeked dscpId</param>
        /// <param name="attribId">attribId, that is needed</param>
        /// <returns>JPSchemaModifierValue</returns>
        public short findTheDscpAttribSchemValue(short dscpId, byte attribId)
        {
            try
            {
                return seekThisDiscipline(dscpId).findTheSeekedAttribSchemaValue(attribId);
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

    }
}
