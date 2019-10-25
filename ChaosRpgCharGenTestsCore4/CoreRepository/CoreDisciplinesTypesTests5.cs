using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CharModel;

namespace ChaosRpgCharGen.CharRepository2.Tests
{

    [TestClass()]
    public class CoreDisciplinesTypesTests5
    {
        CoreDisciplinesTypesRepo dt = new CoreDisciplinesTypesRepo();

        #region dscpName

        [TestMethod()]
        public void findTheDscpNameTest1()
        {
            try
            {
                string text = dt.findTheDscpName(6);
                if (text != "Esés")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest2()
        {
            try
            {
                string text = dt.findTheDscpName(30);
                if (text != "Kereskedő")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest3()
        {
            try
            {
                string text = dt.findTheDscpName(68);
                if (text != "Szájról olvasás")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest4()
        {
            try
            {
                string text = dt.findTheDscpName(89);
                if (text != "Varázsszer készítés")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest5()
        {
            try
            {
                string text = dt.findTheDscpName(108);
                if (text != "Zsongás")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest6()
        {
            try
            {
                string text = dt.findTheDscpName(156);
                if (text != "Szexualitás")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest7()
        {
            try
            {
                string text = dt.findTheDscpName(146);
                if (text != "Történelem")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest8()
        {
            try
            {
                string text = dt.findTheDscpName(112);
                if (text != "Élőholtak ismerete")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest9()
        {
            try
            {
                string text = dt.findTheDscpName(136);
                if (text != "Létsík ismeret")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest10()
        {
            try
            {
                string text = dt.findTheDscpName(80);
                if (text != "Áldozat/meditáció/felfrissülés")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest11()
        {
            try
            {
                string text = dt.findTheDscpName(0);
                Assert.Fail("It gives back result for wrong dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpNameTest12()
        {
            try
            {
                string text = dt.findTheDscpName(190);
                Assert.Fail("It gives back result for wrong dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion


    }
}