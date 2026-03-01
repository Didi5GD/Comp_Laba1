using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            
            currentDocument = documents[0];
            UpdateOpenFilesMenu();
        }

        private void CreateNewDocument()
        {
            string fileName = $"File{nextNewFileNumber++}";
            var newDoc = new DocumentTab(fileName);
            documents.Add(newDoc);
            if (currentDocument == null)
            {
                SwitchToDocument(newDoc);

                UpdateOpenFilesMenu();
            }
            UpdateOpenFilesMenu();
        }

        private void UpdateOpenFilesMenu()
        {
            menuStrip3.Items.Clear();
            if (documents.Count == 0)
            {
                var emptyItem = new ToolStripMenuItem("(нет файлов)");
                emptyItem.Enabled = false;
                menuStrip3.Items.Add(emptyItem);
                return;
            }
            foreach (var doc in documents)
            {

                var item = new ToolStripMenuItem(doc.FileName);
                item.Tag = doc;

                item.Click += (s, e) => {
                    var clickedDoc = (s as ToolStripMenuItem)?.Tag as DocumentTab;
                    if (clickedDoc != null && clickedDoc != currentDocument)
                    {
                        SwitchToDocument(clickedDoc);
                    }
                };
                menuStrip3.Items.Add(item);
            }
            HighlightCurrentDocument();
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

            HighlightCurrentDocument();
        }

        private void HighlightCurrentDocument()
        {
            foreach (ToolStripMenuItem item in menuStrip3.Items)
            {
                var doc = item.Tag as DocumentTab;

                if (doc != null)
                {
                    if (doc == currentDocument)
                    {
                        item.Font = new Font(item.Font, FontStyle.Bold);
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.Font = new Font(item.Font, FontStyle.Regular);
                        item.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void CloseCurrentDocument()
        {

            DialogResult result = MessageBox.Show($"Закрыть документ \"{currentDocument.FileName}\" без сохраниния?",
                                                   "Подтверждение",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int index = documents.IndexOf(currentDocument);
                documents.Remove(currentDocument);

                int newIndex = (index >= documents.Count) ? documents.Count - 1 : index;
                SwitchToDocument(documents[newIndex]);
                UpdateOpenFilesMenu();
            }
        }

        private void OpenTextFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"D:\Универ\6 семестр\Компиляторы\Comp_Laba1";

                openFileDialog.Title = "Открыть текстовый файл";
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    try
                    {
                        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);

                        var newDoc = new DocumentTab(fileName);
                        newDoc.TextContent = fileContent;
                        newDoc.FilePath = filePath;

                        documents.Add(newDoc);
                        SwitchToDocument(newDoc);
                        UpdateOpenFilesMenu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при открытии файла: {ex.Message}",
                                        "Ошибка",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void SaveFile()
        {
            if (currentDocument == null) return;
            if (string.IsNullOrEmpty(currentDocument.FilePath))
            {
                SaveFileAs();
            }
            else
            {
                try
                {
                    File.WriteAllText(currentDocument.FilePath, currentDocument.TextContent, Encoding.UTF8);
                    currentDocument.IsModified = false;

                    UpdateOpenFilesMenu();

                    this.Text = $"Редактор - {currentDocument.FileName}";

                    MessageBox.Show("Файл успешно сохранён",
                                   "Сохранение",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                                   "Ошибка",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFileAs()
        {
            if (currentDocument == null) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string defaultPath = @"D:\Универ\6 семестр\Компиляторы\Comp_Laba1";
                if (Directory.Exists(defaultPath))
                {
                    saveFileDialog.InitialDirectory = defaultPath;
                }

                saveFileDialog.Title = "Сохранить файл как...";
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (!string.IsNullOrEmpty(currentDocument.FileName))
                {
                    saveFileDialog.FileName = currentDocument.FileName;
                }
                else
                {
                    saveFileDialog.FileName = "Новый документ.txt";
                }

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = saveFileDialog.FileName;
                        string fileName = Path.GetFileName(filePath);

                        File.WriteAllText(filePath, currentDocument.TextContent, Encoding.UTF8);

                        currentDocument.FilePath = filePath;
                        currentDocument.FileName = fileName;
                        currentDocument.IsModified = false;

                        UpdateOpenFilesMenu();

                        this.Text = $"Редактор - {fileName}";

                        MessageBox.Show($"Файл сохранён как:\n{filePath}",
                                       "Сохранение",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}",
                                       "Ошибка",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    }
                }
            }
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
            CreateNewDocument();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void file1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentDocument();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTextFile();
        }

        private void пToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenTextFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void пToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
    }
}
