using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Comp_Laba1
{
    public class ListWorks
    {
        List<DocumentTab> documents = new List<DocumentTab>();
        DocumentTab currentDocument = new DocumentTab();


        private void CreateNewDocument(List<DocumentTab> documents, int nextNewFileNumber, DocumentTab currentDocument)
        {
            string fileName = $"File{nextNewFileNumber++}";
            var newDoc = new DocumentTab(fileName);
            documents.Add(newDoc);

            // Если это первый документ, делаем его текущим
            if (currentDocument == null)
            {
                SwitchToDocument(newDoc);
            }

            // Обновляем список файлов в меню
            UpdateOpenFilesMenu();
        }

        
        //private void SwitchToDocument(DocumentTab doc)
        //{
        //    if (doc == null) return;

        //    // Сохраняем текущий документ перед переключением
        //    if (currentDocument != null)
        //    {
        //        currentDocument.TextContent = textBox.Text;
        //    }

        //    // Переключаемся на новый документ
        //    currentDocument = doc;
        //    textBox.Text = doc.TextContent;
        //    this.Text = $"Редактор - {doc.GetDisplayName()}";
        //}
    }
}
