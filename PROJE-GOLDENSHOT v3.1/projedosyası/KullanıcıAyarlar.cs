using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projedosyası
{
    public partial class KullanıcıAyarlar : Form
    {
        public string gelen = Form1.degistir.ToString();
        public KullanıcıAyarlar()
        {
            InitializeComponent();
        }

        private void KullanıcıAyarlar_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            üçüncü.PasswordChar = '*';
            if (gelen == "Telefon") 
            {
                label2.Text = "Mevcut";
                label3.Text = "Yeni";
                label4.Text = "Şifreniz";
                birinci.MaxLength = 11;
                bunifuPictureBox2.Visible = false;
            }
            if (gelen == "Mail")
            {
                label2.Text = "Mevcut";
                label3.Text = "Yeni";
                label4.Text = "Şifreniz";
                bunifuPictureBox2.Visible = false;
            }
            if (gelen == "Şifre")
            {
                label2.Text = "Mevcut";
                label3.Text = "Yeni";
                label4.Text = "Yeni";
                birinci.PasswordChar = '*';
                ikinci.PasswordChar = '*';
                bunifuPictureBox1.Visible = false;

            }

        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {


            if (gelen == "Telefon")
            {
                if(birinci.Text == genel.uye.Uye_telefon.ToString()) 
                {
                    if(üçüncü.Text == genel.uye.Uye_Sifre.ToString())
                    {
                        if (ikinci.Text == "")
                        {
                            label1.Visible = true; label1.Text = "Lütfen yeni Tel-no girin";
                        }
                        else
                        {
                            genel.uye.Uye_telefon = Convert.ToDecimal(ikinci.Text);
                            genel.db.SaveChanges();
                            MessageBox.Show("Telefon Numarası değiştirildi");
                            this.Close();
                        }
                    }
                    else { label1.Visible = true; label1.Text = "Şifreniz doğrulanamadı"; }
                }
                else { label1.Visible = true; label1.Text = "Mevcut numaranız doğrulanamadı"; }
            }



            if (gelen == "Mail")
            {
                if (birinci.Text == genel.uye.Uye_Email.ToString())
                {
                    if (üçüncü.Text == genel.uye.Uye_Sifre.ToString())
                    {   
                        if(ikinci.Text == "") 
                        {
                            label1.Visible = true; label1.Text = "Lütfen yeni E-mailinizi girin";
                        }
                        else
                        {
                            genel.uye.Uye_Email = ikinci.Text;
                            genel.db.SaveChanges();
                            MessageBox.Show("E-Mail Adresi değiştirildi");
                            this.Close();
                        }
                    }
                    else { label1.Visible = true; label1.Text = "Şifreniz doğrulanamadı"; }
                }
                else { label1.Visible = true; label1.Text = "Mevcut E-Mail doğrulanamadı"; }
            }




            if (gelen == "Şifre")
            {
                if (birinci.Text == genel.uye.Uye_Sifre.ToString())
                {
                    if (ikinci.Text == "")
                    {
                        label1.Visible = true; label1.Text = "Lütfen yeni şifrenizi girin";
                    }
                    else 
                    {
                        if (ikinci.Text == üçüncü.Text)
                        {
                            genel.uye.Uye_Sifre = ikinci.Text;
                            genel.db.SaveChanges();
                            MessageBox.Show("Şifreniz değiştirildi");
                            this.Close();
                        }
                        else
                        {
                            label1.Visible = true; label1.Text = "Yeni şifreniz doğrulanamadı";
                        }
                    }

                }
                else { label1.Visible = true; label1.Text = "Mevcut şifreniz doğrulanamadı"; }
            }

        }

        private void üçüncü_TextChange(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            if (üçüncü.PasswordChar == '*') { üçüncü.PasswordChar = '\0'; } else { üçüncü.PasswordChar = '*'; }
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            if (ikinci.PasswordChar == '*') { ikinci.PasswordChar = '\0'; } else { ikinci.PasswordChar = '*'; }
        }
    }
}
