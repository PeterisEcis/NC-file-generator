using System;
using System.Collections.Generic;
using System.Linq;
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
}
