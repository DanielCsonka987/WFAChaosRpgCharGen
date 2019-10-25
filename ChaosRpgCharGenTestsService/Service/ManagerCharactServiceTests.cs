using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ChaosRpgCharGen.Service.Tests
{
    [TestClass()]
    public class ManagerCharactServiceTests
    {
        [TestMethod()]
        public void Service_WithWrongCharId()
        {
            try
            {
                ManageCharactService mcs = new ManageCharactService(3);
                Assert.Fail("It loded in, depite no attributes!");
            }
            catch (ManageCharactServException)
            {
                //ATTRIBUTE REPOSITORY TRHOWS SUCH ERROR - IT MUST
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void Service_BrandNewCharacter_getSomeJPAndDscp()
        {
            try
            {
                //NO DISCIPLINES OR JP-S - ONLY TRUNK AND STAT DATAS
                ManageCharactService mcs = new ManageCharactService(2);
                if (mcs.JPGeneralInfo_countTheSumAvailableJP(3000) != 3000)
                    Assert.Fail("Problem at Available JP - starter case");
                if (mcs.JPGeneralInfo_countTheSumCollectedJP(3000) != 3000)
                    Assert.Fail("Problem at SummCollected JP - starter case");
                if (mcs.JPGeneralInfo_countTheSumSpentJP() != 0)
                    Assert.Fail("Problem at SpentJP, it is not null - sarter case");

                //ADD SAME JP
                mcs.JPManagerWinodw_saveTheNewJPPortion(2000);
                if (mcs.JPGeneralInfo_countTheSumAvailableJP(3000) != 5000)
                    Assert.Fail("Problem at Available JP - JP added 1 case");
                mcs.JPManagerWinodw_saveTheNewJPPortion(1000);
                if (mcs.JPGeneralInfo_countTheSumAvailableJP(3000) != 6000)
                    Assert.Fail("Problem at Available JP - JP added 2 case");

                //REMOVE A JP PORTION
                mcs.JPManagerWindow_removeTheExistingJPPortion(1);
                if (mcs.JPGeneralInfo_countTheSumAvailableJP(3000) != 4000)
                    Assert.Fail("Problem at Available JP - JP removed 1 case");

                //ADD NEW DSCP - DATAS FOR JP ALL PRESENTED AT WINDOW -> IT IS WITH REREADING
                int jpOfNew = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(71, 1, 4, false, 5, false);
                // 150 JP (LVL 1 BENEFICIAL)
                // MODIFIER FROM STAT is 0 (STAT VALUE 55, SCHEMA 50 -> FROM 50-70 IS FREE )
                // TRAIN MODIF 100% -> JP 2x

                if (jpOfNew != 300)
                    Assert.Fail("Wrong JP amont at 71, value: " + jpOfNew);
                if (mcs.DscpNewWindow_areThereAllRequirement(71, 0, 1) == 1)
                {
                    if (mcs.DscpGeneralInfo_isThereEnoughJPToGetLevel(3000, jpOfNew))
                        mcs.DscpNewWindow_GainUpBrandNewDscp(71, 0, 5, "Nehéz az élet, meg kell élni", (short)jpOfNew, true);
                }

                //REVISE JP BALANCE
                if (mcs.JPGeneralInfo_countTheSumAvailableJP(3000) != 3700)
                    Assert.Fail("Problem at Available JP - JP spent to LVL1");

                mcs.DscpGeneralProcess_RemoveSelectedLevel(1, 1, 0);
                mcs.JPManagerWindow_removeTheExistingJPPortion(2);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void Service_ExistingCharacter_AttribeRiseMangementTest()
        {
            try
            {
                ManageCharactService mcs = new ManageCharactService(1);

                //REVISE THE BASIS
                DataTable tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 4)
                    Assert.Fail("Wrong spentElement amount - at begining " + tempSpent.Rows.Count);
                DataTable tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 0)
                    Assert.Fail("Wrong unspentElement amount - at begining " + tempUnspent.Rows.Count);

                //GAIN FIRST DSCP WITH NORMAL LEVEL
                mcs.DscpNewWindow_GainUpBrandNewDscp(31, 0, 2, "komoly munka", 150, true);  //LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(2, 250, true);     //LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(2, 400, true);     //LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(2, 1050, false);   //LVL 4 -> 1pc type 1
                mcs.DscpRiseSpec_GainNormalLevel(2, 1200, false);   //LVL 5 -> 1pc type 1


                //REVISE THE CONTENT OF LISTS
                tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 4)
                    Assert.Fail("Wrong spentElement amount - at LVL 4");
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 2)
                    Assert.Fail("Wrong unspentElement amount - at LVL 4");
                if (tempUnspent.Rows[0]["Sorsz"].ToString() != "2")
                    Assert.Fail("Wrong dscpIndex at unspent LVL 4");
                if (tempUnspent.Rows[0]["Jártasság"].ToString() != "Kovács")
                    Assert.Fail("Wrong dscpName at unspent LVL 4");
                if (tempUnspent.Rows[0]["Tulajdonsága"].ToString() != "Erő")
                    Assert.Fail("Wrong attribName of dscp at unspent LVL 4");
                if (tempUnspent.Rows[0]["Szintje"].ToString() != "4")
                    Assert.Fail("Wrong dscpLevel at unspent LVL 4");
                if (tempUnspent.Rows[0]["Specialitás?"].ToString() != "0")
                    Assert.Fail("Wrong specChategory at unspent LVL 4");
                if (tempUnspent.Rows[0]["Típusa"].ToString() != "4-5-6 sz.")
                    Assert.Fail("Wrong type at unspent LVL 4");

                //REVISE IF THIS IS CHOSEN WHAT COMES AT COMBOBX OF ATTRIB RISE - FINDS OUT WHICH ATTRIB MAY RISEABLE
                List<string> tempAttribChoose = mcs.AttribEnchWindow_collectAttribListToUpgrade(
                        tempUnspent.Rows[0]["Tulajdonsága"].ToString(), "",
                        tempUnspent.Rows[0]["Típusa"].ToString(), ""
                    );

                if (tempAttribChoose.Count != 1 && tempAttribChoose[0] != "Erő")
                    Assert.Fail("Wrong choosable attribName arrived");


                //USE THE ATTRIB UNSPENT POINT -> SPENDING ONLY ONE DSCP IN THAT
                mcs.AttribEnchWindow_saveTheNewAttribEnchancement(
                    tempAttribChoose[0],                        //COMES FROM COMBOBOX OTHER WAY
                    tempUnspent.Rows[0]["Típusa"].ToString(),   //TYPE MUST BE SAME EVEN IF 2 ROW IS SELECTED

                    Convert.ToInt32(tempUnspent.Rows[0]["Sorsz"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Szintje"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Specialitás?"].ToString()),

                    0, 0, 0    //POSSIBLY ANOTHER ROW, HERE EMPTY
                );

                //REVISE THE CONTENT OF LISTS
                tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 5)
                    Assert.Fail("Wrong spentElement amount - at LVL 4");
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 1)
                    Assert.Fail("Wrong unspentElement amount - after spent LVL 4 attribRise");

                //REMOVE THE ATTRIB RISE - REVERT THAT
                int attribEnchInd = Convert.ToInt32(tempSpent.Rows[4]["Sorsz"].ToString());
                int dscpIndex1 = Convert.ToInt32(tempSpent.Rows[4]["Sorsz-1"].ToString());
                bool dscpSpec1 = tempSpent.Rows[4]["Specializáció-1?"].ToString()=="Igen"?true:false;
                int dscpIndex2 = Convert.ToInt32(tempSpent.Rows[4]["Sorsz-2"].ToString());
                bool dscpSpec2 = tempSpent.Rows[4]["Specializáció-1?"].ToString() == "Igen" ? true : false;
                mcs.AttribEncWindow_removeTheAttribEnchancement(dscpIndex1, dscpSpec1,
                    dscpIndex2, dscpSpec2, attribEnchInd);

                tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 4)
                    Assert.Fail("Wrong spentElement amount - at LVL 4 revert");
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 2)
                    Assert.Fail("Wrong unspentElement amount - at LVL 4 revert");

                //GAIN SECOND DSCP
                mcs.DscpNewWindow_GainUpBrandNewDscp(100, 0, 3, "nemtommi", 300, false);    //LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(3, 400, false);    //LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(3, 500, false);    //LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(3, 600, false);    //LVL 4 -> 1pc type 1

                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                tempAttribChoose = mcs.AttribEnchWindow_collectAttribListToUpgrade(
                        tempUnspent.Rows[0]["Tulajdonsága"].ToString(),
                        tempUnspent.Rows[2]["Tulajdonsága"].ToString(),
                        tempUnspent.Rows[0]["Típusa"].ToString(),
                        tempUnspent.Rows[2]["Típusa"].ToString()
                    );

                if (tempAttribChoose.Count != 3)
                    Assert.Fail("Wrong attribNames at overlapping attempt " + tempAttribChoose.Count);

                //ADD NEW OVERLAPPING ONE - DSCP1 LVL 4 + DSCP2 LVL 4
                mcs.AttribEnchWindow_saveTheNewAttribEnchancement(
                    tempAttribChoose[0],

                    tempUnspent.Rows[0]["Típusa"].ToString(),
                    Convert.ToInt32(tempUnspent.Rows[0]["Sorsz"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Szintje"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Specialitás?"].ToString()),

                    Convert.ToInt32(tempUnspent.Rows[2]["Sorsz"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[2]["Szintje"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[2]["Specialitás?"].ToString())
                    );

                tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 5)
                    Assert.Fail("It cannot see the overlapping attrib ench!");
                attribEnchInd = Convert.ToInt32(tempSpent.Rows[4]["Sorsz"].ToString());
                dscpIndex1 = Convert.ToInt32(tempSpent.Rows[4]["Sorsz-1"].ToString());
                dscpIndex2 = Convert.ToInt32(tempSpent.Rows[4]["Sorsz-2"].ToString());
                if (dscpIndex1 != 31 && dscpIndex2 != 100 && attribEnchInd != 5)
                    Assert.Fail("Wrong attrib ech is overlapped");

                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 1)
                    Assert.Fail("It cannot see the only one attribPoint from dscp1");

                //REMOVE THE DSCP 1
                mcs.DscpGeneralProcess_RemoveSelectedLevel(2, 1, 0);

                tempSpent = mcs.AttribEnchWindow_collectTheCharSpentAttribEnchances();
                if (tempSpent.Rows.Count != 4)
                    Assert.Fail("It still sees the overlapping attrib ench, not removed!");
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 1)
                    Assert.Fail("It cannot see the only one attribPoint from dscp2 - that freed");

                //ADDING A THIRD DSCP - WITH SPEC
                mcs.DscpNewWindow_GainUpBrandNewDscp(31, 0, 2, "komoly munka ujra", 150, true);  //LVL 1
                mcs.DscpRiseSpec_GainNewSpecialisation(4, 400, 1, "Páncélok", "spec for test2");
                mcs.DscpRiseSpec_GainNewSpecialisation(4, 900, 1, "Páncélok", "spec for test2");    //LVL 2 -> 1pc TYPE 1

                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 2)
                    Assert.Fail("It cannot see the specUnspent attribs");

                //ATTIB ENCH WITH NORMAL AND SPEC LEVELS TYPE 1
                mcs.AttribEnchWindow_saveTheNewAttribEnchancement(
                    "Erő",
                    tempUnspent.Rows[0]["Típusa"].ToString(),
                    Convert.ToInt32(tempUnspent.Rows[0]["Sorsz"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Szintje"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[0]["Specialitás?"].ToString()),

                    Convert.ToInt32(tempUnspent.Rows[1]["Sorsz"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[1]["Szintje"].ToString()),
                    Convert.ToInt32(tempUnspent.Rows[1]["Specialitás?"].ToString())
                );

                //DELET SPEC -> NEEDED TO REMOVE THE ATTRIB-ENCH AS WELL
                mcs.DscpGeneralProcess_RemoveSelectedLevel(4, 1, 1);
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 1)
                    Assert.Fail("It cannot see the specUnspent attribs after removed the spec");

                //REMOVE THE ONLY LEVEL THAT GIVES ATTRIB-ENCHANCE
                mcs.DscpGeneralProcess_RemoveSelectedLevel(3, 4, 0);
                tempUnspent = mcs.AttribEnchWindow_collectTheCharUnspentAttribEnchances();
                if (tempUnspent.Rows.Count != 0)
                    Assert.Fail("It cannot see the specUnspent attribs after removed the LVL 4 -> no source");

                mcs.DscpGeneralProcess_RemoveSelectedLevel(3, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(4, 1, 0);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void Service_ExistingCharacter_DscpRequirementAnalyzeNonSevereTest()
        {
            try
            {
                //95 NON SEVERE (2) <-119 (1)
                ManageCharactService mcs = new ManageCharactService(2);

                //ANALYZE NON-SEVERE DSCP_2 (95) IS PERMITTED TO GAIN - NEGATIVE CONTROLS
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 1) != 2)
                    Assert.Fail("1 - It sees the requirements exist, but none there");
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 4) != 2)
                    Assert.Fail("2- It sees the requirenemts exist, but none there");

                //DSCP_1 - REQUIREMENT OF DSCP_2 AND DSCP_3
                //GAIN THE DSCP 2 REQUIREMNET
                mcs.DscpNewWindow_GainUpBrandNewDscp(119, 0, 8, "al-al előfeltétel 1", 300, true);//DSCP 1 LVL 1

                //ANALYZE THE REQUIREMENT AGAIN - POSITIVE CONTROL
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 2) != 1)
                    Assert.Fail("3 - It cant see requirement exists 119 LVL 1");

                //GAIN THE NEEDED DSCP_2 (95)
                //DSCP_2 - NON SEVCVERE REQUIR
                mcs.DscpNewWindow_GainUpBrandNewDscp(95, 1, 10, "gyengen fuggo", 300, true); //DSCP 2 LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(2, 540, true);  //DSCP 2 LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(2, 700, true);  //DSCP 2 LVL 3

                //ANALYZE THE REQUIREMENT AGAIN - NEGATIVE CONTROL
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 4) != 2)
                    Assert.Fail("4 - It sees there is 119 LVL 2, but no there");
                mcs.DscpRiseSpec_GainNormalLevel(1, 400, false);  //DSCP 1 LVL 2

                //ANALYZE THE REQUIREMENT AGAIN - POSITIVE CONTROL
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 4) != 1)
                    Assert.Fail("5 - It cannot see ther is 119 LVL 2");

                //GAIN SOME DSCP_2
                mcs.DscpRiseSpec_GainNormalLevel(2, 910, true);  //DSCP 2 LVL 4
                mcs.DscpRiseSpec_GainNormalLevel(2, 1120, true);  //DSCP 2 LVL 5
                mcs.DscpRiseSpec_GainNormalLevel(2, 1410, true);  //DSCP 2 LVL 6

                //AT THRASHOLD REANALYZE - NEGATIVE CONTROL
                if (mcs.DscpNewWindow_areThereAllRequirement(95, 1, 7) != 2)
                    Assert.Fail("6 - It sees there is 119 LVL 3, but no there");


                mcs.DscpGeneralProcess_RemoveSelectedLevel(1, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(2, 1, 0);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void Service_ExistingCharacter_DscpRequirementAnalyzeSevereTest()
        {
            try
            {
                //98 (5) <- 76 (2)  <-119 (1)
                //98 (5) <- 129 (4) <-143 (3)

                ManageCharactService mcs = new ManageCharactService(2);

                if (mcs.DscpNewWindow_areThereAllRequirement(76, 1, 2) != 2)
                    Assert.Fail("0 - It cannot see there is no the requriement of 76 LVL 1");

                //GAIN THE DSCP 1->INDEX 1
                mcs.DscpNewWindow_GainUpBrandNewDscp(119, 0, 8, "al-al előfeltétel 1", 300, true);//DSCP 1 LVL 1

                //ANALYZE THE REQUIREMENT AGAIN - POSITIVE CONTROL
                if (mcs.DscpNewWindow_areThereAllRequirement(76, 1, 1) != 1)
                    Assert.Fail("1 - It cannot see there are the proper requirements to gain 76 LVL 1!");

                //DSCP_3 REQUIREMENT OF DSCP_2->INDEX 2
                mcs.DscpNewWindow_GainUpBrandNewDscp(76, 1, 7, "elofeltetl 1", 400, true);  //DSCP 2 LVL 1

                if (mcs.DscpNewWindow_areThereAllRequirement(76, 1, 2) != 2)
                    Assert.Fail("2 - It cannot see there is no the requriement of 76 LVL 2");
                mcs.DscpRiseSpec_GainNormalLevel(1, 500, true);  //DSCP 1 LVL 2

                if (mcs.DscpNewWindow_areThereAllRequirement(76, 1, 2) != 1)
                    Assert.Fail("3 - It cannot see there is already the requirement of 76 LVL 2");


                //GAIN SOME DSCP_1 AMD DSCP_2
                mcs.DscpRiseSpec_GainNormalLevel(1, 610, true);  //DSCP 1 LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(1, 800, true);  //DSCP 1 LVL 4
                mcs.DscpRiseSpec_GainNormalLevel(1, 1070, true);  //DSCP 1 LVL 5
                mcs.DscpRiseSpec_GainNormalLevel(1, 1270, true);  //DSCP 1 LVL 6

                mcs.DscpRiseSpec_GainNormalLevel(2, 550, true);  //DSCP 2 LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(2, 600, true);  //DSCP 2 LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(2, 800, true);  //DSCP 2 LVL 4
                mcs.DscpRiseSpec_GainNormalLevel(2, 1100, true);  //DSCP 2 LVL 5
                mcs.DscpRiseSpec_GainNormalLevel(2, 1400, true);  //DSCP 2 LVL 6

                //GAIN DSCP_3->3 REQUIREMENT OF DSCP_4
                mcs.DscpNewWindow_GainUpBrandNewDscp(143, 0, 8, "al-al előfeltétel 2", 300, true);   //DSCP 3 LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(3, 400, false);  //DSCP 3 LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(3, 500, true);  //DSCP 3 LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(3, 600, true);  //DSCP 3 LVL 4
                mcs.DscpRiseSpec_GainNormalLevel(3, 860, true);  //DSCP 3 LVL 5
                mcs.DscpRiseSpec_GainNormalLevel(3, 1040, true);  //DSCP 3 LVL 6

                //GAIN DSCP_4->4 
                mcs.DscpNewWindow_GainUpBrandNewDscp(129, 1, 8, "előfeltétel 2", 400, true);   //DSCP 4 LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(4, 540, true);  //DSCP 4 LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(4, 770, true);  //DSCP 4 LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(4, 940, true);  //DSCP 4 LVL 4

                //DSCP_5->5 - SEVERE REQUIREMENT WITH SPEC
                mcs.DscpNewWindow_GainUpBrandNewDscp(89, 1, 0, "a fo jartassag", 500, true);   //DSCP 5 LVL 1
                mcs.DscpRiseSpec_GainNormalLevel(5, 540, true);  //DSCP 5 LVL 2
                mcs.DscpRiseSpec_GainNormalLevel(5, 700, true);  //DSCP 5 LVL 3
                mcs.DscpRiseSpec_GainNormalLevel(5, 910, true);  //DSCP 5 LVL 4

                if (mcs.DscpNewWindow_areThereAllRequirement(89, 1, 5) != 2)
                    Assert.Fail("4 - It sees there are alredy the all requir of 89 LVL 5");
                mcs.DscpRiseSpec_GainNormalLevel(4, 1140, true);  //DSCP 4 LVL 5
                if (mcs.DscpNewWindow_areThereAllRequirement(89, 1, 5) != 1)
                    Assert.Fail("5 - Is cannot see there are already all requir of 89 LVL 5");

                //START TO TEST THE SPEC REQUIREMENT ANALYZE
                //THE 3 CODE IS REMOVED FROM METHOD

                mcs.DscpRiseSpec_GainNormalLevel(5, 1120, true);  //DSCP 5 LVL 5
                mcs.DscpRiseSpec_GainNormalLevel(4, 1340, true);  //DSCP 4 LVL 6
                mcs.DscpRiseSpec_GainNormalLevel(5, 1410, true);  //DSCP 5 LVL 6

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(4, 2, 1) != 2)
                    Assert.Fail("8 - It  cannot see the 89 has no spec LVL1 - not possible gain LVL 2!");

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(4, 1, 1) != 1) //REQUIRES THE DSCP_3 LVL 5 AND DSCP_4 LVL6
                    Assert.Fail("8 - It cannot see the 89 is ready to specialize!");    //SPEC INDEX 1

                //GAIN SPEC LVL 1
                mcs.DscpRiseSpec_GainNewSpecialisation(4, 3000, 1, "Adott terület", "spec1"); //DSCP 4 SPEC LVL 1

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(4, 2, 1) != 1)
                    Assert.Fail("10 - It  cannot see the 89 is ready to has spec LVL2!");

                mcs.DscpRiseSpec_GainNewSpecialisation(4, 3600, 1, "Egy varázslattípusra", "spec1"); //DSCP 4 SPEC LVL 2

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(4, 3, 1) != 3)
                    Assert.Fail("11 - It  cannot see the 89 is already reach max spec!");

                mcs.DscpGeneralProcess_RemoveSelectedLevel(1, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(2, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(3, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(4, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(5, 1, 0);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void Service_ExistingCharacter_DscpSpecDetailedRequirementTest()
        {
            try
            {
                //dscp 8 spec requirGr 1 or 2 or 3 -> ATTRIBUTE (1) REQUIReMENT
                //ATTRIB 3-4-5; REQUIR 0

                //dscp 66 spec requirGr 1 -> DSCP TYPE (2) REQUIREMENT
                //REQUIR 66 AND 7 TYPE

                //dscp 90 spec requirGr 1 or 3 -> DSCP TYPE (2) REQUIREMENT
                //REQUIR 119 AND 3 / 7 TYPE

                ManageCharactService mcs = new ManageCharactService(2);

                //TEST FOR SPEC REQUIREMENT - ONLY ATTRIB POSITIVE CASE
                mcs.DscpNewWindow_GainUpBrandNewDscp(8, 0, 3, "first", 300, true);
                mcs.DscpRiseSpec_GainNormalLevel(1, 350, true);
                mcs.DscpRiseSpec_GainNormalLevel(1, 460, true);
                mcs.DscpRiseSpec_GainNormalLevel(1, 580, true);
                mcs.DscpRiseSpec_GainNormalLevel(1, 780, true); //LVL5
                
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(1, 1, 1) != 1)
                    Assert.Fail("2-It sees the dscp8 is NOT ready to spec LVL 1");
                
                mcs.DscpRiseSpec_GainNewSpecialisation(1, 600, 1, "Akadályfutás", "valami");
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(1, 2, 1) != 1)
                    Assert.Fail("3-It sees the dscp8 is NOT ready to spec LVL 2");
                mcs.DscpRiseSpec_GainNewSpecLevel(1, 800, 1);

                //TEST FOR SPEC REQUIREMENT - ONLY ATTRIB POSITIVE CASE
                //PROBE THE 3.REQUIR CONFIG - Efficiency only 33
                
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(1, 1, 3) != 2)
                    Assert.Fail("4-It sees the attrib4 requir is reach the min!");
                    
                
                //TEST FOR SPECQEWUIREMENT - DSCP-TYPE NEED CASE
                mcs.DscpNewWindow_GainUpBrandNewDscp(66, 1, 8, "sec", 340, false);
                mcs.DscpRiseSpec_GainNormalLevel(2, 340, true);
                mcs.DscpRiseSpec_GainNormalLevel(2, 340, true);
                mcs.DscpRiseSpec_GainNormalLevel(2, 340, true);
                mcs.DscpRiseSpec_GainNormalLevel(2, 340, true); //LVL 5
                
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreNormalLvl(2, 1) != 2)
                    Assert.Fail("5-It cannot see the lack of TYPE 7 DSCP");
                    
                mcs.DscpNewWindow_GainUpBrandNewDscp(127, 0, 8, "requirement", 400, false); //TYPE 7 DSCP FOR SPECIALIZATION OF DSCP 2
                mcs.DscpRiseSpec_GainNormalLevel(3, 500, false);
                mcs.DscpRiseSpec_GainNormalLevel(3, 600, false);    //LVL 3

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(2, 1, 1) != 2)
                    Assert.Fail("6-It cannot see the lack of TYPE 7 DSCP LVL 4 TO SPECIALIZE DSCP 2");

                mcs.DscpRiseSpec_GainNormalLevel(3, 770, false); //LVL 4

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(2, 1, 1) != 1)
                    Assert.Fail("7-It cannot see there is the TYPE 7 DSCP LVL 4 TO SPECIALIZE DSCP 2");

                mcs.DscpRiseSpec_GainNewSpecialisation(2, 800, 1, "Egy tényező és típus alapján", "that"); //SPEC LVL 1
                
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(2, 2, 1) != 2)
                    Assert.Fail("8-It cannot see the lack of TYPE 7 DSCP LVL 5 TO SPECIALUTE DSCP 2 TO SPEC_LVL 2");
                    
                mcs.DscpRiseSpec_GainNormalLevel(3, 870, false); //DSCP 3 LVL 5
                
                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(2, 2, 1) != 1)
                    Assert.Fail("9-It cannot see the there isTYPE 7 DSCP LVL 5 TO SPECIALIZE DSCP 2 TO SPEC LVL 2");
                mcs.DscpRiseSpec_GainNewSpecLevel(2, 1200, 1); //SPEC LVL 2

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainMoreSpecLvl(2, 3, 1) != 3)
                    Assert.Fail("10-It cannot see this spec reached the LVL max");

                //TEST FOR SPECREQUIREMENT - STRICT DSCP AND DSCP TYPE NEEDED AS WELL
                mcs.DscpNewWindow_GainUpBrandNewDscp(90, 1, 7, "third", 240, true);
                mcs.DscpRiseSpec_GainNormalLevel(4, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(4, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(4, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(4, 400, true);

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(4, 1, 1) != 2)
                    Assert.Fail("11-It cannot see there is no requir of the spec - none dscp and dscp type2");

                mcs.DscpNewWindow_GainUpBrandNewDscp(119, 0, 3, "requir of 90", 340, true);
                mcs.DscpRiseSpec_GainNormalLevel(5, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(5, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(5, 400, true);
                mcs.DscpRiseSpec_GainNormalLevel(5, 400, true); //LVL 5

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(4, 1, 1) != 2)
                    Assert.Fail("12-It cannot see there is no requir of the spec - dscp type2");

                mcs.DscpNewWindow_GainUpBrandNewDscp(37, 0, 4, "requir type2", 450, false);
                mcs.DscpRiseSpec_GainNormalLevel(6, 500, true);
                mcs.DscpRiseSpec_GainNormalLevel(6, 500, true);
                mcs.DscpRiseSpec_GainNormalLevel(6, 500, true);
                mcs.DscpRiseSpec_GainNormalLevel(6, 500, true); //LVL5

                if (mcs.DscpRiseSpec_areThereAllRequirement_GainBrandNewSpec(4, 1, 1) != 1)
                    Assert.Fail("13-It cannot see there are all requir of the spec");


                mcs.DscpGeneralProcess_RemoveSelectedLevel(1, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(2, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(3, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(4, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(5, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(6, 1, 0);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void Service_ExistingCharacter_DscpJPCountTest()
        {
            try
            {
                ManageCharactService mcs = new ManageCharactService(2);

                //dscp 88, NO TRAINING PENALTY; NO ATTRIBUTE-VALUE PENALTY; BUT BENEFICIAL -> LVL 1 = 950 BASIC
                int res1 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(80, 5, 5, false, 10, true);
                if (res1 != 950)
                    Assert.Fail("1-No correct valuse " + res1);

                //dscp 31, NO TRAINING PENALTY, 100% ATTRIBUTE-VALUE PENALTY; NOT BENEFICIAL -> LVL 7 = 1950 BASIC
                int res2 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(42, 7, 5, false, 2, true);
                if (res2 != 3900)
                    Assert.Fail("2-No correct valuse " + res2);

                //dscp 149, 100% TRAINING PENALTY, -15% ATTRIBUTE-VALUE BENEFIT; NOT BENEFICIAL -> LVL 3 = 550
                int res3 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(149, 3, 5, false, 7, false);
                if (res3 != 1017)
                    Assert.Fail("3-No correct valuse " + res3);

                //dscp 3, NO TRAINING PENALTY, -30% ATTRIBUTE-VALUE BENEFIT; NOT BENEFICIAL -> LVL 4 = 900
                int res4 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(74, 4, 8, false, 7, true);
                if (res4 != 630)
                    Assert.Fail("4-No correct valuse " + res4);

                //dscp 72 spec LVL 1; NO AFFECT OF TRAINING AT ALL; 15% ATTRIBUTE-VALUE PENALTY; BUT BENEFICIAL -> LVL 1 = 500
                int res5 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(38, 1, 3, true, 4, false);
                if (res5 != 575)
                    Assert.Fail("5-No correct valuse " + res5);

                //dscp 72 spec LVL 2; NO AFFECT OF TRAINING AT ALL; 30% ATTRIBUTE-VALUE PENALTY; NOT BENEFICIAL -> LVL 1 = 900
                int res6 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(136, 2, 3, true, 8, false);
                if (res6 != 1170)
                    Assert.Fail("6-No correct valuse " + res6);

                //dscp 72 spec LVL 2; NO AFFECT OF TRAINING AT ALL; NO AFFECT ATTRIBUTE-VALUE; BUT BENEFICIAL -> LVL 1 = 750
                int res7 = mcs.DscpNewWindow_calculateTheFinalJPForThisDscp(127, 2, 7, true, 8, false);
                if (res7 != 750)
                    Assert.Fail("7-No correct valuse " + res7);

                //BASIC JP-VALUES PRESENTER
                int res8 = mcs.DscpNewWindow_getTheBasicJPForThisDscp(3, 1, 4, true);
                if (res8 != 500)
                    Assert.Fail("8-No correct value " + res8);
                int res9 = mcs.DscpNewWindow_getTheBasicJPForThisDscp(112, 6, 3, false);
                if (res9 != 1550)
                    Assert.Fail("9-No correct value " + res9);
                int res10 = mcs.DscpNewWindow_getTheBasicJPForThisDscp(31, 8, 3, false);
                if (res10 != 2000)
                    Assert.Fail("10-No correct value " + res10);

                //TRAINING VALUE PRESENTER
                //NOT PERMITTED TRAINING
                int res11 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(119, 2, 7, true, true);   //MENTOR NEEDED
                if (res11 != 0)
                    Assert.Fail("11-No correct value " + res11);
                //PERMITTED TRAINING
                int res12 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(119, 4, 7, false, true);  //SPEC, NO VALUE
                if (res12 != 0)
                    Assert.Fail("12-No correct value " + res12);
                int res13 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(65, 2, 3, true, false);  //SPEC, NO VALUE
                if (res13 != 0)
                    Assert.Fail("13-No correct value " + res13);
                int res14 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(109, 2, 7, false, false);  //BASE VALUE 250; 100% PENALTY
                if (res14 != 250)
                    Assert.Fail("14-No correct value " + res14);
                int res15 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(59, 6, 3, false, false);  //BASE VALUE 1550; 50% PENALTY
                if (res15 != 775)
                    Assert.Fail("15-No correct value " + res15);
                int res16 = mcs.DscpNewWindow_getTheTraningModifJp_ValueForThisDscpToShow(59, 3, 3, false, true);  //BASE VALUE 550; 0% PENALTY
                if (res16 != 0)
                    Assert.Fail("16-No correct value " + res16);

                //ATTRIB MODIFIER VALUE PRESENTER
                int res17 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(82, 3, 5, false, 7);    //BASE VALUE 400; -30% MODIF
                if (res17 != -120)
                    Assert.Fail("17-No correct value " + res17);
                int res18 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(98, 10, 3, false, 9);    //BASE VALUE 3500; 100% MODIF
                if (res18 != 3500)
                    Assert.Fail("18-No correct value " + res18);
                int res19 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(116, 1, 7, true, 8);    //BASE VALUE 500; 30% MODIF
                if (res19 != 150)
                    Assert.Fail("19-No correct value " + res19);
                int res20 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(146, 2, 3, true, 8);    //BASE VALUE 900; 15% MODIF
                if (res20 != 135)
                    Assert.Fail("20-No correct value " + res20);
                int res21 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(56, 8, 3, false, 5);    //BASE VALUE 2400; 0 MODIF
                if (res21 != 0)
                    Assert.Fail("21-No correct value " + res21);

                int res22 = mcs.DscpNewWindow_getTheAttribSchemaModifJP_ValueForThisDscpToShow(140, 5, 8, false, 7);    //BASE VALUE 950; -15% MODIF
                if (res22 != -142)
                    Assert.Fail("22-No correct value " + res22);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void Service_ExistingCharacter_DscpRequirementTextsConvertingsTest()
        {
            try
            {
                ManageCharactService mcs = new ManageCharactService(2);
                
                //DSCP NORMAL REQUIREMENTS
                List<string> resRequirColl = mcs.DscpNewWindow_collectDscpRequir(120);  //IT HAS 2 PC ONLY DSCP REQUIR
                if (mcs.DscpNewWindow_findTheDscpChosenRequirGroupId(120, resRequirColl[0]) != 1)
                    Assert.Fail("1-Wrong requir Id");
                if (mcs.DscpNewWindow_findTheDscpChosenRequirGroupId(120, resRequirColl[1]) != 2)
                    Assert.Fail("2-Wrong requir Id");
                
                resRequirColl = mcs.DscpNewWindow_collectDscpRequir(159);   //IT HAS 2 PC TWIN DSCP REQUIR
                if (mcs.DscpNewWindow_findTheDscpChosenRequirGroupId(159, resRequirColl[0]) != 1)
                    Assert.Fail("3-Wrong requir Id");
                if (mcs.DscpNewWindow_findTheDscpChosenRequirGroupId(159, resRequirColl[1]) != 2)
                    Assert.Fail("4-Wrong requir Id");

                resRequirColl = mcs.DscpNewWindow_collectDscpRequir(48);    //IT HAS 1 PC ONLY DSCP REQUIR
                if (mcs.DscpNewWindow_findTheDscpChosenRequirGroupId(48, resRequirColl[0]) != 1)
                    Assert.Fail("5-Wrong requir Id");
                
                //DSCP SPEC REQIREMENTS - ATTRIB BASED
                mcs.DscpNewWindow_GainUpBrandNewDscp(8, 0, 3, "newOne", 400, false);  //IT HAS ATTRIB SPEC REQUIRS
                string[] resSpecRequirColl = mcs.DscpRiseSpec_getTheSelectedSpecPossibleRequirs(1);
                
                if (resSpecRequirColl.Length != 3)
                    Assert.Fail("6-Wrong amount requirSpecs");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(1, resSpecRequirColl[0]) != 1)
                    Assert.Fail("7-Wrong requirSpecId");
                    
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(1, resSpecRequirColl[1]) != 2)
                    Assert.Fail("8-Wrong requirSpecId");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(1, resSpecRequirColl[2]) != 3)
                    Assert.Fail("9-Wrong requirSpecId");
                
                //DSCP SPEC ERQUIREMENTS - ONIQUE DSCP BASED
                mcs.DscpNewWindow_GainUpBrandNewDscp(5, 0, 4, "newTwo", 500, false);
                resSpecRequirColl = mcs.DscpRiseSpec_getTheSelectedSpecPossibleRequirs(2);

                if (resSpecRequirColl.Length != 1)
                    Assert.Fail("10-Wrong amount of requirSpecId");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(2, resSpecRequirColl[0]) != 1)
                    Assert.Fail("11-Wrong requirSpec arrived");
                    
                //DSCP SPEC REQUIREMENTS - DSCP TYPE AND UNIQUE DSCP BASED
                mcs.DscpNewWindow_GainUpBrandNewDscp(90, 0, 4, "newThird", 600, false);
                resSpecRequirColl = mcs.DscpRiseSpec_getTheSelectedSpecPossibleRequirs(3);
                //throw new Exception("Here\n" + resSpecRequirColl[0] + "\n" + resSpecRequirColl[1] + "\n" + resSpecRequirColl[2]);
                if (resSpecRequirColl.Length != 3)
                    Assert.Fail("12-Wrong amount requirSpec arrived");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(3, resSpecRequirColl[0]) != 1)
                    Assert.Fail("13-Wrong requirSpec arrived");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(3, resSpecRequirColl[1]) != 2)
                    Assert.Fail("14-Wrong requirSpec arrived");
                if (mcs.DscpRiseSpec_getBackTheSelectedSpecRequirementGroupId(3, resSpecRequirColl[2]) != 3)
                    Assert.Fail("15-Wrong requirSpec arrived");
                mcs.DscpGeneralProcess_RemoveSelectedLevel(1, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(2, 1, 0);
                mcs.DscpGeneralProcess_RemoveSelectedLevel(3, 1, 0);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
    }
}