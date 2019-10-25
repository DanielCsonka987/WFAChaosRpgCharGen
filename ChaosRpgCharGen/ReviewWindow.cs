using ChaosRpgCharGen.CoreRepository;
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
    public partial class ReviewWindow : MetroFramework.Forms.MetroForm
    {
        private ReviewCharactService serviceReview;

        public ReviewWindow()
        {
            try
            {
                InitializeComponent();
                serviceReview = new ReviewCharactService();
            }
            catch(CoreRepositoryException e)
            {
                openMessage(e.Message, "Hiba történt!", MessageBoxIcon.Error);
            }
            catch(Exception e)
            {
                openMessage(e.Message, "Hiba történt!", MessageBoxIcon.Error);
            }
        }

        #region EVENTS METHODS

        /// <summary>
        /// Event to manage the will of user to exit this program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Event to manage the will of user to create a new character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbtnNew_Click(object sender, EventArgs e)
        {
            ReviewCreateCharWindow charCreateWin = new ReviewCreateCharWindow(this,serviceReview);
            this.Hide();
            charCreateWin.Show();
        }
        /// <summary>
        /// Event to manage the will of user to open an existing charater
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrdCharList.SelectedRows[0].Index != -1)
                {
                    int charId = getTheCharacterIdFromDtgrvChar();
                    openTheManagerWindow(charId);
                }
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, "Hiba történt!", MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Event to manage the will of user to delet an existing character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrdCharList.SelectedRows[0].Index != -1)
                {
                    DialogResult res = MetroFramework.MetroMessageBox.Show(this, "Biztos hogy törlöd?",
                        "Törlés megerősítése", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 200);
                    if (res == DialogResult.Yes)
                    {
                        int charId = getTheCharacterIdFromDtgrvChar();
                        //Debug.WriteLine(charId.ToString());
                        if (serviceReview.removeExisitingCharacter(charId))
                            openMessage("A törlés sikeres!", "Végrehajtva!", MessageBoxIcon.Information);
                        else
                            openMessage("A törlés sikertelen!", "Hiba történt!", MessageBoxIcon.Error);
                        renewTheDataGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                openMessage(ex.Message, "Hiba történt!", MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// LOADS IN THE CHARACTERS TO THE WINDOW-GRIDVIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                renewTheDataGrid();
        }
        #endregion

        #region helper methods
        /// <summary>
        /// RENEW THE CHARATER LIST
        /// </summary>
        private void renewTheDataGrid()
        {
            dtgrdCharList.DataSource = serviceReview.getListOfCharacters();
            dtgrdCharList.Columns[0].Width = 45;
            dtgrdCharList.Columns[1].Width = 175;

        }
        /// <summary>
        /// FINDS THE CHOSEN CHARACTER'S CHARID
        /// </summary>
        /// <returns>charId</returns>
        private int getTheCharacterIdFromDtgrvChar()
        {
            object charId = dtgrdCharList.SelectedRows[0].Cells[0].Value;
            return Convert.ToInt32(charId);
        }
        /// <summary>
        /// OPENS A MESSAGEBOX
        /// </summary>
        /// <param name="message">the message text</param>
        /// <param name="title">the title text</param>
        /// <param name="type">message type</param>
        private void openMessage(string message, string title, MessageBoxIcon type)
        {
            MetroFramework.MetroMessageBox.Show(this, message, title,
                MessageBoxButtons.OK, type, 200);
        }
        /// <summary>
        /// OPENS THE MANAGER WINDOW OF A CHARACTER
        /// </summary>
        /// <param name="charId">needed charId</param>
        private void openTheManagerWindow(int charId)
        {
            ManagerWindow manager = new ManagerWindow(this, serviceReview.findTheExistingCharacter(charId));
            manager.Show();
            this.Hide();
        }


        #endregion

        private void lblDetails_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewConfigAndHelp rch = new ReviewConfigAndHelp(serviceReview);
                rch.Show();
            }
            catch(Exception ex)
            {
                openMessage(ex.Message, "Hiba történt!", MessageBoxIcon.Error);
            }

        }
    }
}
