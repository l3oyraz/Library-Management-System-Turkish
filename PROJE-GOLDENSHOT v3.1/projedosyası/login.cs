
using DevExpress.Utils.Serializing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using DevExpress.Utils;
using DevExpress.Xpo.DB.Helpers;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Eventing.Reader;
using Bunifu.UI.WinForms.BunifuButton;

namespace projedosyası
{
    public partial class login : Form
    {
        public static string uyeadi = "isim bulunamadı";//Diğer formlardaki kullanıcı adı texti için değisken       
        public login()
        {
            InitializeComponent();
            this.AcceptButton = LGirisYap;
            // Form yüklendiğinde Escape tuşunu işlemek için KeyPreview özelliğini true yapalım
            this.KeyPreview = true;
            // Escape tuşuna basıldığında KayitSayfasi_KeyPress metodu tetiklensin
            this.KeyPress += login_KeyPress;
        }
        private void login_Load(object sender, EventArgs e)
        {
            Sifretxtbox.UseSystemPasswordChar = true;
        }


        //GİRİŞ YAP KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void bunifuButton2_Click(object sender, EventArgs e) //GİRİŞ YAP BUTONU 
        {
            var uye = genel.db.Kutuphane_Uye_listesi.Where(x => x.Uye_Adi == adGiristxt.Text && x.Uye_Sifre == Sifretxtbox.Text).ToList();

             if (string.IsNullOrEmpty(adGiristxt.Text) || string.IsNullOrEmpty(Sifretxtbox.Text)) 
             {
                 MessageBox.Show("Lütfen Bilgilerinizi Tam Giriniz");
             }
             else 
             { 
                 if (uye.Count == 1)
                 {
                     genel.uye = uye.First();
                     uyeadi = genel.uye.Uye_Adi+" "+genel.uye.Uye_Soyadi;
                     anaSayfa anaSayfa = new anaSayfa();
                     anaSayfa.Show();

                     Hide();
                 }
                 else
                     label1.Visible = true;
             } 
           // anaSayfa anaSayfa = new anaSayfa();
            //anaSayfa.Show();
         }

         //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


            //VİSİBLE TUŞU KODLARI 
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
            private void bunifuPictureBox4_Click(object sender, EventArgs e)
            {
               Sifretxtbox.UseSystemPasswordChar = !Sifretxtbox.UseSystemPasswordChar;
            }
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//




            //ÇIKIŞ VE KAYIT OL BUTONU KODLARI
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
            private void bunifuPictureBox3_Click(object sender, EventArgs e) //ÇIKIŞ BUTONU 
            {
            if (Form1.abc == true) { this.Hide(); }
            else { DialogResult result = MessageBox.Show("Uygulamayı kapatmak istiyor musunuz?", "Kapatma Onayı", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                { Application.Exit(); }
            }
            }
            private void label2_Click(object sender, EventArgs e)
            {
                KayitSayfasi kayitSayfasi = new KayitSayfasi();
                kayitSayfasi.Show();
                this.Hide();
            }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


        //MİSAFİR KULLANICI KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void label3_Click(object sender, EventArgs e)
        {
            uyeadi = "Misafir Kullanıcı";
            anaSayfa anaSayfa = new anaSayfa();
            anaSayfa.Show();
            this.Hide();
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


        //SİFREMİ UNUTTUM 
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void label4_Click(object sender, EventArgs e)
        {
            SifremiUnuttum frm = new SifremiUnuttum();
            frm.Show();
            Hide();
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                // Escape tuşuna basıldığında yapılacak işlem
                bunifuPictureBox3_Click(sender, e);
            }
        }








    }





}






