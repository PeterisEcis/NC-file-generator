using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NC_file_generator;

namespace NCTest
{
    [TestClass]
    public class SecondFilePath
    {

        [TestMethod]
        public void TestSecondFilePath1()
        {
            string TestPath1 = "abc.nc";
            string ExpectedPath1 = "abc_webs.nc";
            string newPath1 = NCFile.GetSecondFilePath(TestPath1);
            Assert.AreEqual(ExpectedPath1, newPath1, "1");
        }

        [TestMethod]
        public void TestSecondFilePath2()
        {
            string TestPath2 = "a.b.c.nc";
            string ExpectedPath2 = "a.b.c_webs.nc";
            string newPath2 = NCFile.GetSecondFilePath(TestPath2);
            Assert.AreEqual(ExpectedPath2, newPath2, "2");
        }

        [TestMethod]
        public void TestSecondFilePath3()
        {
            string TestPath3 = "abc_webs.nc";
            string ExpectedPath3 = "abc.nc";
            string newPath3 = NCFile.GetSecondFilePath(TestPath3);
            Assert.AreEqual(ExpectedPath3, newPath3, "3");
        }

        [TestMethod]
        public void TestSecondFilePath4()
        {
            string TestPath4 = "ab._webs.cd.nc";
            string ExpectedPath4 = "ab._webs.cd_webs.nc";
            string newPath4 = NCFile.GetSecondFilePath(TestPath4);
            Assert.AreEqual(ExpectedPath4, newPath4, "4");
        }

        [TestMethod]
        public void TestSecondFilePath5()
        {
            string TestPath5 = ".nc";
            string ExpectedPath5 = "_webs.nc";
            string newPath5 = NCFile.GetSecondFilePath(TestPath5);
            Assert.AreEqual(ExpectedPath5, newPath5, "5");
        }

        [TestMethod]
        public void TestSecondFilePath6()
        {
            string TestPath6 = "";
            string ExpectedPath6 = "";
            string newPath6 = NCFile.GetSecondFilePath(TestPath6);
            Assert.AreEqual(ExpectedPath6, newPath6, "6");
        }

        [TestMethod]
        public void TestSecondFilePath7()
        {
            string TestPath7 = "\\Desktop\\Something\\LedZeppelin_webs.nc";
            string ExpectedPath7 = "\\Desktop\\Something\\LedZeppelin.nc";
            string newPath7 = NCFile.GetSecondFilePath(TestPath7);
            Assert.AreEqual(ExpectedPath7, newPath7, "7");
        }
    }

    [TestClass]
    public class FileName
    {
        [TestMethod]
        public void TestFileName1()
        {
            string testPath1 = "M:\\4_ATTISTIBA\\II_BIM\\II-87\\0_Task\\NC_faili\\RG1D-3.nc";
            string newName1 = NCFile.GetFileNameFromPath(testPath1);
            string ExpectedName1 = "RG1D-3.nc";
            Assert.AreEqual(ExpectedName1, newName1, "1");

        }

        [TestMethod]
        public void TestFileName2()
        {
            string testPath2 = "test.nc";
            string ExpectedName2 = "test.nc";
            string newName2 = NCFile.GetFileNameFromPath(testPath2);
            Assert.AreEqual(ExpectedName2, newName2, "2");
        }

        [TestMethod]
        public void TestFileName3()
        {
            string testPath3 = "";
            string ExpectedName3 = "";
            string newName3 = NCFile.GetFileNameFromPath(testPath3);
            Assert.AreEqual(ExpectedName3, newName3, "3");
        }

        [TestMethod]
        public void TestFileName4()
        {
            string testPath4 = "45";
            string ExpectedName4 = "45";
            string newName4 = NCFile.GetFileNameFromPath(testPath4);
            Assert.AreEqual(ExpectedName4, newName4, "4");
        }
    }

    [TestClass]
    public class ReadFile
    {
        [TestMethod]
        public void FileNotEmpty()
        {
            var fileName = "C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\test.nc";
            string ExpectedOutput = "123321abcd";
            var TempResult = NCFile.ReadFile(fileName);
            string result = string.Join("", TempResult);
            Assert.AreEqual(ExpectedOutput, result);
        }

        [TestMethod]
        public void FileEmpty()
        {
            var fileName = "C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\test_webs.nc";
            var result = NCFile.ReadFile(fileName);
            Assert.AreEqual(0, result.Count);
        }
    }

    [TestClass]
    public class MainFile
    {
        [TestMethod]
        public void FindMainFile()
        {
            var fileName1 = "test_webs.nc";
            var fileName2 = "test.nc";
            var result = NCFile.FindMainFile(fileName1, fileName2);
            Assert.AreEqual(fileName2, result);
        }
    }

    [TestClass]
    public class Header
    {
        [TestMethod]
        public void GetProfileForFile1()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string Expected = "  PL8";
            string result = NCFile.GetPlateProfile(fullProfile, 1);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileForFile3()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string Expected = "  PL25";
            string result = NCFile.GetPlateProfile(fullProfile, 3);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileForFile4()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string Expected = "  PL20";
            string result = NCFile.GetPlateProfile(fullProfile, 4);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileHeightForFile1()
        {
            string fullProfile = "  UHQ320*6-15*150-10*317-4-15";
            string Expected = "     310.00";
            string result = NCFile.GetProfileHeight(fullProfile, 1);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileHeightForFile3()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string Expected = "     200.00";
            string result = NCFile.GetProfileHeight(fullProfile, 3);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileHeightForFile4()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string Expected = "     496.00";
            string result = NCFile.GetProfileHeight(fullProfile, 4);
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void GetProfileHeightDiff()
        {
            string fullProfile = "UHQ320*6-15*150-10*317-4-15";
            string fullProfile1 = "UHQ320*6-10*150-10*317-4-15";
            string fullProfile2 = "UHQ320*6-33*150-10*317-14-15";
            double expected = 10;
            double expected1 = 6;
            double expected2 = 20;
            double result = NCFile.GetProfileHeightDiff(fullProfile);
            double result1 = NCFile.GetProfileHeightDiff(fullProfile1);
            double result2 = NCFile.GetProfileHeightDiff(fullProfile2);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod]
        public void GetThicknessForFile1()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string expected = "       8.00";
            string result = NCFile.GetThickness(fullProfile, 1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetThicknessForFile3()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string expected = "      25.00";
            string result = NCFile.GetThickness(fullProfile, 3);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetThicknessForFile4()
        {
            string fullProfile = "  UHQ320*8-25*200-20*496-6";
            string expected = "      20.00";
            string result = NCFile.GetThickness(fullProfile, 4);
            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]

    public class TargetDir
    {
        [TestMethod]
        public void GetDirectoryFromPath()
        {
            string path = "M:\\4_ATTISTIBA\\II_BIM\\II-87\\0_Task\\NC_faili\\RG1D-3.nc";
            string expected = "M:\\4_ATTISTIBA\\II_BIM\\II-87\\0_Task\\NC_faili";
            string result = NCFile.GetDirectoryFromPath(path);
            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]

    public class DataGeneration
    {
        [TestMethod]
        public void GetLineNr2()
        {
            var fileContent = NCFile.ReadFile("C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\RG1D-22.nc");
            int expected = 26;
            int result = NCFile.GetLineNr(2, fileContent);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetLineNr3()
        {
            var fileContent = NCFile.ReadFile("C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\RG1D-22.nc");
            int expected = 32;
            int result = NCFile.GetLineNr(3, fileContent);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetLineNr4()
        {
            var fileContent = NCFile.ReadFile("C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\RG1D-22.nc");
            int expected = 38;
            int result = NCFile.GetLineNr(4, fileContent);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetLineNr1()
        {
            var fileContent = NCFile.ReadFile("C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\RG1D-22.nc");
            int expected = 44;
            int result = NCFile.GetLineNr(1, fileContent);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetLineNr()
        {
            var fileContent = NCFile.ReadFile("C:\\Users\\peteris.ecis\\Documents\\GitHub\\NC-file-generator\\Testing\\RG1D-22.nc");
            int expected = -1;
            int result = NCFile.GetLineNr(5, fileContent);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPosition()
        {
            string line = "  o       0.00s     21.00       0.00       0.00       0.00       0.00       0.00";
            int expected = 20;
            int result = NCFile.GetPosition(line);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReadCoord()
        {
            string line = "  o       0.00s     21.00       0.00       0.00       0.00       0.00       0.00";
            double expected = 21.00;
            double result = NCFile.ReadCoord(line);
            Assert.AreEqual(expected, result);
        }

        //[TestMethod]

        //public void ChangeCoord()
        //{
        //    var data = new List<string>();
        //    data.Add("  v       0.00o     10.00       0.00       0.00       0.00       0.00       0.00");
        //    data.Add("      12064.00      10.00       0.00       0.00       0.00       0.00       0.00");
        //    data.Add("      12064.00     324.00       0.00       0.00       0.00       0.00       0.00");
        //    data.Add("          0.00     324.00       0.00       0.00       0.00       0.00       0.00");
        //    data.Add("          0.00      10.00       0.00       0.00       0.00       0.00       0.00");
        //    var expected = new List<string>();
        //    expected.Add("  v       0.00o      0.00       0.00       0.00       0.00       0.00       0.00");
        //    expected.Add("      12064.00       0.00       0.00       0.00       0.00       0.00       0.00");
        //    expected.Add("      12064.00     314.00       0.00       0.00       0.00       0.00       0.00");
        //    expected.Add("          0.00     314.00       0.00       0.00       0.00       0.00       0.00");
        //    expected.Add("          0.00       0.00       0.00       0.00       0.00       0.00       0.00");
        //    var result = NCFile.ChangeCoordinates(data);
        //    Assert.AreEqual(expected, result);
        //}
    }
}
