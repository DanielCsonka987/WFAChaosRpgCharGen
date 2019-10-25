using ChaosRpgCharGen.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreRepository
{
    /// <summary>
    /// INTERFACE FOR ATTRIBUTES, RACES, SIZES REPOSITORY
    /// </summary>
    interface ICoreAttribsRacesSizes
    {

        //ABOUT SIZES
        string findTheNameOfThisSize(byte sizeId);
        byte fintTheBasicQuicknessOfThisSize(byte sizeId);  //AT THE BEGINING OF CHARACTER CREATION - USER MAY MODIFY AND FINAL VALUE STORED IN CHAR DB
        List<CoreOneSizeDef> collectSizeDef();

        //ABOUT RACES
        string findTheNameOfThisRace(short raceId);     //IT IS SHOWED AT MAIN MANAGER WINDOW - NEEDS A LOT
        byte findTheSizeOfThisRace(short raceId);       //IT IS NEEDED A LOT FOR SIZE NAME

        short[] findTheStarterStatsOfThisRace(short raceId);    //AT THE BEGINING OF CHARACTER CREATION - USER STAT NUMBERS ARE SUPERVISED WITH THIS - 1->4->7->10 ATTRIB ID-S ORDER IN THE ARRAY

        //ABOUT ATTRIBS
        string findTheNameOfThisAttrib(byte attribId);  //IT IS NEEDED TO SHOW NEW DSCP GETTING UP - CHOOSING A DSCP ATTRIB TO A PROPER MANAGEMENT
        byte findTheAttriIdThisAttribName(string attribName);   //AT ALL CASES WHERE USER CHOOSE AN ATTRIB AND SYSTEM NEEDS THE ID OF THAT
    }
}
