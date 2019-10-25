using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository3.Tests
{
    [TestClass()]
    public class CoreJPLevelsSchemasTests
    {
        #region NormalLevelAverageJPCost
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(1);
                if (res != 200)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(3);
                if (res != 550)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(4);
                if (res != 900)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest4()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(10);
                if (res != 3500)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(7);
                if (res != 1950)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelAverageJPTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelAverageJP(11);
                Assert.Fail("It gives JPValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region NormalLevelBenefJPCost
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(1);
                if (res != 150)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(6);
                if (res != 1250)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(8);
                if (res != 2000)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest4()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(9);
                if (res != 1450)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(2);
                if (res != 250)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheNormalLevelBeneficJPTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheNormalLevelBeneficJP(14);
                Assert.Fail("It gives JPValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region SpecLevelAveragJPCost
        [TestMethod()]
        public void findTheSpecLevelAverageJPTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelAverageJP(1);
                if (res != 600)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecLevelAverageJPTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelAverageJP(2);
                if (res != 900)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecLevelAverageJPTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelAverageJP(11);
                Assert.Fail("It gives JPValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region SpecLevelBenefJPCost
        [TestMethod()]
        public void findTheSpecLevelBeneficJPTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelBeneficJP(1);
                if (res != 500)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecLevelBeneficJPTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelBeneficJP(2);
                if (res != 750)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSpecLevelBeneficJPTest()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSpecLevelBeneficJP(14);
                Assert.Fail("It gives JPValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region JPSchemaRiserForNormalLevel
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(1);
                if (res != 0)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(4);
                if (res != 10)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(7);
                if (res != 20)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest4()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(10);
                if (res != 30)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(6);
                if (res != 10)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(9);
                if (res != 20)
                    Assert.Fail("It gives wrong SchemaRiserValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheSchemaRiserForThisNormalLevelTest7()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheSchemaRiserForThisNormalLevel(21);
                Assert.Fail("It gives SchemaRiserValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region DscpRequirementRiserForSpecLevel
        [TestMethod()]
        public void findTheRequirementRiserForThisSpecLevelTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheRequirementRiserForThisSpecLevel(1);
                if (res != 0)
                    Assert.Fail("It gives wrong LevelValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheRequirementRiserForThisSpecLevelTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheRequirementRiserForThisSpecLevel(2);
                if (res != 1)
                    Assert.Fail("It gives wrong SchemaValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheRequirementRiserForThisSpecLevelTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheRequirementRiserForThisSpecLevel(12);
                Assert.Fail("It gives SchemaValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region AttribRiserForForSpecLevels
        [TestMethod()]
        public void findTheAttributeRiserForThisSpecTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheAttributeRiserForThisSpec(1);
                if (res != 0)
                    Assert.Fail("It gives wrong JPValue for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheAttributeRiserForThisSpecTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheAttributeRiserForThisSpec(2);
                if (res != 10)
                    Assert.Fail("It gives wrong JPValue for levelMark " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findTheAttributeRiserForThisSpecTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findTheAttributeRiserForThisSpec(22);
                if (res != 0)
                    Assert.Fail("It gives JPValue for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region SchemaModifierForEachLevels
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(10, 50);
                if (res != -30)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(100, 50);
                if (res != 100)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(50, 76);
                if (res != -15)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest14()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(70, 57);
                if (res != 30)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(40, 31);
                if (res != 15)
                    Assert.Fail("It gives wrong JPModifier for levelMark " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findSchemaModifForThisJPSchemaTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findSchemaModifForThisJPSchema(111, 300);
                Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region PracticeModifForEachLevels
        [TestMethod()]
        public void findPracticeModifForThisLevelTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(10);
                if (res != 0)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(2);
                if (res != 100)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(3);
                if (res != 100)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest4()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(7);
                if (res != 0)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(6);
                if (res != 50)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(5);
                if (res != 50)
                    Assert.Fail("It gives wrong JPModifier for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findPracticeModifForThisLevelTest7()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findPracticeModifForThisLevel(12);
                Assert.Fail("It gives  JPModifier for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region AttributeFeedbackValuesForEachNormalLevel
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(8);
                if (res != 2)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(10);
                if (res != 3)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(3);
                if (res != 0)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest4()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(5);
                if (res != 1)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest5()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(2);
                if (res != 0)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest6()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(4);
                if (res != 1)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisNormalLevelTest7()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisNormalLevel(18);
                Assert.Fail("It gives AttribFeedbackType for bad levelMark");
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region AttributeFeedbackValuesForEachSpecLevel
        [TestMethod()]
        public void findThereAttribFeedbackForThisSpecLevelTest1()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisSpecLevel(1);
                if (res != 0)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisSpecLevelTest2()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisSpecLevel(2);
                if (res != 1)
                    Assert.Fail("It gives wrong AttribFeedbackType for levelMark");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happend " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void findThereAttribFeedbackForThisSpecLevelTest3()
        {
            try
            {
                CoreJPLevelsSchemasRepo jls = new CoreJPLevelsSchemasRepo();
                short res = jls.findThereAttribFeedbackForThisSpecLevel(4);
                Assert.Fail("It gives AttribFeedbackType for bad levelMark");
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