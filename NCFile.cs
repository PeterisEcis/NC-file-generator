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
        public static bool GenerateAllFiles(string path1, string path2, string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                targetDir = GetDirectoryFromPath(path1);
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
            else
            {
                bool flag = true;
                for (int i = 1; i < 5; i++)
                {
                    if (!GenerateFile(file1Content, file2Content, targetDir, i))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    return true;
                }
            }
        return false;
        }

        public static string GetSecondFilePath(string filePath)
        {
            string SecondFilePath = "";
            string[] strArr = filePath.Split('.');
            if (strArr.Length >= 2)
            {
                if (strArr[strArr.Length - 1] == "nc")
                {
                    if (strArr[strArr.Length - 2].Contains("_webs"))
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

        public static string GetFileNameFromPath(string filePath)
        {
            string fileName;
            string[] tempArr = filePath.Split('\\');
            fileName = tempArr[tempArr.Length - 1];
            return fileName;
        }

        public static string GetDirectoryFromPath(string filePath)
        {
            string DirPath;
            string[] strArr = filePath.Split('\\');
            Array.Resize(ref strArr, strArr.Length - 1);
            DirPath = string.Join("\\", strArr);
            return DirPath;
        }

        public static List<string> ReadFile(string path)
        {
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

        private static bool GenerateFile(List<string> file1Content, List<string> file2Content, string targetPath, int nr)
        {
            string fileName = file1Content[3].Trim() + "-" + nr.ToString() + ".nc";
            string filePath = targetPath + "\\" + fileName;
            var TopPart = GenerateHeader(nr, file1Content);
            var data = new List<string>();
            var BOData = new List<string>();
            if (nr == 1 || nr == 2)
            {
                data = GetMainDataForFile(nr, file2Content);
                BOData = GetBOData(nr, file2Content);
            }
            else
            {
                data = GetMainDataForFile(nr, file1Content);
                BOData = GetBOData(nr, file1Content);
            }
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
                    foreach (string s in data)
                    {
                        sw.WriteLine(s);
                    }
                    foreach (string s in BOData)
                    {
                        sw.WriteLine(s);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("The file" + fileName + "could not be created: " + e.Message, "Error", MessageBoxButtons.OK);
                return false;
            }
        }


        private static List<string> GenerateHeader(int nr, List<string> file1Content)
        {
            var Header = new List<string>();
            string fullProfile = file1Content[8];
            Header.Add(file1Content[0]);
            string[] strArr = file1Content[1].Split('.');
            strArr[strArr.Length - 2] += "-" + nr.ToString();
            string temp = string.Join(".", strArr);
            Header.Add(temp);
            Header.Add(file1Content[2]);
            Header.Add(file1Content[3] + "-" + nr.ToString());
            Header.Add(file1Content[4]);
            Header.Add(file1Content[5] + "-" + nr.ToString());
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
            int lineNr = 15;
            string line = file1Content[lineNr];
            while(line.Length <2 || (line[0] != 'A' && line[1] != 'K'))
            {
                Header.Add(line);
                lineNr++;
                line = file1Content[lineNr];
            }
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
                    double profHeightTemp = RemoveLettersFromProfile(strArr[0]);
                    profHeightTemp -= GetProfileHeightDiff(fullProfile);
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
            double temp1 = double.Parse(tempString.Split('-')[1]);
            string tempString1 = strArr[3];
            double temp2 = double.Parse(tempString1.Split('-')[1]);

            //AK6 = temp1
            //AK14 = temp2
            //no excel
            //=IF(AK6<=10;6;IF((AK14*1.4)<=10;10;IF(((AK14*1.4)-MOD(AK14*1.4;5)+5)>=AK6;AK6;(AK14*1.4)-MOD(AK14*1.4;5)+5)))
            if (temp1 <= 10)
            {
                HeightDiff = 6;
            }
            else
            {
                if (temp2 * 1.4 <= 10)
                {
                    HeightDiff = 10;
                }
                else
                {
                    if (temp2 * 1.4 - (temp2 * 1.4) % 5 + 5 >= temp1)
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

        public static List<string> GetMainDataForFile(int nr, List<string> fileContent)
        {
            var data = new List<string>();
            int lineNr = GetLineNr(nr, fileContent);
            data.Add(fileContent[lineNr]);
            string temp = fileContent[++lineNr];
            temp = temp.Remove(2, 1).Insert(2, "v");
            if (char.IsLetter(temp[14]))
            {
                temp = temp.Remove(14, 1).Insert(14, "u");
            }
            if (char.IsLetter(temp[25]))
            {
                temp = temp.Remove(25, 1).Insert(25, " ");
            }
            data.Add(temp);
            string line = fileContent[++lineNr];
            while ((line[0] != 'A' && line[1] != 'K') && (line[0] != 'B' && line[1] != 'O'))
            {
                if (char.IsLetter(line[25]))
                {
                    line = line.Remove(25, 1).Insert(25, " ");
                }
                data.Add(line);
                lineNr++;
                line = fileContent[lineNr];
            }
            return data;
        }
        public static List<string> GetBOData(int nr, List<string> fileContent)
        {
            var data = new List<string>();
            int lineNr = GetBOLineNr(nr, fileContent);
            if (lineNr != -1)
            {
                data.Add(fileContent[lineNr]);
                string line = fileContent[++lineNr];
                while ((line[0] != 'B' && line[1] != 'O') && (line[0] != 'E' && line[1] != 'N'))
                {
                    line = line.Remove(2, 1).Insert(2, "v");
                    data.Add(line);
                    lineNr++;
                    line = fileContent[lineNr];
                }
                data.Add("EN");
            }
            else
            {
                data.Add("EN");
            }
            return data;
        }

        public static int GetLineNr(int nr, List<string> fileContent)
        {
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Length >= 2)
                {
                    string temp = fileContent[i];
                    if (temp[0] == 'A' && temp[1] == 'K')
                    {
                        string temp1 = fileContent[i + 1];
                        if (temp1.Length >= 3)
                        {
                            if (nr == 1 && temp1[2] == 'h') return i;
                            if (nr == 2 && temp1[2] == 'v') return i;
                            if (nr == 3 && temp1[2] == 'o') return i;
                            if (nr == 4 && temp1[2] == 'u') return i;
                        }
                    }
                }
            }
            return -1;
        }

        public static int GetBOLineNr(int nr, List<string> fileContent)
        {
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Length >= 2)
                {
                    string temp = fileContent[i];
                    if (temp[0] == 'B' && temp[1] == 'O')
                    {
                        string temp1 = fileContent[i + 1];
                        if (temp1.Length >= 3)
                        {
                            if (nr == 1 && temp1[2] == 'h') return i;
                            if (nr == 2 && temp1[2] == 'v') return i;
                            if (nr == 3 && temp1[2] == 'o') return i;
                            if (nr == 4 && temp1[2] == 'u') return i;
                        }
                    }
                }
            }
            return -1;
        }

        public static List<string> ChangeCoordinates(List<string> data)
        {
            var newData = new List<string>();
            string firstLine = data[0];
            double diff = ReadCoord(firstLine);
            if (diff != 0.00)
            {
                for(int i=0; i<data.Count; i++)
                {
                    string line = data[i];
                    int pos = GetPosition(line);
                    double temp = ReadCoord(line);
                    double newVal = temp - diff;
                    string newValString = newVal.ToString();
                    int OldStrLen = temp.ToString().Length;
                    int NewStrLen = newValString.Length;
                    if (OldStrLen - NewStrLen != 0)
                    {
                        for (int j = 0; j < (OldStrLen - NewStrLen); j++)
                        {
                            newValString = newValString.Insert(0, " ");
                        }
                    }
                    line = line.Remove(pos, OldStrLen).Insert(pos, newValString);
                    newData.Add(line);
                }
                return newData;
            }
            else
            {
                return data;
            }
        }

        public static int GetPosition(string line)
        {
            for (int i = 15; i < line.Length; i++)
            {
                if(line[i] != ' ')
                {
                    return i;
                }
            }
            return -1;
        }

        public static double ReadCoord(string line)
        {
            string OGCoord = "";
            int pos = GetPosition(line);
            if(pos != -1) OGCoord = line.Substring(pos, 10);

            if (OGCoord.Length == 0)
            {
                return 0.00;
            }
            else
            {
                OGCoord = OGCoord.Trim();
                while (char.IsLetter(OGCoord[OGCoord.Length - 1])) OGCoord = OGCoord.Remove(OGCoord.Length - 1);
                double diff = double.Parse(OGCoord);
                return diff;
            }
        }

        public static double RemoveLettersFromProfile(string profile)
        {
            while (!char.IsNumber(profile[0])) profile = profile.Remove(0, 1);
            return double.Parse(profile);
        }
    }
}
