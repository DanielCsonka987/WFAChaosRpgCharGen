using ChaosRpgCharGen.CharModel;
using ChaosRpgCharGen.GeneralModel;
using ChaosRpgCharGen.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosRpgCharGen
{
    public partial class ManageSideDisciplRiseSpec : MetroFramework.Forms.MetroForm
    {
        private Form theParentMainManager;
        private ManageCharactService theManagerService;
        private int theManagedDscpIndex;
        private DscpSideWinMode theModeOfWindow;
        private GeneralOneTrunkEntity theManagedChar;

        //FOR BOTH NORMAL AND SPEC MODE
        private byte selectedDscpNextLevel;
        private byte selectedDscpAttrib;
        private byte selectedLevel_ToManage;
        private int selectedLevelFinalJP;
        private bool readyToGainNewLevel;

        //FOR NORMAL MODE
        private bool selectedMentoring;

        //FOR SPEC MODE
        private byte selectedSpecIndex_ToManage;
        private byte selectedSpecRequirIndex;
        private string selectedSpecAreaText;
        private string selectedSpecNotation;
        private bool readyToGainNewSpecialization;

        public ManageSideDisciplRiseSpec(Form parentManager, DscpSideWinMode mode, int managedDscpIndex,
            ManageCharactService serv, GeneralOneTrunkEntity charTrunk)
        {
            try
            {
                InitializeComponent();
                theParentMainManager = parentManager;
                theManagerService = serv;
                theManagedDscpIndex = managedDscpIndex;
                this.Show();
                theModeOfWindow = mode;
                theManagedChar = charTrunk;

                if (mode == DscpSideWinMode.Details)
                    adjustTheDetailsMode();
                if (mode == DscpSideWinMode.RiseNorm)
                    adjustTheNormalMode();
                if (mode == DscpSideWinMode.RiseSpec)
                    adjustTheSpecialMode();
                fillUpGeneralDatasToLabels();
            }
            catch (Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }

        #region general helpers
        /// <summary>
        /// LOAD INT HE GENERAL DATAS OF CHOSEN DSCP TO SEE AT ALL MODES
        /// </summary>
        private void fillUpGeneralDatasToLabels()
        {
            string[] datas = theManagerService.DscpRiseSpec_collectThisDscpSeveralDetails(theManagedDscpIndex);
            lblDscpName.Text = datas[0];
            lblDscpType.Text = datas[1];
            lblDscpLevel.Text = datas[2];
            lblDscpAttrib.Text = datas[3];
            lblAttribValue.Text = datas[4];
            lblAttribSchema.Text = datas[5];

            lblDscpStudiabil.Text = datas[6];
            lblDscpRequirImport.Text = datas[7];
            lblDscpRequir.Text = datas[8];
            lblDscpSumJP.Text = datas[9];
            lblDscpDescr.Text = datas[10];

            selectedDscpAttrib = theManagerService.DscpRiseSpec_getTheAttribIdOfThisDscp(theManagedDscpIndex);
        }
        /// <summary>
        /// ADJUST THE NORMAL LEVEL LAYERS TO MAIN AT DETAILS AND NORMAL MODE
        /// </summary>
        private void adjustDtgrvMain_AtDetailsAndNormal()
        {
            dtgrvDscpMain.Columns[0].Width = 40;    //LVL
            dtgrvDscpMain.Columns[1].Width = 50;    //JP
            dtgrvDscpMain.Columns[2].Width = 80;    //MetorOrTrain
            dtgrvDscpMain.Columns[3].Width = 80;    //GIVE ATTRIB
        }
        /// <summary>
        /// MANAGES THE RESET FIELDS AND VARIABLE AT NORMAL AND SPEC CASES
        /// </summary>
        private void resetMutualTextBoxesAndVariables()
        {
            readyToGainNewLevel = false;
            lblTextAvailableJP.Text = theManagerService.JPGeneralInfo_countTheSumAvailableJP(theManagedChar.theStarterJPValue).ToString();
            lblTextAttribModif.Text = "";
            lblTextTrainModif.Text = "";
            lblTextBasicJPValue.Text = "";
            lblTextFinalJPValue.Text = "";
        }

        /// <summary>
        /// HANDLE THE EVENT OF MESSAGE
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        private void openMessage(string message, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Fontos!", MessageBoxButtons.OK, type, 200);
        }

        /// <summary>
        /// HANDLE THE EVENT OF MESSAGE WITH RESULT
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private DialogResult openDialogMessage(string message)
        {
            return MetroFramework.MetroMessageBox.Show(this, message, "Biztos?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 200);
        }
        //HANDLE THE EXIT
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //HANDLE THE EXIT
        private void ManageSideDisciplRiseSpec_FormClosed(object sender, FormClosedEventArgs e)
        {
            theParentMainManager.Show();
        }

        #endregion

        #region details mode
        /// <summary>
        /// ADJUST THE VIEW AT DETAOLS MODE
        /// </summary>
        private void adjustTheDetailsMode()
        {
            dtgrvDscpMain.DataSource = theManagerService.DscpRiseSpec_collectThisDscpNormalLevelsDeep(theManagedDscpIndex);
            adjustDtgrvMain_AtDetailsAndNormal();
            dtgrvDscpMinor.DataSource = theManagerService.DscpRiseSpec_collectThisDscpSpecLevelsDeep(theManagedDscpIndex);
            adjustDtgrvMinor_AtDetail();
            dtgrvDscpMinor.Visible = true;
            pnlRiser.Visible = false;
            pnlSpecRiser.Visible = false;
            btnDscpDelete.Visible = false;
        }
        /// <summary>
        /// ADJUST THE SPEC LEVEL LAYERS AT MINOR
        /// </summary>
        private void adjustDtgrvMinor_AtDetail()
        {
            dtgrvDscpMinor.Columns[0].Width = 40;    //SPEC INDEX
            dtgrvDscpMinor.Columns[1].Width = 40;    //LVL
            dtgrvDscpMinor.Columns[2].Width = 50;    //JP
            dtgrvDscpMinor.Columns[3].Width = 70;    //AREA
            dtgrvDscpMinor.Columns[4].Width = 80;    //DESCR
            dtgrvDscpMinor.Columns[5].Width = 80;    //GIVE ATTRIB
        }
        #endregion

        #region spec mode

        //ADJUST VIEW AT START
        private void adjustTheSpecialMode()
        {
            renewSpecModeDataTable();
            dtgrvDscpMinor.Visible = false;
            pnlRiser.Visible = true;
            pnlSpecRiser.Visible = true;
            btnDscpDelete.Visible = true;
            resetSpecModeManageTextBoxesAndVariables();
            cmbBxSpecRequirs.Items.AddRange(theManagerService.DscpRiseSpec_getTheSelectedSpecPossibleRequirs(
                theManagedDscpIndex));
            cmbBSpecAreas.Items.AddRange(
                theManagerService.DscpRiseSpec_collectAllProperSpecAreasToGainNew(theManagedDscpIndex));
            resetSpecModeNewDetailsPermissionAreasAndVariables();

            lblTitle.Text = "Jártasság specializációk";
            lblSpecTitle.Text = "Új specializáció választása";
            chckBxDscpMentor.Visible = false;
        }

        /// <summary>
        /// HANDLE THE COMBOBOX EVENT IF USER CHOOSES AREA FOR NEW SPEC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBSpecAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                readyToGainNewLevel = false;
                readyToGainNewSpecialization = false;
                selectedSpecIndex_ToManage = 0;
                resetSpecModeManageTextBoxesAndVariables();
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// HANDLE THE COMBOBOX EVENT IF USER CHOOSES REQUIREMENTS FOR NEW SPEC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbBxSpecRequirs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                readyToGainNewLevel = false;
                readyToGainNewSpecialization = false;
                selectedSpecIndex_ToManage = 0;
                resetSpecModeManageTextBoxesAndVariables();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// HANDLE THE TEXTBOX EVENT IF USER CHOOSES DESCRIPTION FOR NEW SPEC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBSpecNote_TextChanged(object sender, EventArgs e)
        {
            try
            {
                readyToGainNewLevel = false;
                readyToGainNewSpecialization = false;
                selectedSpecIndex_ToManage = 0;
                resetSpecModeManageTextBoxesAndVariables();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// HANDLE THE EVENT OF NEW SPEC GETTING UP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGainSpecialisation_Click(object sender, EventArgs e)
        {
            try
            {
                if (readyToGainNewSpecialization)
                {
                    if (theManagerService.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(theManagedDscpIndex, 1, selectedSpecRequirIndex) == 1)
                    {
                        if (theManagerService.DscpGeneralInfo_isThereEnoughJPToGetLevel(theManagedChar.theStarterJPValue,
                            selectedLevelFinalJP))
                        {
                            theManagerService.DscpRiseSpec_GainNewSpecialisation(theManagedDscpIndex, (short)selectedLevelFinalJP,
                                selectedSpecRequirIndex, selectedSpecAreaText, selectedSpecNotation);
                            resetSpecModeNewDetailsPermissionAreasAndVariables();
                            renewSpecModeDataTable();
                        }
                        else
                        {
                            errorProviderMistake.SetError(pnlSpecRiser, "Nincs elegendő JP!");
                            resetSpecModeNewDetailsPermissionToGainNew();
                        }
                    }
                    else
                    {
                        errorProviderMistake.SetError(pnlSpecRiser, "A specializációhoz nincs meg a szükséges előfeltétel!");
                        resetSpecModeNewDetailsPermissionToGainNew();
                    }
                }
                else
                {
                    errorProviderMistake.Clear();
                    if (cmbBSpecAreas.SelectedText != "" || cmbBxSpecRequirs.SelectedText != "" ||
                        txtBSpecNote.Text != "")
                    {
                        setTheNewSpecializationDatas();
                        manageTheNewSpecPresentation();
                    }
                    else
                    {
                        errorProviderMistake.SetError(pnlSpecRiser, "Minden mező kitöltés szükséges!");
                    }

                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        private void renewSpecModeDataTable()
        {
            dtgrvDscpMain.DataSource = theManagerService.DscpRiseSpec_collectThisDscpSpecLevelsDeep(theManagedDscpIndex);
            dtgrvDscpMain.Columns[0].Width = 40;    //SPEC INDEX
            dtgrvDscpMain.Columns[1].Width = 40;    //LVL
            dtgrvDscpMain.Columns[2].Width = 50;    //JP
            dtgrvDscpMain.Columns[3].Width = 70;    //AREA
            dtgrvDscpMain.Columns[4].Width = 80;    //DESCR
            dtgrvDscpMain.Columns[5].Width = 80;    //GIVE ATTRIB
        }

        private void resetSpecModeManageTextBoxesAndVariables()
        {
            btnDscpRiseLevel.Text = "Számol";
            readyToGainNewLevel = false;
            selectedDscpNextLevel = theManagerService.DscpRiseSpec_getTheNextDscpNormOrSpecLevel(
                theManagedDscpIndex, selectedSpecIndex_ToManage);
            if (selectedDscpNextLevel > 2 || selectedDscpNextLevel == 0)
                lblTextNextLevel.Text = "";
            else
                lblTextNextLevel.Text = selectedDscpNextLevel.ToString();
            resetMutualTextBoxesAndVariables();
        }

        private void resetSpecModeNewDetailsPermissionToGainNew()
        {
            btnGainSpecialisation.Text = "Számol";
            readyToGainNewSpecialization = false;
            resetMutualTextBoxesAndVariables();
        }

        private void resetSpecModeNewDetailsPermissionAreasAndVariables()
        {
            btnGainSpecialisation.Text = "Számol";

            cmbBSpecAreas.SelectedIndex = -1;
            cmbBxSpecRequirs.SelectedIndex = -1;
            txtBSpecNote.Text = "";
            lblTextNextLevel.Text = "";
            readyToGainNewSpecialization = false;
            resetMutualTextBoxesAndVariables();
        }
        private void manageTheNewSpecPresentation()
        {
            lblTextNextLevel.Text = "1";
            btnGainSpecialisation.Text = "Felvétel";
            lblTextAttribModif.Text = theManagerService.DscpRiseSpec_getTheAttribSchemaJPModif(
                theManagedDscpIndex, 1, theManagedChar.theBeneficDscp, true, selectedDscpAttrib).ToString();
            lblTextBasicJPValue.Text = theManagerService.DscpRiseSpec_getTheBasicJPForThisDscp(
                theManagedDscpIndex, 1, theManagedChar.theBeneficDscp, true).ToString();

            selectedLevelFinalJP = theManagerService.DscpRiseSpec_calculateTheFinalJPForThisDscp(
                theManagedDscpIndex, 1, theManagedChar.theBeneficDscp, true, selectedDscpAttrib, false);
            lblTextFinalJPValue.Text = selectedLevelFinalJP.ToString();
        }
        private void setTheNewSpecializationDatas()
        {
            selectedSpecAreaText = cmbBSpecAreas.SelectedItem.ToString();
            selectedSpecRequirIndex = theManagerService.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(
                theManagedDscpIndex, cmbBxSpecRequirs.SelectedItem.ToString());
            selectedSpecNotation = txtBSpecNote.Text;

            readyToGainNewSpecialization = true;
        }

        private void manageTheSpecLevelGain()
        {
            if (theManagerService.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(theManagedDscpIndex, 2, selectedSpecIndex_ToManage) == 1)
            {
                if (theManagerService.DscpGeneralInfo_isThereEnoughJPToGetLevel(theManagedChar.theStarterJPValue,
                    selectedLevelFinalJP))
                {
                    theManagerService.DscpRiseSpec_GainNewSpecLevel(theManagedDscpIndex, (short)selectedLevelFinalJP,
                        selectedSpecIndex_ToManage);
                }
                else
                    errorProviderMistake.SetError(pnlSpecRiser,"Nincs elegendő JP!");
            }
            else
                errorProviderMistake.SetError(pnlSpecRiser, "A specializációhoz nincs meg a szükséges előfeltétel!");
        }


        private void manageTheSpecLevelPresentaion()
        {
            byte nextLevel = theManagerService.DscpRiseSpec_getTheNextDscpNormOrSpecLevel(theManagedDscpIndex, selectedSpecIndex_ToManage);
            if (nextLevel > 2)
            {
                lblTextNextLevel.Text = "";
                readyToGainNewLevel = false;
                resetSpecModeNewDetailsPermissionAreasAndVariables();
            }
            else
            {
                readyToGainNewLevel = true;
                btnDscpRiseLevel.Text = "Felvétel";
                lblTextNextLevel.Text = "2";
                lblTextAttribModif.Text = theManagerService.DscpRiseSpec_getTheAttribSchemaJPModif(
                    theManagedDscpIndex, 2, theManagedChar.theBeneficDscp, true, selectedDscpAttrib).ToString();
                lblTextBasicJPValue.Text = theManagerService.DscpRiseSpec_getTheBasicJPForThisDscp(
                    theManagedDscpIndex, nextLevel, theManagedChar.theBeneficDscp, true).ToString();
                selectedLevelFinalJP = theManagerService.DscpRiseSpec_calculateTheFinalJPForThisDscp(
                    theManagedDscpIndex, nextLevel, theManagedChar.theBeneficDscp, true, selectedDscpAttrib, false);
                lblTextFinalJPValue.Text = selectedLevelFinalJP.ToString();
            }
        }

        #endregion

        #region normal mode

        //ADJUST VIEW AT START
        private void adjustTheNormalMode()
        {
            dtgrvDscpMinor.Visible = false;
            pnlRiser.Visible = true;
            pnlSpecRiser.Visible = false;
            btnDscpDelete.Visible = true;
            lblSpecTitle.Visible = false;
            resetNormModeTextBoxesAndVariables();

        }

        //HANDLE THE USER WILL TO LEARN WITH MENTOR
        private void chckBxDscpMentor_CheckedChanged(object sender, EventArgs e)
        {
            if (theModeOfWindow == DscpSideWinMode.RiseNorm)
            {
                selectedMentoring = chckBxDscpMentor.Checked;
                if(readyToGainNewLevel)
                    manageTheNormalLevelJPPresentaion();
            }
        }

        /// <summary>
        /// MANAGES THE PROCESS OF READY NORMAL LEVEL UP
        /// </summary>
        private void manageTheNormalLevelGain()
        {
            errorProviderMistake.Clear();
            if (theManagerService.DscpRiseSpec_helpOfMentorIsNeeded(theManagedDscpIndex) && !selectedMentoring)
            {
                errorProviderMistake.SetError(chckBxDscpMentor, "A jártasság mentor segítéségt követeli meg!");
                return;
            }
            if (theManagerService.DscpRiseSpec_areThereAllRequirement_GainMoreNormalLvl(theManagedDscpIndex, selectedDscpNextLevel) == 1)
            {
                if (theManagerService.DscpGeneralInfo_isThereEnoughJPToGetLevel(theManagedChar.theStarterJPValue, selectedLevelFinalJP))
                {
                    theManagerService.DscpRiseSpec_GainNormalLevel(theManagedDscpIndex, (short)selectedLevelFinalJP, selectedMentoring);
                    resetNormModeTextBoxesAndVariables();
                }
                else
                    errorProviderMistake.SetError(chckBxDscpMentor, "Nincs elegendő JP!");
            }
            else
                errorProviderMistake.SetError(chckBxDscpMentor, "Nincs meg a szükséges előfeltétel!");
        }
        /// <summary>
        /// MANAGES THE PROCESS OF COUNTING BEFORE NORMAL LEVEL UP
        /// </summary>
        private void manageTheNormalLevelJPPresentaion()
        {
            btnDscpRiseLevel.Text = "Felvétel";
            errorProviderMistake.Clear();
            lblTextAttribModif.Text = theManagerService.DscpRiseSpec_getTheAttribSchemaJPModif(
              theManagedDscpIndex, selectedDscpNextLevel, theManagedChar.theBeneficDscp, false, selectedDscpAttrib).ToString();
            lblTextTrainModif.Text = theManagerService.DscpRiseSpec_getTheTrainJPModif(theManagedDscpIndex, selectedDscpAttrib,
                theManagedChar.theBeneficDscp, false, selectedMentoring).ToString();
            lblTextBasicJPValue.Text = theManagerService.DscpRiseSpec_getTheBasicJPForThisDscp(theManagedDscpIndex,
                selectedDscpNextLevel, theManagedChar.theBeneficDscp, false).ToString();
            if (theManagerService.DscpRiseSpec_helpOfMentorIsNeeded(theManagedDscpIndex) && !selectedMentoring)
            {
                errorProviderMistake.SetError(chckBxDscpMentor, "A jártasság mentor segítéségt követeli meg!");
                return;
            }
            selectedLevelFinalJP = theManagerService.DscpRiseSpec_calculateTheFinalJPForThisDscp(theManagedDscpIndex,
                selectedDscpNextLevel, theManagedChar.theBeneficDscp, false, selectedDscpAttrib, selectedMentoring);
            lblTextFinalJPValue.Text = selectedLevelFinalJP.ToString();
            readyToGainNewLevel = true;
        }

        private void resetNormModeTextBoxesAndVariables()
        {
            dtgrvDscpMain.DataSource = theManagerService.DscpRiseSpec_collectThisDscpNormalLevelsDeep(theManagedDscpIndex);
            adjustDtgrvMain_AtDetailsAndNormal();
            selectedDscpNextLevel = theManagerService.DscpRiseSpec_getTheNextDscpNormOrSpecLevel(theManagedDscpIndex, 0);
            if (selectedDscpNextLevel == 11)
            {
                lblTextNextLevel.Text = "";
                btnDscpRiseLevel.Visible = false;
                chckBxDscpMentor.Visible = false;
            }
            else
            {
                btnDscpRiseLevel.Visible = true;
                btnDscpRiseLevel.Text = "Számol";
                chckBxDscpMentor.Visible = true;
                lblTextNextLevel.Text = selectedDscpNextLevel.ToString();
            }
            chckBxDscpMentor.Checked = false;
            resetMutualTextBoxesAndVariables();

        }

        #endregion

        #region general events for normal and spec mode

        /// <summary>
        /// EVENT OF RISE LEVEL AT NORMAL AND SPEC MODE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDscpRiseLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if(dtgrvDscpMain.SelectedRows.Count > 0)
                {
                    if(theModeOfWindow == DscpSideWinMode.RiseSpec)
                    {
                        if (readyToGainNewLevel)
                        {
                            manageTheSpecLevelGain();
                            resetSpecModeManageTextBoxesAndVariables();
                            renewSpecModeDataTable();
                        }
                        else
                            manageTheSpecLevelPresentaion();
                    }
                    if(theModeOfWindow == DscpSideWinMode.RiseNorm)
                    {
                        if (readyToGainNewLevel)
                            manageTheNormalLevelGain();
                        else
                            manageTheNormalLevelJPPresentaion();
                    }
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// EVENT OF DELETION AT NORMAL AND SPEC MODE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDscpDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrvDscpMain.SelectedRows.Count > 0)
                {
                    if (theModeOfWindow == DscpSideWinMode.RiseSpec)
                    {
                        if (openDialogMessage("Biztos hogy törli a kijelölt specializációtípus szintjét?")
                            == DialogResult.Yes)
                        {
                            theManagerService.DscpGeneralProcess_RemoveSelectedLevel(theManagedDscpIndex,
                                selectedLevel_ToManage, selectedSpecIndex_ToManage);
                            resetSpecModeManageTextBoxesAndVariables();
                            renewSpecModeDataTable();
                        }

                    }
                    if (theModeOfWindow == DscpSideWinMode.RiseNorm)
                    {
                        if (openDialogMessage("Biztos hogy törli a kijelölt és az e-feletti szinteket?")
                            == DialogResult.Yes)
                        {
                            theManagerService.DscpGeneralProcess_RemoveSelectedLevel(theManagedDscpIndex, selectedLevel_ToManage, 0);
                            resetNormModeTextBoxesAndVariables();
                        }
                }
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// DTGRV MAIN SELECTION EVENT AT NORMAL AND SPEC MODE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtgrvDscpMain_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (theModeOfWindow == DscpSideWinMode.Details)
                    return;
                if (dtgrvDscpMain.SelectedRows.Count > 0)
                {
                    int dtgrvIndex = dtgrvDscpMain.SelectedRows[0].Index;
                    //SPEC MODE -> DATAS NEEDED TO FILL THE TEXTBOXES AND MANAGE VARIABLES
                    if (theModeOfWindow == DscpSideWinMode.RiseSpec)
                    {
                        selectedSpecIndex_ToManage = Convert.ToByte(dtgrvDscpMain.Rows[dtgrvIndex].Cells[0].Value); //IMPORTANT AT RISE LVL AND DELETE
                        selectedLevel_ToManage = Convert.ToByte(dtgrvDscpMain.Rows[dtgrvIndex].Cells[1].Value);     //IMPORTANT AT DELETE
                        resetSpecModeNewDetailsPermissionAreasAndVariables();
                    }
                    //NORMAL MODE -> DATAS NEEDED TO FILL THE MANAGE VARIALBES
                    if (theModeOfWindow == DscpSideWinMode.RiseNorm)
                    {
                        selectedLevel_ToManage = Convert.ToByte(dtgrvDscpMain.Rows[dtgrvIndex].Cells["Szint"].Value);
                    }
                    readyToGainNewLevel = false;
                    readyToGainNewSpecialization = false;
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }


        #endregion


    }
}
