using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePadSharp
{
    public partial class frmNote : Form
    {
        public frmNote()
        {
            InitializeComponent();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rchYazi.SelectedText);
            rchYazi.SelectedText = ""; 
            ///rchYazi.Cut() ile de yapılabilir.
           
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (rchYazi.SelectedText.Length > 0)
            {                            
                kesToolStripMenuItem.Enabled = true;
                kopyalaToolStripMenuItem.Enabled = true;
            }
            else
            {
                kesToolStripMenuItem.Enabled = false;
                kopyalaToolStripMenuItem.Enabled = false;
            }
            if(Clipboard.GetText().Length>0)
            {
                yapistirToolStripMenuItem.Enabled = true;
            }
            else
            {
                yapistirToolStripMenuItem.Enabled = false;
            }
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchYazi.Copy();
        }

        private void yapistirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rchYazi.Paste();
        }
    }
}
