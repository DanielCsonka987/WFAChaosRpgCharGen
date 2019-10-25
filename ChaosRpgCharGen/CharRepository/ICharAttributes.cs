using ChaosRpgCharGen.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1
{
    /// <summary>
    /// INTERFACE OF CHAR-ATTRIBUTE REPOSITORY
    /// </summary>
    interface ICharAttributes
    {
        //AT NORMAL REVIEW, ATTRIB MANAEGER WINDOW
        short getTheFullValueOfThisAttribute(byte attribId);    //SHOW THE ATTRIBUTES - REVIEW OF DSCP OR OTHER
        short[] getTheAttributesCollectively_ToShowFinal();  //SHOW THE ATTRIBS REVIEW
        short[,] getTheAttrubutesCollectively_ToShowInDetails(); //SHOW THE ATTRIBS DETAILS

        //MANAGE THE RISING, MANAGER ATTRIB WINDOW
        int addOnePointRiseToSpecifAttribute(byte attribId, int dscpId1, int dscpId2,
            byte pointType);
        void removeOneAttribEnchElement(int attribEnchIndex);      //REMOVES THE CHOSEN ATTRIB ID
        List<CharOneAttribEnch> collectTheAttributeEnchList_ToShow();   //FOR ANALYSATION OF UNPENT ATTRIBS

        //AT DSCP DELETE ANALYZE
        List<int[]> collectAttribEnchThatOverlapsWithThese(int[] attribEnchIndexes, int dscpIndexUnderDelete);  //attribEcnh-s and dscpIndexe
                //that overlaps with deleting attribEnch-s at type 1
    }
}
