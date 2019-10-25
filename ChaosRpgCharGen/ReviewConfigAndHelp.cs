using ChaosRpgCharGen.GeneralRepository;
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
    public partial class ReviewConfigAndHelp : MetroFramework.Forms.MetroForm
    {
        private ReviewCharactService theReviewService;
        private GeneralBeneficMediator theGbmService;
        private byte thePicCounter;
        public ReviewConfigAndHelp(ReviewCharactService serv)
        {

            InitializeComponent();
            theGbmService = new GeneralBeneficMediator();
            theReviewService = serv;
            bool[] actualConfigDscpBenef = theReviewService.loadInTheBeneficialConfig();
            chckBDscpBenefOrdinary.Checked = actualConfigDscpBenef[0];
            chckBDscpBenefProff.Checked = actualConfigDscpBenef[1];
            thePicCounter = 1;
            adjustHelperPic();
        }

        private void adjustHelperPic()
        {
            if (thePicCounter == 10)
                thePicCounter = 1;
            else if (thePicCounter == 0)
                thePicCounter = 9;
            pctrBxAppDescr.Image = Image.FromFile("HelperPics/help0"+ thePicCounter + ".png");
            pctrBxAppDescr.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnConfSave_Click(object sender, EventArgs e)
        {
            theGbmService.saveBeneficConfig(1, chckBDscpBenefOrdinary.Checked);
            theGbmService.saveBeneficConfig(2, chckBDscpBenefProff.Checked);
        }

        private void mBtnNext_Click(object sender, EventArgs e)
        {
            thePicCounter++;
            adjustHelperPic();
        }

        private void mBtnPrevious_Click(object sender, EventArgs e)
        {
            thePicCounter--;
            adjustHelperPic();
        }
    }
}
