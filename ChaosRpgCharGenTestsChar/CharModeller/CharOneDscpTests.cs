using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel.Tests
{
    [TestClass()]
    public class CharOneDscpTests
    {
        [TestMethod()]
        public void gainNewAndRemoveLevelTest()
        {
            try
            {
                //ADDING SINGLE DSCP -> TRY OVERRISING ITS LEVEL -> REMOVE ALL
                CharOneDscp dscp = new CharOneDscp(3, 3, 13, 1, 4, 1, "valami");
                byte resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 0)
                    Assert.Fail("Wrong levelMark arrived before adding" + resLevel);
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer arrived, despite no spec, it sees!");

                //ADDING LEVELS
                dscp.gainNewLevel(3, true, 400);
                resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 1)
                    Assert.Fail("Wrong levelMark arrived after adding " + resLevel);
                if (dscp.getTheNormalLayers().Count > 1)
                    Assert.Fail("Wrong level amount in list after first adding");
                dscp.gainNewLevel(3, true, 400);//2.lev
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);//5.lev
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);
                dscp.gainNewLevel(3, true, 400);//10.lev
                resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 10)
                    Assert.Fail("Wrong levelMark arrived after 10. adding " + resLevel);

                //TRY OVERSTEP THE 10th TRASHIOLD
                dscp.gainNewLevel(3, true, 400);//11.lev - NOT PERMITTED
                if (dscp.getTheNormalLayers().Count > 10)
                    Assert.Fail("Wrong level amount in list after 11. adding");
                resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 10)
                    Assert.Fail("Wrong levelMark arrived after 11. adding " + resLevel);

                //REMOEVE SOME LEVELS
                dscp.removeTheseLevels(3, 5);
                resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 4)
                    Assert.Fail("Wrong levelMark arrived after removing 6 level " + resLevel);
                if (dscp.getTheNormalLayers().Count > 4)
                    Assert.Fail("Wrong level amount in list after removing all level ");

                //REMOVE ALL OTHER LEVELS
                dscp.removeTheseLevels(3, 1);
                resLevel = dscp.getTheHighestLevelMark();
                if (resLevel != 0)
                    Assert.Fail("Wrong levelMark arrived after removing 6 level " + resLevel);
                if (dscp.getTheNormalLayers().Count > 0)
                    Assert.Fail("Wrong level amount in list after removing all level ");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void gainNewSpecAndRemoveThemLevelTest1()
        {
            try
            {
                //TEST OF A SINGLE SPEC -> TRY OVERRISING ITS LEVEL -> REMOVE ALL
                CharOneDscp dscp = new CharOneDscp(3, 3, 12, 1, 4, 1, "valami");

                //REVISION OF INDICATORS
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("1-Wrong answer at spec existence despite none there");
                byte levelMark = dscp.getTheHighestSpecLevelMark(1, 1, "valami izé");
                if (levelMark != 0)
                    Assert.Fail("2-Wrong level mark before add new speclevel " + levelMark);

                //FIRST LEVEL
                dscp.gainNewSpecLevel(3, 1110, 1, 1, "valami izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("3-Wrong answer at spec existence despite there is one level");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(1);
                if (levelMark != 1)
                    Assert.Fail("4-Wrong level mark after add one speclevel " + levelMark);
                if (!dscp.isThereAlreadyThisSpecType(1, 1, "valami izé"))
                    Assert.Fail("5-It cant find this specific spec");

                //SECOND LEVEL
                dscp.gainNewSpecLevel(3, 1110, 1, 1, "valami izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("6-Wrong answer at spec existence despite there is two level");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(1);
                if (levelMark != 2)
                    Assert.Fail("7-Wrong level mark after add two speclevel " + levelMark);

                //TRY THIRD LEVEL ADDING
                dscp.gainNewSpecLevel(3, 3111, 1, 1, "valami izé");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(1);
                if (levelMark != 2)
                    Assert.Fail("8-Wrong level mark after add third times speclevel " + levelMark);

                //REMOVE ALL
                dscp.removeTheseSpecLevels(3, 1, 1);
                if (dscp.getTheHighestSpecLevelMark(1, 1, "valami izé") != 0)
                    Assert.Fail("9-Wrong level mark after renove all speclevel at this type");
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("10-Wrong answer at spec existence despite already none there");

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void gainNewSpecAndRemoveThemLevelTest2()
        {
            try
            {
                //TEST OF SEVERAL SPEC AT SAME DSCP -> ADD 2nd LEVEL, THAN REMOVE EACH ONE-BY-ONE
                CharOneDscp dscp = new CharOneDscp(3, 4, 20, 1, 3, 0, "valami");

                //REVISION OF INDICATIORS
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite none there");
                byte levelMark = dscp.getTheHighestSpecLevelMark(1, 1, "valami izé");
                if (levelMark != 0)
                    Assert.Fail("Wrong level mark before add new speclevel");

                //ADD FIRST SPEC - LVL 1
                dscp.gainNewSpecLevel(3, 1110, 1, 1, "valami izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite there is one level");
                if (!dscp.isThereAlreadyThisSpecType(1, 1, "valami izé"))
                    Assert.Fail("It cant find first spec");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(1);
                if (levelMark != 1)
                    Assert.Fail("Wrong level mark after add first spec " + levelMark);

                //ADD SECOND SPEC - LVL 1
                dscp.gainNewSpecLevel(3, 2001, 1, 1, "még vm izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite there is two level");
                if (!dscp.isThereAlreadyThisSpecType(1, 1, "valami izé"))
                    Assert.Fail("It cant find first spec");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(2);
                if (levelMark != 1)
                    Assert.Fail("Wrong level mark after add second spec " + levelMark);

                //ADD 2nd LEVEL OF SECOND SPEC
                dscp.gainNewSpecLevel(3, 2111, 1, 1, "még vm izé");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(2);
                if (levelMark != 2)
                    Assert.Fail("Wrong level mark after add 2nd times speclevel SECOND " + levelMark);

                //REMOVE THE 2nd LEVEL OF SECOND SPEC
                dscp.removeTheseSpecLevels(3, 2, 2);
                if (dscp.getTheNextSpecLevelMarkBySpecInxed(2) != 1)
                    Assert.Fail("Wrong level mark after remove 2nd speclevel at SECOND");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite still there are two lvl 1 there");

                //REMOVE THE 1st LEVEL OF FIRST SPEC
                dscp.removeTheseSpecLevels(3, 1, 1);
                if (dscp.getTheHighestSpecLevelMark(1, 1, "valami izé") != 0)
                    Assert.Fail("Wrong level mark after remove 1nd speclevel at FIRST");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite still there are two lvl 1 there");

                //REMOVE THE 1st LEVEL OF SECOND SPEC
                dscp.removeTheseSpecLevels(3, 2, 1);
                if (dscp.getTheHighestSpecLevelMark(2, 1, "még vm izé") != 0)
                    Assert.Fail("Wrong level mark after remove 1nd speclevel at FIRST");
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite none spec supposed to be there");

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void gainNewSpecAndRemoveThemLevelTest3()
        {
            try
            {
                //TEST OF SEVERAL SPEC AT SAME DSCP -> ADD 2nd LEVEL, THAN REMOVE EACH ONE-BY-ONE
                CharOneDscp dscp = new CharOneDscp(3, 4, 20, 1, 3, 0, "valami");

                //REVISION OF INDICATIORS
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite none there");
                byte levelMark = dscp.getTheHighestSpecLevelMark(1, 1, "valami izé");
                if (levelMark != 0)
                    Assert.Fail("Wrong level mark before add new speclevel");

                //ADD FIRST SPEC - LVL 1
                dscp.gainNewSpecLevel(3, 1110, 1, 1, "valami izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite there is one level");
                if (!dscp.isThereAlreadyThisSpecType(1, 1, "valami izé"))
                    Assert.Fail("It cant find first spec");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(1);
                if (levelMark != 1)
                    Assert.Fail("Wrong level mark after add first spec " + levelMark);

                //ADD SECOND SPEC - LVL 1
                dscp.gainNewSpecLevel(3, 2001, 1, 1, "még vm izé");
                if (!dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite there is two level");
                if (!dscp.isThereAlreadyThisSpecType(1, 1, "valami izé"))
                    Assert.Fail("It cant find first spec");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(2);
                if (levelMark != 1)
                    Assert.Fail("Wrong level mark after add second spec " + levelMark);

                //ADD 2nd LEVEL OF SECOND SPEC
                dscp.gainNewSpecLevel(3, 2111, 1, 1, "még vm izé");
                levelMark = dscp.getTheNextSpecLevelMarkBySpecInxed(2);
                if (levelMark != 2)
                    Assert.Fail("Wrong level mark after add 2nd times speclevel SECOND " + levelMark);

                dscp.removeAllSpecLevels(3);

                if (dscp.getTheHighestSpecLevelMark(1, 1, "valami izé") != 0)
                    Assert.Fail("Wrong level mark after remove 1nd speclevel at FIRST");
                if (dscp.getTheHighestSpecLevelMark(2, 1, "még vm izé") != 0)
                    Assert.Fail("Wrong level mark after remove 1nd speclevel at FIRST");
                if (dscp.isThereSpecAtAll())
                    Assert.Fail("Wrong answer at spec existence despite none spec supposed to be there");

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void sumFullJPAndAttribRise_ThisDscpTest()
        {
            try
            {
                CharOneDscp dscp = new CharOneDscp(3, 3, 54, 3, 3, 0, "test");
                //ADDING FEW LEVEL
                dscp.gainNewLevel(3, false, 200);    //1
                dscp.gainNewLevel(3, true, 400);     //2
                dscp.gainNewLevel(3, false, 600);    //3
                dscp.gainNewSpecLevel(3, 400, 1, 1, "something"); //sp1

                //REVISE THE JP COUNTING
                int jpAmount = dscp.sumFullJP_ThisDscp();
                if (jpAmount != 1600)
                    Assert.Fail("Wrong JP amount arrived at 3+1");

                //REVISE ATTRIB ENCHANCMENT - ONE GIVES A POINT
                if (dscp.isThereUnspentAttribs())
                    Assert.Fail("It sees the only attrib developing before adding that one");

                //SOME MORE LEVEL - THAT GIVES ONE ATTRIB POINT
                dscp.gainNewLevel(3, true, 700);      //4
                jpAmount = dscp.sumFullJP_ThisDscp();
                if (jpAmount != 2300)
                    Assert.Fail("Wrong JP amount arrived at 4+1");
                List<CharDscpLevel> temp = dscp.getTheNormalLayers();
                if (!dscp.isThereUnspentAttribs())
                    Assert.Fail("It can't see the spendable attribPoint after added that one " +
                        temp[0].theLevelHasAttribRise + " " + temp[0].theAttribRiseIsSpent + " " + temp[0].theLevelMark + "\n" +
                        temp[1].theLevelHasAttribRise + " " + temp[1].theAttribRiseIsSpent + " " + temp[1].theLevelMark + "\n" +
                        temp[2].theLevelHasAttribRise + " " + temp[2].theAttribRiseIsSpent + " " + temp[2].theLevelMark + "\n" +
                        temp[3].theLevelHasAttribRise + " " + temp[3].theAttribRiseIsSpent + " " + temp[3].theLevelMark + "\n");

                //SPENDING ONE POINT ATTRIB RISE
                dscp.setSpentTheAttribPoint_FromNormalLevel(3, 4, 1);
                if (dscp.isThereUnspentAttribs())
                    Assert.Fail("It can't see the only attribPoint after deleted");

                dscp.removeTheseSpecLevels(3, 1, 1);
                dscp.removeTheseLevels(3, 1);

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void manageAttripSpendingTest()
        {
            try
            {
                CharOneDscp dscp = new CharOneDscp(3, 1, 20, 1, 3, 0, "valami");

                //TESTRING THE ATTRIB RISE OF NORMAL LEVEL
                dscp.gainNewLevel(3, true, 300);    //LVL 1
                dscp.gainNewLevel(3, true, 400);    //LVL 2
                dscp.gainNewLevel(3, true, 450);    //LVL 3

                if (dscp.isThereUnspentAttribs())
                    Assert.Fail("It sees unspentAttib - there is no yet");

                //FIRST ATTRIB RISE - LVL 4
                dscp.gainNewLevel(3, true, 700);    //LVL 4 -> type1
                if (!dscp.isThereUnspentAttribs())
                    Assert.Fail("It cannot see unspentAttrib from LVL 4");
                List<CharDscpUnspentAttrib> temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 1)
                    Assert.Fail("It cannot see the unspentAttib in list from LVL 4");
                if (temp[0].thePointType != 1)
                    Assert.Fail("It cannot see well the unspentAttrib type from LVL 4");
                if (temp[0].theDscpIndex != 1)
                    Assert.Fail("It carries wrong dscpIndex from LVL 4");
                if (temp[0].theAttribThatConnectedWith != 3)
                    Assert.Fail("It carries wrong attribId from LVL 4");

                //MORE NORMAL LEVELS
                dscp.gainNewLevel(3, true, 800);    //LVL 5 -> type1
                dscp.gainNewLevel(3, true, 900);    //LVL 6 -> type1
                dscp.gainNewLevel(3, true, 1000);    //LVL 7 -> type2
                dscp.gainNewLevel(3, true, 1200);    //LVL 8 -> type2
                dscp.gainNewLevel(3, true, 1500);    //LVL 9 -> type2
                dscp.gainNewLevel(3, true, 1800);    //LVL 10 -> type2

                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 7)
                    Assert.Fail("It not sees all unspentAttribs " + temp.Count);
                if (temp[2].thePointType != 1)
                    Assert.Fail("It carries wrong unspentAttrib type from LVL 6");
                if (temp[3].thePointType != 2)
                    Assert.Fail("It carries wrong unspentAttrib type from LVL 7");
                if (temp[5].thePointType != 2)
                    Assert.Fail("It carries wrong unspentAttrib type from LVL 9");
                if (temp[6].thePointType != 3)
                    Assert.Fail("It carries wrong unspentAttrib type from LVL 10");

                //TESTING THE ATTRIB RISE OF SPEC LEVEL
                dscp.gainNewSpecLevel(3, 2001, 1, 1, "még vm izé");
                dscp.gainNewSpecLevel(3, 2001, 1, 1, "még vm izé"); //LVL 2->type1

                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 8)
                    Assert.Fail("It not sees all unspentAttribs - from spec" + temp.Count);
                if (temp[7].thePointType != 1)
                    Assert.Fail("It carries wrong unspentAttrib type from spec LVL 2");

                //SPENDING FROM NORMAL LEVELS
                dscp.setSpentTheAttribPoint_FromNormalLevel(3, 5, 1);
                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 7)
                    Assert.Fail("It cannot spending attribRise from normal lvl5 at appliance side");

                //SPENDING FROM SPEC LEVELS
                dscp.setSpentTheAttribPoint_FromSpecLevel(3, 1, 2);
                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 6)
                    Assert.Fail("It cannot spending attribRise from spec lvl2 at appliance side");

                //TESTING THE HELPER UNKNOWN ATTRIB ENCH TYPE
                int tempIndex1 = dscp.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 1);
                if (tempIndex1 != 0)
                    Assert.Fail("Wrong specIndex/Identifier arrived from helperMethod 1 " + tempIndex1);
                int tempIndex2 = dscp.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 2);
                if (tempIndex2 != 1)
                    Assert.Fail("Wrong specIndex/Identifier arrived from helperMethod 2 " + tempIndex2);

                //REMOVED METHOD
                //NEGATIVE TEST - INDEX 1 IS NOT A SPEC
                //int tempIndex3 = dscp.findTheSpecIndexByAttribEnchant_ThatSurelyASpec_HelperRemoveAttribEnch(1);
                //if (tempIndex3 != 0)
                  //  Assert.Fail("Wrong specIndex/Identifier arrived from helperMethod 3 " + tempIndex3);
                //int tempIndex4 = dscp.findTheSpecIndexByAttribEnchant_ThatSurelyASpec_HelperRemoveAttribEnch(2);
                //if (tempIndex4 != 1)
                  //  Assert.Fail("Wrong specIndex/Identifier arrived from helperMethod 4 " + tempIndex4);

                //REVERT ATTRIB SPENDING FROM NORMAL LEVEL
                dscp.setUnspentTheAttribPoint_FromNormalLevel(3, 1);
                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 7)
                    Assert.Fail("It cannot revert spending attribRise from normal lvl5 at appliance side");

                //REVERT ATTRIB SPENDING FROM SPEC LEVEL
                dscp.setUnspentTheAttribPoint_FromSpecLevel(3, 2);
                temp = dscp.collectTheUnspentAttribs();
                if (temp.Count != 8)
                    Assert.Fail("It cannot revert spending attribRise from spec lvl2 at appliance side");

                dscp.removeAllSpecLevels(3);
                dscp.removeTheseLevels(3, 1);
                if (dscp.isThereUnspentAttribs())
                    Assert.Fail("It not sees the specLevel attribPoint");

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }



        [TestMethod()]
        public void manageAttripSpendingSideMethodsTest()
        {
            try
            {
                //ONE DSCP GAIN
                CharOneDscp dscp1 = new CharOneDscp(3, 1, 90, 1, 10, 1, "valami1");
                dscp1.gainNewLevel(3, true, 300);    //LVL 1
                dscp1.gainNewLevel(3, true, 400);    //LVL 2
                dscp1.gainNewLevel(3, true, 600);    //LVL 3
                dscp1.gainNewLevel(3, true, 750);    //LVL 4
                dscp1.gainNewLevel(3, true, 900);    //LVL 5
                dscp1.gainNewLevel(3, true, 1150);    //LVL 6

                //COMBINED TYPE 1 ATTRIB SPENDING CASE -> SAME DSCP
                dscp1.setSpentTheAttribPoint_FromNormalLevel(3, 4, 1);
                dscp1.setSpentTheAttribPoint_FromNormalLevel(3, 5, 1);

                //SECOND DSCP GAIN
                CharOneDscp dscp2 = new CharOneDscp(3, 2, 76, 1, 10, 1, "valami2");
                dscp2.gainNewLevel(3, false, 200);  //LVL 1
                dscp2.gainNewLevel(3, false, 400);  //LVL 2
                dscp2.gainNewLevel(3, false, 700);  //LVL 3
                dscp2.gainNewLevel(3, false, 900);  //LVL 4
                
                //COMBINED TYPE 1 ATTRIB SPENDING CASE -> DIFF DSCP
                dscp1.setSpentTheAttribPoint_FromNormalLevel(3, 6, 2);
                dscp2.setSpentTheAttribPoint_FromNormalLevel(3, 4, 2);

                //REVISE THE NORM LEVELS HELPER SEEKEINGS
                byte specIndex1 = dscp1.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 2);
                byte specIndex2 = dscp2.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 2);

                if (specIndex1 != 0 || specIndex2 != 0)
                    Assert.Fail("Wrong specIndex arrived " + dscp1 + " " + dscp2);

                //GET SPECIALISATIONS
                dscp1.gainNewSpecLevel(3, 1200, 1, 1, "Spec1");
                dscp1.gainNewSpecLevel(3, 1800, 1, 1, "Spec1");
                dscp2.gainNewSpecLevel(3, 1100, 1, 1, "Spec2");
                dscp2.gainNewSpecLevel(3, 1900, 1, 1, "Spec2");

                
                //COMBINED TYPE 1 ATTRIB SPENDING CASE -> DIFF DSCP WITH SPEC LEVELS
                dscp1.setSpentTheAttribPoint_FromSpecLevel(3, 1, 3);
                dscp2.setSpentTheAttribPoint_FromSpecLevel(3, 1, 3);

                //REVISE THE NORM LEVELS HELPER SEEKEINGS
                specIndex1 = dscp1.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 3);
                specIndex2 = dscp2.helperUnspentTheAttribPoint_UnknownTypeDetection(3, 3);
                if (specIndex1 != 1 || specIndex2 != 1)
                    Assert.Fail("Wrong specIndex arrived " + dscp1 + " " + dscp2);

                //ANALYZE THE DELETION-ATTRIB REMANAGER HELPER PROCESSES
                int[] tempAttribRisenNormalLevels = dscp1.findTheAttribEnchantOfNormalLevels_UnderDeletion(1);
                if (tempAttribRisenNormalLevels.Length != 3)
                    Assert.Fail("Wrong number of attribEnchIndexes arrived at analyze dscp1 " + tempAttribRisenNormalLevels.Length);
                if (tempAttribRisenNormalLevels[0] != 1 || tempAttribRisenNormalLevels[1] != 1 || tempAttribRisenNormalLevels[2] != 2)
                    Assert.Fail("Wrong attribEnchIndexes arrived at dscp1 norm levels");

                int tempAttribRisenSpecLevel = dscp1.findTheAttribEnchantOfThisSpecIndex_UnderDeletion(1);
                if (tempAttribRisenSpecLevel != 3)
                    Assert.Fail("Wrong attribEnchIndex arrived at analyze dscp1 normal levels");

                int[] tempAllAttribEnch = dscp1.findAllAttribEnchantOfThisDscp_UnderDeletion();
                if (tempAllAttribEnch.Length != 4)
                    Assert.Fail("Wrong number of attribEnchIndexes arribed at analyze dscp1 ");
                if (tempAllAttribEnch[0] != 1 || tempAllAttribEnch[1] != 1 || tempAllAttribEnch[2] != 2 || tempAllAttribEnch[3] != 3)
                    Assert.Fail("Wrong attribEnchIndex arrived at analyze dscp1 all levels");


                dscp1.removeAllSpecLevels(3);
                dscp1.removeTheseLevels(3, 1);
                dscp2.removeAllSpecLevels(3);
                dscp2.removeTheseLevels(3, 1);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
    }
}