using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp_Laba1
{
    public class DocumentTab
    {
        public string FileName { get; set; }
        public string TextContent { get; set; }
        public bool IsModified { get; set; }
        public int CurrentVersionIndex { get; set; }
        public bool CanRedo { get; private set; }

        public DocumentTab(string filePath = "")
        {
            FileName = filePath;
            TextContent = "";
            IsModified = false;
            CurrentVersionIndex = 0;
            CanRedo = false;
        }
        public void UpdateText(string newText)
        {
            if (newText.Length > TextContent.Length)
            {
                TextContent = newText;
                CurrentVersionIndex = newText.Length;
                CanRedo = false;
                IsModified = true;
            }
            else if (newText != TextContent)
            {
                TextContent = newText;
                CurrentVersionIndex = newText.Length;
                CanRedo = false;
                IsModified = true;
            }
        }

        public bool Undo(ref string text, ref int cursorPosition)
        {
            if (CurrentVersionIndex <= 0) return false;
            CurrentVersionIndex--;
            text = TextContent.Substring(0, CurrentVersionIndex);
            cursorPosition = CurrentVersionIndex;
            CanRedo = true;
            return true;
        }

        public bool Redo(ref string text, ref int cursorPosition)
        {
            if (!CanRedo || CurrentVersionIndex >= TextContent.Length) return false;
            CurrentVersionIndex++;
            text = TextContent.Substring(0, CurrentVersionIndex);
            cursorPosition = CurrentVersionIndex;
            if (CurrentVersionIndex >= TextContent.Length)
                CanRedo = false;

            return true;
        }
    }
}
