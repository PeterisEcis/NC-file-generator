using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NC_file_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if(textBox1.Text != "" && Directory.Exists(textBox1.Text))
                {
                    folderBrowserDialog.SelectedPath = textBox1.Text;
                }
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                }

            }
            if (Directory.Exists(filePath))
            {
                textBox1.Text = filePath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string dir = textBox1.Text;
            if (Directory.Exists(dir))
            {
                string[] filePaths = Directory.GetFiles(dir, "*.nc");
                foreach(string path in filePaths)
                {
                    if (!path.Contains("_webs"))
                    {
                        string path2 = NCFile.GetSecondFilePath(path);
                        if (File.Exists(path2))
                        {
                            textBox3.AppendText(NCFile.GetFileNameFromPath(path)+'\n');
                            textBox3.AppendText(NCFile.GetFileNameFromPath(path2)+'\n');
                        }
                    }
                }
            }
            else
            {
                textBox3.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sourceDir = textBox1.Text;
            string targetDir = textBox2.Text;
            string SuccessFiles = "";
            string FailedFiles = "";
            if (Directory.Exists(sourceDir))
            {
                string[] filePaths = Directory.GetFiles(sourceDir, "*.nc");
                if (filePaths.Length == 0) MessageBox.Show("No .nc files were found in " + sourceDir, "Error", MessageBoxButtons.OK);
                else
                {
                    foreach (string path in filePaths)
                    {
                        if (!path.Contains("_webs"))
                        {
                            string path2 = NCFile.GetSecondFilePath(path);
                            if (File.Exists(path2))
                            {
                                //MessageBox.Show(path);
                                if (NCFile.GenerateAllFiles(path, path2, targetDir))
                                {
                                    SuccessFiles += NCFile.GetFileNameFromPath(path) + '\n';
                                }
                                else
                                {
                                    FailedFiles += NCFile.GetFileNameFromPath(path) + '\n';
                                }
                            }
                            //else
                            //{
                            //    MessageBox.Show("_webs file for " + NCFile.GetFileNameFromPath(path) + " was not found", "Error", MessageBoxButtons.OK);
                            //}
                        }
                    }
                    MessageBox.Show("Successfully generated files for:\n" + SuccessFiles + "File generation failed for:\n" + FailedFiles);
                }
            }
            else
            {
                MessageBox.Show("Invalid file path!\nPlease select valid file!", "Error", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (textBox1.Text != "" && Directory.Exists(textBox1.Text))
                {
                    folderBrowserDialog.SelectedPath = textBox1.Text;
                }
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                }

            }
            if (Directory.Exists(filePath))
            {
                textBox2.Text = filePath;
            }
        }
    }
}
