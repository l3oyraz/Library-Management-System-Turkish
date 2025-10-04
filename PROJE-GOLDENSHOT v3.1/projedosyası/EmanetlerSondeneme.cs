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

namespace projedosyası
{
    public partial class EmanetlerSondeneme : Form
    {
        public EmanetlerSondeneme()
        {
            InitializeComponent();
        }
        private Size initialSize;
        private bool isMaximized = false;
        private Point lastPoint;

        //GÜNCELLEME İŞLEMLERİ İÇİN SAYFA İÇİ DEĞİŞKENLER
        public static int secilenID = 99999;
        public static int secilenID2;
        public static string secilenad;
        public static string secilensoyad;
        public static string tckontrol = "yanlış";
        public static int secilenID3;
        public static int a;
        public static int ucretlendirilecekID;
        DateTime bugun = Convert.ToDateTime(KitaplarSondeneme.secilitarih);
        DateTime songun;
        private void EmanetlerSondeneme_Load(object sender, EventArgs e)
        {
            kitaplist();
            ChangeDatagridviewDesign();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;

            maskedTextBox1.Visible = false;
            maskedTextBox1.MaxLength = 11;
            label1.Visible = false;
            comboBox1.DataSource = genel.db.Kutuphane_Uye_listesi.Select(x => new { x.Uye_ID, uyead = x.Uye_Adi + " " + x.Uye_Soyadi }).ToList();
            comboBox1.DisplayMember = "uyead";
            comboBox1.ValueMember = "Uye_ID";
            if (genel.uye.Uye_Yetki == "Uye")
            {
                comboBox1.DataSource = genel.db.Kutuphane_Uye_listesi
                    .Select(x => new { x.Uye_ID, uyead = x.Uye_Adi + " " + x.Uye_Soyadi })
                    .ToList();
                comboBox1.DisplayMember = "uyead";
                comboBox1.ValueMember = "Uye_ID";

                if (genel.uye.Uye_Yetki == "Uye")
                {
                    comboBox1.DataSource = genel.db.Kutuphane_Uye_listesi
                        .Select(x => new { x.Uye_ID, uyead = x.Uye_Adi + " " + x.Uye_Soyadi })
                        .ToList();
                    comboBox1.DisplayMember = "uyead";
                    comboBox1.ValueMember = "Uye_ID";

                    // comboBox1'e veri yüklendikten sonra uygun bir Uye_ID seçin
                    int selectedIndex = genel.uye.Uye_ID - 1;
                    if (selectedIndex >= 0 && selectedIndex < comboBox1.Items.Count)
                    {
                        comboBox1.SelectedIndex = selectedIndex;
                    }
                  
                }
            }
        }
        private void EmanetlerSondeneme_Activated(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(comboBox1.SelectedValue);//TC doğrulamadan sonra combobox değişip değişmediği kontrolü
            if (a == Convert.ToInt32(comboBox1.SelectedValue)) { tckontrol = "yanlış"; label1.Text = "Doğrulanamadı"; label1.Visible = true; label1.ForeColor = Color.Red; 
        }
    }
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        //DATAGRİD LİSTELEME KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void kitaplist()
        {
            if (genel.uye.Uye_Yetki == "Uye")//EĞER GİREN KİŞİ ÜYE İSE LİSTELEMEYİ KİŞİSEL YAPACAK KOD
            {
                dataGridView1.DataSource = genel.db.Emanet_Kitap_Alanlar.
                    Where
                    (x =>
                         x.EmanetAlan_Adi + " " + x.EmanetAlan_Soyad == genel.uye.Uye_Adi + " " + genel.uye.Uye_Soyadi
                    ).ToList();
                label3.Visible = false;
            }
            else { dataGridView1.DataSource = genel.db.Emanet_Kitap_Alanlar.ToList(); }
           

        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


        //DATAGRİD  KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void ChangeDatagridviewDesign()
        {
            dataGridView1.BackgroundColor = Color.LightGray;
            dataGridView1.DefaultCellStyle.Font = new Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView1.ClearSelection();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 100, 88);//65; 100; 88 215, 239, 184
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.LightGreen;//175 238 238
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(8, 188, 164);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
        }
        //public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    secilenID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
        //    secilenID2 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["EmanetKitap_ID"].Value);
        //    secilenad = dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_Adi"].Value.ToString();
        //    secilensoyad = dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_Soyad"].Value.ToString();
        //    songun = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["EmanetAldiği_Tarih"].Value);
        //    ucretlendirilecekID = (int)dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_ID"].Value;
        //}
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // İndekslerin geçerli olup olmadığını kontrol et
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count && e.ColumnIndex >= 0)
            {
                try
                {
                    secilenID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    secilenID2 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["EmanetKitap_ID"].Value);
                    secilenad = dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_Adi"].Value.ToString();
                    secilensoyad = dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_Soyad"].Value.ToString();
                    songun = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["EmanetAldiği_Tarih"].Value);
                    ucretlendirilecekID = (int)dataGridView1.Rows[e.RowIndex].Cells["EmanetAlan_ID"].Value;
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Geçersiz satır seçimi.");
            }
        }



        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //İŞLEM ONAYLAMA KODLARI 
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        //public void bunifuImageButton4_Click(object sender, EventArgs e)
        //{
        //    if (tckontrol == "doğru")
        //    {
        //        if (secilenID == 99999)
        //        {
        //            MessageBox.Show("Lütfen bir kitap seçin");
        //        }
        //        else
        //        {
        //            if(secilenID3 == secilenID)
        //            {
        //                TimeSpan borc = bugun - songun;
        //                int borcmiktar = borc.Days * 10;
        //                int aranacakID = secilenID;
        //                var emanetsecilen = genel.db.Emanet_Kitap_Alanlar.Find(aranacakID);
        //                var ktplarartan = genel.db.Kitaplarimiz.Find(secilenID2);
        //                var sayi = Convert.ToInt32(ktplarartan.Kitap_Adet);

        //                ktplarartan.Kitap_Adet = sayi + 1;
        //                genel.db.Emanet_Kitap_Alanlar.Remove(emanetsecilen);

        //                if (borcmiktar > 0) { MessageBox.Show("Teslim gecikme ücreti "+borcmiktar+"TL."); } else { MessageBox.Show("Teslim gecikme ücreti bulunmamaktadır."); }

        //                if(ucretlendirilecekID == genel.uye.Uye_ID) 
        //                {
        //                    int a = Convert.ToInt32(genel.uye.Uye_Borc.Value);
        //                    genel.uye.Uye_Borc = a + borcmiktar;
        //                    genel.uye.Uye_ToplamOkunan = genel.uye.Uye_ToplamOkunan + 1;
        //                }
        //                else 
        //                {
        //                    var uye = genel.db.Kutuphane_Uye_listesi.Find(ucretlendirilecekID);
        //                    int a = Convert.ToInt32(uye.Uye_Borc.Value);
        //                    uye.Uye_Borc = a + borcmiktar;
        //                    uye.Uye_ToplamOkunan = uye.Uye_ToplamOkunan + 1;
        //                }


        //                genel.db.SaveChanges();
        //                this.Close();
        //            }
        //            else { MessageBox.Show("Lütfen TC kimlik no doğrulayın"); }

        //        }



        //    }
        //    else { MessageBox.Show("Lütfen TC kimlik no doğrulayın"); }

        //}
        public void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            if (tckontrol == "doğru")
            {
                if (secilenID == 99999)
                {
                    MessageBox.Show("Lütfen bir kitap seçin");
                }
                else
                {
                    if (secilenID3 == secilenID)
                    {
                        TimeSpan borc = bugun - songun;
                        int borcmiktar = borc.Days * 10;
                        int aranacakID = secilenID;
                        var emanetsecilen = genel.db.Emanet_Kitap_Alanlar.Find(aranacakID);
                        var ktplarartan = genel.db.Kitaplarimiz.Find(secilenID2);
                        var sayi = Convert.ToInt32(ktplarartan.Kitap_Adet);

                        ktplarartan.Kitap_Adet = sayi + 1;
                        genel.db.Emanet_Kitap_Alanlar.Remove(emanetsecilen);

                        if (borcmiktar > 0) { MessageBox.Show("Teslim gecikme ücreti " + borcmiktar + "TL."); } else { MessageBox.Show("Teslim gecikme ücreti bulunmamaktadır."); }

                        int yeniBorc = 0; // Yeni borc değeri

                        if (ucretlendirilecekID == genel.uye.Uye_ID)
                        {
                            int a = Convert.ToInt32(genel.uye.Uye_Borc.Value);
                            yeniBorc = a + borcmiktar;
                            genel.uye.Uye_Borc = yeniBorc < 0 ? 0 : yeniBorc; // Eğer yeni borc eksi ise 0 olarak ayarla
                            genel.uye.Uye_ToplamOkunan = genel.uye.Uye_ToplamOkunan + 1;
                        }
                        else
                        {
                            var uye = genel.db.Kutuphane_Uye_listesi.Find(ucretlendirilecekID);
                            int a = Convert.ToInt32(uye.Uye_Borc.Value);
                            yeniBorc = a + borcmiktar;
                            uye.Uye_Borc = yeniBorc < 0 ? 0 : yeniBorc; // Eğer yeni borc eksi ise 0 olarak ayarla
                            uye.Uye_ToplamOkunan = uye.Uye_ToplamOkunan + 1;
                        }

                        genel.db.SaveChanges();
                        this.Close();
                    }
                    else { MessageBox.Show("Lütfen TC kimlik no doğrulayın"); }

                }
            }
            else { MessageBox.Show("Lütfen TC kimlik no doğrulayın"); }
        }

        private void bunifuLabel15_Click(object sender, EventArgs e)
        {
            bunifuImageButton4_Click(sender, e);
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //TC DOĞRULAMA KODU
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void button1_Click(object sender, EventArgs e)
        {
            if (secilenID == 99999)
            {
                MessageBox.Show("Lütfen bir kitap seçin");
            }
            else { 
                if (comboBox1.Text == secilenad + " " + secilensoyad)
                    {
                        if (comboBox1.Text == login.uyeadi)
                        {
                            secilenID3 = secilenID;
                            label1.Visible = true;
                            label1.Text = "Doğrulandı";
                            label1.ForeColor = Color.Green;
                            tckontrol = "doğru";
                            a = Convert.ToInt32(comboBox1.SelectedValue);
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
                                        secilenID3 = secilenID;
                                        label1.Visible = true;
                                        label1.Text = "Doğrulandı";
                                        label1.ForeColor = Color.Green;
                                        tckontrol = "doğru";
                                    }
                                    else { label1.Text = "Doğrulanamadı"; label1.Visible = true; label1.ForeColor = Color.Red; }
                             }

                        }

                    }
                    else { MessageBox.Show("Emanet alan üye doğrulanan tc ile uyuşmuyor"); }
            }
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //KİŞİSEL FİLTRELEME KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void txtKitapAramaA_TextChanged(object sender, EventArgs e)
        {

                  dataGridView1.DataSource = genel.db.Emanet_Kitap_Alanlar.
                     Where
                     (x => x.EmanetKitap_Adi.Contains(txtKitapAramaA.Text) ||
                          x.EmanetKitap_Turu.Contains(txtKitapAramaA.Text) ||
                          x.EmanetAlan_Adi.Contains(txtKitapAramaA.Text) ||
                          x.EmanetAlan_Email.Contains(txtKitapAramaA.Text) 
                     ).ToList();
        }

        private void label3_Click(object sender, EventArgs e)
        {
                dataGridView1.DataSource = genel.db.Emanet_Kitap_Alanlar.
                Where
                (x => 
                     x.EmanetAlan_Adi + " " + x.EmanetAlan_Soyad == genel.uye.Uye_Adi+" "+genel.uye.Uye_Soyadi 
                ).ToList();
        }


        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//




    }
}