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
using Microsoft.Identity.Client;

namespace projedosyası
{
    public partial class profil : Form
    {
        public profil()
        {
            InitializeComponent();
        }
        private string db = "Data Source=BOYRAZ;Initial Catalog=Kutuphane_Projesi;Integrated Security=True;TrustServerCertificate=True";
        public static string x;
        

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
                string selectedFilePath = openFileDialog.FileName;
                bunifuImageButton1.Image = Image.FromFile(selectedFilePath);
            }
        }

        private void KaUye_AditxtBox_TextChanged(object sender, EventArgs e)
        {
            //KaUye_AditxtBox.PlaceholderText=login.x.ToString();
            //Soyisim.PlaceholderText=login.y.ToString();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void profil_Load(object sender, EventArgs e)
        {
            KaUye_AditxtBox_TextChanged(sender, e);
            ProfilGetir();
        }

        private void bunifuPictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void ProfilGetir()
        {
            string uyeAdi = "";    
            string uyeSoyadi = "";
            string uyeTC = "";
            string uyeEmail = "";
            string uyeTelefon = "";
            string uyeSifre = "";

        
            string sorgu = "SELECT Uye_Adi, Uye_Soyadi, Uye_TC, Uye_Email, Uye_Telefon, Uye_Sifre FROM Kutuphane_Uye_listesi WHERE Uye_Adi = @Uye_Adi";


            using (SqlConnection baglanti = new SqlConnection(db))
            {

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                {

                    komut.Parameters.AddWithValue("@Uye_Adi", uyeAdi);

                    try
                    {

                        baglanti.Open();


                        using (SqlDataReader okuyucu = komut.ExecuteReader())
                        {
                            if (okuyucu.Read())
                            {
                                uyeAdi = okuyucu.GetString(0);
                                uyeSoyadi = okuyucu.GetString(1);
                                uyeTC = okuyucu.GetString(2);
                                uyeEmail = okuyucu.GetString(3);
                                uyeTelefon = okuyucu.GetString(4);
                                uyeSifre = okuyucu.GetString(5);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    }
                }
                UyeE_mailtxtBox.PlaceholderText = uyeEmail;
                KaUye_AditxtBox.PlaceholderText = uyeAdi;
                Soyisim.PlaceholderText = uyeSoyadi;
                KaUye_TctxtBox.PlaceholderText = uyeTC;
                KaUye_TeltxtBox.PlaceholderText = uyeTelefon;
                K_Sifretxtbox.PlaceholderText = uyeSifre;

            }

        }

        private void bunifuPictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
