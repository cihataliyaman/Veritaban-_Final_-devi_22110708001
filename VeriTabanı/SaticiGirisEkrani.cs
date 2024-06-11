using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeriTabanı
{
    public partial class SaticiGirisEkrani : Form
    {
        public SaticiGirisEkrani()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Satici satici = new Satici();
            satici.Ad = textAd.Text;
            satici.Soyad = textSoyad.Text;
            satici.Sifre = textSifre.Text;
            satici.Mail = textMail.Text;
            satici.Telefon = maskedTextTelefon.Text;

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.SaticiEkle(satici);

            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            string saticiMail = textBox1.Text;
            string saticiSifre = textBox2.Text;
            Satici satici = vi.SaticiKullaniciAdiSifreKontrolEt(saticiMail, saticiSifre);
            if (!string.IsNullOrWhiteSpace(satici.Ad))
            {
                SaticiSayfa form = new SaticiSayfa(satici);
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Ya da Şifreniz Hatalı");
            }

        }
    }
}
