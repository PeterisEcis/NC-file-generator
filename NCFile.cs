using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NC_file_generator
{
    public class NCFile
    {
        public static void GenerateFiles(string path1) {
            string path2 = GetSecondFilePath(path1);
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
                //GenerateFile1();
                //GenerateFile2();
                //GenerateFile3();
                //GenerateFile4();
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
    }
}
