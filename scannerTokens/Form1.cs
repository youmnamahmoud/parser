using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scannerTokens
{
    public partial class tokens : Form
    {

        public class OpenFile
        {
            // Read text file and return it in array of string
            public string[] GetFile(string OpenedFilePath)
            {
                return System.IO.File.ReadAllLines(OpenedFilePath);
            }
        }
        
        public tokens()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string OpenedFilePath = openFileDialog1.FileName;
                OpenFile newFile = new OpenFile();
                string[] FileData = newFile.GetFile(OpenedFilePath);
                FileDataBox.Text = "";
                foreach (string line in FileData)
                {
                    FileDataBox.Text += (line + "\r\n");
                }
            }
        }

        private void tokens_Load(object sender, EventArgs e)
        {
            ParserTreeView.Hide();
            parseLabel.Hide();
        }
        private void FillGrid(List<KeyValuePair<string, string>> ScannerData, ref DataGridView ScannerGridView)
        {
            ScannerGridView.RowCount = 1;
            foreach (var value in ScannerData)
            {
                ScannerGridView.RowCount++;
                ScannerGridView[0, ScannerGridView.RowCount - 2].Value = value.Key;
                ScannerGridView[1, ScannerGridView.RowCount - 2].Value = value.Value;
            }
        }

        private void TokensButton_Click(object sender, EventArgs e)
        {
            ScannerGridView.Visible = true;
            string[] FileData = FileDataBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            CompilerScanner newScanner = new CompilerScanner();
            List<KeyValuePair<string, string>> ScannerData = new List<KeyValuePair<string, string>>();
            newScanner.StartScanner(FileData, ref ScannerData);
            FillGrid(ScannerData, ref ScannerGridView);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ScannerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void restart_Click(object sender, EventArgs e)
        {
            /*FileDataBox.Clear();
            ScannerGridView.Rows.Clear();
            ScannerGridView.Hide();
            ParserTreeView.Hide();
            parseLabel.Hide();
            parseButton.Hide();*/
            Application.Restart();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void parseButton_Click(object sender, EventArgs e)
        {
            ParserTreeView.Show();
            parseLabel.Show();
            string[] FileData = FileDataBox.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            CompilerScanner newScanner = new CompilerScanner();
            List<KeyValuePair<string, string>> ScannerData = new List<KeyValuePair<string, string>>();
            newScanner.StartScanner(FileData, ref ScannerData);

            TreeNode ParserTreeRoot = null;

            Parser newParser = new Parser();
            bool IsParserTreeDone = newParser.CreateParseTree(ScannerData, ref ParserTreeRoot);
            ParserTreeView.Nodes.Clear();
            if (ParserTreeRoot != null)
                this.ParserTreeView.Nodes.Add(ParserTreeRoot);
            ParserTreeView.ExpandAll();
        }

    }
}
