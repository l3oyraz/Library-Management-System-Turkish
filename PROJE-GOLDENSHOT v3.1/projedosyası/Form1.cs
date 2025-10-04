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
    public partial class Form1 : Form

    {   public static Boolean abc = false;  //LOGİN İÇİN DEĞİŞKEN 
        public static string degistir = "boş";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           label18.Text = genel.uye.Uye_Adi.ToString();
           label16.Text = genel.uye.Uye_Soyadi.ToString();
           
           label11.Text = genel.uye.Uye_Email.ToString();
           label10.Text = genel.uye.Uye_telefon.ToString();

           label4.Text = genel.uye.Uye_Adi.ToString()+" "+genel.uye.Uye_Soyadi.ToString();
           label4.Text.ToUpper();
           label2.Text = genel.uye.Uye_Yetki.ToString();
           label5.Text = genel.uye.Uye_ToplamOkunan.ToString();
           label6.Text = genel.uye.Uye_Borc.ToString() + "TL";
           var sayi = genel.db.Emanet_Kitap_Alanlar.Where(x=> x.EmanetAlan_ID==genel.uye.Uye_ID).ToList() ;
           label8.Text = sayi.Count.ToString() ;
           if(genel.uye.Uye_Yetki == "Tam") { bunifuPictureBox6.Visible = true; } else { bunifuPictureBox6.Visible=false; }
        }



        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            abc = true;
            login lgn = new login();
            lgn.Show();

        }

        private void label22_Click(object sender, EventArgs e)
        {
            degistir = "Telefon";
            KullanıcıAyarlar frm = new KullanıcıAyarlar(); frm.ShowDialog();
            this.Hide();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            label22_Click(sender,e);
        }

        private void label21_Click(object sender, EventArgs e)
        {
            degistir = "Mail";
            KullanıcıAyarlar frm = new KullanıcıAyarlar(); frm.ShowDialog();
            this.Hide();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            label21_Click(sender, e);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            degistir = "Şifre";
            KullanıcıAyarlar frm = new KullanıcıAyarlar(); frm.ShowDialog();
            this.Hide();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            label20_Click(sender, e);
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            string uyetc = genel.uye.Uye_Tc.ToString();
            if (label14.Text == uyetc) { label14.Text = "***********";} else { label14.Text = uyetc.ToString();}
            if(label14.Text == "***********") { label14.Text = uyetc;}
        }
    }
}
