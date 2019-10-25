using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.GeneralModel;
using ChaosRpgCharGen.GeneralRepository;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosRpgCharGen.Service
{
    /// <summary>
    /// SERVICE REVIEW CHARACTERS
    /// </summary>
    public class ReviewCharactService : IReviewCharactService
    {
        private GeneralReviewTrunkRepo charactRepo;
        /// <summary>
        /// CONSTRUCTOR OF SERVICE REVIEW WINDOW
        /// </summary>
        public ReviewCharactService()
        {
            charactRepo = new GeneralReviewTrunkRepo(); //IT IS NEEDED FOR SURE
        }

        #region methods of loading in characters

        /// <summary>
        /// CREATES THE LOST FOR LIST OF REVIEW WINDOW
        /// </summary>
        /// <returns>datatable of characters in system</returns>
        public DataTable getListOfCharacters()
        {
            List<GeneralOneTrunkEntity> list = charactRepo.findTheSystemCharacters();

            DataColumn id = new DataColumn("ID");
            id.DataType = typeof(int);
            DataColumn name = new DataColumn("Karakternév");
            id.DataType = typeof(string);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { id, name });

            foreach (GeneralOneTrunkEntity oneChar in list)
            {
                dt.Rows.Add(new object[] { oneChar.theCharId, oneChar.theCharName });
            }
            return dt;
        }
        /// <summary>
        /// OPENS AN EXISTING CHARACTER TO MANAGE IN MANAGER WINDOW
        /// </summary>
        /// <param name="charId">charId</param>
        public GeneralOneTrunkEntity findTheExistingCharacter(int charId)
        {
            return charactRepo.getCharEntityToOpen(charId);
        }

        #endregion

        #region methods to create characters
        /// <summary>
        /// GETTER OF CORE-DSCP-TYPES TO CREATE NEW CHAR
        /// </summary>
        /// <returns></returns>
        public List<CoreOneDscpType> collectCoreDscpTypeCollection()
        {
            try
            {
                CoreDisciplinesTypesRepo repo = new CoreDisciplinesTypesRepo();
                return repo.collectTheAllTypesOfDscps_InColl();
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// GETTER OF CORE-SIZES TO CREATE NEW CHAR
        /// </summary>
        /// <returns></returns>
        public List<CoreOneSizeDef> collectCoreSizeCollection()
        {
            try
            {
                CoreAttribsRacesSizesRepo repo = new CoreAttribsRacesSizesRepo();
                return repo.collectSizeDef();
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        /// <summary>
        /// GETTER OF CORE-RACES TO CREATE NEW CHAR
        /// </summary>
        /// <returns></returns>
        public List<CoreOneRaceDetails> collectCoreRaceCollection()
        {
            try
            {
                CoreAttribsRacesSizesRepo repo = new CoreAttribsRacesSizesRepo();
                return repo.collectTheRaces();
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        /// <summary>
        /// CREATES A NEW CHARACTER TO THE SYSTEM - IT CREATES THE TRUNK AND STATS
        /// </summary>
        /// <param name="newChar">character trunk</param>
        /// <param name="attribValues">full attrib palett</param>
        /// <returns>the charId of new character</returns>
        public int addNewCharacterIntoSystem(GeneralOneTrunkEntity newChar, short[] attribValues)
        {
            try
            {
                return charactRepo.addNewCharacterIntoSystem(newChar, attribValues);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region methods of remove a character

        /// <summary>
        /// REMOVES A CHOSEN CHARACTER FROM SYSTEM
        /// </summary>
        /// <param name="charId">charId</param>
        public bool removeExisitingCharacter(int charId)
        {
            try
            {
                return charactRepo.removeCharacterFromSystem(charId);
            }
            catch(Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }
        #endregion

        #region configuration managment

        public bool[] loadInTheBeneficialConfig()
        {
            try
            {
                GeneralBeneficMediator gbm = new GeneralBeneficMediator();
                return gbm.loadInBeneficConfig();
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        public void saveTheBeneficConfigToDB(bool ordinaryDscpBenef, bool proffesDscpBenef)
        {
            try
            {
                GeneralBeneficMediator gbm = new GeneralBeneficMediator();
                gbm.saveBeneficConfig(1, ordinaryDscpBenef);
                gbm.saveBeneficConfig(2, proffesDscpBenef);
            }
            catch (Exception e)
            {
                throw new ManageCharactServException(e.TargetSite + "->" + e.Message);
            }
        }

        #endregion

    }
}
