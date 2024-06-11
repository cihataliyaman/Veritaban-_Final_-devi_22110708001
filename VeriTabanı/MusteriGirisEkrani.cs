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
    public partial class MusteriGirisEkrani : Form
    {
        public MusteriGirisEkrani()
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
            Musteri musteri = new Musteri();
            musteri.Ad= textAd.Text;
            musteri.Soyad = textSoyad.Text;
            musteri.Mail = textMail.Text;
            musteri.Sifre = textSifre.Text;
            musteri.Telefon = maskedTextTelefon.Text;

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.MusteriEkle(musteri);

            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            string mailAdresi = textBox1.Text;
            string sifre = textBox2.Text;
            Musteri musteri = vi.MusteriKullaniciAdiSifreKontrolEt(mailAdresi,sifre);
            if (!string.IsNullOrWhiteSpace(musteri.Ad))
            {
                MusteriSayfa form = new MusteriSayfa(musteri);
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
