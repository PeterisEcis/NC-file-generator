using System;
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
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Title = "Select main .nc file";
                openFileDialog.Filter = "NC files (*.nc)|*.nc";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }

            }
            textBox1.Text = filePath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text;
            string targetPath = textBox2.Text;
            if (File.Exists(filePath))
            {
                NCFile.GenerateAllFiles(filePath, targetPath);
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
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                }

            }
            textBox2.Text = filePath;
        }
    }
}
