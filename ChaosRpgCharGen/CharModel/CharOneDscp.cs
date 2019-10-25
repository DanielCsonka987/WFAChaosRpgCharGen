using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel
{
    /// <summary>
    /// MODEL-REPO OF CHAR ONE DISCIPLINE
    /// IT CONTAINS TWO TYPES OF LEVELS (NORMAL AND SPECIALZATION) -> IT KEEP UP LIST-COLLECTION
    /// </summary>
    public class CharOneDscp : ICharOneDscp
    {
        public int theDisciplIndex { get; }    //primary kew in DB - but with same charId
        public short theDisciplineId { get; }   //quality definition
        public byte theDisciplType { get; }     //helper type definition, makes somethings easier
        public byte theDisciplAttribId { get; } //chosen attribute, one from predefined pool in core, and chosen by user
        public byte theDisciplRequir { get; }   //chosen requirement, one forom predefined pool in core and chosen by user
        public string theDisciplNote { get; }   //dscp description connected to the individual dscp

        private List<CharDscpLevel> theNormalLevelColl;
        private List<CharDscpSpecLevel> theSpecLevelColl;

        /// <summary>
        /// CONSTRUCTORT OF A DISCIPLINE OF CHARACTER
        /// </summary>
        /// <param name="index">dscpIndex</param>
        /// <param name="dscpId">dscpId</param>
        /// <param name="type">typeId</param>
        /// <param name="attribId">connected attrib</param>
        /// <param name="reqir">the chosen dscpRequir, it is depended on</param>
        /// <param name="dscpDescr">dscp descr</param>
        public CharOneDscp(int charId, int index, short dscpId, byte type, 
            byte attribId, byte reqir, string dscpDescr)
        {
            try
            {
                theDisciplIndex = index;
                theDisciplineId = dscpId;
                theDisciplType = type;
                theDisciplAttribId = attribId;
                theDisciplRequir = reqir;
                theDisciplNote = dscpDescr;
                theNormalLevelColl = extractNormalLevels(charId);
                theSpecLevelColl = extractSpecLevels(charId);
            }
            catch (Exception e)
            {
                throw new CharModelRepoException(e.TargetSite + "->" + e.Message);
            }
        }

        #region helper extraction meghods
        private string queryToLoadInLevels = "SELECT discipl_level, discipl_metorated, discipl_levelFinalJP," +
            " discipl_hasThereAttrPointFromThis, discipl_isThisPointSpent FROM character_chosenLevel" +
            " WHERE discipl_index=@dscpIndex AND character_id=@charId;";
        /// <summary>
        /// LOADS IN THE NORMAL LEVES OF THIS DSCP
        /// </summary>
        /// <returns>normLevel collection</returns>
        private List<CharDscpLevel> extractNormalLevels(int charId)
        {
            try
            {
                List<CharDscpLevel> temp = new List<CharDscpLevel>();
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                new KeyValuePair<string, object>("@charId", charId)};
                DataAccess.ConnectToDB();
                List<object[]> res = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInLevels, queryDatas, 2);
                foreach (object[] row in res)
                {
                    CharDscpLevel level = new CharDscpLevel(byte.Parse(row[0].ToString()),
                        byte.Parse(row[1].ToString()) == 1 ? true : false, short.Parse(row[2].ToString()),
                        byte.Parse(row[3].ToString()) == 1 ? true : false,
                        Int32.Parse(row[4].ToString()));
                    temp.Add(level);
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        private string queryToLoadInSpecials = "SELECT discipl_specIndex, discipl_level, discipl_levelFinalJP, discipl_requirSpec," +
            " discipl_specialAreaGroup, discipl_specialDescr, discipl_isThisPointSpent" +
            " FROM character_chosenSpec" +
            " WHERE discipl_index=@dscpIndex AND character_id=@charId;";
        /// <summary>
        /// LOADS IN THE SPECIALISATION LEVELS OF THIS DSCP
        /// </summary>
        /// <returns>specLevel collection</returns>
        private List<CharDscpSpecLevel> extractSpecLevels(int charId)
        {
            List<CharDscpSpecLevel> temp = new List<CharDscpSpecLevel>();
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                    new KeyValuePair<string, object>("@charId",charId)};
                DataAccess.ConnectToDB();
                List<object[]> res = DataAccess.ExecuteSQL_prep_outTable(queryToLoadInSpecials, queryDatas,2);
                foreach (object[] row in res)
                {
                    CharDscpSpecLevel level = new CharDscpSpecLevel(byte.Parse(row[0].ToString()),
                        byte.Parse(row[1].ToString()),
                        short.Parse(row[2].ToString()), byte.Parse(row[3].ToString()),
                        byte.Parse(row[4].ToString()), row[5].ToString(),
                        byte.Parse(row[6].ToString()));
                    temp.Add(level);
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        #endregion

        #region normal levels
        private string queryToAddNewNormalLevel =
            "INSERT INTO character_chosenLevel (discipl_index, character_id, discipl_level, discipl_levelFinalJP," +
            "discipl_metorated, discipl_hasThereAttrPointFromThis, discipl_isThisPointSpent)" +
            " VALUES (@dscpIndex,@charId,@dscpLevel,@dscpJP,@mentority,@comesPoint,@pointSpent);";
        /// <summary>
        /// CREATE A NEW LEVEL OF THIS DISCIPLINE
        /// </summary>
        /// <param name="level">level</param>
        /// <param name="mentor_practice">mentorated or practiced</param>
        /// <param name="jpCost">jp</param>
        public void gainNewLevel(int charId, bool mentor_practice, short jpCost)
        {
            try
            {
                bool attribRiseOption = false;
                byte nextLevel = getTheNextLevelMark();
                //DEFINE IS THERE ACCIDENTLY OVERLEVELING THE DSCP
                if (nextLevel > 10)
                    return;
                //DEFINE IS THERE POSSIBLLE THE ATTRIB RISE - IT IS FROM LVL 4
                if (nextLevel >= 4)
                    attribRiseOption = true;
                //ADD TO LIST
                CharDscpLevel newlevel = new CharDscpLevel(nextLevel, mentor_practice, jpCost, attribRiseOption, 0);
                theNormalLevelColl.Add(newlevel);
                //ADD TO DB
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                    new KeyValuePair<string, object>("@charId",charId),
                    new KeyValuePair<string, object>("@dscpLevel", nextLevel),
                    new KeyValuePair<string, object>("@dscpJP", jpCost),
                    new KeyValuePair<string, object>("@mentority", mentor_practice),
                    new KeyValuePair<string, object>("@comesPoint", attribRiseOption),
                    new KeyValuePair<string, object>("@pointSpent", 0)};
                DataAccess.ConnectToDB();
                if(!DataAccess.ExecuteNonSQL_prepManyParam(queryToAddNewNormalLevel, queryDatas, 7))
                    throw new CharModelRepoException("Szint adatbázisba jegyzés elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Új szint bejegyzése sikertelen ennél: "
                    + theDisciplIndex  + " sorszám->" + theDisciplineId + "\n" + e.Message);
            }
        }

        private string queryToRemoveSomeNormalLevel =
            "DELETE FROM character_chosenLevel WHERE discipl_index=@dscpIndex AND discipl_level>=@levelMark" +
            " AND character_id=@charId;";

        /// <summary>
        /// REMOVE SOME LEVEL FROM THE DISCIPLINE
        /// </summary>
        /// <param name="levelFrom">level from needed</param>
        public void removeTheseLevels(int charId, byte levelFrom)
        {
            try
            {
                //REMOVE FROM LIST
                theNormalLevelColl.RemoveAll(x => x.theLevelMark >= levelFrom);

                //REMOVE FROM DB
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                    new KeyValuePair<string, object>("@levelMark", levelFrom),
                    new KeyValuePair<string, object>("@charId",charId)  };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveSomeNormalLevel, queryDatas, 3))
                    throw new CharModelRepoException("A szint(ek) adatbázisból eltávolítása elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("A jártasság szintjeinek eltávolítása sikertelen ennél: " +
                    +theDisciplIndex+ " sorszám->"+ theDisciplineId +"\n" + e.Message);
            }
        }
        /// <summary>
        /// ANALYZE THE LEVEL COLLECTION WHICH IS THE HIGHEST LEVEL
        /// </summary>
        /// <returns>highest level mark</returns>
        public byte getTheHighestLevelMark()
        {
            try
            {
                byte max = 0;
                foreach (CharDscpLevel level in theNormalLevelColl)
                {
                    if (level.theLevelMark > max)
                        max = level.theLevelMark;
                }
                return max;
            }
            catch (Exception ex)
            {
                throw new CharModelRepoException("Sikertelen a legmagasabb szint elérése: " + theDisciplIndex +
                    " sorszám->" + theDisciplineId + "\n" + ex.Message);
            }
        }
        /// <summary>
        /// ANALYZES AND FINDS THIS DISCIPL NEXT LEVEL MARK
        /// </summary>
        /// <returns>next level</returns>
        private byte getTheNextLevelMark()
        {
            try
            {
                byte temp = getTheHighestLevelMark();
                temp++;
                return temp;
            }
            catch (Exception ex)
            {
                throw new CharModelRepoException("Sikertelen a következő szint elérése: " + theDisciplIndex +
                    " sorszám->" + theDisciplineId + "\n" + ex.Message);
            }
        }

        /*
        /// <summary>
        /// ANALYZE THE LEVEL COLLECTION - GIVES BACK THE HIGHEST LEVEL DATAS - REVIEW CASE
        /// </summary>
        /// <returns>the specific level datas</returns>
        public CharDscpLevel getTheHighestLevel()
        {
            try
            {
                byte max = getTheHighestLevelMark();
                return theNormalLevelColl.Find(x => x.theLevelMark == max);
            }
            catch (Exception ex)
            {
                throw new CharModelRepoException("Sikertelen a legmagasabb szint elérése: " + theDisciplIndex +
                    " sorszám->" + theDisciplineId + "\n" + ex.Message);
            }
        }
        */
        /// <summary>
        /// SEEKS THE NORMAL LAYERS OF NORMAL LEVELS - DETAILS CASE
        /// </summary>
        /// <returns></returns>
        public List<CharDscpLevel> getTheNormalLayers()
        {
            return theNormalLevelColl;
        }
        #endregion

        #region spec levels gain
        /// <summary>
        /// CREATE A SPEC LEVEL TO THE DISCIPLINE
        /// </summary>
        /// <param name="jp">jp</param>
        /// <param name="reqgroup">spec requir group, is chosen</param>
        /// <param name="area">chosen area</param>
        /// <param name="descr">description</param>
        public void gainNewSpecLevel(int charId, short jp, byte reqgroup, byte area, string descr)
        {
            try
            {
                if (isThereAlreadyThisSpecType(reqgroup, area, descr))
                    handleTheSpecLevelRise_existingOne(charId, jp, reqgroup, area, descr);
                else
                    handleTheSpecLevelCreation_notExistingOne(charId, jp, reqgroup, area, descr);
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /// <summary>
        /// ANALYZE IS THERE ALREADY THIS SPECIFIC SPECIALISATION
        /// </summary>
        /// <param name="area">seeked area</param>
        /// <param name="descr">seeked descr</param>
        /// <returns>is there this specialisation</returns>
        public bool isThereAlreadyThisSpecType(byte specGroup, byte area, string descr)
        {
            try
            {
                foreach (CharDscpSpecLevel level in theSpecLevelColl)
                {
                    if (level.theSpecArea == area && level.theSpecDescr == descr &&
                        level.theLevelRequirGroup == specGroup)
                        return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";"  + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// HELPER METHOD TO GAIN NEW LEVEL OF A SURELY EXISTING SPECIALISATION
        /// </summary>
        /// <param name="jp">jp</param>
        /// <param name="reqgroup">spec requir group, is chosen</param>
        /// <param name="area">chosen area</param>
        /// <param name="descr">description</param>
        /// <param name="comesAttribPoint">it gives attribValue rise</param>
        private void handleTheSpecLevelRise_existingOne(int charId, short jp, byte reqgroup, byte area,
            string descr)
        {
            try
            {
                byte specIndexUnderRise = getThisUniqueSpecIndex(reqgroup, area, descr);
                if (specIndexUnderRise == 0)
                    throw new CharModelRepoException("A művelet alatt áló specializáció azonosítója nincs meg!");
                byte nextLevelMark = getTheNextSpecLevelMark(specIndexUnderRise);
                if (nextLevelMark > 2)  //IF THE LEVEL REACHED THE MAX
                    return;
                //ADD TO LIST
                CharDscpSpecLevel newlevel = new CharDscpSpecLevel(specIndexUnderRise, nextLevelMark,
                    jp, reqgroup, area, descr, 0);
                theSpecLevelColl.Add(newlevel);
                //SAVE TO DB
                saveTheNewSpecLevelToDB(charId, specIndexUnderRise, nextLevelMark,
                    jp, reqgroup, area, descr);
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// FINDES THE SEEKED DSCP SPECID THAT ALREADY EXIST AND LEVEL NEEDED TO RISE
        /// </summary>
        /// <param name="specReqGroup">resquirGroup of seeked spec</param>
        /// <param name="specArea">area of seeked spec</param>
        /// <param name="specDescr">descr of seeked spec</param>
        /// <returns></returns>
        private byte getThisUniqueSpecIndex(byte specReqGroup, byte specArea, string specDescr)
        {
            try
            {
                foreach (CharDscpSpecLevel level in theSpecLevelColl)
                {
                    if (level.theLevelRequirGroup == specReqGroup && level.theSpecArea == specArea && level.theSpecDescr == specDescr)
                        return level.theSpecIndex;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);

            }
        }
        /// <summary>
        /// ANALYZES AND FINDS THE NEXT SPEC LEVEL OF THIS DISCIPL
        /// </summary>
        /// <param name="reqgroup">idnetifier requgroup</param>
        /// <param name="area">identifier area</param>
        /// <param name="descr">identifier descr</param>
        /// <returns>next spec level</returns>
        private byte getTheNextSpecLevelMark(byte specIndex)
        {
            try
            {
                byte max = 0;
                foreach (CharDscpSpecLevel level in theSpecLevelColl)
                {
                    if (level.theLevelMark > max && level.theSpecIndex == specIndex)
                        max = level.theLevelMark;
                }
                max++;
                return max;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);

            }
        }
        /// <summary>
        /// HELPER METHOD TO GAIN NEW LEVEL OF A SURELY NOT EXISTING SPECIALISATION
        /// </summary>
        /// <param name="jp">jp</param>
        /// <param name="reqgroup">spec requir group, is chosen</param>
        /// <param name="area">chosen area</param>
        /// <param name="descr">description</param>
        private void handleTheSpecLevelCreation_notExistingOne(int charId, short jp, byte reqgroup,
            byte area, string descr)
        {
            try
            {
                //ADD TO LIST
                byte nextSpecIndex = getTheNextAvailableSpecIndex(charId);
                CharDscpSpecLevel newSpeclevel = new CharDscpSpecLevel(nextSpecIndex, 1, jp, reqgroup,
                    area, descr, 0);
                theSpecLevelColl.Add(newSpeclevel);
                //SAVE TO DB
                saveTheNewSpecLevelToDB(charId, nextSpecIndex, 1, jp, reqgroup, area, descr);
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);

            }
        }
        /// <summary>
        /// FINDS THE NEXT AVAILABLE SPECIALISATION INDEX FROM CHAR
        /// </summary>
        /// <returns>specIndex</returns>
        private byte getTheNextAvailableSpecIndex(int charId)
        {
            try
            {
                byte indexMax = 0;
                foreach (CharDscpSpecLevel dsl in theSpecLevelColl)
                {
                    if (dsl.theSpecIndex > indexMax)
                        indexMax = dsl.theSpecIndex;
                }
                indexMax++;
                return indexMax;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        private string queryToAddNewSpecLevel =
            "INSERT INTO character_chosenSpec (discipl_index,discipl_specIndex,character_id, discipl_requirSpec," +
            "discipl_specialAreaGroup,discipl_specialDescr,discipl_level,discipl_levelFinalJP, discipl_isThisPointSpent)" +
            " VALUES (@dscpIndex,@specIndex,@charId,@requirGroup,@areaGroup,@descr,@levelMark,@jp,0);";
        /// <summary>
        /// HELPER METHOD TO SAVE TO THE DB THE NEW SPEC LEVELS
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="specIndex">specIndex</param>
        /// <param name="levelMark">nextLevelMark</param>
        /// <param name="jp">JP cost</param>
        /// <param name="reqgroup">chosen specRequirGroup</param>
        /// <param name="area">chosenArea</param>
        /// <param name="descr">descriptino</param>
        private void saveTheNewSpecLevelToDB(int charId, byte specIndex, byte levelMark, short jp,
            byte reqgroup, byte area, string descr)
        {
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex),
                new KeyValuePair<string, object>("@specIndex", specIndex),
                new KeyValuePair<string, object>("@charId",charId),
                new KeyValuePair<string, object>("@requirGroup", reqgroup),
                new KeyValuePair<string, object>("@areaGroup", area),
                new KeyValuePair<string, object>("@descr", descr),
                new KeyValuePair<string, object>("@levelMark", levelMark),
                new KeyValuePair<string, object>("@jp", jp)
            };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToAddNewSpecLevel, queryDatas, 8))
                    throw new CharModelRepoException("A specializáció adatbázisba jegyzése elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + " specIndex " + specIndex + ";" + e.TargetSite + " " + e.Message);
            }
        }

        #endregion

        #region spec levels remove
        private string queryToRemoveSpecLevels =
            "DELETE FROM character_chosenSpec WHERE discipl_index=@dscpIndex AND discipl_specIndex=@specIndex AND" +
            " discipl_level>=@level AND character_id=@charId;";
        /// <summary>
        /// REMOVE SOME SPEC SPECIALISATION
        /// </summary>
        /// <param name="specIndex">unwanted specIndex</param>
        /// <param name="levelFrom">the level needed to remove</param>
        public void removeTheseSpecLevels(int charId, byte specIndex, byte levelFrom)
        {
            try
            {
                //REMOVES FROM LIST
                theSpecLevelColl.RemoveAll(x => x.theSpecIndex == specIndex && x.theLevelMark >= levelFrom);
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                    new KeyValuePair<string, object>("@specIndex", specIndex),
                    new KeyValuePair<string, object>("@level", levelFrom),
                    new KeyValuePair<string, object>("@charId",charId) };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveSpecLevels, queryDatas, 4))
                    throw new CharModelRepoException("A specializáció szintje(i)nek eltávolítása elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + " specIndex " + specIndex + ";" + e.TargetSite + " " + e.Message);
            }
        }

        private const string queryToRemoveAllSpecLevels = "DELETE FROM character_chosenSpec" +
            " WHERE character_id=@charId AND discipl_index=@dscpIndex";
        /// <summary>
        /// REMOVE ALL SPECIALIZATION FROM DB
        /// </summary>
        /// <param name="charId">charId to remove</param>
        public void removeAllSpecLevels(int charId)
        {
            try
            {
                if (theSpecLevelColl.Count == 0)
                    return;
                theSpecLevelColl.Clear();
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@charId",charId),
                    new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex)
                };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveAllSpecLevels, queryDatas, 2))
                    throw new CharModelRepoException("Minden specializáció eltávolítása elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        #endregion

        #region spec leves generals
        /// <summary>
        /// ANALYZE IS THERE ALREADY SPECIALISATION AT ALL
        /// FOR SHOW AT THE SURFACE VIEW ON MANAGER
        /// </summary>
        /// <returns>is there already specalisation</returns>
        public bool isThereSpecAtAll()
        {
            try
            {
                if (theSpecLevelColl.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE MAX LEVLE OF A SPECIFIC SPECIALISATION AT CASE LEVEL RISE
        /// CASE OF REQUIREMENT SEEKING
        /// </summary>
        /// <param name="specRequirGroup"></param>
        /// <param name="specNote"></param>
        /// <returns></returns>
        public byte getTheHighestSpecLevelMark(byte specRequirGroup, byte areaId, string specNote)
        {
            try
            {
                byte temp = 0;
                foreach (CharDscpSpecLevel sp in theSpecLevelColl)
                {
                    if (sp.theLevelRequirGroup == specRequirGroup && sp.theSpecArea == areaId &&
                        sp.theSpecDescr == specNote && sp.theLevelMark > temp)
                        temp = sp.theLevelMark;
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        public byte getTheHighestSpecLevelMark(byte specIndex)
        {
            try
            {
                byte temp = 0;
                foreach (CharDscpSpecLevel sp in theSpecLevelColl)
                {
                    if (sp.theSpecIndex == specIndex)
                        temp = sp.theLevelMark;
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }


        public byte getTheNextSpecLevelMarkBySpecInxed(byte specIndex)
        {
            try
            {
                byte temp = 0;
                foreach (CharDscpSpecLevel sp in theSpecLevelColl)
                {
                    if (sp.theSpecIndex == specIndex && sp.theLevelMark > temp)
                    {
                        temp = sp.theLevelMark;
                    }
                }
                if (temp == 0)
                    return 1;   //IT IS STILL NOT THERE
                else
                    return temp;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /*
        /// <summary>
        /// SEEKS AND ANALYZE IN SPECIALISATION COLLECTION - FINDS ALL HIGHEST LEVELS - REVIEW CASE
        /// </summary>
        /// <returns></returns>
        public List<CharDscpSpecLevel> getTheHighestSpecLevels()
        {
            List<CharDscpSpecLevel> theHighestSpecColl = new List<CharDscpSpecLevel>();
            foreach (CharDscpSpecLevel level in theSpecLevelColl)
            {
                //IT REMOVES THE EXISTING FIRST LEVEL THEORETICLY THE LEVELS ARE IN ROW
                if (theHighestSpecColl.Exists(x => x.theSpecArea == level.theSpecArea &&
                      x.theSpecDescr == level.theSpecDescr &&
                      x.theLevelRequirGroup == level.theLevelRequirGroup &&
                      x.theLevelMark != level.theLevelMark))
                {
                    int index = theHighestSpecColl.FindIndex(x => x.theLevelMark == (level.theLevelMark - 1));
                    theHighestSpecColl.RemoveAt(index);
                }
                theHighestSpecColl.Add(level);  //ADD THIS LEVEL - IF THERE WAS ALREADY ONE, IT IS THE SECOND
            }
            return theHighestSpecColl;
        }
        */
        /// <summary>
        /// SEEKS ALL THE SPECIALISTAION LEVELS DATA - DETAILS CASE
        /// </summary>
        /// <returns></returns>
        public List<CharDscpSpecLevel> getTheSpecLayers()
        {
            return theSpecLevelColl;
        }



        /// <summary>
        /// ANALYZE IS THERE THE DSCP SPEC LEVELS
        /// CASE REMOVING
        /// </summary>
        /// <param name="specIndex"></param>
        /// <param name="levelToDelet"></param>
        /// <returns></returns>
        public bool isThereThisSpecLevels(byte specIndex, byte levelToDelet)
        {
            foreach (CharDscpSpecLevel level in theSpecLevelColl)
            {
                if (level.theSpecIndex == specIndex && level.theLevelMark == levelToDelet)
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// COLLECTS THE SPEC-GROUP-ID AND AREA-ID OF A SPECIALISATION
        /// </summary>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public byte[] findTheSpecRequirGroupAndArea_AtSpecLvlRise(byte specIndex)
        {
            try
            {
                byte[] tempDatas = new byte[2];
                foreach (CharDscpSpecLevel spec in theSpecLevelColl)
                {
                    if (spec.theSpecIndex == specIndex)
                    {
                        tempDatas[0] = spec.theLevelRequirGroup;
                        tempDatas[1] = spec.theSpecArea;
                        break;
                    }
                }
                return tempDatas;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " +  theDisciplineId
                    + " specIndex " + specIndex +";" + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// FINDS A SPEC DSCPRIPTION
        /// </summary>
        /// <param name="specIndex"></param>
        /// <returns></returns>
        public string findTheSpecDescr_AtSpecLvlRise(byte specIndex)
        {
            try
            {
                foreach (CharDscpSpecLevel spec in theSpecLevelColl)
                {
                    if (spec.theSpecIndex == specIndex)
                    {
                        return spec.theSpecDescr;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + " specIndex " + specIndex + ";" + e.TargetSite + " " + e.Message);
            }
        }


        public byte getTheChosenSpecRequirGroupIdOfThis(byte specIndex)
        {
            try
            {
                foreach (CharDscpSpecLevel spec in theSpecLevelColl)
                {
                    if (spec.theSpecIndex == specIndex)
                    {
                        return spec.theLevelRequirGroup;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + " specIndex " + specIndex + ";" + e.TargetSite + " " + e.Message);
            }
        }

        #endregion

        /// <summary>
        /// ANALYZE THE SUM JP, SPENT TO THIS DSCP - WITH SPECIALISTATIONS AS WELL
        /// </summary>
        /// <returns>valus of JP</returns>
        public int sumFullJP_ThisDscp()
        {
            try
            {
                int sum = 0;
                foreach (CharDscpLevel level in theNormalLevelColl)
                    sum += level.theLevelJP;
                foreach (CharDscpSpecLevel level in theSpecLevelColl)
                    sum += level.theLevelJP;
                return sum;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        #region attribSpending presenters
        /// <summary>
        /// ANALYZE THE DISCIPL COLLECTION IS THERE AN UNSPENT ATTRIB RISING LEVEL
        /// </summary>
        /// <returns>true=there is / false=none spendable</returns>
        public bool isThereUnspentAttribs()
        {
            try
            {
                foreach (CharDscpLevel level in theNormalLevelColl)
                {
                    if (level.theLevelHasAttribRise && level.theAttribRiseIsSpent == 0)
                        return true;
                }
                if (theSpecLevelColl.Count > 0)
                {
                    foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                    {
                        if (lev.theLevelMark == 2)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS THE LEVELS FRON THIS DISCIPL THATS ATTRIB POINT IS UNSPENT
        /// ONLY NORMAL LEVELS CAN BE SPENDABLE -> SPEC IS AUTOMATIC
        /// </summary>
        /// <returns></returns>
        public List<CharDscpUnspentAttrib> collectTheUnspentAttribs()
        {
            try
            {
                List<CharDscpUnspentAttrib> coll = new List<CharDscpUnspentAttrib>();
                foreach (CharDscpLevel level in theNormalLevelColl)
                {
                    byte levPoint = 0;
                    if (level.theLevelHasAttribRise && level.theAttribRiseIsSpent == 0)
                    {
                        if (level.theLevelMark == 4 || level.theLevelMark == 5 || level.theLevelMark == 6)
                            levPoint = 1;
                        if (level.theLevelMark == 7 || level.theLevelMark == 8 || level.theLevelMark == 9)
                            levPoint = 2;
                        if (level.theLevelMark == 10)
                            levPoint = 3;
                        CharDscpUnspentAttrib point = new CharDscpUnspentAttrib(
                           theDisciplIndex, theDisciplAttribId, level.theLevelMark, 0, levPoint);
                        coll.Add(point);
                    }
                }
                if (theSpecLevelColl.Count > 0)
                {
                    foreach (CharDscpSpecLevel level in theSpecLevelColl)
                    {
                        if (level.theSpecLevelAttribPointSpent == 0 && level.theLevelMark == 2)
                        {
                            CharDscpUnspentAttrib point = new CharDscpUnspentAttrib(
                                    theDisciplIndex, theDisciplAttribId, 2, level.theSpecIndex, 1);
                            coll.Add(point);
                        }
                    }
                }
                return coll;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// FINDS SOME DETAILS OF A NORMAL LEVELS THAT WAS THE SOURCE OF ATTRIB ENCHANCE
        /// HELPS THE LEVEL DELETION AND ORDERING ATTRIB RISE GOES WELL
        /// </summary>
        /// <param name="attribEnchIndex"></param>
        /// <param name="modeOfSerach"></param>
        /// <returns></returns>
        public int[] findDetailsSpentAttribPoint(int attribEnchIndex, byte modeOfSerach)
        {
            try
            {
                int[] tempFinal = null;
                if (modeOfSerach != 2)
                    tempFinal = new int[4];   //MAXIMAL AMOUNT OF VALUES - 1 SET OF LEVELS - AT MODE 1 AND 3
                else
                    tempFinal = new int[8];   //MAXIMAL AMOUNT OF VALUES - 2 SET OF LEVELS - AT MODE 2
                byte counter = 0;
                foreach (CharDscpLevel lev in theNormalLevelColl)
                {
                    if (lev.theAttribRiseIsSpent == attribEnchIndex)
                    {
                        tempFinal[counter++] = theDisciplIndex;
                        tempFinal[counter++] = theDisciplineId;
                        tempFinal[counter++] = lev.theLevelMark;
                        tempFinal[counter++] = 0;
                        if (modeOfSerach != 2)
                            break;
                    }
                }
                //CHECK IS THERE STILL NEED OF SEARCH IN SPEC-COLL - POSSIBLE MAKES QUICKER
                if (tempFinal[0] != 0 && modeOfSerach != 2)
                    return tempFinal;

                if (theSpecLevelColl.Count != 0)
                {
                    foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                    {
                        if (lev.theSpecLevelAttribPointSpent == attribEnchIndex)
                        {
                            tempFinal[counter++] = theDisciplIndex;
                            tempFinal[counter++] = theDisciplineId;
                            tempFinal[counter++] = lev.theLevelMark;
                            tempFinal[counter] = 1;
                            if (modeOfSerach != 2)
                                break;
                        }
                    }
                }
                return tempFinal;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        #endregion

        #region attribSpending processes

        private string queryToModifyDscpNormalLevelToAttribIsSpent =
            "UPDATE character_chosenLevel SET discipl_isThisPointSpent=@attribEnchIndex WHERE" +
            " character_id=@charId AND discipl_index=@dscpIndex AND discipl_level=@levelMark AND discipl_hasThereAttrPointFromThis=1;";
        /// <summary>
        /// SETS SPENT THE ATTRIB RISER POINT FROM A NORMAL LEVEL
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="level">level needed to alter</param>
        public void setSpentTheAttribPoint_FromNormalLevel(int charId, byte level, int attribEnchIndex)
        {
            try
            {
                bool elementFound = false;
                //MODIFY LIST
                foreach (CharDscpLevel lev in theNormalLevelColl)
                {
                    if (lev.theLevelMark == level)
                    {
                        lev.theAttribRiseIsSpent = attribEnchIndex;
                        elementFound = true;
                        break;
                    }
                }
                if (!elementFound)
                    throw new CharModelRepoException("Jártasságból a tulajdonságpont forrás listából nem került ki!");
                //MODIFY DB
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                     new KeyValuePair<string, object>("@attribEnchIndex",attribEnchIndex),
                     new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex),
                     new KeyValuePair<string, object>("@levelMark", level),
                     new KeyValuePair<string, object>("@charId", charId)
                };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToModifyDscpNormalLevelToAttribIsSpent, queryDatas, 4))
                    throw new CharModelRepoException("Jártasságból a tulajdonságpont forrás adatnázisba jegyzése elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        private const string queryToSpendSpecialLevelToAttribPoint =
            "UPDATE character_chosenSpec SET discipl_isThisPointSpent=@attribEnchInd WHERE" +
            " discipl_index=@dscpIndex AND character_id=@charId AND discipl_specIndex=@specIndex AND discipl_level=2;";
        public void setSpentTheAttribPoint_FromSpecLevel(int charId, byte specIndex, int attribEnchIndex)
        {
            try
            {
                bool elementFound = false;
                //MODIFY LIST
                foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                {
                    if (lev.theSpecIndex == specIndex && lev.theLevelMark == 2)
                    {
                        lev.theSpecLevelAttribPointSpent = attribEnchIndex;
                        elementFound = true;
                        break; 
                    }
                }
                if (!elementFound)
                    throw new CharModelRepoException("Jártasságspecializációból a tulajdonságpont forrás listából nem került ki!");
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@attribEnchInd",attribEnchIndex),
                    new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex),
                    new KeyValuePair<string, object>("@charId",charId),
                    new KeyValuePair<string, object>("@specIndex",specIndex),
                };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToSpendSpecialLevelToAttribPoint, queryDatas, 4))
                    throw new CharModelRepoException("Jártasságspecializációból a tulajdonságpont forrás adatnázisba jegyzése elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }


        private const string queryToRevertNormalLevelAttribSpending =
            "UPDATE character_chosenLevel SET discipl_isThisPointSpent=0 WHERE" +
            " character_id=@charId AND discipl_index=@dscpIndex AND discipl_hasThereAttrPointFromThis=1" +
            " AND discipl_isThisPointSpent=@attribEnchInd;";
        /// <summary>
        /// HELPER METHOD AT REVERT ATTRIB SPENDING FROM NORMAL DSCP LEVELS - SOURCE PART
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="level">levelMark</param>
        public void setUnspentTheAttribPoint_FromNormalLevel(int charId, int attribEnchIndex)
        {
            try
            {
                bool elementFound = false;
                //MODIFY LIST
                foreach (CharDscpLevel lev in theNormalLevelColl)
                {
                    if (lev.theAttribRiseIsSpent == attribEnchIndex)
                    {
                        lev.theAttribRiseIsSpent = 0;
                        elementFound = true;
                    }
                }
                if (!elementFound)
                    throw new CharModelRepoException("Jártasságból a tulajdonságpont forrás listából nem került vissza!");
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@charId",charId),
                    new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex),
                    new KeyValuePair<string, object>("@attribEnchInd",attribEnchIndex)
                };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRevertNormalLevelAttribSpending, queryDatas, 3))
                    throw new CharModelRepoException("Jártasságból a tulajdonságpont visszavonás forrás adatnázisba jegyzése elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        private const string queryToRevertSpecialLevelAttribSpending =
            "UPDATE character_chosenSpec SET discipl_isThisPointSpent=0 WHERE" +
            " discipl_index=@dscpIndex AND character_id=@charId AND discipl_level=2" +
            " AND discipl_isThisPointSpent=@attribEnchIndex;";
        /// <summary>
        /// HELPER METHOD AT REVERT ATTIB SPENDING FROM SPEC DSCP LEVELS - SOURCE PART
        /// </summary>
        /// <param name="charId">charId</param>
        /// <param name="dscpIndex">dscpIndex</param>
        /// <param name="specIndex">specIndex</param>
        public void setUnspentTheAttribPoint_FromSpecLevel(int charId, int attribEnchIndex)
        {
            try
            {
                bool elementFound = false;
                //MODIFY LIST
                foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                {
                    if (lev.theSpecLevelAttribPointSpent == attribEnchIndex && lev.theLevelMark == 2)
                    {
                        lev.theSpecLevelAttribPointSpent = 0;
                        elementFound = true;
                        break;
                    }
                }
                if (!elementFound)
                    throw new CharModelRepoException("Jártasságspecializációból a tulajdonságpont forrás listából nem került vissza!");
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@charId",charId),
                    new KeyValuePair<string, object>("@dscpIndex",theDisciplIndex),
                    new KeyValuePair<string, object>("@attribEnchIndex",attribEnchIndex)
                };

                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRevertSpecialLevelAttribSpending, queryDatas, 3))
                    throw new CharModelRepoException("Jártasságspecializációból a tulajdonságpont visszavonás forrás adatnázisba jegyzése elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /*
        /// <summary>
        /// FINDS THE SPEC-INDEX OF A SPEC LEVEL
        /// HELPS THE LEVEL DELETION AND ORDERING ATTRIB RISE GOES WELL
        /// </summary>
        /// <param name="attribEnchIndex"></param>
        /// <returns></returns>
        public byte findTheSpecIndexByAttribEnchant_ThatSurelyASpec_HelperRemoveAttribEnch(int attribEnchIndex)
        {
            byte[] tempRes = new byte[2];
            byte counter = 0;
            foreach (CharDscpSpecLevel lev in theSpecLevelColl)
            {
                if (lev.theSpecLevelAttribPointSpent == attribEnchIndex && lev.theLevelMark == 2)
                    tempRes[counter++] = lev.theSpecIndex;
            }
            return tempRes;
        }*/
        #endregion

        #region helper of dscpDeletion AttribEnhantment management ordering

        private const string queryIoFindAtNormalTableTheDscpEnchSource =
            "SELECT COUNT(discipl_isThisPointSpent) FROM character_chosenLevel WHERE" +
            " character_id=@charId AND discipl_index=@dscpIndex AND discipl_isThisPointSpent=@attribEnchIndex;";
        private const string queryIoFindAtSpecTableTheDscpEnchSource =
            "SELECT discipl_specIndex FROM character_chosenSpec WHERE" +
            " character_id=@charId AND discipl_index=@dscpIndex AND discipl_isThisPointSpent=@attribEnchIndex" +
            " AND discipl_level=2;";

        /// <summary>
        /// FINDS THE PROPER SPEC INDEX TO RESET THE OVERLAPPING DSCP AT ATTRIB-ENCH TYPE 1 THAT INTERFARES
        /// HELPS THE LEVEL DELETION AND ORDERING ATTRIB RISE GOES WELL
        /// </summary>
        /// <param name="theManagedCharId">charId</param>
        /// <param name="attribEnchIndex">attribEnchIndex</param>
        /// <returns>specIndex=0 -> normal level / >0 -> spec level</returns>
        public byte helperUnspentTheAttribPoint_UnknownTypeDetection(int theManagedCharId, int attribEnchIndex)
        {
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@charId", theManagedCharId),
                    new KeyValuePair<string, object>("@dscpIndex", theDisciplIndex),
                    new KeyValuePair<string, object>("@attribEnchIndex", attribEnchIndex)
                };

                //SEEK THE ATTRIB-ENCH-INDEX AT NORMAL LEVELS
                DataAccess.ConnectToDB();
                object tempNorm = DataAccess.ExecuteSQL_prep_outCell(queryIoFindAtNormalTableTheDscpEnchSource, queryDatas, 3);
                byte res = Convert.ToByte(tempNorm.ToString());
                if (res == 1)
                    return 0;

                //SEEK THE ATTRIB-ENCH-INDEX AT SPEC LECELS
                DataAccess.ConnectToDB();
                object tempSpec = DataAccess.ExecuteSQL_prep_outCell(queryIoFindAtSpecTableTheDscpEnchSource, queryDatas, 3);
                res = Convert.ToByte(tempSpec.ToString());
                return res;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + " attribEnchIndex " + attribEnchIndex +" ;" + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE ATTRIB-ENCHANT-INDEXES THAT CONNECTED TO THIS DSCP
        /// </summary>
        /// <returns></returns>
        public int[] findAllAttribEnchantOfThisDscp_UnderDeletion()
        {
            try
            {
                List<int> tempAttribEnchIndexOfLevels = new List<int>();
                foreach (CharDscpLevel lev in theNormalLevelColl)
                {
                    if (lev.theAttribRiseIsSpent > 0)
                        tempAttribEnchIndexOfLevels.Add(lev.theAttribRiseIsSpent);
                }
                foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                {
                    if (lev.theSpecLevelAttribPointSpent > 0)
                        tempAttribEnchIndexOfLevels.Add(lev.theSpecLevelAttribPointSpent);
                }
                return tempAttribEnchIndexOfLevels.ToArray();
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE ATTRIB ENCH INDEX OF LVL 2 AT STRICT SPEC
        /// </summary>
        /// <param name="specIndex">specIndex</param>
        /// <returns>attribEnchIndex</returns>
        public int findTheAttribEnchantOfThisSpecIndex_UnderDeletion(byte specIndex)
        {
            try
            {
                foreach (CharDscpSpecLevel lev in theSpecLevelColl)
                {
                    if (lev.theSpecIndex == specIndex && lev.theLevelMark == 2)
                    {
                        return lev.theSpecLevelAttribPointSpent;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }

        /// <summary>
        /// FINDS THE ALL ATTRIB ENCHANTS OF DSCP THAT UNDER DELETION
        /// </summary>
        /// <param name="levelFrom">levelFrom under delete</param>
        /// <returns>attribEnchIndexes</returns>
        public int[] findTheAttribEnchantOfNormalLevels_UnderDeletion(byte levelFrom)
        {
            try
            {
                List<int> result = new List<int>();
                foreach (CharDscpLevel lev in theNormalLevelColl)
                {
                    if (lev.theLevelMark >= levelFrom && lev.theAttribRiseIsSpent > 0)
                        result.Add(lev.theAttribRiseIsSpent);
                }
                return result.ToArray();
            }
            catch (Exception e)
            {
                throw new CharModelRepoException("Index " + theDisciplIndex + " id " + theDisciplineId
                    + ";" + e.TargetSite + " " + e.Message);
            }
        }
        #endregion
    }
}
