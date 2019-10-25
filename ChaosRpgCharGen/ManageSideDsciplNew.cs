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
    public partial class ManageSideDsciplNew : MetroFramework.Forms.MetroForm
    {
        ManageCharactService theServiceChar;
        Form parentManagerWin;
        private GeneralOneTrunkEntity theCharTrunk;

        private bool readyToGetUPDscp = false;

        private short selectedDscpType;
        private short selectedDscpId;
        private short selectedDscpReqiurGroup;
        private short selectedDscpAttrib;
        private bool selectedMentorOrTraining;
        private int selectedDscpJPValue;

        public ManageSideDsciplNew(Form parentManager, ManageCharactService serv, GeneralOneTrunkEntity theCharTrunkElement)
        {
            try
            {
                InitializeComponent();
                theServiceChar = serv;
                parentManagerWin = parentManager;
                theCharTrunk = theCharTrunkElement;
                resetTheAreasToBeginnerState();
            }
            catch (Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }



        #region user dscp selection

        private void cmbBDscpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBDscpType.SelectedIndex > -1)
                {
                    selectedDscpId = -1;
                    nullify_AttirAndRequir_AtRechooseTypeOrName();
                    selectedDscpType = theServiceChar.DscpNewWindow_getTheDscpTypeId(cmbBDscpType.Text);
                    cmbBDscpName.SelectedIndex = -1;
                    cmbBDscpName.Items.Clear();
                    cmbBDscpName.Text = "";
                    cmbBDscpName.Items.AddRange(theServiceChar.DscpNewWindow_collectDscpNames_TextList(
                        (byte)selectedDscpType).ToArray());
                    txtBTypeCosting.Text = theServiceChar.DscpNewWindow_getTheDscpBasicCosting_ToText(
                        (byte)selectedDscpType, theCharTrunk.theBeneficDscp);
                    reviseTheUserMadeAllChoosing();
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void cmbBDscpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBDscpName.SelectedIndex > -1)
                {
                    nullify_AttirAndRequir_AtRechooseTypeOrName();
                    selectedDscpId = theServiceChar.DscpNewWindow_getTheDscpId(cmbBDscpName.Text);
                    cmbBDscpRequirs.Items.AddRange(
                        theServiceChar.DscpNewWindow_collectDscpRequir(selectedDscpId).ToArray());
                    cmbBDscpAttib.Items.AddRange(
                        theServiceChar.DscpNewWindow_collectDscpAttirb(selectedDscpId).ToArray());
                    txtBDscpStudiab.Text = theServiceChar.DscpNewWindow_getTheDscpStudiability_ToText(selectedDscpId);
                    reviseTheUserMadeAllChoosing();
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void cmbBDscpAttib_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBDscpAttib.SelectedIndex > -1)
                {
                    selectedDscpAttrib = theServiceChar.AttribGeneral_getTheAttribIdOfThisAttribName(cmbBDscpAttib.Text);
                    txtBChosenAttrib.Text = theServiceChar.AttribGeneral_getTheThisAttribFullValue(
                        (byte)selectedDscpAttrib).ToString();
                    txtBAttribSchema.Text = theServiceChar.DscpNewWindow_getTheDscpChosenJPAttribSchemaModifLevelsText_ToShow(
                        selectedDscpId, (byte)selectedDscpAttrib);
                    reviseTheUserMadeAllChoosing();
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void cmbbDscpRequirs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBDscpRequirs.SelectedIndex > -1)
                {
                    selectedDscpReqiurGroup = theServiceChar.DscpNewWindow_findTheDscpChosenRequirGroupId(selectedDscpId,
                        cmbBDscpRequirs.SelectedItem.ToString());
                    reviseTheUserMadeAllChoosing();
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void chckBDscpMentor_CheckedChanged(object sender, EventArgs e)
        {
            selectedMentorOrTraining = chckBDscpMentor.Checked;
            if (readyToGetUPDscp)
                startCountJPValues();
            else
                reviseTheUserMadeAllChoosing();
        }

        private void btnDscpGetup_Click(object sender, EventArgs e)
        {
            try
            {
                errorProviderMistake.Clear();
                if (readyToGetUPDscp)
                {
                    theServiceChar.DscpNewWindow_GainUpBrandNewDscp(selectedDscpId, (byte)selectedDscpReqiurGroup,
                        (byte)selectedDscpAttrib, txtBDscpDescr.Text, (short)selectedDscpJPValue,
                        selectedMentorOrTraining);
                    resetTheAreasToBeginnerState();
                }
                else
                {
                    startCountJPValues();
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void startCountJPValues()
        {
            if (theServiceChar.DscpNewWindow_areThereAllRequirement(selectedDscpId,
                        (byte)selectedDscpReqiurGroup, 1) == 1)
            {
                selectedDscpJPValue = theServiceChar.DscpNewWindow_calculateTheFinalJPForThisDscp(
                    selectedDscpId, 1, theCharTrunk.theBeneficDscp, false, (byte)selectedDscpAttrib,
                    selectedMentorOrTraining);

                if (theServiceChar.DscpGeneralInfo_isThereEnoughJPToGetLevel(
                    theCharTrunk.theStarterJPValue, selectedDscpJPValue))
                {
                    lblTextAttribModJP.Text = theServiceChar.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(
                        selectedDscpId, 1, theCharTrunk.theBeneficDscp, false, (byte)selectedDscpAttrib).ToString();
                    lblTextTrainModJP.Text = theServiceChar.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(
                        selectedDscpId, 1, theCharTrunk.theBeneficDscp, false, selectedMentorOrTraining).ToString();
                    lblTextAvailableJP.Text = theServiceChar.JPGeneralInfo_countTheSumAvailableJP(theCharTrunk.theStarterJPValue).ToString();
                    lblTextDscpFinalJP.Text = selectedDscpJPValue.ToString();
                    lblTextBaiscJP.Text = theServiceChar.DscpNewWindow_getTheBasicJPForThisDscp(
                        selectedDscpId, 1, theCharTrunk.theBeneficDscp, false).ToString();

                    btnDscpGetup.Text = "Felvétel";
                    readyToGetUPDscp = true;
                }
                else
                    errorProviderMistake.SetError(chckBDscpMentor,"A szükséges JP-vel nem rendelkezik!");
            }
            else
                errorProviderMistake.SetError(chckBDscpMentor,"A szükséges előfeltételek nincsenek meg!");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageSideDsciplNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentManagerWin.Show();
        }
        #endregion


        #region revisors, helpers and adjusters


        private void reviseTheUserMadeAllChoosing()
        {
            if (selectedDscpReqiurGroup > -1 && selectedDscpType > -1 && selectedDscpId > -1 && selectedDscpAttrib > -1)
            {
                btnDscpGetup.Enabled = true;
                btnDscpGetup.Text = "JP számítás";
            }
            else
            {
                btnDscpGetup.Enabled = false;
            }
            readyToGetUPDscp = false;
        }

        private void resetTheAreasToBeginnerState()
        {
            errorProviderMistake.Clear();

            selectedDscpType = -1;
            selectedDscpId = -1;
            cmbBDscpType.Items.AddRange(theServiceChar.DscpNewWindow_collectDscpTyes_TextList().ToArray());
            cmbBDscpType.SelectedIndex = -1;
            cmbBDscpName.SelectedIndex = -1;
            cmbBDscpName.Items.Clear();
            cmbBDscpName.Text = "";
            nullify_AttirAndRequir_AtRechooseTypeOrName();
            chckBDscpMentor.Checked = false;

            lblTextAvailableJP.Text = "";
            lblTextAttribModJP.Text = "";
            lblTextTrainModJP.Text = "";
            lblTextBaiscJP.Text = "";
            lblTextDscpFinalJP.Text = "";
        }

        private void nullify_AttirAndRequir_AtRechooseTypeOrName()
        {
            errorProviderMistake.Clear();

            cmbBDscpAttib.SelectedIndex = -1;
            cmbBDscpAttib.Items.Clear();
            cmbBDscpAttib.Text = "";
            cmbBDscpRequirs.SelectedIndex = -1;
            cmbBDscpRequirs.Items.Clear();
            cmbBDscpRequirs.Text = "";

            selectedDscpReqiurGroup = -1;
            selectedDscpAttrib = -1;

            readyToGetUPDscp = false;
        }

        private void openMessage(string message, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Fontos!", MessageBoxButtons.OK, type, 200);
        }
        #endregion



    }
}
