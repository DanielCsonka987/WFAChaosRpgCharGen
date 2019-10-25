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
    /// REPORSIORY OF DSCP JP LEVELS AND JP-SCHEMAS
    ///     ->JP-Level collections (normal-spec)
    ///     ->JPSchema collections
    /// </summary>
    public class CoreJPLevelsSchemasRepo :ICoreJPLevelsSchemas
    {
        private List<CoreOneJPSchema> theJPSchemaCollection;
        private List<CoreOneJPNormLevel> theJPNormalLevelCollection;
        private List<CoreOneJPSpecLevel> theJPSpecLevelCollection;

        public CoreJPLevelsSchemasRepo()
        {
            try
            {
                theJPSchemaCollection = new List<CoreOneJPSchema>();
                extractJPSchemasFromDB();
                theJPNormalLevelCollection = new List<CoreOneJPNormLevel>();
                extractJPNormalLevelsFromDB();
                theJPSpecLevelCollection = new List<CoreOneJPSpecLevel>();
                extractJPSpecLevelsFromDB();
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #region general helper methods

        private string queryToLoadInJPSchema =
            "SELECT JPschema, levelJP_intervalDown, levelJP_intervalUp, levelJP_modifier" +
            " FROM chaos_attrib_JPschema;";

        private void extractJPSchemasFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInJPSchema);
                foreach (object[] row in temp)
                {
                    CoreOneJPSchema newOne = new CoreOneJPSchema(
                        short.Parse(row[0].ToString()), short.Parse(row[1].ToString()),
                        short.Parse(row[2].ToString()), short.Parse(row[3].ToString())
                    );
                    theJPSchemaCollection.Add(newOne);
                }
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private string queryToLoadInNormLevel = 
            "SELECT discipl_level, level_attribJPSchemaAdditioner, level_studyPracticePenalty," +
            " discipl_levelAveragePrice , discipl_levelBeneficPrice , discipl_attribLevelFeedback" +
            " FROM chaos_JPLevels_normal;";

        private void extractJPNormalLevelsFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInNormLevel);
                foreach (object[] row in temp)
                {
                    CoreOneJPNormLevel newOne = new CoreOneJPNormLevel(
                        byte.Parse(row[0].ToString()), short.Parse(row[1].ToString()),
                        short.Parse(row[2].ToString()), short.Parse(row[3].ToString()),
                        short.Parse(row[4].ToString()), byte.Parse(row[5].ToString())
                    );
                    theJPNormalLevelCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        private string queryToLoadInSpecLevel =
            "SELECT discipl_specLevel, discipl_levelSpecAveragePrice, discipl_levelSpecBenefPrice," +
            "discipl_requirLevelRising, discipl_requirAttribRising,discipl_attribSpecLevelFeedback" +
            " FROM chaos_JPLevels_special;";

        private void extractJPSpecLevelsFromDB()
        {
            try
            {
                DataAccess.ConnectToDB();
                List<object[]> temp = DataAccess.ExecuteSQL_normal_outTable(queryToLoadInSpecLevel);
                foreach (object[] row in temp)
                {
                    CoreOneJPSpecLevel newOne = new CoreOneJPSpecLevel(
                        byte.Parse(row[0].ToString()), short.Parse(row[1].ToString()),
                        short.Parse(row[2].ToString()), byte.Parse(row[3].ToString()),
                        short.Parse(row[4].ToString()), byte.Parse(row[5].ToString())
                    );
                    theJPSpecLevelCollection.Add(newOne);
                }
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED NORMAL LEVEL
        /// </summary>
        /// <param name="levelMarking">seeked levelMarking</param>
        /// <returns>level norm record</returns>
        private CoreOneJPNormLevel seekTheNormalLevel(short levelMarking)
        {
            try
            {
                CoreOneJPNormLevel temp = theJPNormalLevelCollection.Find(x => x.theLevelMark == levelMarking);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Ilyen normál jártasságszint nincs!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED SPECIALISATION LEVEL
        /// </summary>
        /// <param name="levelMarking">seeked levelMarking</param>
        /// <returns>level spec record</returns>
        private CoreOneJPSpecLevel seekTheSpecialisationLevel(short levelMarking)
        {
            try
            {
                CoreOneJPSpecLevel temp = theJPSpecLevelCollection.Find(x => x.theSpecLevelMark == levelMarking);
                if (temp != null)
                    return temp;
                else
                    throw new CoreRepositoryException("Ilyen specializációs szint nincs!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED JPSCHEMA RECORD
        /// </summary>
        /// <param name="schemaId">seeked schemaId</param>
        /// <returns>jpShcema record</returns>
        private List<CoreOneJPSchema> seekTheJPSchema(short schemaId)
        {
            try
            {
                List<CoreOneJPSchema> temp = theJPSchemaCollection.Where(x => x.theJpSchemaId == schemaId).ToList();
                if (temp.Count != 0)
                    return temp;
                else
                    throw new CoreRepositoryException("Ilyen tulajdonság-sémaértékhez köthető JP-módosító nincs!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region BASCI JP GETTERS

        /// <summary>
        /// GETS THE BASIC AVERAGE JP VALUE OF A NORMAL DSCP LEVEL
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>baisc average JP</returns>
        public short findTheNormalLevelAverageJP(byte levelMark)
        {
            return seekTheNormalLevel(levelMark).theLevelAverageCost;
        }
        /// <summary>
        /// FINDS THE BAISC BENEFICIAL JP VALUE OF A NORMAL DSCP LEVEL
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>basic benef JP</returns>
        public short findTheNormalLevelBeneficJP(byte levelMark)
        {
            try
            {
                if (levelMark < 0 || levelMark > 10)
                    throw new CoreModelException("Ilyen szint egy jártasságnál nem megengedett! " + levelMark);

                short res = seekTheNormalLevel(levelMark).theLevelBeneficCost;
                if (res > 0)
                    return res;
                else
                    throw new CoreModelException("Nincs bemutatható JP érték!");
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException("Az előnyös jártasság-alapköltség keresés sikertelen!" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE BASIC AVERAGE JP VALUE OF SPECIAL DSCP LEVEL
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>basic average JP</returns>
        public short findTheSpecLevelAverageJP(byte levelMark)
        {
            try
            {
                if (levelMark < 0 || levelMark > 2)
                    throw new CoreModelException("Ilyen szint egy specializációnál nem megengedett! " + levelMark);

                short res = seekTheSpecialisationLevel(levelMark).theSpecLevelAverageCost;
                if (res > 0)
                    return res;
                else
                    throw new CoreModelException("Nincs bemutatható JP érték!");
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException("Az átlagos jártasság-alapköltség keresés sikertelen!" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE BASIC BENEFICIAL JP VALUE OF SPECIAL DSCP LEVEL
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>basic benef HO</returns>
        public short findTheSpecLevelBeneficJP(byte levelMark)
        {
            return seekTheSpecialisationLevel(levelMark).theSpecLevelBeneficCost;
        }

        #endregion

        #region dscp requirement management

        /// <summary>
        /// FINDS THE SEEKED SCHEMA RISEMENT THAT IS WITH LEVEL GETTING HIGHER
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>JPSchema additional value</returns>
        public short findTheSchemaRiserForThisNormalLevel(byte levelMark)
        {
            try
            {
                return seekTheNormalLevel(levelMark).theLevelSchemaAdditioner;
            }
            catch(Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S SPECIALISATION DSCP-REQUIREMENT RISEMENT THAT WITH LEVEL IS GETTING HIGHER
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>spec dscp requir addtitional value</returns>
        public byte findTheRequirementRiserForThisSpecLevel(byte levelMark)
        {
            try
            {
                return seekTheSpecialisationLevel(levelMark).theRequirLevelRisiing;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED DSCP'S SPECIALISATION ATTRIB-REQUIREMENT RISEMENT
        /// THAT WITH LEVEL IS GETTING HIGHER
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>spec attrib requir addtional value</returns>
        public short findTheAttributeRiserForThisSpec(byte levelMark)
        {
            try
            {
                return seekTheSpecialisationLevel(levelMark).theRequirAttribRisiing;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region manage JP modifiers

        /// <summary>
        /// FINDS THE SEEKED JPSCHEMA MODIFIER VALUE - FINISH THE COUNTING FINAL JP
        /// </summary>
        /// <param name="schemaId">seeked JPSchemaId</param>
        /// <param name="charAttribValue">the attribValue of character</param>
        /// <returns>JPSchema modifier</returns>
        public short findSchemaModifForThisJPSchema(short schemaId, short charAttribValue)
        {
            try
            {
                List<CoreOneJPSchema> temp = seekTheJPSchema(schemaId);
                foreach (CoreOneJPSchema schema in temp)
                {
                    if (schema.theModifIntervalMin <= charAttribValue &&
                        schema.theModifIntervalMax >= charAttribValue)
                    {
                        return schema.theSchemaModifier;
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED PRACTICE MODIFIER OF DSCP THAT WITH LEVEL GETTING LOWER
        /// FINISH THE COUNTING FINAL JP AT NORMAL DSCP LEVELS
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>practice modifier</returns>
        public short findPracticeModifForThisLevel(byte levelMark)
        {
            try
            {
                return seekTheNormalLevel(levelMark).thePracticePenalty;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

        #region attribFeedback manage

        /// <summary>
        /// FINDS THE SEEKED NORMAL LEVEL'S ATTRIB FEEDBACK POINT
        /// </summary>
        /// <param name="levelMark">seeked levelMark</param>
        /// <returns>attribFeedbackPointType</returns>
        public byte findThereAttribFeedbackForThisNormalLevel(byte levelMark)
        {
            try
            {
                return seekTheNormalLevel(levelMark).theLevelAttribFeedback;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// FINDS THE SEEKED SPECIALISATION LEVEL'S ATTRIB FEEDBACK POINT
        /// </summary>
        /// <param name="levelMark">levelMark</param>
        /// <returns>attribFeedbackPointType</returns>
        public byte findThereAttribFeedbackForThisSpecLevel(byte levelMark)
        {
            try
            {
                return seekTheSpecialisationLevel(levelMark).theSpecLevelAttribFeedback;
            }
            catch (Exception e)
            {
                throw new CoreRepositoryException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion
    }
}
