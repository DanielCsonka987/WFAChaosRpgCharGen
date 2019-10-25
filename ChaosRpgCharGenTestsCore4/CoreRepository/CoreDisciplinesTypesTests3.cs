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
    public class CoreDisciplinesTypesTests3
    {
        CoreDisciplinesTypesRepo dt = new CoreDisciplinesTypesRepo();

        #region dscpSpecAreaTextOfDscpId
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest1()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(5,1);
                if (text != "Egy adott fajra")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest2()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(90, 1);
                if (text != "Egy varázslattípusra")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest3()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(31, 1);
                if (text != "Fegyverek")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest4()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(31, 2);
                if (text != "Páncélok")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest5()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(31, 3);
                if (text != "Általános")
                    Assert.Fail("It gives dscpName for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecAreaTextForThisDscpTest6()
        {
            try
            {
                string text = dt.findTheSpecAreaTextForThisDscp(0, 1);
                if(text != "")
                    Assert.Fail("It gives specAreaText for bad dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region isThereSpec
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest1()  //NO SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(50);
                if (res)
                    Assert.Fail("It shows there is/are spec for dscpId " +res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest2()  //NO SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(120);
                if (res)
                    Assert.Fail("It shows there is/are spec for dscpId " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest3()  //NO SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(161);
                if (res)
                    Assert.Fail("It shows there is/are spec for dscpId " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest4()  //NO SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(13);
                if (res)
                    Assert.Fail("It shows there is/are spec for dscpId " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest5()  //THERE IS SOME SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(1);
                if (!res)
                    Assert.Fail("It shows there is/are spec for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest6()  //THERE IS SOME SPEC
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(72);
                if (!res)
                    Assert.Fail("It shows there is/are spec for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest7()  //BAD DSCPID
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(0);
                Assert.Fail("It shows result for bad dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereSpecialisationForThisDscpTest8()  //BAD DSCPID
        {
            try
            {
                bool res = dt.isThereSpecialisationForThisDscp(190);
                Assert.Fail("It shows result for bad dscpId");
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