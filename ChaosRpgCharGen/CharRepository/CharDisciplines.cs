using ChaosRpgCharGen.CharModel;
using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1
{
    /// <summary>
    /// REOPSITORY OF DISCIPLINES OF CHARACTER
    /// MANAGED THE ATTRIB-ENCHANTMENT AT SOURCE SIDE
    /// SUMMARISE THE UNSPENT ATTRIB-POINTS -> PRESENTED AT VIEW-ATTRIB-MANAGE
    /// </summary>
    public class CharDisciplines :ICharDisciplines
    {
        private List<CharOneDscp> theDisciplColl;
        private List<CharDscpUnspentAttrib> theUnspentColl;

        private int theManagedCharId;
        /// <summary>
        /// CONSTRUCTOR OF DISCIPLINE COLLECTION OF THE CHARACTER
        /// </summary>
        public CharDisciplines(int charId)
        {
            try
            {
                theManagedCharId = charId;
                theDisciplColl = extractCharDscpFromDB();
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #region DSCP helper extraction methods

        private string queryToLoadInDscps =
            "SELECT discipl_index, discipl_id, discipl_type, discipl_chosenAttrib, discipl_requirNormal," +
            " discipl_notation" +
            " FROM character_chosenDiscipl WHERE character_id=@charId";
        /// <summary>
        /// EXTRACT DSCPS OF A CHARACTER
        /// </summary>
        /// <returns></returns>
        private List<CharOneDscp> extractCharDscpFromDB()
        {
            try
            {
                List<CharOneDscp> temp = new List<CharOneDscp>();
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("@charId", theManagedCharId) };

                DataAccess.ConnectToDB();
                List<object[]> res = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInDscps, queryDatas, 1);

                if (res.Count != 0)
                {
                    foreach (object[] row in res)
                    {
                        CharOneDscp dscp = new CharOneDscp(theManagedCharId, int.Parse(row[0].ToString()),
                            short.Parse(row[1].ToString()), byte.Parse(row[2].ToString()),
                            byte.Parse(row[3].ToString()), byte.Parse(row[4].ToString()),
                            row[5].ToString());

                        temp.Add(dscp);
                    }
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED CHAR DSCP BY ITS DSCP_INDEX
        /// </summary>
        /// <param name="dscpIndex">seeked dscpIndex</param>
        /// <returns>dscp</returns>
        private CharOneDscp seekThisDscp(int dscpIndex)
        {
            try
            {
                CharOneDscp temp = theDisciplColl.Find(x => x.theDisciplIndex == dscpIndex);
                if (temp == null)
                    throw new CharRepositoryException("Nincs ilyen jártasság");
                else
                    return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region DSCP normal brandNew

        /// <summary>
        /// ADD NEW DISCIPL TO THE CHAR COLLECTION - FROM PROGRAM
        /// </summary>
        /// <param name="dscpId">dscpId</param>
        /// <param name="type">dscpType</param>
        /// <param name="attribId">attribId</param>
        /// <param name="dscpRequir">chosen requir</param>
        /// <param name="dscpDescr">dscp descr</param>
        /// <param name="mentor_practice">mentorated or not</param>
        /// <param name="jpCost">jp value</param>
        public void addNewDiscipl(short dscpId, byte type, byte attribId, string dscpDescr, 
            bool mentor_practice, short jpCost, byte dscpRequir)
        {
            try
            {
                CharOneDscp newDscp = new CharOneDscp(theManagedCharId, findTheNextDscpIndex(), dscpId, type,
                    attribId, dscpRequir, dscpDescr);
                newDscp.gainNewLevel(theManagedCharId, mentor_practice, jpCost);

                theDisciplColl.Add(newDscp);
                //DB SAVE
                saveNewDscpToDb(newDscp);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE NEXT DISCIPLINE INDEX IN THE DB
        /// </summary>
        /// <returns>next dscp index</returns>
        private int findTheNextDscpIndex()
        {
            try
            {
                int indexDscpMax = 0;
                //FROM LIST
                if (theDisciplColl.Count != 0)
                    indexDscpMax = theDisciplColl.Max(x => x.theDisciplIndex);
                indexDscpMax++;
                return indexDscpMax;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        private const string queryToSaveNewDscp = "INSERT INTO character_chosenDiscipl (discipl_index," +
            " character_id, discipl_id, discipl_type, discipl_chosenAttrib, discipl_requirNormal," +
            " discipl_notation) VALUES" +
            " (@dscpIndex,@charId,@dscpId,@typeId,@attribId,@dscpRequir,@notation);";
        /// <summary>
        /// HELPER METHOD TO SAVE THE NEW DSCP TO DB
        /// </summary>
        /// <param name="newDscp">newly created dscp</param>
        private void saveNewDscpToDb(CharOneDscp newDscp)
        {
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                new KeyValuePair<string, object>("@dscpIndex",newDscp.theDisciplIndex),
                new KeyValuePair<string, object>("@charId",theManagedCharId),
                new KeyValuePair<string, object>("@dscpId",newDscp.theDisciplineId),
                new KeyValuePair<string, object>("@typeId",newDscp.theDisciplType),
                new KeyValuePair<string, object>("@attribId",newDscp.theDisciplAttribId),
                new KeyValuePair<string, object>("@dscpRequir",newDscp.theDisciplRequir),
                new KeyValuePair<string, object>("@notation",newDscp.theDisciplNote)
            };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToSaveNewDscp, queryDatas, 7))
                    throw new CharRepositoryException("Új jártasság adatbázisba rögzítés elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region DSCP normal riseAndRemoveNormLevel
        /// <summary>
        /// RISIES THE LEVEL OF THE CHOSEN DISCIPL
        /// </summary>
        /// <param name="dscpIndex">disciplIndex</param>
        /// <param name="mentor_practice">mentorated or not</param>
        /// <param name="jpCost">jp amount</param>
        /// <param name="isThisRisiesAttrib">system defines is this affect its attrib</param>
        public void riseThisDiscipl(int dscpIndex, bool mentor_practice, short jpCost)
        {
            try
            {
                CharOneDscp risenDscp = theDisciplColl.Find(x => x.theDisciplIndex == dscpIndex);
                risenDscp.gainNewLevel(theManagedCharId, mentor_practice, jpCost);
            }
            catch(Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// REMOVES THE CHOSEN DISCIPL LEVELS
        /// </summary>
        /// <param name="dscpIndex">seeked one index</param>
        /// <param name="levelFrom">level from need to delete</param>
        public void removeDisciplLevelFrom(int dscpIndex, byte levelFrom)
        {
            try
            {
                CharOneDscp dscpToDecrease = seekThisDscp(dscpIndex);
                dscpToDecrease.removeTheseLevels(theManagedCharId, levelFrom);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region DSCP remove one with all parts

        /// <summary>
        /// REMOVE WHOLE DSCP
        /// </summary>
        /// <param name="dscpIndex"></param>
        public void removeThisWholeDiscipl(int dscpIndex)
        {
            try
            {
                CharOneDscp dscpToRemove = seekThisDscp(dscpIndex);
                dscpToRemove.removeTheseLevels(theManagedCharId, 1);
                dscpToRemove.removeAllSpecLevels(theManagedCharId);

                theDisciplColl.Remove(dscpToRemove);
                removeDscpTrunkFromDb(dscpIndex);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private const string queryToRemoveDscpTrunk = "DELETE FROM character_chosenDiscipl" +
            " WHERE discipl_index=@dscpIndex AND character_id=@charId;";

        private void removeDscpTrunkFromDb(int dscpIndex)
        {
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("@dscpIndex", dscpIndex),
                new KeyValuePair<string, object>("@charId",theManagedCharId)
            };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveDscpTrunk, queryDatas, 2))
                    throw new CharRepositoryException("A jártasság-törzs eltávolítása elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region DSCP presenter
        /// <summary>
        /// COLLECTS THE NEEDED DSCP LAYERS
        /// </summary>
        /// <param name="dscpIndex">seeked dscp</param>
        /// <returns>collection</returns>
        public List<CharDscpLevel> collectThisDisciplLayers(int dscpIndex)
        {
            try
            {
                CharOneDscp temp = seekThisDscp(dscpIndex);
                return temp.getTheNormalLayers();
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS THE NEEDED DSCP AND COLLECTS ITS SPECIFIC INFOS
        /// 0=id, 1=attribId, 2=requir, 3=maxLevel, 4=JPInThat
        /// </summary>
        /// <param name="dscpIndex">seeked dscp</param>
        /// <returns>datacollection</returns>
        public int[] collectDisciplDetails_ToShow(int dscpIndex)
        {
            try
            {
                CharOneDscp temp = seekThisDscp(dscpIndex);
                int[] content = new int[5];
                content[0] = temp.theDisciplineId;
                content[1] = temp.theDisciplAttribId;
                content[2] = temp.theDisciplRequir;
                content[3] = temp.getTheHighestLevelMark();
                content[4] = temp.sumFullJP_ThisDscp();
                return content;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        
        /// <summary>
        /// SEEKDS THE NEEDED DSCPS NOTING
        /// </summary>
        /// <param name="dscpIndex">seeked dscpIndex</param>
        /// <returns>note</returns>
        public string findThisDscpNoteOfThisDscp(int dscpIndex)
        {
            try
            {
                string descrip = seekThisDscp(dscpIndex).theDisciplNote;
                if (descrip == "" || descrip == null)
                    descrip = "Nincs ilyen";
                return descrip;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        
        /// <summary>
        /// FINDS THE HIGHEST LEVELS OF ALL DISCIPLINE - REVIEW
        /// </summary>
        /// <returns>data collection of highest levels</returns>
        public List<CharDscpSurfaceAboutLevels> collectAllDisciplSurface()
        {
            try
            {
                List<CharDscpSurfaceAboutLevels> surface = new List<CharDscpSurfaceAboutLevels>();
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    CharDscpSurfaceAboutLevels temp = new CharDscpSurfaceAboutLevels(
                        dscp.theDisciplIndex, dscp.theDisciplineId,
                        dscp.getTheHighestLevelMark(), dscp.sumFullJP_ThisDscp(),
                        dscp.theDisciplNote, dscp.isThereSpecAtAll());
                    surface.Add(temp);
                }
                return surface;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// COUNTS TOGETHER THE ALL JP THAT ALREADY SPENT
        /// </summary>
        /// <returns>JP value</returns>
        public int sumAllJPOfDscp()
        {
            try
            {
                int sum = 0;
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    sum += dscp.sumFullJP_ThisDscp();
                }
                return sum;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S SPENT JP BY ITS INDEX
        /// </summary>
        /// <param name="dscpIndex">seeked dscpIndex</param>
        /// <returns>jp value of this dscp</returns>
        public int sumTheJPOfThisDscp(int dscpIndex)
        {
            try
            {
                int sum = seekThisDscp(dscpIndex).sumFullJP_ThisDscp();
                return sum;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DSCP'S DSCPID BY ITS INDEX
        /// </summary>
        /// <param name="dscpIndex">seekded dscpIndex</param>
        /// <returns>dscpId</returns>
        public short findTheDscpIdForThisDscp(int dscpIndex)
        {
            try
            {
                return theDisciplColl.Find(x => x.theDisciplIndex == dscpIndex).theDisciplineId;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S REQUIREMENT ATTRIB
        /// </summary>
        /// <param name="dscpIndex">seeked dscpIndex</param>
        /// <returns>dscp connected attribId</returns>
        public byte findTheDscpAttribForThisDscp(int dscpIndex)
        {
            return seekThisDscp(dscpIndex).theDisciplAttribId;
        }

        public byte findTheDscpRequirGroupIfOdThisDscp(int dscpIndex)
        {
            return seekThisDscp(dscpIndex).theDisciplRequir;
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S NEXT LEVEL TILL THE MAX - IF IT IS GIVES THE POSSIBLE MAX BACK
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public byte getTheNextLevelMark(int dscpIndex, byte specIndex)
        {
            try
            {
                if (specIndex > 0)
                {
                    byte tempLevelSpecMark = seekThisDscp(dscpIndex).getTheNextSpecLevelMarkBySpecInxed(specIndex);
                    if (tempLevelSpecMark == 1)
                        return 2;
                    else if (tempLevelSpecMark == 2)
                        return 3;
                }
                else
                {
                    byte tempLevelMark = seekThisDscp(dscpIndex).getTheHighestLevelMark();
                    if (tempLevelMark == 10)
                        return 11;
                    else
                        return ++tempLevelMark;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region DSCP specialization

        /// <summary>
        /// ADDS NEW LEVEL OF SPECIALISATION OF THE SPECIFIC DISCIPLINE
        /// </summary>
        /// <param name="dscpIndex">the index of specific dscp</param>
        /// <param name="jp">jp of level</param>
        /// <param name="reqgroup">requir group</param>
        /// <param name="area">chosen area</param>
        /// <param name="descr">chosen descr</param>
        /// <param name="givesItAttrib">gives it an attrib rise</param>
        public void addNewDsciplSpecialLevel(int dscpIndex, short jp, byte reqgroup,
            byte area, string descr)
        {
            try
            {
                CharOneDscp dscpGetSpec = seekThisDscp(dscpIndex);
                dscpGetSpec.gainNewSpecLevel(theManagedCharId, jp, reqgroup, area, descr);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REMOVES THE CHOSEN DSCP'S STRICT SPECIALISATION LEVEL(s)
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="specIndex"></param>
        /// <param name="levelFrom"></param>
        public void removeThisDsciplSpecFrom(int dscpIndex, byte specIndex, byte levelFrom)
        {
            try
            {
                if (seekThisDscp(dscpIndex).isThereThisSpecLevels(specIndex, levelFrom))
                {
                    seekThisDscp(dscpIndex).removeTheseSpecLevels(theManagedCharId, specIndex, levelFrom);
                }
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE SEEKED DISCIPL AND DEFINE HAS IT A SPECIAL AT ALL
        /// </summary>
        /// <param name="dscpIndex">seeked dscp index</param>
        /// <returns>has that one or not</returns>
        public bool hasThisDisciplSpecialis(int dscpIndex)
        {
            CharOneDscp dscpGetSpec = seekThisDscp(dscpIndex);
            return dscpGetSpec.isThereSpecAtAll();
        }
        /// <summary>
        /// SEEKS THE SEEKED SPECIALIS IN A SPECIFIC DSCP - ARE THERE LIKE THIS
        /// </summary>
        /// <param name="dscpIndex">seeked index</param>
        /// <param name="specReqgroup">specRequGroup</param>
        /// <param name="area">specArea</param>
        /// <param name="descr">specDescr</param>
        /// <returns></returns>
        public bool hasThisDisciplSpecWithThisArea(int dscpIndex, byte specReqgroup, byte area, string descr)
        {
            try
            {
                CharOneDscp temp = seekThisDscp(dscpIndex);
                return temp.isThereAlreadyThisSpecType(specReqgroup, area, descr);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /*
        /// <summary>
        /// FINDS THE HIGHEST LEVELS OF ALL DISCIPLINES SPECIALISATIONS
        /// </summary>
        /// <returns></returns>
        public List<CharDscpSpecLevel> collectAllSpecSurface()
        {
            try
            {
                List<CharDscpSpecLevel> surface = new List<CharDscpSpecLevel>();
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    surface.AddRange(dscp.getTheHighestSpecLevels());
                }
                return surface;
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("Az adott karakter-jártassághoz specializációjának" +
                    " áttekintője elérhetetlen!\n" + ex.Message);
            }
        }
        */
        /// <summary>
        /// COLLECTS THE SPECIALISTAION LEVELS OF CHOSEN DSCP 
        /// </summary>
        /// <param name="dscpIndex">seeked index</param>
        /// <returns>collection</returns>
        public List<CharDscpSpecLevel> collectThisDisciplSpecAllLayers(int dscpIndex)
        {
            try
            {
                CharOneDscp temp = seekThisDscp(dscpIndex);
                return temp.getTheSpecLayers();
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE AREA AND SPEC-REQUIREMENT OF SPEC OF A DSCP
        /// AT RISE LVL OF SPECIALISATION
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public byte[] findSpecRequirGroupAndAreaByItsSpecIndex_AtSpecLvlRise(int dscpIndex, byte specIndex)
        {
            try
            {
                byte[] temp = seekThisDscp(dscpIndex).findTheSpecRequirGroupAndArea_AtSpecLvlRise(specIndex);
                if (temp[0] != 0 && temp[1] != 0)   //THESE ARE SURELY NOT 0
                    return temp;
                else
                    throw new CharRepositoryException("Hiba történt a specializáció adatainak visszakeresésekor!");
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// fINDS THE SPEC-DESCRIPTION OF A DSCP
        /// AT RISE OF LVL OF SPECIALISATIoN
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public string findSpecDescrByItsSpecIndex_AtSpecLvlRise(int dscpIndex, byte specIndex)
        {
            try
            {
                return seekThisDscp(dscpIndex).findTheSpecDescr_AtSpecLvlRise(specIndex);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region dscp seeker - requiremnet revisors
        /* NO USED
        /// <summary>
        /// FINDS IN THE CHAR COLLECTION THE SEEKED DSCP REQUIR EXISTENCE
        /// </summary>
        /// <param name="seekedDscpId">dscpId - identif</param>
        /// <param name="seekedDscpReqGroup">dscpRequir - identif</param>
        /// <param name="seekedDscpDescr">identif the proper - identif</param>
        /// <param name="levelNeed">levelMark is needed</param>
        /// <param name="isSevere">manage is needed severe</param>
        /// <returns>true=thereIs / false=notThere</returns>
        public bool findThisDscpRequir_NormWithTrunkDetails(short seekedDscpId, byte seekedDscpReqGroup,
             string seekedDscpDescr, byte levelNeed, bool isSevere)
        {
            try
            {
                if (seekedDscpId == 0)
                    return true;
                if (!isSevere)
                    levelNeed = (byte)((levelNeed / 3) + 1);
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    if (dscp.theDisciplineId == seekedDscpId && dscp.theDisciplRequir == seekedDscpReqGroup
                        && dscp.getTheHighestLevelMark() >= levelNeed &&
                        dscp.theDisciplNote == seekedDscpDescr)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("A karakter-jártasságok közötti sokparaméteres előfeltétel keresés sikertelen!\n" + ex.Message);
            }
        }
        */
        /// <summary>
        /// FINDS IN THE CHAR COLLECTION THE SEEKED DSCP REQUIR EXISTENCE
        /// IT IS FOR STRICT DSCP SEEKING GERENALLY BUT SEEK NORMAL DSCP LEVEL REQUIR NEEDED PRESELECTION
        /// </summary>
        /// <param name="seekedDscpId">dscpId - identif</param>
        /// <param name="levelNeed">levelMark is needed</param>
        /// <returns>true=thereIs / false=notThere</returns>
        public bool findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(short seekedDscpId, byte levelNeed)
        {
            try
            {
                if (seekedDscpId == 0)
                    return true;
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    if (dscp.theDisciplineId == seekedDscpId)
                    {
                        if (dscp.getTheHighestLevelMark() >= levelNeed)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS IN THE CHAR COLLECTION THE SEEKED DSCP TYPES
        /// </summary>
        /// <param name="seekedDscpType">typeId</param>
        /// <param name="levelNeed">requirement level</param>
        /// <returns>true=thereIs / false=notThere</returns>
        public bool findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(short seekedDscpType, byte levelNeed)
        {
            try
            {
                if (theDisciplColl.Count == 0)
                    return false;
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    if (dscp.theDisciplType == seekedDscpType && dscp.getTheHighestLevelMark() >= levelNeed)
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE MAX LEVEL OF THE DSCP TO DEFINE IF PERMITTED TO RISE SPEC LEVEL
        /// </summary>
        /// <param name="dscpIndexToSpec"></param>
        /// <param name="dscpSpecRequirGroup"></param>
        /// <param name="dscpSpecNote"></param>
        /// <returns></returns>
        public byte findTheMaxSpecLevelOfThisDscp_ToDefineIfSpecPermitted(int dscpIndexToSpec, 
            byte dscpSpecRequirGroup, byte areaId, string dscpSpecNote)
        {
            try
            {
                return seekThisDscp(dscpIndexToSpec).getTheHighestSpecLevelMark(dscpSpecRequirGroup, areaId, dscpSpecNote);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE MAX LEVEL OF THE DSCP TO DEFINE IF PERMITTED TO RISE SPEC LEVEL
        /// </summary>
        /// <param name="dscpIndexToSpec"></param>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public byte findTheMaxSpecLevelOfThisDscp_ToDefineIfSpecPermitted(int dscpIndexToSpec, byte specIndex)
        {
            try
            {
                return seekThisDscp(dscpIndexToSpec).getTheHighestSpecLevelMark(specIndex);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        public byte findTheSpecRequirIdOfThisDscpSpec_ToDefineIfSpecPermitted(int dscpIndexToSpec, byte specIndex)
        {
            try
            {
                return seekThisDscp(dscpIndexToSpec).getTheChosenSpecRequirGroupIdOfThis(specIndex);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }


        /// <summary>
        /// FINDS IN CHAR DSCP_COLLECTION THE SEEKED DSCP AND ITS LEVEL
        /// </summary>
        /// <param name="seekedDscpId">seeked dscpId</param>
        /// <param name="neededLevel">needed level</param>
        /// <param name="isSevere">manage is need to be severe or not</param>
        /// <returns></returns>
        public bool findThisDscpRequir_IndividualHaveTheLevel_NormalCase(short seekedDscpId, byte neededLevel,
            bool isSevere)
        {
            try
            {
                if (isSevere)
                    return findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(seekedDscpId, neededLevel);
                else
                    return isThereSeekedDscpRequir_NotSevere(seekedDscpId, neededLevel);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// HELPER METHOD OF DSCP REQUIR ANALYZE
        /// DIRECT ANALYZE THE CHAR DSCP COLLECTION FOR A SPECIFIC DSCP AND ITS LEVEL
        /// IT MANAGES THE NOT SEVERE CASE
        /// </summary>
        /// <param name="seekedDscpId">seeked dscpId</param>
        /// <param name="neededLevel">needed level</param>
        /// <returns></returns>
        private bool isThereSeekedDscpRequir_NotSevere(short seekedDscpId, byte neededLevel)
        {
            try
            {
                if (neededLevel >= 1 && neededLevel <= 3)
                    neededLevel = 1;
                else if (neededLevel >= 4 && neededLevel <= 6)
                    neededLevel = 2;
                else if (neededLevel >= 7 && neededLevel <= 9)
                    neededLevel = 3;
                else
                    neededLevel = 4;
                return findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(seekedDscpId, neededLevel);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region manaege dscp-attrib developing management

        /// <summary>
        /// ANALYZE THE DISCIPL ALL ATTIRB RISES POINTS
        /// </summary>
        /// <returns>collection of possbileAttribRise</returns>
        public List<CharDscpUnspentAttrib> collectPossiblyUnspentAttribPoints()
        {
            try
            {
                theUnspentColl = new List<CharDscpUnspentAttrib>();
                foreach (CharOneDscp dscp in theDisciplColl)
                {
                    if (dscp.isThereUnspentAttribs())
                        theUnspentColl.AddRange(dscp.collectTheUnspentAttribs());
                }
                return theUnspentColl;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REMOVES AN UNSPENT ATTRIB POINT FROM COLLECTION - SOURCE PART
        /// CHECKS THE DSCP LEVEL TO SPENT
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="dscpIndex">the dscp where it is spent</param>
        /// <param name="disciplLevel"the level that spent></param>
        /// <param name="isSpec_specIndex">specIndex, if it is spec > 0</param>
        public void removeUnpentAttribPoint_itIsSpent(int dscpIndex, byte disciplLevel,
            byte isSpec_specIndex, int attribEnchIndex)
        {
            try
            {
                if (isSpec_specIndex > 0)
                    seekThisDscp(dscpIndex).setSpentTheAttribPoint_FromSpecLevel(
                        theManagedCharId, isSpec_specIndex, attribEnchIndex);
                else
                    seekThisDscp(dscpIndex).setSpentTheAttribPoint_FromNormalLevel(
                        theManagedCharId, disciplLevel, attribEnchIndex);
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// REDEFINE THE DSCP LEVEL THAT ATTRIB POINT FROM THAT IS NOT SPENT - SOURCE PART
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="level">level</param>
        /// <param name="specIndex">specIndex, if it is spec >0</param>
        public void addUnspentAttribPoint_resetAttribSpending(int dscpIndex, bool isItSpec, int attribEnchIndex)
        {
            try
            {
                if (isItSpec)
                    seekThisDscp(dscpIndex).setUnspentTheAttribPoint_FromSpecLevel(
                        theManagedCharId, attribEnchIndex);
                else
                    seekThisDscp(dscpIndex).setUnspentTheAttribPoint_FromNormalLevel(
                        theManagedCharId, attribEnchIndex);

            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// REDEFINE THE DSCP LEVEL THAT ATTRIB POINT FROM THAT IS NOT SPENT - SOURCE PART
        /// IT FINDS THE PROPER SPECINDEXING AND EXECUTE PROCESS
        /// </summary>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="attribEnchIndex">attribEnchIndex to reset</param>
        public void addUnspentAttribPoint_resetAttribSpending_UnknownType(int dscpIndex, int attribEnchIndex)
        {
            try
            {
                byte properSpecIndex = seekThisDscp(dscpIndex).helperUnspentTheAttribPoint_UnknownTypeDetection(
                    theManagedCharId, attribEnchIndex);
                addUnspentAttribPoint_resetAttribSpending(dscpIndex, properSpecIndex > 0 ? true : false,
                    attribEnchIndex);
            }
            catch(Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SEEKS SOME DETAILS OF SPENT ATTRIBUTE POINT FROM DSCP NORMAL LEVEL OR SPEC LEVEL
        /// </summary>
        /// <param name="dscpIndex1">dscpIndex1</param>
        /// <param name="dscpIndex2">dscpIndex2</param>
        /// <param name="attribEnchIndex">attribEnchIndex that level seeked</param>
        /// <returns>dscpId, levelMark, norm/spec</returns>
        public int[] findDetailsSpentAttribPoint(int dscpIndex1, int dscpIndex2, int attribEnchIndex)
        {
            try
            {
                if (dscpIndex2 == 0)
                    return seekThisDscp(dscpIndex1).findDetailsSpentAttribPoint(attribEnchIndex, 1);
                else if (dscpIndex1 == dscpIndex2)
                    return seekThisDscp(dscpIndex1).findDetailsSpentAttribPoint(attribEnchIndex, 2);
                else
                {
                    int[] tempFirstDescr = seekThisDscp(dscpIndex1).findDetailsSpentAttribPoint(attribEnchIndex, 3);
                    int[] tempSecondDescr = seekThisDscp(dscpIndex2).findDetailsSpentAttribPoint(attribEnchIndex, 3);
                    int[] finalDescr = new int[8];
                    for (byte i = 0; i < tempFirstDescr.Length; i++)
                    {
                        finalDescr[i] = tempFirstDescr[i];
                        finalDescr[i + 4] = tempSecondDescr[i];
                    }
                    return finalDescr;
                }
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /*  NOT USED
        /// <summary>
        /// SEEKS THE SPECINDEX TO REMOVE ATTRIB ENCHANT BY DSCPINDEX AND ATTRIBENCHANCE-INDEX
        /// </summary>
        /// <param name="dscpIndex"></param>
        /// <param name="attribEnchIndex"></param>
        /// <returns>max 2 specIndex arrives</returns>
        public byte findSpecIndexBySpentAttribPoint_HelperRemovingAttribEnch(int dscpIndex, int attribEnchIndex)
        {
            return seekThisDscp(dscpIndex).findTheSpecIndexByAttribEnchant_ThatSurelyASpec_HelperRemoveAttribEnch(attribEnchIndex);
        }*/
        /// <summary>
        /// FINDS THE ALL ATTRIB ENCHANT INDEXES THAT POINT SPENT AND NEED TO DELETE BECAUSE OF LEVEL DELETE
        /// </summary>
        /// <param name="dscpIndex">dscpIndex under alter</param>
        /// <param name="levelFrom">levelMark from delete</param>
        /// <param name="specIndex">specIndex</param>
        /// <returns>attribEnchIndexes</returns>
        public int[] collectTheAttribEnchOfThisDscpIndexFromLevel_UnderDirectDeletion(int dscpIndex,
            byte levelFrom, byte specIndex)
        {
            try
            {
                if (specIndex > 0)  //A SPEC AND SOME ITS LEVEL IS UNDER DELETION
                {
                    int[] temp = new int[] { seekThisDscp(dscpIndex).findTheAttribEnchantOfThisSpecIndex_UnderDeletion(specIndex) };
                    return temp;
                }
                else if (specIndex == 0 && levelFrom > 1)   //SOME NORMAL LEVEL IS UNSER DELTION
                    return seekThisDscp(dscpIndex).findTheAttribEnchantOfNormalLevels_UnderDeletion(levelFrom);
                else
                    return seekThisDscp(dscpIndex).findAllAttribEnchantOfThisDscp_UnderDeletion();    //ALL DSCP IS NEEDED TO DELET
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }


        #endregion
    }
}
