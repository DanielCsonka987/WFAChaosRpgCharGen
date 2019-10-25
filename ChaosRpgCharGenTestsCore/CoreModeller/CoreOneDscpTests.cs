using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharModel.Tests
{

    [TestClass()]
    public class CoreOneDscpTests
    {
        #region SeekedRequirCombinToGroupId
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest1()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(1, 1, "Akrobatika", true, false);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 6 });
                if (res != 1)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch(Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest2()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(60, 4, "Csapdák eltávolítása", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 0 });
                if (res != 1)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest3()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(60, 4, "Csapdák eltávolítása", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 137 });
                if (res != 2)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest4()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(80, 5, "Áldozat/meditáció/felfrissülés", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 74 });
                if (res != 1)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest5()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(80, 5, "Áldozat/meditáció/felfrissülés", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 76 });
                if (res != 2)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest6()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(80, 5, "Áldozat/meditáció/felfrissülés", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 77 });
                if (res != 3)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest7()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(86, 5, "Rúnarajzolás", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 125, 152 });
                if (res != 1)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest8()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(86, 5, "Rúnarajzolás", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 125, 31 });
                if (res != 2)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest9()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(86, 5, "Rúnarajzolás", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 125, 158 });
                if (res != 3)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }

        
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest10()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(88, 5, "Varázsírás", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 76, 117, 135 });
                if (res != 1)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest11()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(88, 5, "Varázsírás", true, true);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 74, 117, 135 });
                if (res != 2)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest12()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(74, 5, "Elhivatottság", true, false);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 999 });
                if (res != 2)
                    Assert.Fail("Wrong requirGroupId arrived " + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest13()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(61, 5, "Elhivatottság", true, false);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 31 });
                Assert.Fail("A requirGroupId arrived for wrong resuirComb" + res);
            }
            catch (CoreModelException) { }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindSeekedDscpRequirIdTest14()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(61, 5, "Elhivatottság", true, false);
                byte res = dscp.findSeekedDscpRequirId(new short[] { 12, 34, 2000 });
                Assert.Fail("A requirGroupId arrived for wrong resuirComb" + res);
            }
            catch (CoreModelException) { }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion

        #region DscpAttribSchemaValueForAttribId
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest1()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(14, 0, "Lovaglás", true, false);
                short res = dscp.findTheSeekedAttribSchemaValue(5);
                if(res != 40)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest2()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(41, 3, "Fantomsebzés", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(4);
                if (res != 60)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest3()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(66, 4, "Méregkeverés", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(8);
                if (res != 50)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest4()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(82, 5, "Eszenciapótlás", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(10);
                if (res != 50)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest5()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(93, 5, "Zónaérzékelés", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(10);
                if (res != 60)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest6()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(93, 5, "Zónaérzékelés", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(7);
                if (res != 50)
                    Assert.Fail("Wrong attribScmeaValue for dscpAttribId" + res);
            }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest7()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(30, 2, "Kereskedő", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(0);
                Assert.Fail("The attribScmeaValue for bad dscpAttribId" + res);
            }
            catch (CoreModelException)    { }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest8()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(79, 5, "Aszkézis", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(5);
                Assert.Fail("The attribScmeaValue for bad dscpAttribId" + res);
            }
            catch (CoreModelException) { }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void CoreOneDscpFindTheSeekedAttribSchemaValueTest9()
        {
            try
            {
                CoreOneDscp dscp = new CoreOneDscp(161, 9, "Tetoválás", true, true);
                short res = dscp.findTheSeekedAttribSchemaValue(25);
                Assert.Fail("The attribScmeaValue for bad dscpAttribId" + res);
            }
            catch (CoreModelException)  { }
            catch (Exception e)
            {
                Assert.Fail("Error appeared " + e.Message + " " + e.TargetSite);
            }
        }
        #endregion
    }
}