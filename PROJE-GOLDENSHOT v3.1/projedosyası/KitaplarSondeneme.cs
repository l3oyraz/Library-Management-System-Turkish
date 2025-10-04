using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Data.Helpers;
using System.Diagnostics.Eventing.Reader;

namespace projedosyası
{
    public partial class KitaplarSondeneme : Form
    {

        private Size initialSize;
        private bool isMaximized = false;
        private Point lastPoint;


        //KİTAP ÖDÜNÇ İÇİN DEĞİŞKENLER
        public static string kitapadiOdunc = "boş";
        public static string kitapyazariOdunc;
        public static string kitapbasimOdunc;
        public static string kitapturuOdunc;
        public static int kitapsayiOdunc;
        public static string secilihucre;
        public static string kitapBarkodOdunc;
        public static string kitapIDOdunc;

        public static DateTime secilitarih; 
        
        public KitaplarSondeneme()
        {
            InitializeComponent();
            dataGridView1.ClearSelection();
        }

        public void KitaplarSondeneme_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            DatePicker1.Value = System.DateTime.Now;
            kitapListele();
            ChangeDatagridviewDesign();
            label4_Click(sender, e);
            if (genel.uye.Uye_Yetki == "Uye")//YETKİ TİCKİ 
            {
                bunifuPictureBox6.Visible = false;
            }
            
            
        }
        private void KitaplarSondeneme_Activated(object sender, EventArgs e)
        {
            kitapListele();
            secilitarih = DatePicker1.Value;
        }



        //UYE ADI LABEL KODU
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void label4_Click(object sender, EventArgs e)
        {
            login login = new login();
            label4.Text = login.uyeadi;
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //KİTAP LİSTELEME , ARAMA ve KATEGORİLENDİRME KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void kitapListele()
        {
            dataGridView1.DataSource = genel.db.Kitaplarimiz.ToList();
        }
        public void bunifuButton1_Click(object sender, EventArgs e)
        {
            kitapListele();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
              dataGridView1.DataSource = genel.db.Kitaplarimiz.
              Where
              (x => x.Kitap_Adi.Contains(bunifuTextBox1.Text) ||
                   x.Kitap_yazar.Contains(bunifuTextBox1.Text) ||
                   x.Kitap_Turu.Contains(bunifuTextBox1.Text) ||
                   x.Kitap_Konusu.Contains(bunifuTextBox1.Text) ||
                   x.Kitap_Yayinevi.Contains(bunifuTextBox1.Text) ||
                   x.Kitap_basimyili.ToString() == bunifuTextBox1.Text
              ).ToList();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dataGridView1.DataSource = genel.db.Kitaplarimiz.
                Where
                (x =>
                     x.Kitap_Konusu == treeView1.SelectedNode.Text

                ).ToList();
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


        //KİTAP ÖDÜNÇ İÇİN SEÇİLİ HÜCRE KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            kitapadiOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_Adi"].Value.ToString();
            kitapyazariOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_yazar"].Value.ToString();
            kitapturuOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_Turu"].Value.ToString();
            kitapbasimOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_Basimyili"].Value.ToString();
            kitapsayiOdunc = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Kitap_Adet"].Value);
            kitapBarkodOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_Barkod"].Value.ToString();
            kitapIDOdunc = dataGridView1.Rows[e.RowIndex].Cells["Kitap_ID"].Value.ToString();
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //LİSTE GÖRÜNÜM KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        public void ChangeDatagridviewDesign()
        {
            dataGridView1.BackgroundColor = Color.LightGray;
            
            dataGridView1.ClearSelection();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(65, 100, 88);//65; 100; 88 215, 239, 184
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.LightGreen;//175 238 238
            
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 9);
            
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
        {
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kullanıcı evet'e tıklarsa, uygulamayı kapat
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void bunifuLabel16_Click(object sender, EventArgs e)
        {
            anaSayfa anaSayfa = new anaSayfa();
            anaSayfa.Show();
            this.Hide();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            anaSayfa anaSayfa = new anaSayfa();
            anaSayfa.Show();
            this.Hide();
        }
        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
            EmanetlerSondeneme emanetlerSondeneme = new EmanetlerSondeneme();
            emanetlerSondeneme.ShowDialog();
        }
        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            EmanetlerSondeneme emanetlerSondeneme = new EmanetlerSondeneme();
            emanetlerSondeneme.ShowDialog();
        }
        private void bunifuLabel3_Click(object sender, EventArgs e)
        {
            UyelerSondeneme uyelerSondeneme = new UyelerSondeneme();
            uyelerSondeneme.ShowDialog();
        }
        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            UyelerSondeneme uyelerSondeneme = new UyelerSondeneme();
            uyelerSondeneme.ShowDialog();
        }
        private void bunifuLabel2_Click(object sender, EventArgs e)
        {
            KitapBagıs kitapBagıs = new KitapBagıs();
            kitapBagıs.ShowDialog();
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            KitapBagıs kitapBagıs = new KitapBagıs();
            kitapBagıs.ShowDialog();
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            KitapOdunc kitapOdunc = new KitapOdunc();
            kitapOdunc.Show();
        }
        private void bunifuLabel5_Click(object sender, EventArgs e)
        {
            KitapOdunc kitapOdunc = new KitapOdunc();
            kitapOdunc.Show(); 
        }
        private void bunifuPictureBox5_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //FORM KONUMLANDIRMA KODLARI
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//
        private void KitaplarSondeneme_MouseMove(object sender, MouseEventArgs e)
        {
            // Fare hareket ettiğinde, formu sürükle
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void KitaplarSondeneme_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Herhangi bir hücre seçildiğinde, seçimi temizle
            dataGridView1.ClearSelection();
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//



        //KOD DENEME
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX//


    }
}
