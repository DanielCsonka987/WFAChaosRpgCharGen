using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.GeneralRepository;
using ChaosRpgCharGen.GeneralModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosRpgCharGen.CharModel;

namespace ChaosRpgCharGen.CharRepository1.Tests
{
    [TestClass()]
    public class CoreSystemCharactersTests
    {
        GeneralReviewTrunkRepo csc = new GeneralReviewTrunkRepo();

        [TestMethod()]
        public void ReviseProcessesTest()
        {
            try
            {
                //CREATE NEW
                GeneralOneTrunkEntity charct = new GeneralOneTrunkEntity(3, "Ember - gnóm", "TesztGener", "It's a test", 5, 4, "közepes", 6, 11444);
                short[] newAttrib = new short[] { 40, 40, 40, 40, 50, 50, 50, 54, 36, 23, 54, 35 };
                int resCharId = csc.addNewCharacterIntoSystem(charct, newAttrib);

                //REVISE THERE IS THAT
                List<GeneralOneTrunkEntity> tempList = csc.findTheSystemCharacters();
                if (tempList.Count != 3 || resCharId != 3)
                {
                    if (resCharId == 0)
                        Assert.Fail("Trunksave failed");
                    if (resCharId == -1)
                        Assert.Fail("StatSave failed");
                    if (resCharId != 3)
                        Assert.Fail("Wrong charId, to open it");
                }
                GeneralOneTrunkEntity temp = csc.getCharEntityToOpen(resCharId);
                if (temp == null)
                    Assert.Fail("It no sees tha seeked character");
                if (!(temp.theCharId == resCharId && temp.theCharName == "TesztGener"))
                    Assert.Fail("It no sees tha seeked character");
                
                //REMOVE THAT ONE
                bool res = csc.removeCharacterFromSystem(resCharId);
                List<GeneralOneTrunkEntity> list = csc.findTheSystemCharacters();
                if (list.Count != 2)
                {
                    if (!res)
                        Assert.Fail("Deletion unsucceed " + res.ToString());
                }
                
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }

        [TestMethod()]
        public void getCharEntityToOpenTest()
        {
            try
            {
                GeneralOneTrunkEntity temp = csc.getCharEntityToOpen(1);
                if (temp == null)
                    Assert.Fail("It no sees tha seeked character");
                if (!(temp.theCharId == 1 && temp.theCharName == "TesztElek"))
                    Assert.Fail("It no sees tha seeked character");
            }
            catch (Exception e)
            {
                Assert.Fail("Error happened " + e.Message + " " + e.TargetSite);
            }
        }
    }
}