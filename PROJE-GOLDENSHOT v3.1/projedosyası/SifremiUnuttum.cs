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
    public partial class SifremiUnuttum : Form
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }
        private void SifremiUnuttum_Load(object sender, EventArgs e)
        {
            bunifuTextBox1.PasswordChar = '*';
            bunifuTextBox2.PasswordChar = '*';
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            decimal tc = Convert.ToDecimal(üçüncü.Text);
            var uye = genel.db.Kutuphane_Uye_listesi.Where(x => x.Uye_Adi == birinci.Text && x.Uye_Soyadi == ikinci.Text && x.Uye_Tc == tc ).ToList();

            if (uye.Count == 1)
            {
                if (bunifuTextBox1.Text == "") { label1.Text = "Lütfen şifrenizi belirleyiniz"; }
                else
                {
                    if (bunifuTextBox1.Text == bunifuTextBox2.Text)
                    {

                        genel.Sifresizuye = uye.First();
                        genel.Sifresizuye.Uye_Sifre = bunifuTextBox2.Text;
                        genel.db.SaveChanges();
                        MessageBox.Show("Şifreniz Başarıyla değiştirildi.");
                        login lgn = new login();
                        lgn.Show();
                        Hide();
                    }
                    else { label1.Text = "Yeni şifre doğrulanamadı"; }
                }
            }
            else
            {
                label1.Visible = true;
                label1.Text = "Kullanıcı bulunamadı";
            }
        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox1.PasswordChar == '*') { bunifuTextBox1.PasswordChar = '\0'; } else { bunifuTextBox1.PasswordChar = '*'; }
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.Show();
            Close();
        }


    }
}
