using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.Databese;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreRepository
{
    /// <summary>
    /// REPOSITORY OF ATTRIBUTES, RACES AND SIZES
    /// </summary>
    public class CoreAttribsRacesSizesRepo : ICoreAttribsRacesSizes
    {
        private List<CoreOneAttrib> theAttribCollection;
        private List<CoreOneRaceDetails> theRaceCollection;
        private List<CoreOneSizeDef> theSizeCollection;

        public CoreAttribsRacesSizesRepo()
        {
            try
            {
                theAttribCollection = new List<CoreOneAttrib>();
                extractAttribsFromDB();
                theRaceCollection = new List<CoreOneRaceDetails>();
                extractRacesFromDB();
                theSizeCollection = new List<CoreOneSizeDef>();
                extractSizesFromDB();
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #region general helper methods

        private string queryToLoadInCoreAttribs =
            "SELECT attrib_id, atrib_name FROM chaos_attribute_stats;";
        /// <summary>
        /// HELPER METHOD TO LOAD IN CORE-ATTRIBS
        /// </summary>
        private void extractAttribsFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInCoreAttribs);
                foreach (object[] row in temp)
                {
                    CoreOneAttrib newOne = new CoreOneAttrib(byte.Parse(row[0].ToString()),
                        row[1].ToString());
                    theAttribCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private string queryToLoadInCoreRaces =
            "SELECT race_id, raceName, physiqueMax, efficiencyMax, conscienceMax," +
            " essencyMax, basicSize FROM chaos_raceBase_attribRestraints;";
        /// <summary>
        /// HELPER METHOD TO LOAD IN CORE-RACE-RESTRICTIONS
        /// </summary>
        private void extractRacesFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInCoreRaces);
                foreach (object[] row in temp)
                {
                    CoreOneRaceDetails newOne = new CoreOneRaceDetails(byte.Parse(row[0].ToString()),
                        row[1].ToString(), byte.Parse(row[6].ToString()),
                        short.Parse(row[2].ToString()), short.Parse(row[3].ToString()),
                        short.Parse(row[4].ToString()), short.Parse(row[5].ToString()));
                    theRaceCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private string queryToLoadInSizeDetails =
            "SELECT basicSize_id, sizeDescr, speedQuicknessBasic FROM chaos_raceSizes;";

        private void extractSizesFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInSizeDetails);
                foreach (object[] row in temp)
                {
                    CoreOneSizeDef newOne = new CoreOneSizeDef(byte.Parse(row[0].ToString()),
                        row[1].ToString(), byte.Parse(row[2].ToString()));
                    theSizeCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED ATTRIBUTE RECORD BY ITS ID
        /// </summary>
        /// <param name="attribId">seeked attribID</param>
        /// <returns>attribute</returns>
        private CoreOneAttrib seekThisAttrib(byte attribId)
        {
            try
            {
                CoreOneAttrib temp = theAttribCollection.Find(x => x.theAttribId == attribId);
                if (temp == null)
                    throw new CoreRepositoryException("Nincs ilyen tulajdonság ezzel az azonosítóval!");
                else
                    return temp;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED ATTRIBUTE RECORD BY ITS NAME
        /// </summary>
        /// <param name="attribName"></param>
        /// <returns></returns>
        private CoreOneAttrib seekThisAttrib(string attribName)
        {
            try
            {
                CoreOneAttrib temp = theAttribCollection.Find(x => x.theAttribName == attribName);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs ilyen tulajdonság ezzel a névvel!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED RACE RECORD
        /// </summary>
        /// <param name="raceId">seeked raceId</param>
        /// <returns>race</returns>
        private CoreOneRaceDetails seekThisRace(short raceId)
        {
            try
            {
                CoreOneRaceDetails temp = theRaceCollection.Find(x => x.theRaceId == raceId);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs ilyen faj ezzel az azonosítóval!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED SIZE RECORD
        /// </summary>
        /// <param name="sizeId">seeked sizeId</param>
        /// <returns>size</returns>
        private CoreOneSizeDef seekThisSize(byte sizeId)
        {
            try
            {
                CoreOneSizeDef temp = theSizeCollection.Find(x => x.theSizeId == sizeId);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Nincs ilyen méret ezzel az azonosítóval!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region attribute management

        /// <summary>
        /// FINDS THE SEEKED ATTRIBUTE'S NAME BY ITS ID
        /// </summary>
        /// <param name="attribId">seeked attribId</param>
        /// <returns>attrib name</returns>
        public string findTheNameOfThisAttrib(byte attribId)
        {
            try
            {
                return seekThisAttrib(attribId).theAttribName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED ATTRIBUTE'S ID BY ITS NAME
        /// </summary>
        /// <param name="attribName">attribName</param>
        /// <returns>attribId</returns>
        public byte findTheAttriIdThisAttribName(string attribName)
        {
            try
            {
                if (attribName == "")
                    return 0;
                else
                    return seekThisAttrib(attribName).theAttribId;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region race management
        /// <summary>
        /// FINDS THE SEEKED RACE'S NAME
        /// </summary>
        /// <param name="raceId">raceId</param>
        /// <returns>race name</returns>
        public string findTheNameOfThisRace(short raceId)
        {
            try
            {
                return seekThisRace(raceId).theRaceName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED RACE'S STARTER ATTRIBUTE MAXIMUMS
        /// 0 = Phyisique, 1 = Efficiency, 2 = Conscience, 3 = Essence
        /// </summary>
        /// <param name="raceId">seeked raceId</param>
        /// <returns>array of values</returns>
        public short[] findTheStarterStatsOfThisRace(short raceId)
        {
            try
            {
                CoreOneRaceDetails temp = seekThisRace(raceId);
                short[] attribValues = new short[] { temp.theAttrPhysuqueMax, temp.theAttrEffucuencyMax,
                    temp.theAttrConscienceMax, temp.theAttrEssencyMax};
                return attribValues;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED RACE'S SIZE NAME
        /// </summary>
        /// <param name="raceId">raceId</param>
        /// <returns>sizeId value</returns>
        public byte findTheSizeOfThisRace(short raceId)
        {
            try
            {
                return seekThisRace(raceId).theRaceSize;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// GETTER OF CORE RACES COLLECTION - FOR CHARACTER CREATION
        /// </summary>
        /// <returns>races</returns>
        public List<CoreOneRaceDetails> collectTheRaces()
        {
            return theRaceCollection;
        }
        #endregion

        #region size management
        /// <summary>
        /// FINDS THE SEEKED SIZE'S NAME
        /// </summary>
        /// <param name="sizeId">sizeId</param>
        /// <returns>size name</returns>
        public string findTheNameOfThisSize(byte sizeId)
        {
            try
            {
                return seekThisSize(sizeId).theSizeName;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED SIZE'S STARTER QUICKNESS
        /// </summary>
        /// <param name="sizeId">sizeId</param>
        /// <returns>starter quickness value</returns>
        public byte fintTheBasicQuicknessOfThisSize(byte sizeId)
        {
            try
            {
                return seekThisSize(sizeId).theBasicSpeedOfSize;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        public List<CoreOneSizeDef> collectSizeDef()
        {
            return theSizeCollection;
        }

        #endregion

    }
}
