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
    /// REPOSITORY OF CHARACTER ATTRIBUTE STATS AND ENCHANCES
    /// NO STAT MODIFY, ONLY THE EXECUTED ATTRIB-ENCHANCES SUMMING 
    ///     -> DISTINGUSHABLE AND PRESENTED AT VIEW-ATTRIB-MANAGE
    /// HANDLE THE OCCURED ATTRIB-ENCHANTMENTS AT APPLIANCE-SIDE
    /// </summary>
    public class CharAttributes : ICharAttributes
    {
        private List<CharOneAttrib> theAttribColl;  //IT WILL NO CHANGE OF BASIC VALUE - NO ADD OR REMOVE
        private List<CharOneAttribEnch> theEnchColl;    //IT CAN BE ADDED OR REMOVED VALUES

        private int theManagedCharId;
        /// <summary>
        /// CONSTRUCTOR OF THE CHARACTER ATTRIBUTE PARAMENTERS
        /// </summary>
        public CharAttributes(int charId)
        {
            try
            {
                theManagedCharId = charId;
                theAttribColl = extractTheCharAttrib();
                theEnchColl = extractTheCharAtrrEnchances();
                recountTheAttributesCollectively();
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #region helper DB reader
        private string queryOfExtractAttribStats =
                "SELECT * FROM character_attribStats WHERE character_id=@charId;";
        /// <summary>
        /// EXTRACT THE STATS FROM DB OF THIS CHARACTER
        /// </summary>
        /// <param name="charId">charId</param>
        /// <returns>lists of attribs</returns>
        private List<CharOneAttrib> extractTheCharAttrib()
        {
            List<CharOneAttrib> temp = new List<CharOneAttrib>();
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("@charId", theManagedCharId)};
                DataAccess.ConnectToDB();
                List<object[]> datas = DataAccess.ExecuteSQL_prep_outTable(queryOfExtractAttribStats, queryDatas, 1);
                if (datas.Count == 0)
                    throw new CharRepositoryException("Nincs elérhető tulajdonság adat!");
                for (byte k = 1; k < datas[0].Length; k++)  //THE 0. ELEMENT IS THE CHAR_ID
                {
                    CharOneAttrib attr = new CharOneAttrib(k, short.Parse(datas[0][k].ToString()));
                    temp.Add(attr);
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        private string queryOfExtractionAttribEcnh =
            "SELECT attrib_echanceIndex, attrib_id, discipl_index1,  discipl_index2, discipl_pointFeedbackType" +
            " FROM character_attribEnchance WHERE character_id=@charId;";
        /// <summary>
        /// EXTRACT THE CHARACTER ATTRIB ENCHANCES
        /// </summary>
        /// <returns></returns>
        private List<CharOneAttribEnch> extractTheCharAtrrEnchances()
        {
            List<CharOneAttribEnch> temp = new List<CharOneAttribEnch>();
            try
            {
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("@charId", theManagedCharId)};
                DataAccess.ConnectToDB();
                List<object[]> datas = DataAccess.ExecuteSQL_prep_outTable(queryOfExtractionAttribEcnh, queryDatas, 1);
                if (datas.Count == 0)
                    return temp;
                for (int i = 0; i < datas.Count; i++)
                {
                    CharOneAttribEnch attr = new CharOneAttribEnch(
                        int.Parse(datas[i][0].ToString()), byte.Parse(datas[i][1].ToString()),
                        short.Parse(datas[i][2].ToString()), short.Parse(datas[i][3].ToString()),
                        byte.Parse(datas[i][4].ToString()));
                    temp.Add(attr);
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region attrib manage - presenters
        /// <summary>
        /// IT RECOUNT THE ALL ENCHANCED POINTS ALL TIMES
        /// </summary>
        private void recountTheAttributesCollectively()
        {
            try
            {
                theAttribColl.ForEach(x => x.theAttributeRisingValue = 0);
                foreach (CharOneAttribEnch ench in theEnchColl)
                {
                    foreach (CharOneAttrib attr in theAttribColl)
                    {
                        if (ench.theAttributeId == attr.theAttribId)
                            attr.theAttributeRisingValue++;
                    }
                }
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// SHARES THE ATTRIBUTE VALUE THAT IS SEEKED - IF A DISCIPLINE DETAILS NEEDS IT
        /// </summary>
        /// <param name="attribId">attribID</param>
        /// <returns>full value</returns>
        public short getTheFullValueOfThisAttribute(byte attribId)
        {
            try
            {
                foreach (CharOneAttrib attr in theAttribColl)
                {
                    if (attr.theAttribId == attribId)
                        return attr.getTheFullAttribValue();
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECTS THE FINAL VALUE OF ATTRIBUTES
        /// </summary>
        /// <returns>final attribValue palette</returns>
        public short[] getTheAttributesCollectively_ToShowFinal()
        {
            try
            {
                theAttribColl.Sort();   //IComparable is written for this
                recountTheAttributesCollectively();
                short[] temp = new short[12];
                for (byte i = 0; i < temp.Length; i++)
                {
                    temp[i] = theAttribColl[i].getTheFullAttribValue();
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// COLLECT THE STARTER AND STAT-RISED VALUES OF ALL STATS
        /// IT IS CONNECTED SELF COMPARATOR TO GET IN ARRAY ALL IN GOOD ORDER
        /// </summary>
        /// <returns></returns>
        public short[,] getTheAttrubutesCollectively_ToShowInDetails()
        {
            try
            {
                theAttribColl.Sort();   //IComparable is written for this
                recountTheAttributesCollectively();
                /*
                short[,] temp = new short[12, 3];   //IT WAS FOR VERIFICATION AT TEST PROJECT THE IComparable WORKS
                for (byte i = 0; i < temp.GetLength(0); i++)
                {
                    temp[i, 0] = theAttribColl[i].theAttribId;
                    temp[i, 1] = theAttribColl[i].theAttributeBaseValue;
                    temp[i, 2] = theAttribColl[i].theAttributeRisingValue;
                }
                */
                short[,] temp = new short[12, 2];
                for (byte i = 0; i < temp.GetLength(0); i++)
                {
                    temp[i, 0] = theAttribColl[i].theAttributeBaseValue;
                    temp[i, 1] = theAttribColl[i].theAttributeRisingValue;
                }
                return temp;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region attribEnch manage processes - result summarize, threat only attribEnchTable

        private string queryToAddNewAttribEnch = "INSERT INTO character_attribEnchance" +
            " (character_id, attrib_echanceIndex, discipl_index1,discipl_index2, attrib_id, discipl_pointFeedbackType)" +
            " VALUES (@charId,@attribEchIndex,@dscp1,@dscp2,@attribId,@pointType);";
        /// <summary>
        /// ADD THE RISEN ATTRIB POINT TO COLLECTIONS - IT IS THE APPLIANCE PART
        /// </summary>
        /// <param name="attribId">attrib that has new point</param>
        /// <param name="dscpIndex1">discipl1 from comes</param>
        /// <param name="dscpIndex2">discipl2 from may comes</param>
        /// <param name="pointType">point chategory</param>
        /// <returns>theAttribEnchIndex to measure at dscpRepo</returns>
        public int addOnePointRiseToSpecifAttribute(byte attribId, int dscpIndex1, int dscpIndex2,
            byte pointType)
        {
            try
            {
                int nextAttribEnch = findTheNextAttribEnchID();
                CharOneAttribEnch temp = new CharOneAttribEnch(nextAttribEnch, attribId,
                    dscpIndex1, dscpIndex2, pointType);
                theEnchColl.Add(temp);

                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[]{
                    new KeyValuePair<string, object>("@charId", theManagedCharId),
                    new KeyValuePair<string, object>("@attribEchIndex", nextAttribEnch),
                    new KeyValuePair<string, object>("@dscp1", dscpIndex1),
                    new KeyValuePair<string, object>("@dscp2", dscpIndex2),
                    new KeyValuePair<string, object>("@attribId", attribId),
                    new KeyValuePair<string, object>("@pointType", pointType)
                };
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToAddNewAttribEnch, queryDatas, 6))
                    throw new CharRepositoryException("A karakter-tulajdonságfejlesztés adatbázisba jegyzése elmaradt!");
                return nextAttribEnch;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// ANALYZE THE ATTRIB ENCH COLLECTION - FINDS THE NEXT INDEX
        /// </summary>
        /// <returns>the next index</returns>
        private int findTheNextAttribEnchID()
        {
            try
            {
                if (theEnchColl.Count == 0)
                    return 1;
                else
                {
                    int max1 = theEnchColl.Max(x => x.theAttribEnchanceIndex);
                    max1++;
                    return max1;
                }
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        private string queryToRemoveAttribEcnhAtAttrib =
            "DELETE FROM character_attribEnchance WHERE attrib_echanceIndex=@attribEnchIndex;";

        /// <summary>
        /// REMOVES THE CHOSEN ATTRIB ENCH - IT IS THE APPLIANCE PART
        /// </summary>
        /// <param name="attribEnchIndex">that needed to remove</param>
        public void removeOneAttribEnchElement(int attribEnchIndex)
        {
            try
            {
                //THIS COMES FROM DELETE DSCP METHOD -> AND THE ATTRIB RISE DIDN'T SPENT THIS CASE
                if (attribEnchIndex == 0)
                    return;

                //REMOVE FORM LIST
                theEnchColl.RemoveAll(x => x.theAttribEnchanceIndex == attribEnchIndex);

                //REMOVE FROM DB
                KeyValuePair<string, object>[] queryDatas = new KeyValuePair<string, object>[] {
                new KeyValuePair<string, object>("@attribEnchIndex", attribEnchIndex)};
                DataAccess.ConnectToDB();
                if (!DataAccess.ExecuteNonSQL_prepManyParam(queryToRemoveAttribEcnhAtAttrib, queryDatas, 1))
                    throw new CharRepositoryException("A tulajdonságfejlesztés adatbázisból kivonása elmaradt!");
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region attribEnch manage - presenters
        /// <summary>
        /// SHARES THE ATTRIBUTE ENCHANCING COLLECTION - CASE DETAIL OF ATTRIBS -> goes to list
        /// </summary>
        /// <returns>attrib enchanes datas</returns>
        public List<CharOneAttribEnch> collectTheAttributeEnchList_ToShow()
        {
            return theEnchColl;
        }

        /// <summary>
        /// FINDS ALL THE ATTRIB-ENCHANTE-INDEXES AND DSCP-INDEXES THAT OVERLAPS WITH THE ENCHANTS OF DSCP UNDER DELETE
        /// </summary>
        /// <param name="attribEnchIndexesThatAffected">attribEnchIndexes, that direct affect the deletion</param>
        /// <param name="dscpIndexUnderDelete">dscpIndex that under deletion</param>
        /// <returns>dscpIndexes, that need to reset attribEnch-ing, and attribEnchIndexes to modify proper level</returns>
        public List<int[]> collectAttribEnchThatOverlapsWithThese(int[] attribEnchIndexesThatAffected, int dscpIndexUnderDelete)
        {
            try
            {
                List<int[]> result = new List<int[]>();
                foreach (int indiv in attribEnchIndexesThatAffected)
                {
                    foreach (CharOneAttribEnch ench in theEnchColl)
                    {
                        if (ench.theAttribEnchanceIndex == indiv)
                        {
                            if (ench.theDisciplIndex_First != ench.theDisciplIndex_Second && ench.theDisciplIndex_Second != 0)
                            {
                                int[] tempResult = new int[2];
                                if (ench.theDisciplIndex_First == dscpIndexUnderDelete)
                                {
                                    tempResult[1] = ench.theAttribEnchanceIndex;
                                    tempResult[0] = ench.theDisciplIndex_Second;
                                }
                                if (ench.theDisciplIndex_Second == dscpIndexUnderDelete)
                                {
                                    tempResult[1] = ench.theAttribEnchanceIndex;
                                    tempResult[0] = ench.theDisciplIndex_First;
                                }
                                result.Add(tempResult);
                            }
                            break;
                        }
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                throw new CharRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion
    }
}
