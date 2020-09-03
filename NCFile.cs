using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NC_file_generator
{
    public class NCFile
    {
        public static void GenerateAllFiles(string path1) {
            string path2 = GetSecondFilePath(path1);
            const string TARGET_PATH = "M:\\4_ATTISTIBA\\II_BIM\\II-87\\0_Task\\NC_faili\\Testing";

            //Sakārto lai galvenais fails vienmēr būtu kā path1
            string MainFilePath = FindMainFile(path1, path2);
            if(MainFilePath == path2)
            {
                path2 = path1;
                path1 = MainFilePath;
            }

            //Reading Files
            List<string> file1Content = ReadFile(path1);
            List<string> file2Content = ReadFile(path2);

            if (file1Content.Count == 0)
            {
                MessageBox.Show("File " + path1 + " is empty", "Error", MessageBoxButtons.OK);
            }
            else if (file2Content.Count == 0)
            {
                MessageBox.Show("File " + path2 + " is empty", "Error", MessageBoxButtons.OK);
            }
            else {
                GenerateFile1(file1Content, file2Content, TARGET_PATH);
                GenerateFile2(file1Content, file2Content, TARGET_PATH);
                GenerateFile3(file1Content, file2Content, TARGET_PATH);
                GenerateFile4(file1Content, file2Content, TARGET_PATH);
                MessageBox.Show("Files successfully generated at: " + TARGET_PATH, "Success!", MessageBoxButtons.OK);
            }
        }

        public static string GetSecondFilePath(string filePath) {
            string SecondFilePath = "";
            string[] strArr = filePath.Split('.');
            if (strArr.Length >= 2)
            {
                if (strArr[strArr.Length - 1] == "nc")
                {
                    if (strArr[strArr.Length-2].Contains("_webs"))
                    {
                        int pos = strArr[strArr.Length - 2].IndexOf("_webs");
                        if (pos >= 0)
                        {
                            strArr[strArr.Length - 2] = strArr[strArr.Length - 2].Remove(pos);
                        }
                    }
                    else
                    {
                        strArr[strArr.Length - 2] += "_webs";
                    }
                    SecondFilePath = string.Join(".", strArr);
                }
            }
            return SecondFilePath;
        }

        public static string GetFileNameFromPath(string filePath) {
            string fileName;
            string[] tempArr = filePath.Split('\\');
            fileName = tempArr[tempArr.Length - 1];
            return fileName;
        }

        public static List<string> ReadFile(string path) {
            var fileContent = new List<string>();
            try
            {
                var fileStream = File.OpenRead(path);

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        fileContent.Add(reader.ReadLine());
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file could not be read: " + e.Message, "Error", MessageBoxButtons.OK);
            }
            return fileContent;
        }

        public static string FindMainFile(string path1, string path2)
        {
            string fileName1 = GetFileNameFromPath(path1);
            string fileName2 = GetFileNameFromPath(path2);
        
            if (fileName1.Contains("_webs"))
            {
                return path2;
            }
            else return path1;
        }

        private static void GenerateFile1(List<string> file1Content, List<string> file2Content, string targetPath)
        {
            string fileName = file1Content[3].Trim() + "-1.nc";
            //string fileNameWithoutExtension = file1Content[3].Trim() + "-1";
            string filePath = targetPath + "\\" + fileName;
            var TopPart = GenerateHeader(1, file1Content);
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach (string s in TopPart)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file" +fileName+ "could not be created: " + e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static void GenerateFile2(List<string> file1Content, List<string> file2Content, string targetPath)
        {
            string fileName = file1Content[3].Trim() + "-2.nc";
            //string fileNameWithoutExtension = file1Content[3].Trim() + "-1";
            string filePath = targetPath + "\\" + fileName;
            var TopPart = GenerateHeader(2, file1Content);
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach (string s in TopPart)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file" + fileName + "could not be created: " + e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static void GenerateFile3(List<string> file1Content, List<string> file2Content, string targetPath)
        {
            string fileName = file1Content[3].Trim() + "-3.nc";
            //string fileNameWithoutExtension = file1Content[3].Trim() + "-1";
            string filePath = targetPath + "\\" + fileName;
            var TopPart = GenerateHeader(3, file1Content);
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach (string s in TopPart)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file" + fileName + "could not be created: " + e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static void GenerateFile4(List<string> file1Content, List<string> file2Content, string targetPath)
        {
            string fileName = file1Content[3].Trim() + "-4.nc";
            //string fileNameWithoutExtension = file1Content[3].Trim() + "-1";
            string filePath = targetPath + "\\" + fileName;
            var TopPart = GenerateHeader(4, file1Content);
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    foreach (string s in TopPart)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file" + fileName + "could not be created: " + e.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private static List<string> GenerateHeader(int nr, List<string> file1Content)
        {
            var Header = new List<string>();
            string fullProfile = file1Content[8];
            Header.Add(file1Content[0]);
            string[] strArr = file1Content[1].Split('.');
            strArr[strArr.Length - 2] += "-" + nr.ToString();
            string temp = string.Join(".",strArr);
            Header.Add(temp);
            Header.Add(file1Content[2]);
            Header.Add(file1Content[3]+"-" + nr.ToString());
            Header.Add(file1Content[4]);
            Header.Add(file1Content[5]+"-" + nr.ToString());
            Header.Add(file1Content[6]);
            Header.Add(file1Content[7]);
            
            string profile = GetPlateProfile(fullProfile, nr);
            Header.Add(profile);
            
            Header.Add("  B");
            Header.Add(file1Content[10]);
            Header.Add(GetProfileHeight(fullProfile, nr));
            
            string thickness = GetThickness(fullProfile, nr);
            Header.Add(thickness);
            Header.Add(thickness);
            Header.Add(thickness);
            Header.Add(file1Content[15]);
            Header.Add(file1Content[16]);
            Header.Add(file1Content[17]);
            Header.Add(file1Content[18]);
            Header.Add(file1Content[19]);
            Header.Add(file1Content[20]);
            Header.Add(file1Content[21]);
            Header.Add(file1Content[22]);
            Header.Add(file1Content[23]);
            Header.Add(file1Content[24]);
            Header.Add(file1Content[25]);
            return Header;
        }

        public static string GetPlateProfile(string fullProfile, int nr)
        {
            string plateProfile;
            string[] strArr = fullProfile.Split('*');
            if (nr != 4)
            {
                string[] temp = strArr[1].Split('-');
                if (nr != 3)
                {
                    plateProfile = "  PL" + temp[0];
                }
                else
                {
                    plateProfile = "  PL" + temp[1];
                }
            }
            else
            {
                string[] temp = strArr[2].Split('-');
                plateProfile = "  PL" + temp[1];
            }

            return plateProfile;
        }

        public static string GetProfileHeight(string fullProfile, int nr)
        {
            string ProfileHeight;
            string[] strArr = fullProfile.Split('*');
            if (nr != 4)
            {
                if (nr != 3)
                {
                    string[] temp = strArr[0].Split('Q');
                    double profHeightTemp = double.Parse(temp[1]);
                    profHeightTemp = profHeightTemp - GetProfileHeightDiff(fullProfile);
                    profHeightTemp = Math.Round(profHeightTemp, 2);
                    ProfileHeight = profHeightTemp.ToString();
                    if (!ProfileHeight.Contains(".")) ProfileHeight += ".00";
                }
                else
                {
                    string[] temp = strArr[2].Split('-');
                    ProfileHeight = temp[0] + ".00";
                }
            }
            else
            {
                string[] temp = strArr[3].Split('-');
                ProfileHeight = temp[0] + ".00";
            }
            var repeatWhitespaceCount = 11 - ProfileHeight.Length;
            var whitespace = new string(' ', repeatWhitespaceCount);
            ProfileHeight = whitespace + ProfileHeight;

            return ProfileHeight;
        }

        public static double GetProfileHeightDiff(string fullProfile)
        {
            double HeightDiff;
            //Izmantojot dotu formulu
            string[] strArr = fullProfile.Split('*');
            string tempString = strArr[1];
            int temp1 = int.Parse(tempString.Split('-')[1]);
            string tempString1 = strArr[3];
            int temp2 = int.Parse(tempString1.Split('-')[1]);

            //AK6 = temp1
            //AK14 = temp2
            //no excel
            //=IF(AK6<=10;6;IF((AK14*1.4)<=10;10;IF(((AK14*1.4)-MOD(AK14*1.4;5)+5)>=AK6;AK6;(AK14*1.4)-MOD(AK14*1.4;5)+5)))
            if(temp1<= 10)
            {
                HeightDiff = 6;
            }
            else
            {
                if(temp2*1.4 <= 10)
                {
                    HeightDiff = 10;
                }
                else
                {
                    if(temp2*1.4 - (temp2*1.4)%5 + 5 >= temp1)
                    {
                        HeightDiff = temp1;
                    }
                    else
                    {
                        HeightDiff = temp2 * 1.4 - (temp2 * 1.4) % 5 + 5;
                    }
                }
            }
            return HeightDiff;
        }

        public static string GetThickness(string fullProfile, int nr)
        {
            string thickness;
            string[] strArr = fullProfile.Split('*');
            if (nr != 4)
            {
                string[] temp = strArr[1].Split('-');
                if (nr != 3)
                {
                    thickness = temp[0] + ".00";
                }
                else
                {
                    thickness = temp[1] + ".00";
                }
            }
            else
            {
                string[] temp = strArr[2].Split('-');
                thickness = temp[1] + ".00";
            }

            var repeatWhitespaceCount = 11 - thickness.Length;
            var whitespace = new string(' ', repeatWhitespaceCount);
            thickness = whitespace + thickness;
            return thickness;
        }
    }
}
