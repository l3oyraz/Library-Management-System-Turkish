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
    public partial class LOADİNG : Form
    {
        public LOADİNG()
        {
            InitializeComponent();
        }

        private void LOADİNG_Load(object sender, EventArgs e)
        {

            timer1.Interval = 2000;
            timer1.Enabled = true;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
               login lgn = new login();
                lgn.Show();
                timer1.Enabled=false;
                Hide();
            }
            catch (Exception hata)
            {
               MessageBox.Show(hata.Message);
            }
        }
    }
}
