using ChaosRpgCharGen.GeneralModel;
using ChaosRpgCharGen.Database;
using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.GeneralRepository
{
    /// <summary>
    /// REPOSITORY OF CHARACTERS IN SYSTEM, MANAGES THE EXISTENCE
    /// </summary>
    public class GeneralReviewTrunkRepo : IGeneralReviewCharacterTrunk
    {
        private List<GeneralOneTrunkEntity> theCharacters;
        /// <summary>
        /// CONSTRUCTOR OF SYSTEM CHARACTER REPOSITORY
        /// </summary>
        public GeneralReviewTrunkRepo()
        {
            try
            {
                extractCharactersFromDB();
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Messagee);
            }
        }

        #region character loadin and getter methods

        private const string queryReadInSysCharacters =
            "SELECT character_id, character_trunk.race_id, raceName, character_name, character_descr," +
            " beneficial_discipl, character_trunk.character_realSize, chaos_raceSizes.sizeDescr," +
            " character_realSpeed, character_starterJP" +
            " FROM character_trunk, chaos_raceBase_attribRestraints, chaos_raceSizes" +
            " WHERE character_trunk.race_id=chaos_raceBase_attribRestraints.race_id AND" +
            " chaos_raceBase_attribRestraints.basicSize=chaos_raceSizes.basicSize_id;";

        /// <summary>
        /// HELPER METHOD - EXTRACTING THE CHARACTER TRUNKS FROM DB
        /// </summary>
        private void extractCharactersFromDB()
        {
            try
            {
                theCharacters = new List<GeneralOneTrunkEntity>();
                DataAccess.ConnectToDB();
                List<object[]> datas = DataAccess.ExecuteSQL_normal_outTable(queryReadInSysCharacters);
                for (int i = 0; i < datas.Count; i++)
                {
                    GeneralOneTrunkEntity charEntity = new GeneralOneTrunkEntity(
                        int.Parse(datas[i][0].ToString()), byte.Parse(datas[i][1].ToString()),
                        datas[i][2].ToString(), datas[i][3].ToString(), datas[i][4].ToString(),

                        byte.Parse(datas[i][5].ToString()), byte.Parse(datas[i][6].ToString()),
                        datas[i][7].ToString(), byte.Parse(datas[i][8].ToString()),

                        int.Parse(datas[i][9].ToString()));
                    theCharacters.Add(charEntity);
                }
            }
            catch(Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }


        /// <summary>
        /// GETTER OF CHARACTER LIST OF SYSTEM - SHOWING AT REVIEW WINDOW
        /// </summary>
        /// <returns>collection of characters</returns>
        public List<GeneralOneTrunkEntity> findTheSystemCharacters()
        {
            return theCharacters;
        }
        /// <summary>
        /// GETTER OF A CHARACTER TO OPEN FOR MANAGE THAT
        /// </summary>
        /// <param name="charId">seeked charId</param>
        /// <returns>trunk datas</returns>
        public GeneralOneTrunkEntity getCharEntityToOpen(int charId)
        {
            return theCharacters.Find(x => x.theCharId == charId);
        }

        #endregion

        #region create new character
        /// <summary>
        /// ADD NEW CHARACTER INTO SYSTEM
        /// </summary>
        /// <param name="newChar">character trunk values</param>
        /// <param name="attribValues">character stats</param>
        /// <returns>created charId / 0 if failed Trunk datas / -1 if failed Stat datas </returns>
        public int addNewCharacterIntoSystem(GeneralOneTrunkEntity newChar, short[] attribValues)
        {
            try
            {
                newChar.theCharId = findTheNextCharIdFromDB();
                theCharacters.Add(newChar);
                //DB MANAGEMENT
                KeyValuePair<string, object> id = new KeyValuePair<string, object>("@charId", newChar.theCharId);
                if (!saveTrunkToDb(id, newChar))
                    return 0;
                if (!saveStatToDb(id, attribValues))
                    return -1;
                extractCharactersFromDB();
                return newChar.theCharId;
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);

            }
        }

        /// <summary>
        /// HELPER METHOD - FINDS THE NEXT UNUSED CHARACTER ID
        /// </summary>
        /// <returns>next charId</returns>
        private int findTheNextCharIdFromDB()
        {
            //SEEK MAX IN LIST
            int max1 = theCharacters.Max(x => x.theCharId);
            max1++;
            return max1;
        }
        private const string querySaveNewSysCharacter =
            "INSERT INTO character_trunk(character_id,race_id,race_name,character_name,character_descr," +
            " beneficial_discipl,character_realSize,character_textSize,character_realSpeed,character_starterJP," +
            " lastupdate_timestamp) VALUES" +
            " (@charId,@raceId,@raceName,@charName,@charDescr,@charBenDscp,@charSize,@charSizeText,@charSpeed,@charBasicJp," +
            " DATE());";
        /// <summary>
        /// HELPER METHOD OF ADD NEW CHARACTER - ADD THE NEW CHAR TRUNK TO DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="raceDetails"></param>
        /// <param name="details"></param>
        private bool saveTrunkToDb(KeyValuePair<string, object> id, GeneralOneTrunkEntity newChar)
        {
            try
            {
                //ADDING TRUNK DATAS TO DB
                KeyValuePair<string, object>[] datasToTrunkQuery = new KeyValuePair<string, object>[10];
                datasToTrunkQuery[0] = id;
                datasToTrunkQuery[1] = new KeyValuePair<string, object>("@raceId", newChar.theRaceId);
                datasToTrunkQuery[2] = new KeyValuePair<string, object>("@raceName", newChar.theRaceName);
                datasToTrunkQuery[3] = new KeyValuePair<string, object>("@charName", newChar.theCharName);
                datasToTrunkQuery[4] = new KeyValuePair<string, object>("@charDescr", newChar.theCharDescr);
                datasToTrunkQuery[5] = new KeyValuePair<string, object>("@charBenDscp", newChar.theBeneficDscp);
                datasToTrunkQuery[6] = new KeyValuePair<string, object>("@charSize", newChar.theRealSize);
                datasToTrunkQuery[7] = new KeyValuePair<string, object>("@charSpeed", newChar.theRealSpeed);
                datasToTrunkQuery[8] = new KeyValuePair<string, object>("@charSizeText", newChar.theSizeText);
                datasToTrunkQuery[9] = new KeyValuePair<string, object>("@charBasicJp", newChar.theStarterJPValue);
                DataAccess.ConnectToDB();
                return DataAccess.ExecuteNonSQL_prepManyParam(querySaveNewSysCharacter, datasToTrunkQuery, 10) ? true : false;
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private const string querySaveNewCharStats =
            "INSERT INTO character_attribStats (character_id,attrib1_basicPhyisique,attrib2_basicStrenght,attrib3_basicStamina," +
            "attrib4_basicEfficiency,attrib5_basicDexterity,attrib6_basicReflex,attrib7_basicConscience,attrib8_basicIntegency," +
            "attrib9_basicFortitude,attrib10_basicEssence,attrib11_basicMagic,attrib12_basicEssenceshild) VALUES" +
            " (@charId,@phys,@stren,@stam,@effic,@dext,@refl,@consc,@intel,@fortit,@essenc,@magic,@eccshi);";
        /// <summary>
        /// HELPER METHOD OF ADD NEW CHARACTER - ADD THE NEW CHARACTER STATS TO DB
        /// </summary>
        /// <param name="id">charId</param>
        /// <param name="attribValues">statCollection</param>
        private bool saveStatToDb(KeyValuePair<string, object> id, short[] attribValues)
        {
            try
            {
                //ADDING STAT DATAS TO DB
                KeyValuePair<string, object>[] datasToStatQuery = new KeyValuePair<string, object>[13];
                datasToStatQuery[0] = id;
                datasToStatQuery[1] = new KeyValuePair<string, object>("@phys", attribValues[0]);
                datasToStatQuery[2] = new KeyValuePair<string, object>("@stren", attribValues[1]);
                datasToStatQuery[3] = new KeyValuePair<string, object>("@stam", attribValues[2]);
                datasToStatQuery[4] = new KeyValuePair<string, object>("@effic", attribValues[3]);
                datasToStatQuery[5] = new KeyValuePair<string, object>("@dext", attribValues[4]);
                datasToStatQuery[6] = new KeyValuePair<string, object>("@refl", attribValues[5]);
                datasToStatQuery[7] = new KeyValuePair<string, object>("@consc", attribValues[6]);
                datasToStatQuery[8] = new KeyValuePair<string, object>("@intel", attribValues[7]);
                datasToStatQuery[9] = new KeyValuePair<string, object>("@fortit", attribValues[8]);
                datasToStatQuery[10] = new KeyValuePair<string, object>("@essenc", attribValues[9]);
                datasToStatQuery[11] = new KeyValuePair<string, object>("@magic", attribValues[10]);
                datasToStatQuery[12] = new KeyValuePair<string, object>("@eccshi", attribValues[11]);
                DataAccess.ConnectToDB();
                return DataAccess.ExecuteNonSQL_prepManyParam(querySaveNewCharStats, datasToStatQuery, 13) ? true : false;
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region remove existing character
        /// <summary>
        /// REMOVE A CHARACTER FROM SYSTEM
        /// WORKS EVEN IF THERE IS ONLY ONE TABLE RECORD
        /// </summary>
        /// <param name="charId">the unwanted charId</param>
        /// <returns>tru=successful / false=failed</returns>
        public bool removeCharacterFromSystem(int charId)
        {
            try
            {
                KeyValuePair<string, object> queryData = new KeyValuePair<string, object>("@charId", charId);
                theCharacters.RemoveAll(x => x.theCharId == charId);
                //DB MANAGEMENT
                removeCharacterFromDB(queryData);
                return reviseTheDeletion(queryData);
            }
            catch(Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);

            }
        }

        //REMOVE ATTRIBS
        private const string queryRemoveThisChar_attribEncn = "DELETE FROM character_attribEnchance WHERE character_id=@charId;";
        private const string queryRemoveThisChar_attrib = "DELETE FROM character_attribStats WHERE character_id=@charId;";
        //REMOVE JP
        private const string queryRemoveThisChar_jps = "DELETE FROM character_collectJP WHERE character_id=@charId;";
        //REMOVE DSCP
        private const string queryRemoveThisChar_dscpNorm =
            "DELETE FROM character_chosenLevel WHERE character_id=@charId;";
        private const string queryRemoveThisChar_dscpSpec =
            "DELETE FROM character_chosenSpec WHERE character_id=@charId;";
        private const string queryRemoveThisChar_dscp = "DELETE FROM character_chosenDiscipl WHERE character_id=@charId;";
        //REMOVE TRUNK
        private const string queryRemoveThisChar_trunk = "DELETE FROM character_trunk WHERE character_id=@charId;";
        /// <summary>
        /// HELPER REMOVE METHOD - DELET THE ALL CHARACTER DATAS fROM DB
        /// </summary>
        /// <param name="charId">charId</param>
        private void removeCharacterFromDB(KeyValuePair<string, object> queryData)
        {
            try
            {
                List<OneDBTransactionEntity> task = new List<OneDBTransactionEntity>();
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_attribEncn, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_attrib, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_jps, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_dscpNorm, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_dscpSpec, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_dscp, queryData));
                task.Add(new OneDBTransactionEntity(queryRemoveThisChar_trunk, queryData));
                DataAccess.ConnectToDB();
                DataAccess.ExecuteNonSQL_prepOneParam_Trans(task);
            }
            catch (Exception e)
            {
                throw new GeneralRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private string[] sqlCollToReviseRemove = new string[] {
            "SELECT COUNT(character_id) FROM character_trunk WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_chosenDiscipl WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_chosenSpec WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_chosenLevel WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_collectJP WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_attribStats WHERE character_id=@charId;",
            "SELECT COUNT(character_id) FROM character_attribEnchance WHERE character_id=@charId;"  };

        /// <summary>
        /// HELPER REMOVE METHOD - REVISE THE ALL RECORDS DELETED
        /// </summary>
        /// <param name="charId"></param>
        private bool reviseTheDeletion(KeyValuePair<string, object> queryData)
        {
            DataAccess.ConnectToDB();
            object[] res = DataAccess.ExecuteSQL_prepForManyQuery_outCellRow(sqlCollToReviseRemove, queryData);
            foreach(object cell in res)
            {
                if (Convert.ToInt32(cell) != 0)
                    return false;
            }
            return true;
        }

        #endregion
    }
}
