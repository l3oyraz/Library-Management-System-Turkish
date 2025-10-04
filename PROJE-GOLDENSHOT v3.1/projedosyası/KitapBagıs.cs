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
    public partial class KitapBagıs : Form
    {
        public KitapBagıs()
        {
            InitializeComponent();
        }
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //KİTAP EKLEME BUTONU KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
        {
            if (
                bunifuTextBox1.Text == "" ||
                bunifuTextBox8.Text == "" ||
                bunifuTextBox5.Text == "" ||
                bunifuTextBox6.Text == "" ||
                bunifuTextBox7.Text == "" ||
                bunifuTextBox9.Text == "" ||
                maskedTextBox1.Text == "" ||
                maskedTextBox2.Text == ""
               ) { MessageBox.Show("Lütfen tüm verileri giriniz"); }
            else
            {
                Kitaplarimiz ktp = new Kitaplarimiz();
                ktp.Kitap_Barkod = bunifuTextBox1.Text;
                ktp.Kitap_Adi = bunifuTextBox8.Text;
                ktp.Kitap_yazar = bunifuTextBox5.Text;
                ktp.Kitap_Turu = bunifuTextBox6.Text;
                ktp.Kitap_Konusu = bunifuTextBox7.Text;
                ktp.Kitap_Yayinevi = bunifuTextBox9.Text;
                ktp.Kitap_basimyili = Convert.ToInt32(maskedTextBox1.Text);
                ktp.Kitap_Adet = Convert.ToInt32(maskedTextBox2.Text);

                genel.db.Kitaplarimiz.Add(ktp);
                genel.db.SaveChanges();
                this.Close();
                MessageBox.Show("Kütüphaneye Eklendi");
            }
        }
        private void bunifuLabel15_Click_1(object sender, EventArgs e)
        {
            bunifuImageButton4_Click_1(sender, e);
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



    }
}
