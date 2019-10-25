using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreModel
{
    /// <summary>
    /// MODEL-REPOSITORY OF A CORE DEFINED DISCIPLINE
    /// CONTAINS COLLECTIONS OF DEFINITIONS
    ///     ->attribute pool and its schema requirement
    ///     ->predefinded dscp requirement(s) or it with 0 value (this case no requirement)
    ///     ->ways of specialization
    ///     ->specialisation areas
    /// </summary>
    public class CoreOneDscp : ICoreOneDscp
    {
        public short theDisciplId { get; }      //DB IDENTIFIER - QUARLITY DEFINITION
        public byte theDisciplType { get; }     //TYPE GROUP ID
        public string theDisciplName { get; }       //NAME OF THAT
        public bool theDisciplStrongConnectedRequir { get; }    //STRONGLY CONNECTED IN ITS REQUIREMENTS
        public bool theDisciplMentorOrPracticable { get; }      //TRUE MENTOR NEEDED - FALSE NOT IMPORTANT

        public List<CoreDscpAttribRequir> theAttribRequirCollecton { get; }   //THE ATTRIB REQUIREMENTS (1-3 pc)
        public List<CoreDscpNormalRequir> theRequirCollection { get; }     //THE DSCP REQUIREMENTS FOR HAVE LEVELS
        public List<CoreDscpSpec> theSpecCollection { get; }           //THE SPEC REQUIREMENT POSSIBILITIES
        public List<CoreDscpSpecArea> theSpecAreaCollection { get; }    //THE SPEC AREA DEFINITIONS

        KeyValuePair<string, object>[] queryDatas;

        /// <summary>
        /// CONSTRUCTOR OF A CORE DISCIPLINE
        /// </summary>
        /// <param name="dscpId">the dscp id</param>
        /// <param name="type">dscp type</param>
        /// <param name="name">dscp name</param>
        /// <param name="requirImport">severity of strudy</param>
        /// <param name="studiable">studiability true only mentor / false not needed</param>
        public CoreOneDscp(short dscpId, byte type, string name, bool requirImport, bool studiable)
        {
            try
            {
                theDisciplId = dscpId;
                theDisciplType = type;
                theDisciplName = name;
                theDisciplStrongConnectedRequir = requirImport;
                theDisciplMentorOrPracticable = studiable;
                queryDatas = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("@dscpId", theDisciplId)};
                theSpecCollection = new List<CoreDscpSpec>();
                theSpecAreaCollection = new List<CoreDscpSpecArea>();
                extractDscpSpecials();
                theRequirCollection = new List<CoreDscpNormalRequir>();
                extractDscpRequirFromDB();
                theAttribRequirCollecton = new List<CoreDscpAttribRequir>();
                extractAttribDependencyFromDB();
            }
            catch(Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        #region DB CONTENT EXTRACTORS

        private string queryToLoadInSpecialDetails =
            "SELECT discipl_specialReqGroup, discipl_specialReqId, discipl_specialLevel," +
            " discipl_specialThisMustRise, spec_requestIType FROM chaos_discipline_specialis" +
            " WHERE discipl_id=@dscpId";

        private string queryToLoadInSpecailAras =
            "SELECT spec_areaGroup, spec_areaDescr FROM chaos_discipline_specAreas" +
            " WHERE discipl_id=@dscpId;";
        /// <summary>
        /// LOADS IN THE SPECIALISTAION CONNECTED DATAS
        /// </summary>
        private void extractDscpSpecials()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> specTemp = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInSpecialDetails, queryDatas, 1);
                if (specTemp.Count == 0)
                {
                    CoreDscpSpec newOne = new CoreDscpSpec(0, new short[] { 0 }, new byte[] { 0 }, new byte[] { 0 }, new bool[] { false });
                    theSpecCollection.Add(newOne);
                    return;
                }
                screenAndSaveLoadedInSpecOrRequir(specTemp, true);

                DataAccess.ConnectToDB();
                List<object[]> areaTemp = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInSpecailAras, queryDatas, 1);
                foreach (object[] row in areaTemp)
                {
                    CoreDscpSpecArea newOne = new CoreDscpSpecArea(byte.Parse(row[0].ToString()), row[1].ToString());
                    theSpecAreaCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        private string queryToLoadInRequirement =
            "SELECT discipl_requirNormGroup, discipl_requirId, discipl_requirTypeDef" +
            " FROM chaos_discipline_requrement" +
            " WHERE discipl_id=@dscpId;";
        /// <summary>
        /// LOADS IN THE NORMAL REQUIREMENTS OF THIS DSCP
        /// IF THERE IS NO ONE - SAVES A GROUP-ID 1 WITHS 0 ELEMENTS
        /// </summary>
        private void extractDscpRequirFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInRequirement, queryDatas, 1);
                if (temp.Count == 0)
                {
                    CoreDscpNormalRequir newOne = new CoreDscpNormalRequir(0, new short[] { 0 }, new bool[] { false });
                    theRequirCollection.Add(newOne);
                }
                else
                    screenAndSaveLoadedInSpecOrRequir(temp, false);
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD OF REQUIREMENT AND SPEC LOADING IN
        /// SORTS AND SAVES THE DB RECORDS
        /// </summary>
        /// <param name="elements">QUERY RESULT</param>
        /// <param name="isSpec">is a specialisation?</param>
        private void screenAndSaveLoadedInSpecOrRequir(List<object[]> elements, bool isSpec)
        {
            try
            {
                byte groupIdToSort = byte.Parse(elements[0][0].ToString());
                List<object[]> puffer = new List<object[]>();   //COLLECTS THE SAME GROUP MEMBERS
                for (byte i = 0; i < elements.Count; i++)
                {
                    if (groupIdToSort == byte.Parse(elements[i][0].ToString()))
                        puffer.Add(elements[i]);
                    if (groupIdToSort != byte.Parse(elements[i][0].ToString()))
                    {
                        if (isSpec)
                            saveScreenedSpecialisCollection(byte.Parse(groupIdToSort.ToString()), puffer);
                        else
                            saveScreenedRequirCollection(byte.Parse(groupIdToSort.ToString()), puffer);
                        puffer.Clear();
                        groupIdToSort = byte.Parse(elements[i][0].ToString());
                        puffer.Add(elements[i]);
                    }
                    if (i == elements.Count - 1)
                    {
                        if (isSpec)
                            saveScreenedSpecialisCollection(byte.Parse(groupIdToSort.ToString()), puffer);
                        else
                            saveScreenedRequirCollection(byte.Parse(groupIdToSort.ToString()), puffer);
                    }
                }
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD OF SPECIALISATION LOADING IN
        /// SAVES THE SCREENED CONTENT
        /// </summary>
        /// <param name="requGroupId">groupId of collected group</param>
        /// <param name="puffer">groupmembers</param>
        private void saveScreenedSpecialisCollection(byte requGroupId, List<object[]> puffer)
        {
            try
            {
                short[] requirIdentif = new short[puffer.Count];
                byte[] neededLevel = new byte[puffer.Count];
                byte[] typeRequir = new byte[puffer.Count];
                bool[] neededRise = new bool[puffer.Count];
                for (byte i = 0; i < puffer.Count; i++)
                {
                    short id = short.Parse(puffer[i][1].ToString());
                    byte lev = byte.Parse(puffer[i][2].ToString());
                    byte type = byte.Parse(puffer[i][4].ToString());
                    bool need = byte.Parse(puffer[i][3].ToString()) == 1 ? true : false;
                    requirIdentif[i] = id;
                    neededLevel[i] = lev;
                    typeRequir[i] = type;
                    neededRise[i] = need;
                }
                CoreDscpSpec newOne = new CoreDscpSpec(requGroupId, requirIdentif, neededLevel, typeRequir, neededRise);
                theSpecCollection.Add(newOne);
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// HELPER METHOD OF REQUIREMENT LOADING IN
        /// SAVES THE SCREENED CONTENT
        /// </summary>
        /// <param name="requGroupId">groupId of collected group</param>
        /// <param name="puffer">groupmembers</param>
        private void saveScreenedRequirCollection(byte requGroupId, List<object[]> puffer)
        {
            try
            {
                short[] dscpIds = new short[puffer.Count];
                bool[] isDscpGroup = new bool[puffer.Count];
                for (byte j = 0; j < puffer.Count; j++)
                {
                    dscpIds[j] = short.Parse(puffer[j][1].ToString());
                    isDscpGroup[j] = byte.Parse(puffer[j][2].ToString()) == 1 ? true : false;
                }
                CoreDscpNormalRequir newOne = new CoreDscpNormalRequir(requGroupId, dscpIds, isDscpGroup);
                theRequirCollection.Add(newOne);
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }


        private string queryToLoadInAttribDependecies =
            "SELECT discipl_attribId, discipl_JPschema FROM chaos_discipline_attribute" +
            " WHERE discipl_id=@dscpId";
        /// <summary>
        /// LAODS IN THE ATTRIBUTE DEPENDENCIES OF THIS DSCP
        /// </summary>
        private void extractAttribDependencyFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInAttribDependecies, queryDatas, 1);
                foreach (object[] row in temp)
                {
                    CoreDscpAttribRequir newOne = new CoreDscpAttribRequir(
                        byte.Parse(row[0].ToString()), short.Parse(row[1].ToString())
                        );
                    theAttribRequirCollecton.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region general normal level details

        /// <summary>
        /// GETTER OF DSCP REQUIR STRONG CONNECTIONNESS
        /// </summary>
        /// <returns>true=severe connected / false=light connected</returns>
        public bool isTheRequriementSevere()
        {
            return theDisciplStrongConnectedRequir;
        }
        /// <summary>
        /// GETTER OF DSCP STUDIABILIRY
        /// </summary>
        /// <returns>true=needs mentor / false=practicable</returns>
        public bool mentorIsNeededToStudy()
        {
            return theDisciplMentorOrPracticable;
        }
        /// <summary>
        /// ANALYZE THE REQUIRE COLLECTION IF THERE IS/ARE REQUIREMENT
        /// IF ONE IS 0 IT CONTINUES THE SEEK
        /// </summary>
        /// <returns>true=there is requirement / false=all zero</returns>
        public bool isThereANormRequirement()
        {
            try
            {
                foreach (CoreDscpNormalRequir req in theRequirCollection)
                {
                    foreach (short dscpRequ in req.theDisciplRequirElementId)
                        if (dscpRequ != 0)
                            return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region general spec levels details

        /// <summary>
        /// ANALYTE THE SPEC COLLECTION IS THERE ONE
        /// </summary>
        /// <returns>true=there is spec / false=no spec</returns>
        public bool isThereSpecialisation()
        {
            try
            {
                if (theSpecCollection[0].theDisciplSpecRequirGroup == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THIS DSCP'S SEEKED AREA
        /// </summary>
        /// <param name="areaId">seeked areaId</param>
        /// <returns></returns>
        public string findSeekedSpecAreaText(byte areaId)
        {
            try
            {
                if (theSpecAreaCollection == null || theSpecAreaCollection.Count == 0)
                    return "";
                return theSpecAreaCollection.Find(x => x.theSpecAreaGroup == areaId).theSpecAreaDescr;
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region datacollectiors

        /// <summary>
        /// GETTER IF THE ATTRIBUTE DEPENDENCY COLLECTION
        /// </summary>
        /// <returns>dscp requir collection</returns>
        public List<CoreDscpAttribRequir> collectAllAttribs()
        {
            return theAttribRequirCollecton;
        }
        /// <summary>
        /// GETTER OF THE REQUIREMENT COLLECTION
        /// </summary>
        /// <returns>requirements</returns>
        public List<CoreDscpNormalRequir> collectAllRequir()
        {
            return theRequirCollection;
        }
        /// <summary>
        /// GETTER OF ALL SPEC AREAS
        /// </summary>
        /// <returns>area collection</returns>
        public List<CoreDscpSpecArea> collectAllSpecialisationArea()
        {
            return theSpecAreaCollection;
        }

        #endregion

        #region about requirements

        /// <summary>
        /// GETTER OF SPECIFIC REQUIREMENT(S) FROM COLLECTION
        /// </summary>
        /// <param name="groupId">normGroupId</param>
        /// <returns>collcetion of requirements</returns>
        public CoreDscpNormalRequir findSeekedDscpRequir(byte groupId)
        {
            try
            {
                return theRequirCollection.Find(x => x.theDisciplReqGroup == groupId);
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THIS DSCP'S ONE SPEC REQUIREMENT
        /// </summary>
        /// <param name="groupId">seeked requirement group</param>
        /// <returns>elements of specReqir</returns>
        public CoreDscpSpec findSeekedSpecRequirement_Entity(byte groupId)
        {
            try
            {
                return theSpecCollection.Find(x => x.theDisciplSpecRequirGroup == groupId);
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        public byte findSeekedSpecRequirement_GroupId(short[] elementId, byte[] types)
        {
            try
            {
                foreach (CoreDscpSpec spec in theSpecCollection)
                {
                    bool equality = false;
                    if (spec.theDisciplSpecRequirId.Length != elementId.Length)
                        return 0;
                    for (byte i = 0; i < spec.theDisciplSpecRequirId.Length; i++)
                    {
                        if (spec.theDisciplSpecRequirId[i] == elementId[i] && spec.theDisciplRequirType[i] == types[i])
                            equality = true;
                        else
                            equality = false;
                    }
                    if (equality)
                        return spec.theDisciplSpecRequirGroup;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THIS DSCP'S CHOSEN REQUIR GROUP ID
        /// </summary>
        /// <param name="normReqirGroupCombin">the dscpId combination of the chosem dscp's requir</param>
        /// <returns>requirGroup id</returns>
        public byte findSeekedDscpRequirId(short[] normReqirGroupCombin)
        {
            try
            {
                foreach (CoreDscpNormalRequir d in theRequirCollection)
                {
                    if (normReqirGroupCombin.Length != d.theDisciplRequirElementId.Length)
                        throw new CoreModelException("A jártasság előfeltétel gyűjteménye hiányos!");
                    byte count = (byte)d.theDisciplRequirElementId.Length;
                    for (byte i = 0; i < d.theDisciplRequirElementId.Length; i++)
                    {
                        //return (byte) d.theDisciplRequirElementId[i];
                        //return (byte)normReqirGroupCombin[i];
                        if (d.theDisciplRequirElementId[i] == normReqirGroupCombin[i])
                            count--;
                    }
                    if (count == 0)
                        return d.theDisciplReqGroup;
                }
                //return theRequirCollection.Find(x => x.theDisciplRequirElementId == normReqirGroupCombin).theDisciplReqGroup;
                throw new CoreModelException("A törzsjártasság előfeltétel azonosítóra fordítása eredménytelen!");
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THIS DSCP'S CHOSEN ATTRIB SCHEMA IDENTIFIER
        /// </summary>
        /// <param name="attribId">seeked attribId at this dscp</param>
        /// <returns>starter attribSchema</returns>
        public short findTheSeekedAttribSchemaValue(byte attribId)
        {
            try
            {
                CoreDscpAttribRequir temp = theAttribRequirCollecton.Find(x => x.theAttribId == attribId);
                if (temp != null)
                {
                    if (temp.theAttribStarterSchema != 0)
                    {
                        return temp.theAttribStarterSchema;
                    }
                    else
                        throw new CoreModelException("A jártasságra jellemző tulajdonsághoz tartozó séma nincs meg!");
                }
                else
                    throw new CoreModelException("A keresett tulajdonság a jártasságra nem jellemző!");
            }
            catch (Exception e)
            {
                throw new CoreModelException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion
    }
}
