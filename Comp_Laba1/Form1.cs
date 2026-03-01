using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Comp_Laba1
{
    public partial class Form1 : Form
    {
        MenuWork menuW;
        List<DocumentTab> documents = new List<DocumentTab>();
        DocumentTab currentDocument;
        int nextNewFileNumber = 2;


        public Form1()
        {
            InitializeComponent();
            menuW = new MenuWork(richTextBox1, richTextBox2);
            documents.Add(new DocumentTab("File1"));
            CreateNewDocument();

        }

        private void CreateNewDocument()
        {
            string fileName = $"File{nextNewFileNumber++}";
            var newDoc = new DocumentTab(fileName);
            documents.Add(newDoc);
            if (currentDocument == null)
            {
                SwitchToDocument(newDoc);
            }

            // Обновляем список файлов в меню
            UpdateOpenFilesMenu();
        }

        private void SwitchToDocument(DocumentTab doc)
        {
            if (doc == null) return;
            if (currentDocument != null)
            {
                currentDocument.TextContent = richTextBox1.Text;
            }

            currentDocument = doc;
            richTextBox1.Text = doc.TextContent;

            //подсветка?
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void аToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void иконочкиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void текстToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void file1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
