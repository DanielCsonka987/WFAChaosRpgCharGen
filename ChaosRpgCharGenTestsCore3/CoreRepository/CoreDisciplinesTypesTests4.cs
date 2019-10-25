using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using ChaosRpgCharGen.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CharModel;

namespace ChaosRpgCharGen.CharRepository3.Tests
{

    [TestClass()]
    public class CoreDisciplinesTypesTests4
    {
        CoreDisciplinesTypesRepo dt = new CoreDisciplinesTypesRepo();

        #region isThereRequir
        [TestMethod()]
        public void isThereRequirementofThisDscpTest1() //THERE ARE SOME REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(50);
                if (!res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest2() //THERE ARE SOME REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(86);
                if (!res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest3() //THERE ARE SOME REQUIR
        {
            try
            { 
                bool res = dt.isThereRequirementofThisDscp(92);
                if (!res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest4() //THERE ARE SOME REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(140);
                if (!res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest5() //THERE ARE NO REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(2);
                if (res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest6() //THERE ARE NO REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(53);
                if (res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest7() //THERE ARE NO REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(139);
                if (res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest8() //THERE ARE NO REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(160);
                if (res)
                    Assert.Fail("It shows there is/are resuirement for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest9() //BAD DSCPID
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(0);
                Assert.Fail("It gives result for for dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereRequirementofThisDscpTest10() //THERE ARE NO REQUIR
        {
            try
            {
                bool res = dt.isThereRequirementofThisDscp(300);
                Assert.Fail("It gives result for for dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion
        
        #region isThereFewSpecAreaForDscpId
        /*
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest1() //NO SPEC AT ALL
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(50);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest2() //NO SPEC AT ALL
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(109);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest3() //NO SPEC AT ALL
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(70);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest4() //NO SPEC AT ALL
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(28);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest5() //THERE IS SPEC BUT ONYL ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(54);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest6() //THERE IS SPEC BUT ONYL ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(88);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest7() //THERE IS SPEC BUT ONYL ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(146);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest8() //THERE IS SPEC BUT ONYL ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(5);
                if (res)
                    Assert.Fail("It shows there are severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest9() //THERE MORE THAN ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(1);
                if (!res)
                    Assert.Fail("It shows there are no severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThereFewSpecAreaForThisDscpTest10() //THERE MORE THAN ONE
        {
            try
            {
                bool res = dt.isThereFewSpecAreaForThisDscp(8);
                if (!res)
                    Assert.Fail("It shows there are no severalSpecText for dscpId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        */
        #endregion
        

        #region isThisDscpStudyOriginallyBeneficial
        [TestMethod()]
        public void isThisDscpStudyIsOriginallyBeneficialTest1()
        {
            try
            {
                bool res = dt.isThisDscpStudyIsOriginallyBeneficial(1);
                if (!res)
                    Assert.Fail("It shows it is not beneficial by oroginally");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThisDscpStudyIsOriginallyBeneficialTest2()
        {
            try
            {
                bool res = dt.isThisDscpStudyIsOriginallyBeneficial(2);
                if (!res)
                    Assert.Fail("It shows it is not beneficial by oroginally");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThisDscpStudyIsOriginallyBeneficialTest3()
        {
            try
            {
                bool res = dt.isThisDscpStudyIsOriginallyBeneficial(8);
                if (res)
                    Assert.Fail("It shows it is beneficial by oroginally");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThisDscpStudyIsOriginallyBeneficialTest4()
        {
            try
            {
                bool res = dt.isThisDscpStudyIsOriginallyBeneficial(5);
                if (res)
                    Assert.Fail("It shows it is beneficial by oroginally");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void isThisDscpStudyIsOriginallyBeneficialTest5()
        {
            try
            {
                bool res = dt.isThisDscpStudyIsOriginallyBeneficial(15);
                Assert.Fail("It shows result for bad typeId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region dscpNeedMentor
        [TestMethod()]
        public void mustBeMentorAtStrudyThisDscpTest1()
        {
            try
            {
                bool res = dt.mustBeMentorAtStrudyThisDscp(1);
                if (res)
                    Assert.Fail("It shows study this dscp needs mentor");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustBeMentorAtStrudyThisDscpTest2()
        {
            try
            {
                bool res = dt.mustBeMentorAtStrudyThisDscp(109);
                if (res)
                    Assert.Fail("It shows study this dscp needs mentor");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustBeMentorAtStrudyThisDscpTest3()
        {
            try
            {
                bool res = dt.mustBeMentorAtStrudyThisDscp(111);
                if (!res)
                    Assert.Fail("It shows study this dscp practicable");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustBeMentorAtStrudyThisDscpTest4()
        {
            try
            {
                bool res = dt.mustBeMentorAtStrudyThisDscp(141);
                if (!res)
                    Assert.Fail("It shows study this dscp practicable");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustBeMentorAtStrudyThisDscpTest5()
        {
            try
            {
                bool res = dt.mustBeMentorAtStrudyThisDscp(0);
                Assert.Fail("It shows this dscp exists");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region dscpRequirSevere
        [TestMethod()]
        public void mustTreathSevereTheRequirForThisDscpTest1()
        {
            try
            {
                bool res = dt.mustTreathSevereTheRequirForThisDscp(1);
                if (!res)
                    Assert.Fail("It shows study this dscp requirement is nor severe");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustTreathSevereTheRequirForThisDscpTest2()
        {
            try
            {
                bool res = dt.mustTreathSevereTheRequirForThisDscp(87);
                if (!res)
                    Assert.Fail("It shows study this dscp requirement is nor severe");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustTreathSevereTheRequirForThisDscpTest3()
        {
            try
            {
                bool res = dt.mustTreathSevereTheRequirForThisDscp(162);
                if (!res)
                    Assert.Fail("It shows study this dscp requirement is nor severe");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustTreathSevereTheRequirForThisDscpTest4()
        {
            try
            {
                bool res = dt.mustTreathSevereTheRequirForThisDscp(95);
                if (res)
                    Assert.Fail("It shows study this dscp requirement is severe");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void mustTreathSevereTheRequirForThisDscpTest5()
        {
            try
            {
                bool res = dt.mustTreathSevereTheRequirForThisDscp(1222);
                Assert.Fail("It shows this dscp exists");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region findJPModSchemaValueFordscpId+attribId
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest1()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(5, 7);
                if (res != 50)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest2()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(31, 2);
                if (res != 50)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest3()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(66, 8);
                if (res != 50)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest4()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(79, 9);
                if (res != 50)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest5()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(79, 10);
                if (res != 40)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest6()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(89, 11);
                if (res != 60)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest7()
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(101, 3);
                if (res != 30)
                    Assert.Fail("It gives wrong for dscpId and typic attribId");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest8()  //BAD DSCPID
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(0, 7);
                Assert.Fail("It gives for bad dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest9()  //BAD DSCPID
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(180, 7);
                Assert.Fail("It gives for bad dscpId");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest10()  //BAD ATTRIBID
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(77, 1);
                Assert.Fail("It gives for bad attribId");
            }
            catch (CoreModelException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheDscpAttribSchemValueTest11()  //BAD ATTRIBID
        {
            try
            {
                short res = dt.findTheDscpAttribSchemValue(34, 7);
                Assert.Fail("It gives for bad dscpId");
            }
            catch (CoreModelException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion
    }
}