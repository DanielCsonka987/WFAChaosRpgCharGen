using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CharRepository1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CharModel;

namespace ChaosRpgCharGen.CharRepository1.Tests
{
    [TestClass()]
    public class CharDisciplinesTests
    {

        [TestMethod()]
        public void addNewAndRemoveWholeDisciplTest()
        {
            try
            {
                CharDisciplines dscpRepo = new CharDisciplines(3);

                //CREATE 1st DSCP
                dscpRepo.addNewDiscipl(31, 2, 2, "me csak", false, 400, 0); //1st lvl 1

                //SCREEN THE ACT NORM LEVELS
                List<CharDscpSurfaceAboutLevels> temp = dscpRepo.collectAllDisciplSurface();
                int amount = temp.Count;
                if (amount != 1)
                    Assert.Fail("Not proper element in surface " + amount);
                if (temp[0].theDscpTopLevel != 1)
                    Assert.Fail("Wrong levelMark at list " + temp[0].theDscpTopLevel);

                //ADDING MORE LEVEL
                dscpRepo.riseThisDiscipl(1, true, 460); //1st lvl 2
                dscpRepo.riseThisDiscipl(1, false, 700); //1st lvl 3

                //CREATE 2nd DSCP
                dscpRepo.addNewDiscipl(40, 4, 4, "megegy", false, 500, 0);  //2nd lvl 1
                temp = dscpRepo.collectAllDisciplSurface();
                if (temp[0].theDscpIndex != 1)
                    Assert.Fail("Wrong dscpIndex at first dscp");
                if (temp[1].theDscpIndex != 2)
                    Assert.Fail("Wrong dscpIndex at second dscp");

                dscpRepo.riseThisDiscipl(2, false, 600); //2nd lvl 2
                dscpRepo.riseThisDiscipl(2, false, 700); //2nd lvl 3

                //REVISE THE JP COUNTING - ALL VERSION
                int allJp = dscpRepo.sumAllJPOfDscp();
                if (allJp != 3360)
                    Assert.Fail("Wrong JP result at allCounting " + allJp.ToString());

                //INDIVIDUAL DSCP'S JP COUNTING
                int dscpJp = dscpRepo.sumTheJPOfThisDscp(1);
                if (dscpJp != 1560)
                    Assert.Fail("Wrong JP aresult at oneCounting " + dscpJp.ToString());

                dscpRepo.removeDisciplLevelFrom(2, 3);  //2nd remove lvl 3

                dscpRepo.removeThisWholeDiscipl(1); //remove 1st lvl 1-2-3

                temp = dscpRepo.collectAllDisciplSurface();
                if (temp[0].theDscpIndex != 2 && temp[0].theDscpIndex != 2)
                    Assert.Fail("Wrong dscpIndex at first dscp");

                dscpRepo.removeThisWholeDiscipl(2);

            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void findThisDscpInfosTest()
        {
            try
            {
                CharDisciplines dscpRepo = new CharDisciplines(3);
                dscpRepo.addNewDiscipl(31, 2, 2, "me csak", false, 400, 0); //1st lvl 1

                //DSCP'S COSEN NOTATION SEEKER
                string dscpNote = dscpRepo.findThisDscpNoteOfThisDscp(1);
                if (dscpNote != "me csak")
                    Assert.Fail("Wrong dscp description arrived ");

                //DSCP'S CHOSEN ATTRIB SEELER
                byte dscpAttribId = dscpRepo.findTheDscpAttribForThisDscp(1);
                if (dscpAttribId != 2)
                    Assert.Fail("Wrong attribId arrived " + dscpAttribId);

                //REQUIREMENT SEEKER - AT GENERALLY
                if (!dscpRepo.findThisDscpRequir_IndividualHaveTheLevel_NormalCase(31, 1, true))
                    Assert.Fail("Requirement analyze not find this dscp and its level");

                //REQUIREMENT SEEKER - TYPE VERSION
                if (!dscpRepo.findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(2, 1))
                    Assert.Fail("It cannot see the dscpType has the level");

                //SEEK DSCPID BY ITS DSCPINDEX
                short dscpId = dscpRepo.findTheDscpIdForThisDscp(1);
                if (dscpId != 31)
                    Assert.Fail("Wrong dscpId arrived " + dscpId);



                dscpRepo.removeThisWholeDiscipl(1);

            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message);
            }
        }

        [TestMethod()]
        public void addNewRemoveDsciplSpecialLevelTest()
        {
            try
            {
                CharDisciplines dscpRepo = new CharDisciplines(3);
                dscpRepo.addNewDiscipl(31, 2, 2, "me csak", false, 400, 0); //1st lvl 1

                //REVISION OF SPEC EXISTENCE
                if (dscpRepo.hasThisDisciplSpecialis(1))
                    Assert.Fail("Wrong sign - no spec, but is shows");

                //ADDING 1st SPEC
                dscpRepo.addNewDsciplSpecialLevel(1, 555, 1, 1, "Valami spec");  //spec lvl 1
                if (!dscpRepo.hasThisDisciplSpecialis(1))
                    Assert.Fail("Wrong sign - one spec, but is shows no one");
                if (!dscpRepo.hasThisDisciplSpecWithThisArea(1, 1, 1, "Valami spec"))
                    Assert.Fail("");

                //ADDING 1st SPEC ANOTHER LEVEL
                dscpRepo.addNewDsciplSpecialLevel(1, 888, 1, 1, "Valami spec");   //spec lvl 2

                //OVERLOAD ATTEMPT
                dscpRepo.addNewDsciplSpecialLevel(1, 1000, 1, 1, "Valami spec");  //spec overLevel

                //REVISION AT LAYERS - SEEK THE 2 LVL
                List<CharDscpSpecLevel> temp = dscpRepo.collectThisDisciplSpecAllLayers(1);
                if (temp.Count != 2)
                    Assert.Fail("Wrong amount of specialisation level detected");

                //ADDING 2nd SPECIALISATION
                dscpRepo.addNewDsciplSpecialLevel(1, 505, 2, 1, "Ez más lesz");

                //REVISION REQUIREMENT ANALYZE SEES THE EXISTENCE
                if (!dscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(31, 1))
                    Assert.Fail("It not sees the existance this as requirement");

                //COUNTING JP WITH SOME SPECIALISATION
                int allJP = dscpRepo.sumAllJPOfDscp();
                if (allJP != 2348)
                    Assert.Fail("Wrong allJP amount - with 2 specialisation " + allJP.ToString());

                //COUNTING JP WITH 1st SPEC
                int oneJP = dscpRepo.sumTheJPOfThisDscp(1);
                if (oneJP != 2348)
                    Assert.Fail("Wrong oneJP amount - with 2 specialisation " + oneJP.ToString());

                dscpRepo.removeThisDsciplSpecFrom(1, 1, 1);
                dscpRepo.removeThisDsciplSpecFrom(1, 2, 1);
                if (dscpRepo.hasThisDisciplSpecialis(1))
                    Assert.Fail("Existence sign arrived - spec have been removed");

                dscpRepo.removeThisWholeDiscipl(1);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message);
            }
        }

        [TestMethod()]
        public void manageOfPossiblyUnspentAttribPointsTest()
        {
            try
            {
                CharDisciplines dscpRepo = new CharDisciplines(3);

                //CREATE 1st DSCP
                dscpRepo.addNewDiscipl(31, 2, 2, "Valami", false, 400, 0);  //LVL 1
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 2
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 3
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 4
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 5
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 6
                dscpRepo.riseThisDiscipl(1, false, 600);     //LVL 7

                //REVISE THE ATTRIP SPENDABILITY
                List<CharDscpUnspentAttrib> temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 4)
                    Assert.Fail("It not sees the spendable 4 attribPoint");
                if (temp[0].theDscpIndex != 1)
                    Assert.Fail("It cant identify the point source");
                if (temp[0].theAttribThatConnectedWith != 2)
                    Assert.Fail("It cannot carry the proprer attribType");

                //SPENDING LVL4 ATTRIB POINT
                dscpRepo.removeUnpentAttribPoint_itIsSpent(1, 4, 0, 1);
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 3)
                    Assert.Fail("It not sees the spendable 5 attribPoint");

                //REVERT LVL4 ATRRIB POINT
                dscpRepo.addUnspentAttribPoint_resetAttribSpending(1, false, 1);
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 4)
                    Assert.Fail("It not sees again the spendable 4 attribPoint");

                //ADDING SPEC AND TEST
                dscpRepo.addNewDsciplSpecialLevel(1, 1300, 1, 1, "test spec");  //SPEC LVL 1
                dscpRepo.addNewDsciplSpecialLevel(1, 1400, 1, 1, "test spec");  //SPEC LVL 2

                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 5)
                    Assert.Fail("It not sees again the spendable 5 attribPoint with spec lvl2");

                //SPENDING SPEC LVL ATTRIB POINT
                dscpRepo.removeUnpentAttribPoint_itIsSpent(1, 2, 1, 2);
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 4)
                    Assert.Fail("It not sees again the spendable 4 attribPoint after speding spec attibPoint");

                //ADDING ANOTHER SPEC AND TEST
                dscpRepo.addNewDsciplSpecialLevel(1, 1300, 1, 1, "test spec2"); //SPEC LVL 1
                dscpRepo.addNewDsciplSpecialLevel(1, 1500, 1, 1, "test spec2"); //SPEC LVL 2
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 5)
                    Assert.Fail("It not sees the spendable 6 attribPoint with spec sec lvl2");
                dscpRepo.removeUnpentAttribPoint_itIsSpent(1, 2, 2, 3);
                dscpRepo.addUnspentAttribPoint_resetAttribSpending_UnknownType(1, 3);
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 5)
                    Assert.Fail("It not sees again the spendable 6 attribPoint with spec sec lvl2");

                //REVERT SPEC LVL ATTIP POINT
                dscpRepo.addUnspentAttribPoint_resetAttribSpending(1, true, 2);
                temp = dscpRepo.collectPossiblyUnspentAttribPoints();
                if (temp.Count != 6)
                    Assert.Fail("It not sees again the spendable 5 attribPoint after reverted the spec attibPoint");


                dscpRepo.removeThisWholeDiscipl(1);
            }
            //unspent coll
            //setSpent
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message);
            }
        }


        [TestMethod()]
        public void requirementAnalyzeTest()
        {
            try
            {
                CharDisciplines dscpRepo = new CharDisciplines(2);

                dscpRepo.addNewDiscipl(119, 4, 3, "stg", false, 300, 0);
                dscpRepo.riseThisDiscipl(1, false, 400);
                dscpRepo.riseThisDiscipl(1, false, 400);
                dscpRepo.riseThisDiscipl(1, false, 400);
                dscpRepo.riseThisDiscipl(1, false, 400);
                dscpRepo.riseThisDiscipl(1, false, 400);

                dscpRepo.addNewDiscipl(76, 3, 5, "stg", true, 340, 1);
                dscpRepo.riseThisDiscipl(2, true, 450);
                dscpRepo.riseThisDiscipl(2, true, 450);
                dscpRepo.riseThisDiscipl(2, true, 450);
                dscpRepo.riseThisDiscipl(2, true, 450);
                dscpRepo.riseThisDiscipl(2, true, 450);

                dscpRepo.addNewDiscipl(143, 5, 4, "stg3", false, 300, 0);
                dscpRepo.riseThisDiscipl(3, false, 360);
                dscpRepo.riseThisDiscipl(3, false, 360);
                dscpRepo.riseThisDiscipl(3, false, 360);
                dscpRepo.riseThisDiscipl(3, false, 360);
                dscpRepo.riseThisDiscipl(3, false, 360);

                dscpRepo.addNewDiscipl(129, 6, 5, "stg4", false, 500, 1);
                dscpRepo.riseThisDiscipl(4, true, 450);
                dscpRepo.riseThisDiscipl(4, true, 450);
                dscpRepo.riseThisDiscipl(4, true, 450);
                dscpRepo.riseThisDiscipl(4, true, 450);
                dscpRepo.riseThisDiscipl(4, true, 450);

                dscpRepo.addNewDiscipl(89, 6, 8, "the main", true, 450, 1);

                if (!dscpRepo.findThisDscpRequir_AStrictDscpTypeHasTheLevel_NormalSpecCase(3, 2))
                    Assert.Fail("1 - It cannot see the type exists");
                    
                if (!dscpRepo.findThisDscpRequir_IndividualHaveTheLevel_NormalCase(76, 5, true))
                    Assert.Fail("2 - It cannot see the 76 dscp already exist, but overstepped");
                if (!dscpRepo.findThisDscpRequir_IndividualHaveTheLevel_NormalCase(129, 5, true))
                    Assert.Fail("2 - It cannot see the 76 dscp already exist, but overstepped");
                if (!dscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(129, 6) ||
                    !dscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(76, 6))
                    Assert.Fail("3 - Is cannot see the dscp 89 spec requir for lvl 1 are there");
                if (dscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(76, 7) ||
                    dscpRepo.findThisDscpRequir_AStrictDscpHaveTheLevel_SpecCase(129, 7))
                    Assert.Fail("4 - It sees at the dscp 89 spec requir for lvl2 reqiurements are there");

                dscpRepo.removeThisWholeDiscipl(1);
                dscpRepo.removeThisWholeDiscipl(2);
                dscpRepo.removeThisWholeDiscipl(3);
                dscpRepo.removeThisWholeDiscipl(4);
                dscpRepo.removeThisWholeDiscipl(5);
            }
            catch(Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
    }
            
}