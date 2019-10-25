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
    public partial class ManageSideJP : MetroFramework.Forms.MetroForm
    {
        ManageCharactService theManagerService;
        GeneralOneTrunkEntity theActualManagedChar;
        Form parentManagerWin;

        public ManageSideJP(Form parentManag, GeneralOneTrunkEntity actManagChar,
            ManageCharactService servManager)
        {
            try
            {
                InitializeComponent();
                theManagerService = servManager;
                theActualManagedChar = actManagChar;
                parentManagerWin = parentManag;
                adjustDatasToFields();
            }
            catch (Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }

        private void adjustDatasToFields()
        {
            txtBNewJP.Text = "";
            lblTextSumJP.Text = theManagerService.JPGeneralInfo_countTheSumCollectedJP(
                theActualManagedChar.theStarterJPValue).ToString();
            lblTextSpentJP.Text = theManagerService.JPGeneralInfo_countTheSumSpentJP().ToString();
            lblTextAvailableJP.Text = theManagerService.JPGeneralInfo_countTheSumAvailableJP(
                theActualManagedChar.theStarterJPValue).ToString();
            lblTextCollectedJP.Text = theManagerService.JPGeneralInfo_countTheCollectedJP().ToString();
            lblTextStarterJP.Text = theActualManagedChar.theStarterJPValue.ToString();
            dtgrvJPGainList.DataSource = theManagerService.JPManagerWindow_collectTheJPGains();
            dtgrvJPGainList.Columns[0].Width = 50;
            dtgrvJPGainList.Columns[1].Width = 80;
        }

        private void btnNewJP_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBNewJP.Text != "")
                {
                    if (Int32.TryParse(txtBNewJP.Text, out int value))
                    {
                        theManagerService.JPManagerWinodw_saveTheNewJPPortion(value);
                        adjustDatasToFields();
                    }
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveJP_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrvJPGainList.SelectedRows.Count == 1)
                {
                    int jpGainIndex = Convert.ToInt32(dtgrvJPGainList.SelectedRows[0].Cells[0].Value);
                    theManagerService.JPManagerWindow_removeTheExistingJPPortion(jpGainIndex);
                    adjustDatasToFields();
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageSideJP_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentManagerWin.Show();
        }


        private void openMessage(string message, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, "Fontos!", MessageBoxButtons.OK, type, 200);
        }

    }
}
