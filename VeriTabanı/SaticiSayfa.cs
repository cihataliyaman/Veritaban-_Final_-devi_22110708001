using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace VeriTabanı
{
    public partial class SaticiSayfa : Form
    {
        private readonly Satici _satici;

        public SaticiSayfa(Satici satici)
        {
            _satici = satici;
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Ayakkabi ayakkabi = new Ayakkabi();
            ayakkabi.Marka = textBox1.Text;
            ayakkabi.Model = textBox2.Text;
            ayakkabi.Renk = textBox3.Text;
            ayakkabi.Beden = int.Parse(textBox4.Text);

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.AyakkabiEkle(ayakkabi);

            groupBox1.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Ayakkabilar();
            dataGridView1.DataSource = dt;
            groupBox2.Visible = true;
        }
        private int secilen;

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            groupBox5.Visible = true;
            secilen = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            textBoxAyakkabiID.Text = secilen.ToString();

        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.AyakkabiSil(secilen);
            MessageBox.Show("Ayakkabı başarıyla silindi.");
            groupBox2.Visible = false;
        }


        private void button8_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Ayakkabilar();
            dataGridViewAyakkabilar.DataSource = dt;
            groupBox4.Visible = true;
        }

        private void btnStokIslemleri_Click(object sender, EventArgs e)
        {
            panelStokIslemleri.Visible = true;

        }
        private void button9_Click(object sender, EventArgs e)
        {
            gBoxStokEkle.Visible = true;
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Ayakkabilar();
            dataGridStokAyakkabilar.DataSource = dt;
        }
        private int secilenAyakkabi;

        private void dataGridStokAyakkabilar_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            gBoxStokEkleInner.Visible = true;
            secilenAyakkabi = Convert.ToInt32(dataGridStokAyakkabilar.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnStokEkle_Click(object sender, EventArgs e)
        {
            float fiyat = float.Parse(txtAyakkabiFiyat.Text);
            int stokMiktari = int.Parse(txtEkleStokMiktari.Text);
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.AyakkabiStokEkle(stokMiktari, fiyat, _satici.Satici_Id, secilenAyakkabi);
            MessageBox.Show(secilenAyakkabi + " nolu ayakkabıya stok eklendi.");
            gBoxStokEkleInner.Visible = false;
            panelStokIslemleri.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            gBoxStokSil.Visible = true;
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Ayakkabi_Stok();
            dataGridView2.DataSource = dt;
        }

        private int secilenAyakkabiStok;

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gBoxStokDS.Visible = true;
            secilenAyakkabiStok = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
            int secilenAyakkabiRow = e.RowIndex;
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Ayakkabi_Stok();

            // Fiyat ve Stok_Miktari sütunlarının türünü kontrol et ve uygun şekilde dönüştür
            object fiyatValue = dt.Rows[secilenAyakkabiRow]["Fiyat"];
            object stokMiktariValue = dt.Rows[secilenAyakkabiRow]["Stok_Miktari"];

            float selectedObjectFiyat;
            float selectedObjectStok;

            if (fiyatValue != DBNull.Value)
            {
                selectedObjectFiyat = Convert.ToSingle(fiyatValue);
            }
            else
            {
                selectedObjectFiyat = 0f;
            }

            if (stokMiktariValue != DBNull.Value)
            {
                selectedObjectStok = Convert.ToSingle(stokMiktariValue);
            }
            else
            {
                selectedObjectStok = 0f;
            }

            txtSilStokMiktari.Text = selectedObjectStok.ToString();
            textBox5.Text = selectedObjectFiyat.ToString();

        }

        private void btnStokSil_Click(object sender, EventArgs e)
        {
            float fiyat = float.Parse(textBox5.Text);
            int stokMiktari = int.Parse(txtSilStokMiktari.Text);
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.AyakkabiStokGuncelle(stokMiktari, fiyat, secilenAyakkabiStok);
            panelStokIslemleri.Visible = false;
            gBoxStokDS.Visible = false;
        }


        private void button12_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
    }
}
