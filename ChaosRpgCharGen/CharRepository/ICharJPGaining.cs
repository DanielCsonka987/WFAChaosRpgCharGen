using ChaosRpgCharGen.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1
{
    /// <summary>
    /// INTERFACE OF CHAR-JP-GAINING REPOSITORY
    /// </summary>
    interface ICharJPGaining
    {
        void addNewJPGain(int JPAmount);    //ADD NEW JP AMOUNT TO COLELCTION - CHAR JPGAIN
        void removeThisJPGain(int gainId);  //REMOVES AN EXISITING JP AMOUNT FROM COLLECTION - MISTAKE

        List<CharOneJPGain> getTheJPGainCollection();       //GETTER OF JPGainColl - DETAILS TO list

        int sumAllCollectedJP();        //COLLECTS THE ALL GAINED JP

    }
}
