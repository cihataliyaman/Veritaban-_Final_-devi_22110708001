using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriTabanı
{
    public partial class IlkSayfa : Form
    {
        public IlkSayfa()
        {
            InitializeComponent();
        }
        private void IlkSayfa_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusteriGirisEkrani form = new MusteriGirisEkrani();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaticiGirisEkrani form = new SaticiGirisEkrani();
            form.Show();
        }
    }
}
