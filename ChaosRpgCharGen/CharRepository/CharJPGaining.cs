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
    /// REPOSITORY OF THE JP-S THAT A CHARACTER COLLECTED DURRING DEVELOPMENT
    /// SUMMARISE AND MANAGE - BUT IT NOT CONTAINS THA PREDEFINED, STARTER JP COMES FROM CHAR-GENERATION
    /// </summary>
    public class CharJPGaining : ICharJPGaining
    {
        private int theCharIdUnderManage;
        private List<CharOneJPGain> theGainedJPCollect;
        /// <summary>
        /// CONSTRUCTOR OF THIS REPOSITORY
        /// </summary>
        public CharJPGaining(int charId)
        {
            try
            {
                theCharIdUnderManage = charId;
                theGainedJPCollect = extractJPGainsFromDB();
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("Sikertelen karakter-adat beolvasás! - JártasságPontok\n" + ex.Message);
            }
        }

        #region HELPER EXTRACTOR METHOD

        private string queryToLoadInJPGains = 
            "SELECT jp_index, character_collectedJP FROM character_collectJP WHERE character_id=@charId;";
        /// <summary>
        /// HELPER METHOD TO LOAD IN JPGAIN DATAS FROM DB
        /// </summary>
        /// <returns>collection of JpGains</returns>
        private List<CharOneJPGain> extractJPGainsFromDB()
        {
            List<CharOneJPGain> temp = new List<CharOneJPGain>();
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("@charId", theCharIdUnderManage) };

                DataAccess.ConnectToDB();
                List<object[]> res =  DataAccess.ExecuteSQL_prep_outTable(queryToLoadInJPGains, queryDatas, 1);
                if (res.Count != 0)
                {
                    foreach (object[] row in res)
                    {
                        CharOneJPGain jp = new CharOneJPGain(int.Parse(row[0].ToString()), int.Parse(row[1].ToString()));
                        temp.Add(jp);
                    }
                }
                return temp;
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("A szerzett JP-k beolvasása sikertelen!\n" + ex.Message);
            }
        }

        #endregion

        #region GAIN NEW JP

        private string queryToAddNewJPGain = "INSERT INTO character_collectJP (character_id, jp_index, character_collectedJP)" +
            " VALUES (@charId,@gainJPIndex,@jpValue);";
        /// <summary>
        /// ADDS THE NEW JPGain TO COLLECTION AND DB
        /// </summary>
        /// <param name="JPAmount"></param>
        public void addNewJPGain(int JPAmount)
        {
            try
            {
                int nextJPGainIndex = findTheNextJPGainIndex();
                //ADD TO LIST
                CharOneJPGain newOne = new CharOneJPGain(nextJPGainIndex, JPAmount);
                theGainedJPCollect.Add(newOne);

                //ADD TO DB
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("@charId", theCharIdUnderManage),
                    new KeyValuePair<string, object>("@gainJPIndex", nextJPGainIndex),
                    new KeyValuePair<string, object>("@jpValue", JPAmount)};
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToAddNewJPGain, queryDatas, 3))
                    throw new CharRepositoryException("JP érték adatbázisba jegyzése elmaradt!");
            }
            catch(Exception e)
            {
                throw new CharRepositoryException("A szerzett JP mentése sikertelen\n" + e.Message);
            }
        }

        /// <summary>
        /// SEEKS THE NEXT, UNUSED JPGainId
        /// </summary>
        /// <returns>UNUSED INDEX</returns>
        private int findTheNextJPGainIndex()
        {
            try
            {
                int index = 0;
                if (theGainedJPCollect.Count != 0)
                {
                    index = theGainedJPCollect.Max(x => x.theGainingId);
                    index++;
                    return index;
                }
                else
                    return 1;
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("Szerzett JP index keresési probléma!\n" + ex.Message);
            }
        }

        #endregion

        #region REMOVE EXISTING JP

        private string queryToRemoveAJPGain = "DELETE FROM character_collectJP WHERE jp_index=@gainJPIndex AND character_id=@charId;";
        /// <summary>
        /// REMOVES THE UNWANTED JPGain record
        /// </summary>
        /// <param name="gainId">unwanted record ID</param>
        public void removeThisJPGain(int gainId)
        {
            try
            {
                theGainedJPCollect.RemoveAll(x => x.theGainingId == gainId);

                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@gainJPIndex", gainId),
                    new KeyValuePair<string, object>("@charId", theCharIdUnderManage)
                };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveAJPGain, queryDatas, 2))
                    throw new CharRepositoryException("JP-érték adatbázisból kivonása elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharRepositoryException("A szerzett JP eltávolítása sikertelen!\n" + e.Message);
            }
        }

        #endregion

        #region GETTER AND SUMMARISE METHOD

        /// <summary>
        /// GETTER OF THE JP GAIN COLLECTION - FOR DETAILED JP SURFACE
        /// </summary>
        /// <returns>COLLECTION OF JPGain</returns>
        public List<CharOneJPGain> getTheJPGainCollection()
        {
            return theGainedJPCollect;
        }


        /// <summary>
        /// SUM THE JP AMOUNTS IN COLLECTION
        /// </summary>
        /// <returns>JP value</returns>
        public int sumAllCollectedJP()
        {
            try
            {
                int sum = 0;
                foreach (CharOneJPGain gain in theGainedJPCollect)
                {
                    sum += gain.theJPAmount;
                }
                return sum;
            }
            catch (Exception ex)
            {
                throw new CharRepositoryException("A jártasság teljes JP-jének számolása sikertelen!\n" + ex.Message);
            }
        }

        #endregion
    }
}
