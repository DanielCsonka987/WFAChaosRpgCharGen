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
    public partial class ManageSideAttribEnchance : MetroFramework.Forms.MetroForm
    {
        private ManageCharactService theManagerServise;
        private GeneralOneTrunkEntity theManagedCharact;
        private Form theManagParntWin;

        //FROM DTGRVW - TO DELET IF NEEDED
        private int selectedSpentAttribEnch_ToManage;
        private int selectedSpentDscpIndex1_ToManage;
        private bool selectedSpentDscpSpec1_ToManage;
        private int selectedSpentDscpIndex2_ToManage;
        private bool selectedSpentDscpSpec2_ToManage;

        //TO PROCESS OF SAVE NEW IF NEEDED
        //FROM COMBOBOX
        private string selectedAttribNameToDevelop_BookUp;
        //FROM DTGRVW
        private string selectedTypeChategory_BookUp;
        private int selectedUnspentDscpIndex1_BookUp;
        private string selectedUnspentAttribName1_DefineAttrib;  
        private string selectedUnspentAttribPoint1_DefineAttrib;
        private int selectedUnspentDscpLvl1_BookUp;
        private int selectedUnspentDscpSpec1_BookUp;
        private int selectedUnspentDscpIndex2_BookUp;
        private string selectedUnspentAttribName2_DefineAttrib;
        private string selectedUnspentAttribPoint2_DefineAttrib;
        private int selectedUnspentDscpLvl2_BookUp;
        private int selectedUnspentDscpSpec2_BookUp;

        public ManageSideAttribEnchance(Form managParent, ManageCharactService serv, GeneralOneTrunkEntity charact)
        {
            try
            {
                InitializeComponent();
                theManagParntWin = managParent;
                theManagerServise = serv;
                theManagedCharact = charact;
                loadInDatasToTables();
                loadInDatasToFields();
            }
            catch(Exception ex)
            {
                openMessage("Felület-betöltési hiba:\n" + ex.Message, MessageBoxIcon.Error);
            }
        }

        #region VIEW MANAGERS

        private void loadInDatasToFields()
        {
            string[] attribDetails = theManagerServise.AttribEnchWindow_collectTheCharDetailedAttributes();
            lblTextAttrib1.Text = attribDetails[0];
            lblTextAttrib2.Text = attribDetails[1];
            lblTextAttrib3.Text = attribDetails[2];
            lblTextAttrib4.Text = attribDetails[3];
            lblTextAttrib5.Text = attribDetails[4];
            lblTextAttrib6.Text = attribDetails[5];
            lblTextAttrib7.Text = attribDetails[6];
            lblTextAttrib8.Text = attribDetails[7];
            lblTextAttrib9.Text = attribDetails[8];
            lblTextAttrib10.Text = attribDetails[9];
            lblTextAttrib11.Text = attribDetails[10];
            lblTextAttrib12.Text = attribDetails[11];
        }

        private void loadInDatasToTables()
        {
            dtgrvAttribEnchList.DataSource = theManagerServise.AttribEnchWindow_collectTheCharSpentAttribEnchances();
            dtgrvAttribEnchList.Columns[0].Width = 40;  //ENCH INDEX
            dtgrvAttribEnchList.Columns[1].Width = 80;  //ATTRIB NAME
            dtgrvAttribEnchList.Columns[2].Width = 120; //TYPE
            dtgrvAttribEnchList.Columns[3].Width = 50;  //DSCP INDEX 1
            dtgrvAttribEnchList.Columns[4].Width = 100;  //DSCP NAME 1
            dtgrvAttribEnchList.Columns[5].Width = 60;  //DSCP LVL 1
            dtgrvAttribEnchList.Columns[6].Width = 80;  //DSCP SPEC 1
            dtgrvAttribEnchList.Columns[7].Width = 50;  //DSCP INDEX 2
            dtgrvAttribEnchList.Columns[8].Width = 100;  //DSCP NAME 2
            dtgrvAttribEnchList.Columns[9].Width = 60;  //DsCP LVL 2
            dtgrvAttribEnchList.Columns[10].Width = 80;  //DSCP SPEC 2
            dtgrVPossibleDevelop.DataSource = theManagerServise.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
            dtgrVPossibleDevelop.Columns[0].Width = 40; //DSCP INDEX
            dtgrVPossibleDevelop.Columns[1].Width = 100; //DSCP NAME
            dtgrVPossibleDevelop.Columns[2].Width = 80; //DSCP CONN ATTRIB
            dtgrVPossibleDevelop.Columns[3].Width = 50; //DSCP LVL
            dtgrVPossibleDevelop.Columns[4].Width = 70; //DSCP SPEC
            dtgrVPossibleDevelop.Columns[5].Width = 120; //TYPE
        }

        #endregion

        #region DELETE FROM ENCHANCES LIST
        private void dtgrvAttribEnchList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgrvAttribEnchList.SelectedRows.Count > 0)
                {
                    int dtgrvIndex = dtgrvAttribEnchList.SelectedRows[0].Index;
                    selectedSpentAttribEnch_ToManage = Convert.ToInt32(dtgrvAttribEnchList.Rows[dtgrvIndex].Cells[0].Value);
                    selectedSpentDscpIndex1_ToManage = Convert.ToInt32(dtgrvAttribEnchList.Rows[dtgrvIndex].Cells[3].Value);
                    selectedSpentDscpSpec1_ToManage = dtgrvAttribEnchList.Rows[dtgrvIndex].Cells[6].Value.ToString() == "Igen" ? true:false;
                    selectedSpentDscpIndex2_ToManage = Convert.ToInt32(dtgrvAttribEnchList.Rows[dtgrvIndex].Cells[7].Value);
                    selectedSpentDscpSpec2_ToManage = dtgrvAttribEnchList.Rows[dtgrvIndex].Cells[10].Value.ToString() == "Igen" ? true : false;
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        private void btnAttribMitigate_Click(object sender, EventArgs e)
        {
            try
            {
                if(selectedSpentAttribEnch_ToManage > 0)
                {
                    if (openDialogMessage("Biztos hogy eltávolítja a tulajdonságfejlesztést?") == DialogResult.Yes)
                    {

                        theManagerServise.AttribEncWindow_removeTheAttribEnchancement(selectedSpentDscpIndex1_ToManage, selectedSpentDscpSpec1_ToManage,
                           selectedSpentDscpIndex2_ToManage, selectedSpentDscpSpec2_ToManage, selectedSpentAttribEnch_ToManage);

                        loadInDatasToTables();
                        loadInDatasToFields();

                    }
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region ENCHANCE AN ATTRIB VALUE

        private void dtgrVPossibleDevelop_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                errorProviderMistake.Clear();
                if (dtgrVPossibleDevelop.SelectedRows.Count == 1)
                {
                    int dtgrvIndex = dtgrVPossibleDevelop.SelectedRows[0].Index;
                    selectedTypeChategory_BookUp = dtgrVPossibleDevelop.Rows[dtgrvIndex].Cells[5].Value.ToString();

                    collectDatasFromPossibleDtgrv_SenseDataset1(dtgrvIndex);
                    collectDatasFromPossibleDtgrv_NonSenseDataset2();   //NEUTRALISE VARIABLE-SET-2 DATAS
                    loadInChosableAttribName();
                }
                else if (dtgrVPossibleDevelop.SelectedRows.Count == 2)
                {
                    //POINTER DEFINITION
                    int dtgrvIndex1 = dtgrVPossibleDevelop.SelectedRows[0].Index;
                    int dtgrvIndex2 = dtgrVPossibleDevelop.SelectedRows[1].Index;

                    //IMPORTANT DATA PREVIOUS COLLECT - TO SCREEN
                    selectedTypeChategory_BookUp = dtgrVPossibleDevelop.Rows[dtgrvIndex1].Cells[5].Value.ToString();
                    string tempType = dtgrVPossibleDevelop.Rows[dtgrvIndex2].Cells[5].Value.ToString();

                    //SCREEN IF IS POSSIBLE DSCP COMBINATIONS -> SERVICE COULD TOLERATE BUT NO POINT TO DO OTHER CASE
                    if (selectedTypeChategory_BookUp == "4-5-6 sz." && tempType == selectedTypeChategory_BookUp)
                    {
                        //COLLECTD OTHER DATAS AND FILL UP COMBOBOX WITH ATTIB VALID NAMES
                        collectDatasFromPossibleDtgrv_SenseDataset1(dtgrvIndex1);
                        collectdDatasFromPossibleDtgrv_SenseDataset2(dtgrvIndex2);
                        loadInChosableAttribName();
                    }
                    else
                    {
                        errorProviderMistake.SetError(cmbBNewAttribEnch, "Két elem megjelőlése csak a 4-5-6 szintek esetén lehet!");
                        resetComboBoxAttribChooser();
                    }
                }
                else if (dtgrVPossibleDevelop.SelectedRows.Count > 2)
                    resetComboBoxAttribChooser();
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void collectDatasFromPossibleDtgrv_SenseDataset1(int index1)
        {
            selectedUnspentAttribName1_DefineAttrib = dtgrVPossibleDevelop.Rows[index1].Cells[2].Value.ToString();
            selectedUnspentAttribPoint1_DefineAttrib = dtgrVPossibleDevelop.Rows[index1].Cells[5].Value.ToString();

            selectedUnspentDscpIndex1_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index1].Cells[0].Value);
            selectedUnspentDscpLvl1_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index1].Cells[3].Value);
            selectedUnspentDscpSpec1_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index1].Cells[4].Value);
        }

        private void collectdDatasFromPossibleDtgrv_SenseDataset2(int index2)
        {
            selectedUnspentAttribName2_DefineAttrib = dtgrVPossibleDevelop.Rows[index2].Cells[2].Value.ToString();
            selectedUnspentAttribPoint2_DefineAttrib = dtgrVPossibleDevelop.Rows[index2].Cells[5].Value.ToString();

            selectedUnspentDscpIndex2_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index2].Cells[0].Value);
            selectedUnspentDscpLvl2_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index2].Cells[3].Value);
            selectedUnspentDscpSpec2_BookUp = Convert.ToInt32(dtgrVPossibleDevelop.Rows[index2].Cells[4].Value);
        }

        private void collectDatasFromPossibleDtgrv_NonSenseDataset2()
        {
            selectedUnspentAttribName2_DefineAttrib = "";
            selectedUnspentAttribPoint2_DefineAttrib = "";

            selectedUnspentDscpIndex2_BookUp = 0;
            selectedUnspentDscpLvl2_BookUp = 0;
            selectedUnspentDscpSpec2_BookUp = 0;
        }

        private void loadInChosableAttribName()
        {
            try
            {
                cmbBNewAttribEnch.Items.Clear();
                cmbBNewAttribEnch.Items.AddRange(theManagerServise.AttribEnchWindow_collectAttribListToUpgrade(
                    selectedUnspentAttribName1_DefineAttrib, selectedUnspentAttribName2_DefineAttrib,
                    selectedUnspentAttribPoint1_DefineAttrib, selectedUnspentAttribPoint2_DefineAttrib).ToArray());
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void resetComboBoxAttribChooser()
        {
            cmbBNewAttribEnch.Items.Clear();
            cmbBNewAttribEnch.Text = "";
            cmbBNewAttribEnch.SelectedIndex = -1;
        }

        private void btnAttribEnch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBNewAttribEnch.SelectedIndex > -1)
                {
                    selectedAttribNameToDevelop_BookUp = cmbBNewAttribEnch.SelectedItem.ToString();
                    theManagerServise.AttribEnchWindow_saveTheNewAttribEnchancement(
                        selectedAttribNameToDevelop_BookUp, selectedTypeChategory_BookUp,
                        selectedUnspentDscpIndex1_BookUp, selectedUnspentDscpLvl1_BookUp, selectedUnspentDscpSpec1_BookUp,
                        selectedUnspentDscpIndex2_BookUp, selectedUnspentDscpLvl2_BookUp, selectedUnspentDscpSpec2_BookUp);
                    loadInDatasToTables();
                    loadInDatasToFields();
                }
                else
                    errorProviderMistake.SetError(cmbBNewAttribEnch, "Kérem válasszon fejlesztendő tulajdonságot!");
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region HELPERS AND CLOSE EVENTS

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageSideAttribEnchance_FormClosed(object sender, FormClosedEventArgs e)
        {
            theManagParntWin.Show();
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
}
