using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaosRpgCharGen.CoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosRpgCharGen.CharRepository1.Tests
{
    [TestClass()]
    public class CoreAttribsRacesSizesTests
    {
        #region NameOfAttrib
        [TestMethod()]
        public void findTheNameOfThisAttribTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(1);
                if (text != "Fizikum")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(2);
                if (text != "Erő")
                    Assert.Fail("It found wrong attibName");
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(3);
                if (text != "Szívósság")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(4);
                if (text != "Rátermettség")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(5);
                if (text != "Ügyesség")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(6);
                if (text != "Reflex")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(7);
                if (text != "Tudat")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest8()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(8);
                if (text != "Inteligencia")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest9()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(9);
                if (text != "Lelkierő")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest10()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(10);
                if (text != "Eszencia")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest11()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(11);
                if (text != "Varázserő")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest12()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(12);
                if (text != "Eszenciapajzs")
                    Assert.Fail("It found wrong attibName " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisAttribTest13()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisAttrib(13);
                Assert.Fail("It did not give error for wrong parameter " + text);
            }
            catch (CoreRepositoryException) {  }
            catch (Exception e)
            {
                Assert.Fail("It stopped by exception " + e.Message);
            }
        }
        #endregion

        #region IdOfAttribName
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Fizikum");
                if (id != 1)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Erő");
                if (id != 2)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Szívósság");
                if (id != 3)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Rátermettség");
                if (id != 4)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Ügyesség");
                if (id != 5)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Reflex");
                if (id != 6)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Tudat");
                if (id != 7)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest8()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Inteligencia");
                if (id != 8)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest9()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Lelkierő");
                if (id != 9)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest10()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Eszencia");
                if (id != 10)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest11()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Varázserő");
                if (id != 11)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest12()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Eszenciapajzs");
                if (id != 12)
                    Assert.Fail("It gives the wrong id " + id);
            }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheAttriIdThisAttribNameTest13()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte id = ars.findTheAttriIdThisAttribName("Valami");
                Assert.Fail("It gives an id for a badone " + id);
            }
            catch (CoreRepositoryException){ }
            catch (Exception e)
            {
                Assert.Fail("It stopped by excepion " + e.Message);
            }
        }

        #endregion

        #region NameOfRaceId

        [TestMethod()]
        public void findTheNameOfThisRaceTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(1);
                if (text != "elf(driád)")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(2);
                if (text != "elf")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(3);
                if (text != "ember")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(4);
                if (text != "gennymanó")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(5);
                if (text != "gilf")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(6);
                if (text != "gnóm")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(7);
                if (text != "gyíklény")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest8()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(8);
                if (text != "manó")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest9()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(9);
                if (text != "myor")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest10()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(10);
                if (text != "ogár")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest11()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(11);
                if (text != "ork")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest12()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(12);
                if (text != "roal")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest13()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(13);
                if (text != "törpe")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest14()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(14);
                if (text != "tündérke")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest15()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(15);
                if (text != "tündérmanó")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest16()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(16);
                if (text != "Elf(driád apa)-ember")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest17()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(17);
                if (text != "Elf-gilf")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest18()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(18);
                if (text != "Elf-gnóm")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest19()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(19);
                if (text != "Elf-manó")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest20()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(20);
                if (text != "Elf-ork")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest21()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(21);
                if (text != "Elf-törpe")
                    Assert.Fail("No good raceName for the raceId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisRaceTest22()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisRace(888);
                Assert.Fail("It gives raceName for bad raceId " + text);
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        #endregion

        #region StarterStatsOfRaceId

        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(1);
                if (res[0] != 80 && res[1] != 80 && res[2] != 90 && res[3] != 90 && res[4] != 3)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(2);
                if (res[0] != 70 && res[1] != 90 && res[2] != 90 && res[3] != 90 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(3);
                if (res[0] != 80 && res[1] != 80 && res[2] != 80 && res[3] != 90 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(4);
                if (res[0] != 50 && res[1] != 100 && res[2] != 80 && res[3] != 70 && res[4] != 1)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(5);
                if (res[0] != 90 && res[1] != 90 && res[2] != 90 && res[3] != 100 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(6);
                if (res[0] != 60 && res[1] != 80 && res[2] != 90 && res[3] != 80 && res[4] != 2)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(7);
                if (res[0] != 70 && res[1] != 90 && res[2] != 70 && res[3] != 70 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest8()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(8);
                if (res[0] != 50 && res[1] != 70 && res[2] != 50 && res[3] != 70 && res[4] != 2)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest9()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(9);
                if (res[0] != 100 && res[1] != 100 && res[2] != 100 && res[3] != 100 && res[4] != 5)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest10()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(10);
                if (res[0] != 120 && res[1] != 70 && res[2] != 50 && res[3] != 70 && res[4] != 5)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest11()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(11);
                if (res[0] != 90 && res[1] != 70 && res[2] != 60 && res[3] != 70 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest12()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(12);
                if (res[0] != 80 && res[1] != 80 && res[2] != 80 && res[3] != 100 && res[4] != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheStarterStatsOfThisRaceTest13()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                short[] res = ars.findTheStarterStatsOfThisRace(888);
                Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }

        #endregion

        #region SizeIndicatorOfSizeId
        [TestMethod()]
        public void findTheSizeOfThisRaceTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(1);
                if (res != 3)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }

        [TestMethod()]
        public void findTheSizeOfThisRaceTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(2);
                if (res != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(26);
                if (res != 2)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(23);
                if (res != 4)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(4);
                if (res != 1)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(9);
                if (res != 5)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(14);
                if (res != 1)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest8()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(16);
                if (res != 3)
                    Assert.Fail("No good raceDetails for the raceId " + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest9()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(666);
                Assert.Fail("It gives size for bad raceId " + res.ToString());
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheSizeOfThisRaceTest10()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.findTheSizeOfThisRace(0);
                Assert.Fail("It gives size for bad raceId " + res.ToString());
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }

        #endregion

        #region SizeNameOfSizeId
        [TestMethod()]
        public void findTheNameOfThisSizeTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(1);
                if (text != "apró")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(2);
                if (text != "kicsi")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(3);
                if (text != "közepes")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(4);
                if (text != "közepes")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(5);
                if (text != "nagy")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(6);
                if (text != "Hatalmas")
                    Assert.Fail("No good raceSizeName for the sizeId " + text);
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void findTheNameOfThisSizeTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                string text = ars.findTheNameOfThisSize(66);
                Assert.Fail("It given result for bad sizeId " + text);
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }

        #endregion

        #region BasicQuicknessOfSiteId

        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest1()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(1);
                if(res != 4)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest2()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(2);
                if (res != 5)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest3()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(3);
                if (res != 5)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest4()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(4);
                if (res != 6)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest5()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(5);
                if (res != 8)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest6()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(6);
                if (res != 10)
                    Assert.Fail("Not good quickness value for sizeId" + res.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        [TestMethod()]
        public void fintTheBasicQuicknessOfThisSizeTest7()
        {
            try
            {
                CoreAttribsRacesSizesRepo ars = new CoreAttribsRacesSizesRepo();
                byte res = ars.fintTheBasicQuicknessOfThisSize(88);
                Assert.Fail("It gives quickness value for bad sizeId" + res.ToString());
            }
            catch (CoreRepositoryException) { }
            catch (Exception e)
            {
                Assert.Fail("It made error " + e.Message);
            }
        }
        #endregion

    }
}