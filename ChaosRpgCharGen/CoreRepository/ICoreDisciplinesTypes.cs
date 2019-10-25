using ChaosRpgCharGen.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CoreRepository
{
    /// <summary>
    /// INTERFACE FOR DSCIPLINE REPOSITORY
    /// </summary>
    interface ICoreDisciplinesTypes
    {
        //CHARACTER CREATION
        List<CoreOneDscpType> collectTheAllTypesOfDscps_InColl();

        //AT GAIN BRAND NEW DSCP
        List<string> collectTheAllTypesOfDscps_InString(); //AT CHOOSING BRAND NEW DSCP - SHOWS THE TYPES
        byte findTheDscpTypeIdOfThisTypeText(string typeName);
        List<string> collectTheseTypesOfDscps(byte typeId);     //AT CHOOSING BRAND NEW DSCP - SHOWS SUBCOLLECTION
        short findTheDscpIdOfThisDscpText(string dscpName);
        //List<CoreDscpAttrib> collectTheAllAttribsOfThisDscp(short dscpId);  //AT CHOOSING BRAND NEW DSCP - SHOWS THE POSSIBLE ATTRIBUTE CONNECTIONS


        //AT RISE LEVEL OF A DSCP

        //AT GETTING SPECIALISATION OR RISING ITS LEVEL CASE
        bool isThereSpecialisationForThisDscp(short dscpId);
        string[] collectThePossibleSpecAreasForThisDscp_ToShowTheUser(short dscpId);    //AT CHOOSING NEW SPECIALISTAION - SHOw THE WAYS TO SPECIALISE
        List<CoreDscpSpec> collectTheChosenSpecRequirementsForThisDscp(short dscpId);    //SEEKS THE SPECIALISATION REQUIREMENTS - TO ANALYTE THE PROGRAM IF THERE ARE THE PROPER PREREQUIREMENTS TO HAVE THE CHARACTER

        //GENERAL PURPOSES
        bool mustBeMentorAtStrudyThisDscp(short dscpId);    //AT CHOOSING BREAND NEW OR RISING DSCP - ANALYZE IS THIS NEEDS MENTOR TO STUDY
        bool mustTreathSevereTheRequirForThisDscp(short dscpId);
        bool isThereRequirementofThisDscp(short dscpId);    //AT CHOOSING BRAND NEW OR RISING DSCP  - ANALYTE IF THERE IS AT ALL REQUIREMENT
        bool isThisDscpStudyIsOriginallyBeneficial(byte typeId);   //AT CHOOSING BRAND NEW OF RISE LEVEL - ANALYTE THE ORIGINAL BENEFICIALITY

        byte findTheChosenSpecAreaIdByItsText(short dscpId, string specAreaText);
        string findTheDscpName(short dscpId);
        string findTheDscpTypeName(short dscpId);
        short findTheDscpAttribSchemValue(short dscpId, byte attribId);
        byte findTheTypeIdOfDscp(short dscpId);
        string findTheSpecAreaTextForThisDscp(short dscpId, byte areaId);


        //REQUIREMENT MANGE
        CoreDscpNormalRequir findTheDscpRequirementOfThisDscp_Entity(short dscpId, byte requirGroup);   //SEEKS THE CHOSEN DSCP'S DSCP REQUIREMENT - IT HAS FIEW CASE
        CoreDscpSpec findTheSeekedSpecRequir_Entity(short dscpId, byte specReqGroup);

        List<string> collectThePossibRequirs_OfThisDscpNormalLevels_InStringCombin(short dscpId); //AT CHOOSING BRAND NEW DSCP - sHOWS THE CHOSEN ONES REQUIREMENTS - IF THERE IS
        byte findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(short dscpId, string dscpCombText);

    }


}
