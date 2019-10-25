using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CharModel;

namespace ChaosRpgCharGen.CharRepository1.Tests
{

    [TestClass()]
    public class CoreDisciplinesTypesTests1
    {
        CoreDisciplinesTypesRepo dt = new CoreDisciplinesTypesRepo();

        #region DscpIdOfDscpName
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest1()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Állatgondozás");
                if (res != 3)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " +e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest2()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Szimultán harc");
                if (res != 52)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest3()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Méregkeverés");
                if (res != 66)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest4()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Pszihasználat");
                if (res != 77)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest5()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Varázstárgy készítés");
                if (res != 90)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest6()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Tímár");
                if (res != 34)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest7()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Hajítás");
                if (res != 9)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest8()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Földműves");
                if (res != 27)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest9()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Szobrászat");
                if (res != 158)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest10()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Titkosírás");
                if (res != 145)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest11()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Lerázás");
                if (res != 100)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest12()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Fegyverhasználat");
                if (res != 38)
                    Assert.Fail("It gives wrong dscpId for dscpName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest13()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("");
                Assert.Fail("It gives dscpId for bad dscpName");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest14()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("Valami");
                Assert.Fail("It gives dscpId for bad dscpName");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpIdOfThisDscpTextTest15()
        {
            try
            {
                short res = dt.findTheDscpIdOfThisDscpText("56");
                Assert.Fail("It gives dscpId for bad dscpName");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        #endregion

        #region DscpTypeIdOfTypeName
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest1()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("Hétköznapok");
                if (res != 1)
                    Assert.Fail("It gives wrong typeId for dscpTypeName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest2()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("Védelem");
                if (res != 6)
                    Assert.Fail("It gives wrong typeId for dscpTypeName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest3()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("Tudomány");
                if (res != 8)
                    Assert.Fail("It gives wrong typeId for dscpTypeName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest4()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("Vér");
                if (res != 3)
                    Assert.Fail("It gives wrong typeId for dscpTypeName");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest5()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("45454");
                Assert.Fail("It gives typeId for wrong dscpTypeName");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeIdOfThisTypeTextTest6()
        {
            try
            {
                byte res = dt.findTheDscpTypeIdOfThisTypeText("Valami");
                Assert.Fail("It gives typeId for wrong dscpTypeName");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        #endregion

        #region TypeIdOfDscps
        [TestMethod()]
        public void findTheTypeIdOfDscpTest1()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(30);
                if (res != 2)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest2()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(59);
                if (res != 4)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest3()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(79);
                if (res != 5)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest4()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(126);
                if (res != 7)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest5()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(132);
                if (res != 8)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest6()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(8);
                if (res != 1)
                    Assert.Fail("It gives wrong typeId for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest7()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(0);
                Assert.Fail("It gives typeId for wrong dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheTypeIdOfDscpTest8()
        {
            try
            {
                byte res = dt.findTheTypeIdOfDscp(300);
                Assert.Fail("It gives typeId for wrong dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        #endregion

        #region TypeIdToTypeName
        [TestMethod()]
        public void findTheDscpTypeNameTest1()
        {
            try
            {
                string text = dt.findTheDscpTypeName(30);
                if (text != "Szakmák")
                    Assert.Fail("It gives wrong typeName for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeNameTest2()
        {
            try
            {
                string text = dt.findTheDscpTypeName(160);
                if (text != "Művészet")
                    Assert.Fail("It gives wrong typeName for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeNameTest3()
        {
            try
            {
                string text = dt.findTheDscpTypeName(132);
                if (text != "Tudomány")
                    Assert.Fail("It gives wrong typeName for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeNameTest4()
        {
            try
            {
                string text = dt.findTheDscpTypeName(0);
                Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheDscpTypeNameTest5()
        {
            try
            {
                string text = dt.findTheDscpTypeName(310);
                Assert.Fail("It gives typeName for bad dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message);
            }
        }
        #endregion

    }
}