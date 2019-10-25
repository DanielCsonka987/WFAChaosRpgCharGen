using ChaosRpgCharGen.CoreModel;
using ChaosRpgCharGen.GeneralModel;
using ChaosRpgCharGen.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosRpgCharGen
{
    public partial class ReviewCreateCharWindow : MetroFramework.Forms.MetroForm
    {
        private List<CoreOneRaceDetails> theRaces;
        private List<CoreOneDscpType> theDscpTypes;
        private List<CoreOneSizeDef> theRaceSizes;
        private ReviewWindow parentReview;
        private ReviewCharactService parentService;

        private CoreOneRaceDetails theActualChosenRace;

        private GeneralOneTrunkEntity theFinalCharTrunk;
        private short[] theFinalCharAttribs;
        private int theCharStarterJp;
        private byte theCharRaceId;
        private string theCharRaceName;
        private byte thecharBeneficDscpId;
        private byte theCharSizeId;
        private string theCharSizeText;
        private byte theCharFinalSpeed;
        private string theCharName;

        /// <summary>
        /// CONSTRUCTOR OF CHARACTER CREATION MODAL WINDOW
        /// </summary>
        /// <param name="reviewWindow">paretn window</param>
        /// <param name="racesCollect">race collection from serviceReview</param>
        /// <param name="dscpTypes">dscpType collection from serviceReview</param>
        /// <param name="raceSizes">raceSizes collection from serviceReview</param>
        public ReviewCreateCharWindow(ReviewWindow reviewWindow, ReviewCharactService serv)
        {
            try
            {
                InitializeComponent();
                parentReview = reviewWindow;
                parentService = serv;
                theRaces = serv.collectCoreRaceCollection();
                theDscpTypes = serv.collectCoreDscpTypeCollection();
                theRaceSizes = serv.collectCoreSizeCollection();

                loadInRacesToWindow();
                loadInTypesOfDscpToWindow();
            }
            catch (Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }


        #region loaderIn view elements

        /// <summary>
        /// LOADS IN THE RACES INTO COMBOBOX
        /// </summary>
        private void loadInRacesToWindow()
        {
            cmbBxRaces.Items.Clear();
            foreach(CoreOneRaceDetails race in theRaces)
            {
                cmbBxRaces.Items.Add(race.theRaceName);
            }
        }
        /// <summary>
        /// LOADS IN THE DSCP TYPES INT COMBOBOX
        /// </summary>
        private void loadInTypesOfDscpToWindow()
        {
            for (byte i = 2; i < theDscpTypes.Count; i++)
            {
                cmbBxBenefDscp.Items.Add(theDscpTypes[i].theTypeName);
            }
        }
        private void openMessage(string message, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Fontos!", MessageBoxButtons.OK, type, 200);
        }
        #endregion

        #region EVENTS

        /// <summary>
        /// HANDLE THE USER CHOOSE IN RACE OF THE CHARACTER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBxRaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SEEKS THE CHOSEN RACE DETAILS
                string chosenRaceName = cmbBxRaces.SelectedItem.ToString();
                theActualChosenRace = theRaces.Find(x => x.theRaceName == chosenRaceName);

                //SAVES THE CHOSEN SIZE
                theCharSizeId = theActualChosenRace.theRaceSize;
                theCharSizeText = theRaceSizes.Find(x => x.theSizeId == theCharSizeId).theSizeName;

                //FILLS UT THE STARTER SIZE TITLE AND SPEED
                txtBSize.Text = theCharSizeText;
                txtBSpeed.Text = theRaceSizes.Find(x => x.theSizeId == theCharSizeId).theBasicSpeedOfSize.ToString();

                //SAVES THE CHOSEN RACE ID
                theCharRaceId = theActualChosenRace.theRaceId;
                theCharRaceName = chosenRaceName;

                //LOADS ON FROM THE MAX VALUES
                lblPhysMax.Text = theActualChosenRace.theAttrPhysuqueMax.ToString();
                lblEffMax.Text = theActualChosenRace.theAttrEssencyMax.ToString();
                lblConMax.Text = theActualChosenRace.theAttrConscienceMax.ToString();
                lblEssMax.Text = theActualChosenRace.theAttrEssencyMax.ToString();
            }
            catch (Exception ex)
            {
                openMessage("Hiba történt!\n" + ex.Message, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// HANDLE THE USER CHOOSE IN BENEFICIAL DSCP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBxBenefDscp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string chosenDscpType = cmbBxBenefDscp.SelectedItem.ToString();
                thecharBeneficDscpId = theDscpTypes.Find(x => x.theTypeName == chosenDscpType).theTypeId;
            }
            catch(Exception ex)
            {
                openMessage("Hiba történt!\n" + ex.Message, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// HANDLES THE USER SAVE OF CHARACTER DETAILS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                errorProviderMistake.Clear();
                //REVISIONS
                if (nameIsMesses())
                    return;
                theCharName = txtBName.Text;
                if (raceChoosingMisses())
                    return;
                if (reviseTheAttribs_isThereMistake())  //IT SAVES HERE As WELL, ARRAY INSTANTIATED
                    return;
                if (benefDisciplChoosingMesses())
                    return;
                if (finalSpeedMessesOrNotCorrect())
                    return;
                if (starterJpMessesOrNotCorrect())
                    return;
                string charDescr = txtBxDescr.Text;     //NOT SO IMPORTANT AND NOT DEMAND

                theFinalCharTrunk = new GeneralOneTrunkEntity(
                    theCharRaceId, theCharRaceName, theCharName, charDescr,
                    thecharBeneficDscpId,theCharSizeId, theCharSizeText, theCharFinalSpeed,
                    theCharStarterJp);
                lock (parentService)
                {
                    parentService.addNewCharacterIntoSystem(theFinalCharTrunk, theFinalCharAttribs);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("A mistake appeared at save the character's details!");
                Debug.WriteLine(ex.Message);
                openMessage("Hiba történt!\n" + ex.Message, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// HANDLE THE USER WILL TO STEP BACK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// HANDLRE THE USER WILL AT CLOSING SHOWS THE REVIEW-WINDOW AGAIN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewCreateCharWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentReview.Show();
        }
        #endregion

        #region REVISER OF FIELD EMTYNESS - race, beneficDscp, speed, StarterJP

        /// <summary>
        /// REVISES THE USER COOSES A CHARACTER NAME
        /// </summary>
        /// <returns></returns>
        private bool nameIsMesses()
        {
            if(txtBName.Text == "")
            {
                errorProviderMistake.SetError(txtBName, "Válasszon nevet a karakterének!");
                return true;
            }
            return false;
        }
        /// <summary>
        /// REVISE THE USER CHOOSES A RACE FOR ITS CHARACTER
        /// </summary>
        /// <returns>true=emtpy / false=no problem</returns>
        private bool raceChoosingMisses()
        {
            if (theActualChosenRace == null)
            {
                errorProviderMistake.SetError(cmbBxRaces, "Válasszon egy fajt a karakterének!");
                return true;
            }
            return false;
        }
        /// <summary>
        /// REVISES THE USER CHOOSES A BENEFICIAL DSCP FOR ITS CHARACTER
        /// </summary>
        /// <returns>true=emtpy / false=no problem</returns>
        private bool benefDisciplChoosingMesses()
        {
            if (cmbBxBenefDscp.SelectedIndex == -1)
            {
                errorProviderMistake.SetError(cmbBxBenefDscp, "Válasszon egy előnys jártasságterületet a karakterének!");
                return true;
            }
            return false;
        }
        /// <summary>
        /// REVISES THE USER CHOOSES A CHARACTER SPEED
        /// </summary>
        /// <returns>true=emtpy or incorrect / false=no problem</returns>
        private bool finalSpeedMessesOrNotCorrect()
        {
            if (txtBSpeed.Text != "")
            {
                if (Byte.TryParse(txtBSpeed.Text, out theCharFinalSpeed))
                    return false;
                else
                {
                    errorProviderMistake.SetError(txtBSpeed, "Karakter gyorsaságnak egy számértéket adjon meg!");
                    return true;
                }
            }
            else
            {
                errorProviderMistake.SetError(txtBSpeed, "Határozza meg a karakter gyorsaságát!");
                return true;
            }
        }
        /// <summary>
        /// REVISES THE USER CHOOSES A STARTER JP AND ITS CORRECTNESS
        /// </summary>
        /// <returns>true=emtpy or incorrect / false=no problem</returns>
        private bool starterJpMessesOrNotCorrect()
        {
            if (txtBCollectedJP.Text != "")
            {
                if (Int32.TryParse(txtBCollectedJP.Text, out theCharStarterJp))
                    return false;
                else
                {
                    errorProviderMistake.SetError(txtBCollectedJP, "Kezdő JP-nek egy számértéket adjon meg!");
                    return true;
                }
            }
            else
            {
                errorProviderMistake.SetError(txtBCollectedJP, "Határozza meg a karakter keződ JP-jét!");
                return true;
            }
        }
        #endregion

        #region ATTRIB REVISERS - if these are correct, saves to container

        /// <summary>
        /// REVICES THE ALL ATTRIBUTES - IF CORRECT SAVES ALL
        /// </summary>
        /// <returns>true=mistake / false=no problem</returns>
        private bool reviseTheAttribs_isThereMistake()
        {
            theFinalCharAttribs = new short[12];
            if (physiqueHasMistake())
                return true;
            if (efficiencyHasMistake())
                return true;
            if (conscienceHasMistake())
                return true;
            if (essesnceHasMistake())
                return true;
            return false;
        }
        /// <summary>
        /// REVISES THE STARTER PHYSIQUE ATTRIBS BALANCE
        /// ->RATIO OF MAIN AND (ACTIVE + PASSIVE)/2
        /// ->RATIO OF ACTIVE AND PASSIVE
        /// ->SAVES THE CORRECT VALUES
        /// </summary>
        /// <returns>true=mistake / false=no problem</returns>
        private bool physiqueHasMistake()
        {
            try
            {
                int physMain = Convert.ToInt32(txtBAttrib1.Text);
                int stren = Convert.ToInt32(txtBAttrib2.Text);
                int stamin = Convert.ToInt32(txtBAttrib3.Text);
                if(physMain > theActualChosenRace.theAttrPhysuqueMax)
                {
                    errorProviderMistake.SetError(txtBAttrib3,
                        "Ellenőrizze a tulajdonság értékeket!\nA fajra jellemző fizikum-főérték  a maximumot meghaladja!");
                    return true;
                }
                if (physMain != (Math.Ceiling((decimal)stren + stamin) / 2))
                {
                    errorProviderMistake.SetError(txtBAttrib3,
                        "Ellenőrizze a tulajdonság értékeket!\nNincs meg az egyensúly fizikum-főérték valamint az aktív+passzav jellegei között!");
                    return true;
                }
                else if (stren - stamin > 10 || stamin - stren > 10)
                {
                    errorProviderMistake.SetError(txtBAttrib3,
                        "Ellenőrizze a tulajdonság értékeket!\nAz aktív és passzív típus között 10 pontnál nagyobb a külömbség!");
                    return true;
                }
                else
                {
                    theFinalCharAttribs[0] = (short)physMain;
                    theFinalCharAttribs[1] = (short)stren;
                    theFinalCharAttribs[2] = (short)stamin;
                    return false;
                }
            }
            catch (Exception e)
            {
                errorProviderMistake.SetError(txtBAttrib3, "Kérem, csak számértékeket írjon a tulajdonság mezőkbe!\n" + e.Message);
                return true;
            }
        }
        /// <summary>
        /// REVISES THE STARTER EFFICIENCY ATTRIBS BALANCE
        /// ->RATIO OF MAIN AND (ACTIVE + PASSIVE)/2
        /// ->RATIO OF ACTIVE AND PASSIVE
        /// ->SAVES THE CORRECT VALUES
        /// </summary>
        /// <returns>true=mistake / false=no problem</returns>
        private bool efficiencyHasMistake()
        {
            try
            {
                int efficiMain = Convert.ToInt32(txtBAttrib4.Text);
                int dexter = Convert.ToInt32(txtBAttrib5.Text);
                int reflex = Convert.ToInt32(txtBAttrib6.Text);
                if (efficiMain > theActualChosenRace.theAttrEffucuencyMax)
                {
                    errorProviderMistake.SetError(txtBAttrib6,
                        "Ellenőrizze a tulajdonság értékeket!\nA fajra jellemző rátermettség-főérték  a maximumot meghaladja!");
                    return true;
                }
                if (efficiMain != (Math.Ceiling((decimal)dexter + reflex) / 2))
                {
                    errorProviderMistake.SetError(txtBAttrib6,
                        "Ellenőrizze a tulajdonság értékeket!\nNincs meg az egyensúly rátermettség-főérték valamint az aktív+passzav jellegei között!");
                    return true;
                }
                else if (dexter - reflex > 10 || reflex - dexter > 10)
                {
                    errorProviderMistake.SetError(txtBAttrib6,
                        "Ellenőrizze a tulajdonság értékeket!\nA rátermettség aktív és passzív típus között 10 pontnál nagyobb a külömbség!");
                    return true;
                }
                else
                {
                    theFinalCharAttribs[3] = (short)efficiMain;
                    theFinalCharAttribs[4] = (short)dexter;
                    theFinalCharAttribs[5] = (short)reflex;
                    return false;
                }
            }
            catch (Exception e)
            {
                errorProviderMistake.SetError(txtBAttrib6, "Kérem, csak számértékeket írjon a tulajdonság mezőkbe!\n"+e.Message);
                return true;
            }
        }
        /// <summary>
        /// REVISES THE STARTER CONSCIENCE ATTRIBS BALANCE
        /// ->RATIO OF MAIN AND (ACTIVE + PASSIVE)/2
        /// ->RATIO OF ACTIVE AND PASSIVE
        /// ->SAVES THE CORRECT VALUES
        /// </summary>
        /// <returns>true=mistake / false=no problem</returns>
        private bool conscienceHasMistake()
        {
            try
            {
                int consciMain = Convert.ToInt32(txtBAttrib7.Text);
                int intelig = Convert.ToInt32(txtBAttrib8.Text);
                int fortit = Convert.ToInt32(txtBAttrib9.Text);
                if (consciMain > theActualChosenRace.theAttrEssencyMax)
                {
                    errorProviderMistake.SetError(txtBAttrib9,
                        "Ellenőrizze a tulajdonság értékeket!\nA fajra jellemző tudat-főérték  a maximumot meghaladja!");
                    return true;
                }
                if (consciMain != (Math.Ceiling((decimal)intelig + fortit) / 2))
                {
                    errorProviderMistake.SetError(txtBAttrib9,
                        "Ellenőrizze a tulajdonság értékeket!\nNincs meg az egyensúly tudat-főérték valamint az aktív+passzav jellegei között!");
                    return true;
                }
                else if (intelig - fortit > 10 || fortit - intelig > 10)
                {
                    errorProviderMistake.SetError(txtBAttrib9,
                        "Ellenőrizze a tulajdonság értékeket!\nA tudat aktív és passzív típus között 10 pontnál nagyobb a külömbség!");
                    return true;
                }
                else
                {
                    theFinalCharAttribs[6] = (short)consciMain;
                    theFinalCharAttribs[7] = (short)intelig;
                    theFinalCharAttribs[8] = (short)fortit;
                    return false;
                }
            }
            catch (Exception e)
            {
                errorProviderMistake.SetError(txtBAttrib9, "Kérem, csak számértékeket írjon a tulajdonság mezőkbe!\n"+e.Message);
                return true;
            }
        }
        /// <summary>
        /// REVISES THE STARTER ESSENCE ATTRIBS BALANCE
        /// ->RATIO OF MAIN AND (ACTIVE + PASSIVE)/2
        /// ->RATIO OF ACTIVE AND PASSIVE
        /// ->SAVES THE CORRECT VALUES
        /// </summary>
        /// <returns>true=mistake / false=no problem</returns>
        private bool essesnceHasMistake()
        {
            try
            {
                int essenMain = Convert.ToInt32(txtBAttrib10.Text);
                int magic = Convert.ToInt32(txtBAttrib11.Text);
                int essShild = Convert.ToInt32(txtBAttrib12.Text);
                if (essenMain > theActualChosenRace.theAttrEssencyMax)
                {
                    errorProviderMistake.SetError(txtBAttrib12,
                        "Ellenőrizze a tulajdonság értékeket!\nA fajra jellemző eszencia-főérték a maximumot meghaladja!");
                    return true;
                }
                if (essenMain != (Math.Ceiling((decimal)magic + essShild) / 2))
                {
                    errorProviderMistake.SetError(txtBAttrib12,
                        "Ellenőrizze a tulajdonság értékeket!\nNincs meg az egyensúly eszencia-főérték valamint az aktív+passzav jellegei között!");
                    return true;
                }
                else if (magic - essShild > 10 || essShild - magic > 10)
                {
                    errorProviderMistake.SetError(txtBAttrib12,
                        "Ellenőrizze a tulajdonság értékeket!\nAz eszencia aktív és passzív típus között 10 pontnál nagyobb a külömbség!");
                    return true;
                }
                else
                {
                    theFinalCharAttribs[9] = (short)essenMain;
                    theFinalCharAttribs[10] = (short)magic;
                    theFinalCharAttribs[11] = (short)essShild;
                    return false;
                }
            }
            catch (Exception e)
            {
                errorProviderMistake.SetError(txtBAttrib12, "Kérem, csak számértékeket írjon a tulajdonság mezőkbe!\n"+e.Message);
                return true;
            }
        }

        #endregion

        #region GETTERS TO REVIEW WINDOW GETS THE DATAS- if user finishes the creation

        /// <summary>
        /// GETTER OF TRUNK DETAILS TO REVEIW AND IT'S SERVICE
        /// </summary>
        /// <returns>tha turnk datas</returns>
        public GeneralOneTrunkEntity getCharTrunk()
        {
            return theFinalCharTrunk;
        }
        /// <summary>
        /// GETTER OF ATTRIBUS TO REVIEW AND IT'S SERVICE
        /// </summary>
        /// <returns></returns>
        public short[] getCharAttrib()
        {
            return theFinalCharAttribs;
        }



        #endregion


    }
}
