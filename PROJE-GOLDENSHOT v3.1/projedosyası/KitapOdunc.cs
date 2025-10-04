using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;
using Bunifu.UI.WinForms;
using System.Text.RegularExpressions;

namespace projedosyası
{
    public partial class KitapOdunc : Form
    {
       
        public static string gidenguncelleme = "Değişmedi";
        public static string tckontrol = "yanlış";
        public static int a;
        public KitapOdunc()
        {
            InitializeComponent();
          
        }
        private void KitapOdunc_Load(object sender, EventArgs e)
        {
            var sayi = Convert.ToInt32(KitaplarSondeneme.kitapsayiOdunc);
            DatePicker1.Value = System.DateTime.Now;
            husakads_Click(sender, e);
            maskedTextBox1.Visible = false;
            maskedTextBox1.MaxLength = 11;
            label1.Visible= false;

            //COMBOBOX /////////////////////////////////////////////////////////////
            comboBox1.DisplayMember = "uyead";
            comboBox1.ValueMember = "Uye_ID";
            comboBox1.DataSource = genel.db.Kutuphane_Uye_listesi.Select(x => new { x.Uye_ID, uyead = x.Uye_Adi + " " + x.Uye_Soyadi }).ToList();
            try
            {
                if (sayi > 0 && genel.uye.Uye_Yetki == "Uye")
                {
                    comboBox1.SelectedIndex = genel.uye.Uye_ID - 1;
                }
            } catch { MessageBox.Show(""); };
            //////////////////////////////////////////////////////////////////////////
            
        }
        private void KitapOdunc_Activated(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(comboBox1.SelectedValue);//TC doğrulamadan sonra combobox değişip değişmediği kontrolü
            if (a == Convert.ToInt32(comboBox1.SelectedValue)) { tckontrol = "yanlış"; label1.Text = "Doğrulanamadı"; label1.Visible = true; label1.ForeColor = Color.Red; }
        }
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }





        //KİTAPLAR SAYFASINDAN VERİLERİ GETİREN KODLAR
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void husakads_Click(object sender, EventArgs e)
        {
            KitaplarSondeneme kitaplarSondeneme = new KitaplarSondeneme();
            var sayi = Convert.ToInt32(KitaplarSondeneme.kitapsayiOdunc);

            if (KitaplarSondeneme.kitapadiOdunc.ToString() == "boş")
            {
                MessageBox.Show("Lütfen bir kitap seçin"); this.Close();
            }
            else
            {
                if (sayi == 0) { MessageBox.Show("Emanet alınabilecek adet tükendi"); this.Close(); }
                else
                {
                    husakads.Text = KitaplarSondeneme.kitapadiOdunc.ToString();
                    bunifuLabel1.Text = KitaplarSondeneme.kitapyazariOdunc.ToString();
                    bunifuLabel2.Text = KitaplarSondeneme.kitapbasimOdunc.ToString();
                    bunifuLabel3.Text = KitaplarSondeneme.kitapturuOdunc.ToString();
                }

            }



        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//




        //İŞLEM ONAYLAMA KODLARI 
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void bunifuLabel15_Click(object sender, EventArgs e)
        {
            bunifuImageButton4_Click_1(sender, e);
        }


        private void bunifuImageButton4_Click_1(object sender, EventArgs e)
        {
            //Tarih Değişkenleri
            DateTime SeçiliTarih = Convert.ToDateTime(DatePicker1.Value);
            DateTime SistemTarih = Convert.ToDateTime(DateTime.Now);
            int TarihAralığı = DateTime.Compare(SeçiliTarih,SistemTarih);

            if(tckontrol == "doğru" && a == Convert.ToInt32(comboBox1.SelectedValue)) 
            { 
                if(TarihAralığı == 0 || TarihAralığı<0) 
                {
                    MessageBox.Show("Lütfen ileri bir tarih seçiniz");
                }
                else 
                { 
                    KitaplarSondeneme kitaplarSondeneme = new KitaplarSondeneme();

                    int Guncelle = Convert.ToInt32(KitaplarSondeneme.kitapIDOdunc);
                    int a = Convert.ToInt32(comboBox1.SelectedValue);
                    var deneme2 = genel.db.Kitaplarimiz.Find(Guncelle);
                    var deneme3 = genel.db.Emanet_Kitap_Alanlar;
                    var hesapbilgileri = genel.db.Kutuphane_Uye_listesi.Find(a);
                        //Sayi Düşme 
                    var sayi = Convert.ToInt32(deneme2.Kitap_Adet);
                    deneme2.Kitap_Adet = sayi - 1;
                        //emanetlere yazılacaklar
                    Emanet_Kitap_Alanlar emanet_Kitap_Alanlar = new Emanet_Kitap_Alanlar();
                    {
                            emanet_Kitap_Alanlar.EmanetKitap_Barkod = KitaplarSondeneme.kitapBarkodOdunc.ToString();
                            emanet_Kitap_Alanlar.EmanetKitap_Adi = deneme2.Kitap_Adi;
                            emanet_Kitap_Alanlar.EmanetKitap_Yazar = deneme2.Kitap_yazar;
                            emanet_Kitap_Alanlar.EmanetKitap_Turu = deneme2.Kitap_Turu;
                            emanet_Kitap_Alanlar.EmanetKitap_Basimyili = Convert.ToInt32(deneme2.Kitap_basimyili);
                            emanet_Kitap_Alanlar.EmanetKitap_ID = deneme2.Kitap_ID;
                            emanet_Kitap_Alanlar.EmanetAlan_Adi = hesapbilgileri.Uye_Adi;
                            emanet_Kitap_Alanlar.EmanetAlan_Email = hesapbilgileri.Uye_Email;
                            emanet_Kitap_Alanlar.EmanetAlan_TCno = hesapbilgileri.Uye_Tc;
                            emanet_Kitap_Alanlar.EmanetAlan_Tel = hesapbilgileri.Uye_telefon;
                            emanet_Kitap_Alanlar.EmanetAldiği_Tarih = DatePicker1.Value;
                            emanet_Kitap_Alanlar.EmanetAlan_ID = hesapbilgileri.Uye_ID;
                            emanet_Kitap_Alanlar.EmanetAlan_Soyad = hesapbilgileri.Uye_Soyadi;
                            genel.db.Emanet_Kitap_Alanlar.Add(emanet_Kitap_Alanlar);
                    }
            
            
                    gidenguncelleme = "Değişti";
                    genel.db.SaveChanges();
                    this.Close();
                }
            }
            else { MessageBox.Show("Lütfen TC kimlik no doğrulayın"); }


        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //TC DOĞRULAMA KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == login.uyeadi) 
            {
                a = Convert.ToInt32(comboBox1.SelectedValue);
                label1.Visible = true;
                label1.Text = "Doğrulandı";
                label1.ForeColor = Color.Green; 
                tckontrol = "doğru"; 
            }
            else 
            {
                if (genel.uye.Uye_Yetki == "Uye")
                {
                    MessageBox.Show("Bu işlemi yapabilmek için gereken yetkiye sahip değilsiniz");
                }
                else
                {
                    maskedTextBox1.Visible = true;
                    a = Convert.ToInt32(comboBox1.SelectedValue);
                    var b = genel.db.Kutuphane_Uye_listesi.Find(a);
                    if (maskedTextBox1.Text == b.Uye_Tc.ToString())
                    {
                        label1.Visible = true;
                        label1.Text = "Doğrulandı";
                        label1.ForeColor = Color.Green;
                        tckontrol = "doğru";
                    }
                    else { label1.Text = "Doğrulanamadı"; label1.Visible = true; label1.ForeColor = Color.Red; }
                }
            }

        }



        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//








    }
}
