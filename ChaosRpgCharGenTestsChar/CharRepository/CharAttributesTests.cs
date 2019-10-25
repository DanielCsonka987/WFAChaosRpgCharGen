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
    public class CharAttributesTests
    {
        [TestMethod()]
        public void getTheFullValueOfThisAttributeTest()
        {
            try
            {
                CharAttributes ca = new CharAttributes(1);
                short fullVal1 = ca.getTheFullValueOfThisAttribute(1);
                if (fullVal1 != 60)
                    Assert.Fail("Wrong full value-1 " + fullVal1);
                short fullVal2 = ca.getTheFullValueOfThisAttribute(2);
                if (fullVal2 != 65)
                    Assert.Fail("Wrong full value-2 " + fullVal2);

                short fullVal5 = ca.getTheFullValueOfThisAttribute(5);
                if (fullVal5 != 64)
                    Assert.Fail("Wrong full value-5 " + fullVal2);

            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void getTheAttributesCollectively_ToShowFinalTest1()
        {
            try
            {
                CharAttributes ca = new CharAttributes(1);
                short[] temp = ca.getTheAttributesCollectively_ToShowFinal();
                if (temp[0] != 60 || temp[1] != 65 || temp[2] != 62 ||
                   temp[3] != 63 || temp[4] != 64 || temp[5] != 65 ||
                   temp[6] != 66 || temp[7] != 67 || temp[8] != 68 ||
                   temp[9] != 69 || temp[10] != 50 || temp[11] != 51)
                    Assert.Fail("Wrong values arrived");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void getTheAttributesCollectively_ToShowFinalTest2()
        {
            try
            {
                CharAttributes ca = new CharAttributes(1);
                short[] temp = ca.getTheAttributesCollectively_ToShowFinal();
                if (temp[0] != 60 || temp[1] != 65 || temp[2] != 62 ||
                   temp[3] != 63 || temp[4] != 64 || temp[5] != 65 ||
                   temp[6] != 66 || temp[7] != 67 || temp[8] != 68 ||
                   temp[9] != 69 || temp[10] != 50 || temp[11] != 51)
                    Assert.Fail("Wrong values arrived");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void getTheAttrubutesCollectively_ToShowInDetailsTest1()
        {
            try
            {
                CharAttributes ca = new CharAttributes(1);
                short[,] temp = ca.getTheAttrubutesCollectively_ToShowInDetails();
                /*
                string res = "";    //IT IS FOR TESTING THE IComparator METHOD AT ATTRIB SORT 
                                    //USE IT WITH THE OUTCOMMENTED PART!
                for(byte i = 0; i<temp.GetLength(0); i++)
                {
                    for(byte j = 0; j<temp.GetLength(1); j++)
                    {
                        res += temp[i, j].ToString() + " ";
                    }
                    res += "\n";
                }
                if (temp[0,0] != 1 || temp[0,1] != 60 || temp[0, 2] != 0 ||
                    temp[1,0] != 2 || temp[1,1] != 61 || temp[1, 2] != 4 ||
                    temp[2, 0] != 3 || temp[2,1] != 62 || temp[2, 2] != 0 ||
                    temp[3, 0] != 4 || temp[3, 1] != 63 || temp[3, 2] != 0 ||
                    temp[4, 0] != 5 || temp[4, 1] != 64 || temp[4, 2] != 0 ||
                    temp[5, 0] != 6 || temp[5, 1] != 65 || temp[5, 2] != 0 ||
                    temp[6, 0] != 7 || temp[6, 1] != 66 || temp[6, 2] != 0 ||
                    temp[7, 0] != 8 || temp[7, 1] != 67 || temp[7, 2] != 0 ||
                    temp[8, 0] != 9 || temp[8, 1] != 68 || temp[8, 2] != 0 ||
                    temp[9, 0] != 10 || temp[9, 1] != 69 || temp[9, 2] != 0 ||
                    temp[10, 0] != 11 || temp[10, 1] != 50 || temp[10, 2] != 0 ||
                    temp[11, 0] != 12 || temp[11, 1] != 51 || temp[11, 2] != 0
                   )
                    Assert.Fail("Wrong values arrived " + res);
                 */

                if (temp[0, 0] != 60 || temp[0, 1] != 0 ||
                    temp[1, 0] != 61 || temp[1, 1] != 4 ||
                    temp[2, 0] != 62 || temp[2, 1] != 0 ||
                    temp[3, 0] != 63 || temp[3, 1] != 0 ||
                    temp[4, 0] != 64 || temp[4, 1] != 0 ||
                    temp[5, 0] != 65 || temp[5, 1] != 0 ||
                    temp[6, 0] != 66 || temp[6, 1] != 0 ||
                    temp[7, 0] != 67 || temp[7, 1] != 0 ||
                    temp[8, 0] != 68 || temp[8, 1] != 0 ||
                    temp[9, 0] != 69 || temp[9, 1] != 0 ||
                    temp[10, 0] != 50 || temp[10, 1] != 0 ||
                    temp[11, 0] != 51 || temp[11, 1] != 0
                   )
                    Assert.Fail("Wrong values arrived");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void getTheAttrubutesCollectively_ToShowInDetailsTest2()
        {
            try
            {
                CharAttributes ca = new CharAttributes(2);
                short[,] temp = ca.getTheAttrubutesCollectively_ToShowInDetails();

                if (temp[0, 0] != 62 || temp[0, 1] != 0 ||
                    temp[1, 0] != 63 || temp[1, 1] != 0 ||
                    temp[2, 0] != 62 || temp[2, 1] != 0 ||
                    temp[3, 0] != 61 || temp[3, 1] != 0 ||
                    temp[4, 0] != 65 || temp[4, 1] != 0 ||
                    temp[5, 0] != 67 || temp[5, 1] != 0 ||
                    temp[6, 0] != 60 || temp[6, 1] != 0 ||
                    temp[7, 0] != 61 || temp[7, 1] != 0 ||
                    temp[8, 0] != 61 || temp[8, 1] != 0 ||
                    temp[9, 0] != 65 || temp[9, 1] != 0 ||
                    temp[10, 0] != 63 || temp[10, 1] != 0 ||
                    temp[11, 0] != 61 || temp[11, 1] != 0
                   )
                    Assert.Fail("Wrong values arrived");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }


        [TestMethod()]
        public void addRemoveListingAttributeTest()
        {
            try
            {
                CharAttributes ca = new CharAttributes(1);
                List<CharOneAttribEnch> temp = ca.collectTheAttributeEnchList_ToShow();
                int enchValue = temp.Count;
                if (enchValue != 4)
                    Assert.Fail("Wrong amount arrived before adding " + enchValue);

                ca.addOnePointRiseToSpecifAttribute(3, 34, 0, 2);
                temp = ca.collectTheAttributeEnchList_ToShow();
                enchValue = temp.Count;
                if (enchValue != 5)
                    Assert.Fail("Wrong amount arrived after adding " + enchValue);

                int attrEnchIndex = temp.Find(x => x.theAttributeId == 3 &&
                            x.theDisciplIndex_First == 34).theAttribEnchanceIndex;
                if (attrEnchIndex != 5)
                    Assert.Fail("Wrong attribEcnhIndex generated durring adding method");

                ca.removeOneAttribEnchElement(attrEnchIndex);
                temp = ca.collectTheAttributeEnchList_ToShow();
                if (temp.Count != 4)
                    Assert.Fail("Wring amount arrived after removing " + temp.Count);
                if (temp.Exists(x => x.theAttributeId == attrEnchIndex &&
                         x.theDisciplIndex_First == 34 && x.theAttributeId == 3))
                    Assert.Fail("Wrong attrEnch is deleted");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void removeDscpOverlappingsTest()
        {
            try
            {
                CharAttributes ca = new CharAttributes(2);

                ca.addOnePointRiseToSpecifAttribute(3, 2, 0, 1);
                ca.addOnePointRiseToSpecifAttribute(3, 2, 4, 1);
                ca.addOnePointRiseToSpecifAttribute(3, 2, 1, 1);
                ca.addOnePointRiseToSpecifAttribute(3, 1, 3, 1);

                List<int[]> tempDscpIndexes = ca.collectAttribEnchThatOverlapsWithThese(new int[] { 1, 2, 3 }, 2);
                if (tempDscpIndexes.Count != 2)
                    Assert.Fail("Wrong amount dscpIndex+attribEnchIndex arrived back");
                if (tempDscpIndexes[0][0] != 4 && tempDscpIndexes[0][1] != 2)
                    Assert.Fail("Wrong overlap element at 1.place");
                if (tempDscpIndexes[1][0] != 1 && tempDscpIndexes[1][1] != 3)
                    Assert.Fail("Wrong overlap element at 2.place");

                ca.removeOneAttribEnchElement(1);
                ca.removeOneAttribEnchElement(2);
                ca.removeOneAttribEnchElement(3);
                ca.removeOneAttribEnchElement(4);
                if (ca.collectTheAttributeEnchList_ToShow().Count != 0)
                    Assert.Fail("Wrong attribEnch deletion");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
    }
}