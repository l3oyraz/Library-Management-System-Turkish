using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraBars.Alerter;
using Microsoft.Identity.Client;
using System.Linq;
using DevExpress.Utils.Serializing;


namespace projedosyası
{
    public partial class anaSayfa : Form
    {
        private Size initialSize;
        private bool isMaximized = false;
        private Point lastPoint;

       
        public anaSayfa()
        {
            InitializeComponent();
            txtKitapAramaA.TextChanged += TxtKitapAramaA_TextChanged;
        }

        private void anaSayfa_Load(object sender, EventArgs e)
        {
            DatePicker1.Value = System.DateTime.Now;  
            kitapListele();
            ChangeDatagridviewDesign();
            label2_Click(sender, e);
            label3_Click(sender, e);
            label1_Click(sender, e);
            label4_Click(sender, e);
            label5_Click(sender, e);
            if (label4.Text== "Misafir Kullanıcı") 
            {  // burda misafir kullanıcı giriş buttonuna basıldı ise eğer bazı butonların görünürlüğünü engellemek için kullanılmıştır;
                husakads.Visible = false;
                bunifuImageButton6.Visible = false;
                bunifuImageButton7.Visible = false;
                bunifuLabel1.Visible = false;
                bunifuImageButton8.Visible = false;
                bunifuLabel4.Visible = false;
                bunifuImageButton9.Visible = false;
                bunifuLabel3.Visible = false;
                bunifuPictureBox4.Visible = false;
            }
            if (label4.Text != "Misafir Kullanıcı") 
            {
                if (genel.uye.Uye_Yetki == "Uye")//YETKİ TİCKİ 
                {
                    bunifuPictureBox4.Visible = false;
                }
            }


        }
        private void anaSayfa_Activated(object sender, EventArgs e)
        {
            label1_Click(sender,e);
            label3_Click(sender,e);
        }




        //UYE ADI LABEL KODU
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void label4_Click(object sender, EventArgs e)
        {
            login login = new login();
            label4.Text = login.uyeadi;

        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //KİTAP ÜYE EMANET SAYAÇ KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void label2_Click(object sender, EventArgs e)
        {
            String Kitapsayisi = Convert.ToString(genel.db.Kitaplarimiz.Count());
            label2.Text = Kitapsayisi;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            String uyeler = Convert.ToString(genel.db.Kutuphane_Uye_listesi.Count());
            label3.Text = uyeler;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            String emanetler = Convert.ToString(genel.db.Emanet_Kitap_Alanlar.Count()); // bu labal da emanet alana ların sayısını belirtmiş;
            label1.Text = emanetler;  
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//




        //KİTAP LİSTELEME VE ARAMA KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//

        public void kitapListele()
        {
            dataGridView1.DataSource = genel.db.Kitaplarimiz.ToList();
        }

        private void TxtKitapAramaA_TextChanged(object sender, EventArgs e)
        {
           dataGridView1.DataSource = genel.db.Kitaplarimiz.
                Where
                (x=> x.Kitap_Adi.Contains(txtKitapAramaA.Text) ||
                     x.Kitap_yazar.Contains(txtKitapAramaA.Text) ||
                     x.Kitap_Turu.Contains(txtKitapAramaA.Text) ||
                     x.Kitap_Konusu.Contains(txtKitapAramaA.Text) ||
                     x.Kitap_Yayinevi.Contains(txtKitapAramaA.Text) ||
                     x.Kitap_basimyili.ToString() == txtKitapAramaA.Text 
                ).ToList();
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //LİSTE GÖRÜNÜM KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX// bu kodda dataGridView in tasarımıyla oynamak içim
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
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
       



        //UYGULAMA KAPATMA,KÜÇÜLTME VE FORMLARA YÖNLENDİRME KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX// 
        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {  // Kullanıcıya emin olup olmadığı sorulur
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanıcı evet'e tıklarsa, uygulamayı kapat
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //xxx   //formu alta almak için minimized kommutunu kullanılmıştır hoca ful ekran nasıl alınır diye sora bilir
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            KitaplarSondeneme kitaplarSondeneme = new KitaplarSondeneme();
            kitaplarSondeneme.Show();
            this.Hide();
        }
        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            KitaplarSondeneme kitaplarSondeneme = new KitaplarSondeneme();
            kitaplarSondeneme.Show();
            this.Hide();
        }
        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            UyelerSondeneme Uyeler = new UyelerSondeneme();
            Uyeler.ShowDialog();
        }
        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            EmanetlerSondeneme emanetlerSondeneme = new EmanetlerSondeneme();
            emanetlerSondeneme.ShowDialog();
        }
        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
            EmanetlerSondeneme emanetlerSondeneme = new EmanetlerSondeneme();
            emanetlerSondeneme.ShowDialog();
        }
        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            EmanetlerSondeneme emanetlerSondeneme = new EmanetlerSondeneme();
            emanetlerSondeneme.ShowDialog();
        }
        private void bunifuPictureBox3_Click(object sender, EventArgs e)
        {
          if(label4.Text== "Misafir Kullanıcı") { KayitSayfasi kayitSayfasi = new KayitSayfasi(); kayitSayfasi.Show(); Hide(); }
          else { Form1 form1 = new Form1(); form1.ShowDialog(); }
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //FORM KONUMLANDIRMA KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Fare butona basıldığında, formun son konumunu sakla
            lastPoint = new Point(e.X, e.Y);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Fare hareket ettiğinde, formu sürükle
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private bool isFullScreen = false;
        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (isFullScreen)
            {
                // Tam ekrandan çık, orijinal boyuta ve konuma dön
                this.WindowState = FormWindowState.Normal;
                
                isFullScreen = false;
            }
            else
            {
                // Tam ekrana geçiş yap
                
                this.WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
        }




        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//

    }




}






/*public static class FormProcess
{
    public static void ChangeDatagridviewDesign(this DataGridView datagridview)
    {
           //İlk sütunun gizlenmesini sağlar
            datagridview.RowHeadersVisible = false;

            //Herhangibir sütunun genişliğini o sütunda yer alan en uzun değere göre ayarlar
            datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           
            //Datagridview border sıfırlama
            datagridview.BorderStyle = BorderStyle.None;

            
            //Seçilen hücrenin arkaplan rengini belirleme
            datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 128);

            //Seçilen hücrenin yazı rengini belirleme
            datagridview.DefaultCellStyle.SelectionForeColor = Color.FromArgb(211, 36, 44);

            //Hücredeki yazıları ortalar
            datagridview.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Satırların border'ını yatay çizgi yapma          
            datagridview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;


            //Datagridview başlık özelliğini değiştirmeyi etkinleştirme
            datagridview.EnableHeadersVisualStyles = false;

            //Başlıktaki çizgileri kaldırma
            datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            //Başlık arkaplan rengini belirleme
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(8, 188, 164);

            //Başlık yazi rengini belirleme
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Başlığın satır boyutunu değiştirme
            datagridview.ColumnHeadersHeight = 10;

            //Başlığın seçilen hücre başlığının arkaplan rengini belirleme
            datagridview.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(8, 188, 164);

            //Seçilen hücre başlığının yazı rengini belirleme
            datagridview.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            //Başlık yazı ortalama
            datagridview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Satırı tamamen seçmeyi sağlar
            datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Satır yüksekliğini ayarlar
            datagridview.RowTemplate.Height = 40;

            //Satır eklemeyi engeller
            //datagridview.AllowUserToAddRows = false;

            //Satır silmeyi engeller
            datagridview.AllowUserToDeleteRows = false;

            //Hücrede değişiklik yapmayı engeller, sadece okunmasını sağlar
            datagridview.ReadOnly = true;

            //Satırların yeniden boyutlandırılmasını engeller
            datagridview.AllowUserToResizeRows = false;

            //Sütunların yeniden boyutlandırılmasını engeller
            datagridview.AllowUserToResizeColumns = false;
    }
}*/


