using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CharRepository1;
using ChaosRpgCharGen.CharModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1.Tests
{
    [TestClass()]
    public class CharJPGainingTests
    {
        [TestMethod()]
        public void getTheJPGainCollectionTest()
        {
            try
            {
                CharJPGaining jpgain = new CharJPGaining(1);
                List<CharOneJPGain> temp = jpgain.getTheJPGainCollection();
                if (temp.Count != 2)
                    Assert.Fail("Wrong amount of JP-gain record " + temp.Count);
            }
            catch(CharRepositoryException e)
            {
                Assert.Fail("Error happened " +e.Message + " " + e.TargetSite);
            }
        }
        [TestMethod()]
        public void manageJPGainTest()
        {
            CharJPGaining jp = new CharJPGaining(1);
            int amount = jp.getTheJPGainCollection().Count;

            jp.addNewJPGain(2300);
            List<CharOneJPGain> temp = jp.getTheJPGainCollection();
            int newAmount = temp.Count;
            if(newAmount != amount + 1)
                Assert.Fail("Wrong amount after save new JP-portion");
            int listIndex = temp.FindIndex(x => x.theJPAmount == 2300);
            if (listIndex == -1)
                Assert.Fail("Not found the new JP-portion");
            int newJPGainIndex = temp[listIndex].theGainingId;
            if (newJPGainIndex != 3)
                Assert.Fail("Wrong JPGain index at new JP-portion");
            
            jp.removeThisJPGain(newJPGainIndex);
            temp = jp.getTheJPGainCollection();
            if (temp.Count != 2)
                Assert.Fail("The remove is failed");
            if (    (temp.FindIndex(x => x.theGainingId == newJPGainIndex) != -1) ||
                    (temp.FindIndex(x => x.theJPAmount == 2300) != -1)  )
                Assert.Fail("The remove is really failed");
                
        }


        [TestMethod()]
        public void sumAllCollectedJPTest()
        {
            //PRIMER MEASURE
            CharJPGaining jp = new CharJPGaining(1);
            int sum = jp.sumAllCollectedJP();
            if(sum != 13100)
                Assert.Fail("Wrong summarised JP amount");

            //ADD NEW JP AND SEC MEASURE
            jp.addNewJPGain(2300);
            sum = jp.sumAllCollectedJP();
            if (sum != 15400)
                Assert.Fail("Wrong sum arrived after adding");

            //REMOVE AND MEASURE AGAIN
            int index = jp.getTheJPGainCollection().Find(x => x.theJPAmount == 2300).theGainingId;
            jp.removeThisJPGain(index);
            sum = jp.sumAllCollectedJP();
            if (sum != 13100)
                Assert.Fail("Wrong sum arrived after removing");
        }
    }
}