using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository2.Tests
{

    [TestClass()]
    public class CoreDisciplinesTypesTests2
    {
        CoreDisciplinesTypesRepo dt = new CoreDisciplinesTypesRepo();

        #region requirGroupOfRequirCombin
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest1()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(2, "Nincs feltétele");
                if(res != 0)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest2()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(13, "1) Állatgondozás");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest3()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(60, "4) Mechanika");
                if (res != 2)
                    Assert.Fail("It gives typeName for bad dscpId " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest4()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(88, "2) Mágiahasználat+Írás/olvasás+Holtnyelv ismeret");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId "+ res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest5()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(88, "2) Elhivatottság+Írás/olvasás+Holtnyelv ismeret");
                if (res != 2)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest6()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(96, "1) Mágiaelmélet");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest7()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(96, "3) Mágiahasználat");
                if (res != 2)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest8()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(96, "1) Elhivatottság");
                if (res != 3)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest9()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(96, "2) Eszenciakontol");
                if (res != 4)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest10()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(152, "2) Írás/olvasás+Rajzolás");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest11()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(147, "1) Emberismeret");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest12()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(28, "Nincs feltétele");
                if (res != 1)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheChosenRequirGroupOfThisResuirListElementTest13()
        {
            try
            {
                byte res = dt.findTheChosenRequirGroup_OfThisNormalLevelRequirListElement(28, "2) Csomózás");
                if (res != 2)
                    Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }

        #endregion

    }
}