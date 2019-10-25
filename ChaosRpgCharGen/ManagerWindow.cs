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
using ChaosRpgCharGen.GeneralModel;

namespace ChaosRpgCharGen
{
    public partial class ManagerWindow : MetroFramework.Forms.MetroForm
    {
        private ManageSideAttribEnchance theAttribEnchWin;
        private ManageSideDisciplRiseSpec theDscpbRiseDscpWin;
        private ManageSideDsciplNew theDscpNewWin;
        private ManageSideJP theJPDetailsWin;

        private ManageCharactService theManagerService;
        private Form theParentRevWind;
        private GeneralOneTrunkEntity theCharacterTrunk;

        private int selectedDscpIndex;

        public ManagerWindow(Form parentReviewWin, GeneralOneTrunkEntity charTrunkDatas)
        {
            try
            {
                InitializeComponent();
                theParentRevWind = parentReviewWin;
                theCharacterTrunk = charTrunkDatas;
                //Debug.WriteLine(charTrunkDatas.theCharId.ToString());
                theManagerService = new ManageCharactService(charTrunkDatas.theCharId);
                view_adjustStableDatasToFields();
                view_adjutChangableDatasToFields();
            }
            catch (Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }

        #region ADJUST VIEW CONTROLS AND IST CONTENT

        private void ManagerWindow_Shown(object sender, EventArgs e)
        {
            view_adjutChangableDatasToFields();
        }

        private void view_adjustStableDatasToFields()
        {
            lblTextCharName.Text = theCharacterTrunk.theCharName;
            lblTextRaceName.Text = theCharacterTrunk.theRaceName;
            lblTextSize.Text = theCharacterTrunk.theSizeText;
            lblTextQuickness.Text = theCharacterTrunk.theRealSpeed.ToString();
            txtBCharDescr.Text = theCharacterTrunk.theCharDescr;
        }


        private void view_adjutChangableDatasToFields()
        {
            adjustStatsToFields();
            adjustJPsToFields();
            adjustDscpsAtTableFields();

        }

        private void adjustStatsToFields()
        {
            short[] statDatas = theManagerService.ManagerWindow_getTheCharFullAttributes();
            lblTextAttrib1.Text = statDatas[0].ToString();
            lblTextAttrib2.Text = statDatas[1].ToString();
            lblTextAttrib3.Text = statDatas[2].ToString();
            lblTextAttrib4.Text = statDatas[3].ToString();
            lblTextAttrib5.Text = statDatas[4].ToString();
            lblTextAttrib6.Text = statDatas[5].ToString();
            lblTextAttrib7.Text = statDatas[6].ToString();
            lblTextAttrib8.Text = statDatas[7].ToString();
            lblTextAttrib9.Text = statDatas[8].ToString();
            lblTextAttrib10.Text = statDatas[9].ToString();
            lblTextAttrib11.Text = statDatas[10].ToString();
            lblTextAttrib12.Text = statDatas[11].ToString();
        }

        private void adjustJPsToFields()
        {
            lblTextSumJP.Text = theManagerService.JPGeneralInfo_countTheSumCollectedJP(
                theCharacterTrunk.theStarterJPValue).ToString();
            lblTextSpentJP.Text = theManagerService.JPGeneralInfo_countTheSumSpentJP().ToString();
            lblTextAvailableJP.Text = theManagerService.JPGeneralInfo_countTheSumAvailableJP(
                theCharacterTrunk.theStarterJPValue).ToString();
        }


        private void adjustDscpsAtTableFields()
        {
            dtgvwDisciplines.DataSource = theManagerService.ManagerWindow_collectAllTheDscpSurface();
            dtgvwDisciplines.Columns[0].Width = 30;
            dtgvwDisciplines.Columns[2].Width = 40;
            dtgvwDisciplines.Columns[3].Width = 50;
            dtgvwDisciplines.Columns[5].Width = 50;
        }


        private void adjustDscpButtonsOn()
        {
            btnRemoveDscp.Visible = true;
            btnDetailDscp.Visible = true;
            btnRiseDscp.Visible = true;
            if (theManagerService.DscpRiseSpec_isThereSpecialization(selectedDscpIndex))
                btnSpecDscp.Visible = true;
            else
                btnSpecDscp.Visible = false;
        }

        private void adjustDscpButtonOff()
        {
            btnRemoveDscp.Visible = false;
            btnRiseDscp.Visible = false;
            btnSpecDscp.Visible = false;
            btnDetailDscp.Visible = false;
        }
        #endregion

        #region EVENT ACTIONS
        private void ManagerWindow_Activated(object sender, EventArgs e)
        {
            view_adjutChangableDatasToFields();
        }
        ///BTN EXIT
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //AFTER CLOSE MANAGER WINDOW OPENS THE REVIEW WINDOW - ALL CASES
        private void ManagerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            theParentRevWind.Show();
        }

        //BTN JP MANGE
        private void btnJPManage_Click(object sender, EventArgs e)
        {
            try
            {
                theJPDetailsWin = new ManageSideJP(this, theCharacterTrunk, theManagerService);
                theJPDetailsWin.Show();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN ATTRIB ENCH MANAGE
        private void btnAttribEnch_Click(object sender, EventArgs e)
        {
            try
            {
                theAttribEnchWin = new ManageSideAttribEnchance(this, theManagerService, theCharacterTrunk);
                theAttribEnchWin.Show();
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN NEW DSCP
        private void btnNewDscp_Click(object sender, EventArgs e)
        {

            try
            {
                theDscpNewWin = new ManageSideDsciplNew(this, theManagerService, theCharacterTrunk);
                theDscpNewWin.Show();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN RISE NORM LVL DSCP
        private void btnRiseDscp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvwDisciplines.SelectedRows.Count == 1)
                {
                    openSideDscpManager(DscpSideWinMode.RiseNorm);
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN DETAIL DSCP
        private void btnDetailDscp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvwDisciplines.SelectedRows.Count == 1)
                {
                    openSideDscpManager(DscpSideWinMode.Details);
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN SPEC LVL DSCP
        private void btnSpecDscp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgvwDisciplines.SelectedRows.Count == 1)
                {
                    openSideDscpManager(DscpSideWinMode.RiseSpec);
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        //BTN WHOLE DSCP
        private void btnRemDscp_Click(object sender, EventArgs e)
        {
            if (openDialogMessage("Biztos hogy eltávolítja a jártasság egészét?") == DialogResult.Yes)
            {
                theManagerService.DscpGeneralProcess_RemoveSelectedLevel(selectedDscpIndex, 1, 0);
                view_adjustStableDatasToFields();
            }
        }
        //DTGRVW SELECTED / UNSELECTED
        private void dtgvwDisciplines_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvwDisciplines.SelectedRows.Count > 0)
                {
                    int dtgrvIndex = dtgvwDisciplines.SelectedRows[0].Index;
                    selectedDscpIndex = Convert.ToInt32(dtgvwDisciplines.Rows[dtgrvIndex].Cells[0].Value);

                    adjustDscpButtonsOn();
                }
                else
                    adjustDscpButtonOff();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region SIDE WINDOWS HELPER METHODS
        private void openSideDscpManager(DscpSideWinMode mode)
        {
            try
            {
                if (mode == DscpSideWinMode.RiseSpec &&
                    !theManagerService.DscpRiseSpec_isThereSpecialization(selectedDscpIndex))
                    return;
                theDscpbRiseDscpWin = new ManageSideDisciplRiseSpec(this, mode, selectedDscpIndex,
                    theManagerService, theCharacterTrunk);
                theDscpbRiseDscpWin.Show();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void openMessage(string message, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Fontos!", MessageBoxButtons.OK, type, 200);
        }


        private DialogResult openDialogMessage(string message)
        {
            return MetroFramework.MetroMessageBox.Show(this, message, "Biztos?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 200);
        }
        #endregion


    }


    public enum DscpSideWinMode
    {
        RiseNorm, RiseSpec, Details
    }
}
