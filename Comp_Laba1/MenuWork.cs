using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp_Laba1
{
    //кря работа с меню c 1 файлом
    public class MenuWork
    {
        RichTextBox rtb;
        RichTextBox numberBox;

        DocumentTab documentTabNow { get; set; }
        public MenuWork(RichTextBox richtb, RichTextBox richTextBox2)
        {
            rtb = richtb;
            numberBox = richTextBox2;
        }





    }
}
