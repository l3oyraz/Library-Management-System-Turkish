using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Bunifu.UI.WinForm;

namespace projedosyası
{
    public partial class KayitSayfasi : Form
    {
        public KayitSayfasi()
        {
            InitializeComponent();
          
            this.AcceptButton = LGirisYap;
            
            this.KeyPreview = true;
            this.KeyPress += KayitSayfasi_KeyPress;

        }
        private void KayitSayfasi_Load(object sender, EventArgs e)
        {
            KaUye_TctxtBox.MaxLength = 11;//TEXT UZUNLUK KISITLAMA
            KaUye_TeltxtBox.MaxLength = 11;
            KaUye_TctxtBox.PlaceholderText = "TC";
            KaUye_TeltxtBox.PlaceholderText = "Telefon";
            K_Sifretxtbox.UseSystemPasswordChar = true;
        }
        private void bunifuPictureBox4_Click(object sender, EventArgs e)
        {//FORMDAN FORMA GEÇİŞ 
            login login = new login();
            login.Show();
            this.Close();
        }
        private void KayitSayfasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                // Escape tuşuna basıldığında yapılacak işlem
                bunifuPictureBox4_Click(sender, e);
            }
        }




        //YENİ KULLANICI OLUŞTURMA kODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void LGirisYap_Click(object sender, EventArgs e)
        {
            var uye = genel.db.Kutuphane_Uye_listesi.Where(x => x.Uye_Adi == KaUye_AditxtBox.Text && x.Uye_Soyadi == KaUye_SoyaditxtBox.Text).ToList();

            if (uye.Count == 1)
            { MessageBox.Show("Bu isime sahip bir hesap mevcut"); }
            else
            {
                if (
                        UyeE_mailtxtBox.Text == "" ||
                        KaUye_AditxtBox.Text == "" ||
                        KaUye_SoyaditxtBox.Text == "" ||
                        KaUye_TctxtBox.Text == "" ||
                        KaUye_TeltxtBox.Text == "" ||
                        bunifuTextBox1.Text == "" 
                    ) { MessageBox.Show("Lütfen tüm verileri girdiğinizden emin olunuz"); }
                else {

                        if (Regex.IsMatch(KaUye_TctxtBox.Text, @"\d") && Regex.IsMatch(KaUye_TeltxtBox.Text, @"\d"))
                        {
                            if (K_Sifretxtbox.Text == bunifuTextBox1.Text)
                            {
                                Kutuphane_Uye_listesi yeniuye = new Kutuphane_Uye_listesi();

                                yeniuye.Uye_Email = UyeE_mailtxtBox.Text;
                                yeniuye.Uye_Adi = KaUye_AditxtBox.Text;
                                yeniuye.Uye_Soyadi = KaUye_SoyaditxtBox.Text;
                                yeniuye.Uye_Tc = Convert.ToDecimal(KaUye_TctxtBox.Text);
                                yeniuye.Uye_telefon = Convert.ToDecimal(KaUye_TeltxtBox.Text);
                                yeniuye.Uye_Sifre = bunifuTextBox1.Text;
                                yeniuye.Uye_Borc = 0;
                                yeniuye.Uye_ToplamOkunan = 0;
                                yeniuye.Uye_Yetki = "Uye";
                                genel.db.Kutuphane_Uye_listesi.Add(yeniuye);
                                genel.db.SaveChanges();

                                MessageBox.Show("Kaydınız başarıyla yapılmıştır");
                                login login = new login();
                                login.Show();
                                this.Close();
                            }
                           
                            else { MessageBox.Show("Lütfen Şifrenizi doğru girdiğinizden emin olun"); }
                        }
                        else { MessageBox.Show("Lütfen TC ve Telefon verilerinizi doğru girdiğinizden emin olunuz"); }
                   
                }
            }
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//




        //ŞİFRE GİZLEME KODLARI 
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void bunifuPictureBox1_Click(object sender, EventArgs e) // BURDA ŞİFRE YERİNE PASWORD TXT KULLANILMIŞ BİZ BUNLARI SYSTEM PASWOR YAPALIM
        {
            K_Sifretxtbox.UseSystemPasswordChar = !K_Sifretxtbox.UseSystemPasswordChar;
            bunifuTextBox1_TextChanged(sender,e);
        }

        private void K_Sifretxtbox_TextChanged(object sender, EventArgs e)
        {
            K_Sifretxtbox.UseSystemPasswordChar=true;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            bunifuTextBox1.UseSystemPasswordChar =false;
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
    }
}
