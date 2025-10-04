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
namespace projedosyası
{
    public partial class UyelerSondeneme : Form
    {
        public UyelerSondeneme()
        {
            InitializeComponent();
        }
        private void UyelerSondeneme_Load(object sender, EventArgs e)
        {
            uyelistele();
            ChangeDatagridviewDesign();
        }



        public void uyelistele()
        {
            dataGridView1.DataSource = genel.db.Kutuphane_Uye_listesi.ToList();
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

        }


        public void ChangeDatagridviewDesign()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 239, 184);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(154, 213, 24);
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(8, 188, 164);
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.ColumnHeadersHeight = 10;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
        }




        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}




