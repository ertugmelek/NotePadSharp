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
    public partial class FrmAna : Form
    {
        public FrmAna()
        {
            InitializeComponent();
        }

        bool _degistiMi = false;

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YeniDosyaAc();
        }
        private void YeniDosyaAc() {
            toolStripEnabled(true, bicimlendirmeToolStripMenuItem, harfIslemleriToolStripMenuItem, araToolStripMenuItem);
            frmNote FrmNote = new frmNote();
            FrmNote.MdiParent = this;
            FrmNote.Show();

            FrmNote.yaziRengiToolStripMenuItem.Click += yaziRengiToolStripMenuItem_Click;
            FrmNote.yaziStiliToolStripMenuItem.Click += yaziStiliToolStripMenuItem_Click;
            FrmNote.FormClosing += FrmNote_FormClosing;
            FrmNote.rchYazi.TextChanged += rchYazi_TextChanged;
        }
        void rchYazi_TextChanged(object sender, EventArgs e)
        {
            _degistiMi = true;
        }

        void FrmNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_degistiMi)
            {
                DialogResult sonuc = MessageBox.Show("Kaydetmek istiyor musunuz?", "Kaydet", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (sonuc == DialogResult.Yes)
                {
                    kaydetToolStripMenuItem.PerformClick();
                    _degistiMi = false;
                }
                else if (sonuc == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    _degistiMi = false;
                }
            }
            if (this.MdiChildren.Length == 1)
            {
                toolStripEnabled(false, bicimlendirmeToolStripMenuItem, harfIslemleriToolStripMenuItem, araToolStripMenuItem);
            }

        }

        void yaziStiliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YaziStiliDegis();
        }

        private void YaziStiliDegis()
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Font secilenFont = fontDialog1.Font;
                frmNote frmAktifNote = (frmNote)this.ActiveMdiChild;

                if (frmAktifNote.rchYazi.SelectedText.Length > 0)
                {
                    frmAktifNote.rchYazi.SelectionFont = secilenFont;
                }
                else
                {
                    frmAktifNote.rchYazi.Font = secilenFont;
                }
            }
        }

        void yaziRengiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YaziRengiDegis();
        }

        private void YaziRengiDegis()
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color secilenRenk = colorDialog1.Color;
                frmNote frmAktifNote = (frmNote)this.ActiveMdiChild;

                if (frmAktifNote.rchYazi.SelectedText.Length > 0)
                {
                    frmAktifNote.rchYazi.SelectionColor = secilenRenk;
                }
                else
                {
                    frmAktifNote.rchYazi.ForeColor = secilenRenk;
                }
            }
        }

       internal void toolStripEnabled(bool baslangicMi, params ToolStripMenuItem[] item)
        {
            for (int i = 0; i < item.Length; i++)
            {
                if (baslangicMi)
                {
                    item[i].Enabled = true;
                }
                else
                {
                    item[i].Enabled = false;
                }
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DosyaKaydet();
        }
        private void DosyaKaydet() {
            if (this.MdiChildren.Count() < 1)
            {
                MessageBox.Show("Kaydedilecek dosya yok!!");
            }
            else
            {
                saveFileDialog1.Filter = @"Zengin Metin Belgesi (*.rtf) | *.rtf";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _degistiMi = false;
                    var dosyaAdi = saveFileDialog1.FileName;
                    frmNote frmAktif = (frmNote)this.ActiveMdiChild;
                    frmAktif.rchYazi.SaveFile(dosyaAdi);
                }
            }
        }
        private void acToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DosyaAc();
        }
        private void DosyaAc() {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                /// açıkssaaaaa
                string dosyaYolu = openFileDialog1.FileName;
                string dosyaAdi = openFileDialog1.SafeFileName;
                frmNote frmAcilacak = new frmNote();
                frmAcilacak.MdiParent = this;
                frmAcilacak.rchYazi.LoadFile(dosyaYolu);
                frmAcilacak.Text = dosyaAdi;
                frmAcilacak.FormClosing += FrmNote_FormClosing;
                frmAcilacak.rchYazi.TextChanged += rchYazi_TextChanged;
                frmAcilacak.Show();
            }
        }
        private void yaziRengiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            YaziRengiDegis();
        }

        private void yaziTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YaziStiliDegis();
        }

        private void araToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAra formAra = new frmAra();
            formAra.MdiParent = this;          
            formAra.Controls["btnAra"].Click += FrmAra_BtnAra_Click;
            formAra.Show();
            //FrmAra_BtnAra_Click(new frmAra(), EventArgs.Empty);
        }

        void FrmAra_BtnAra_Click(object sender, EventArgs e)
        {
            string aranan = ((this.ActiveMdiChild) as frmAra).Controls["textBox1"].Text;

            Form[] formlarim = this.MdiChildren;

            foreach (Form item in formlarim)
            {
                if (item is frmNote)
                {
                    frmNote formum = (frmNote)item;
                    for (int i = 0; i < formum.rchYazi.TextLength - aranan.Length; i++)
                    {
                        ////////metin içinde kelime arama
                        //bulur isem
                        formum.rchYazi.Find(aranan, i, formum.rchYazi.TextLength, RichTextBoxFinds.None);
                        formum.rchYazi.SelectionBackColor = Color.Red;
                        //seçilenin rengini değiş..


                    }

                }

            }
           
        }

        //Kaydetme tool stripti içinde dosya kaydet metotunu çağırıyorum.
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            YeniDosyaAc();
        }
        //dosya aç
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            DosyaAc();
        }
        //dosya kaydet
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            DosyaKaydet();
        }
        //kesme işlemi
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            frmNote frmAktif = (frmNote)this.ActiveMdiChild;
            frmAktif.Show();
            frmAktif.rchYazi.Cut();
        }
        //seçili yazıyı clipboarda yollama
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            frmNote frmAktif = (frmNote)this.ActiveMdiChild;
            frmAktif.Show();
            frmAktif.rchYazi.Copy();
        }

        //clipboarddaki yazıyı yapıştırma
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            frmNote frmAktif = (frmNote)this.ActiveMdiChild;
            frmAktif.Show();
            frmAktif.rchYazi.Paste();
        }
        //hakkımızda formunu göstericem
        private void hakkimizdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHakkimizda frmhakkimizda = new FrmHakkimizda();
            frmhakkimizda.MdiParent = this;
            frmhakkimizda.Show();
        }
        //seçili bütün harfleri büyük harf yapıcam
        private void hepsiBuyukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNote frmAktif = (frmNote)this.ActiveMdiChild;
           frmAktif.rchYazi.Text= (frmAktif.rchYazi.SelectedText).ToUpper();
        }
        //seçili bütün harfleri küçük harf yapıcam
        private void hepsiKucukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNote frmAktif = (frmNote)this.ActiveMdiChild;
            frmAktif.rchYazi.Text = (frmAktif.rchYazi.SelectedText).ToLower();
        }
        //print ekranını getiricem
        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult print = printDialog1.ShowDialog();
        }
    }
}
