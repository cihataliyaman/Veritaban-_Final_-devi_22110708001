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
    public partial class MusteriSayfa : Form
    {
        private readonly Musteri _musteri;

        public MusteriSayfa(Musteri musteri)
        {
            _musteri = musteri;
            InitializeComponent();
        }

        private int _sonEklenenSiparisID;
        private void btnSepeteGit_Click(object sender, EventArgs e)
        {
            panelSepet.Visible = true;
            groupBox1.Visible = true;

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            int sonEklenenSiparisID = vi.SiparisOlustur(_musteri.Musteri_Id);

            if(sonEklenenSiparisID != -1)
            {
                _sonEklenenSiparisID = sonEklenenSiparisID;
            }
            else
            {
                MessageBox.Show("HATA! ");
            }

            //foreach (DataRow row in dt.Rows)
            //{
            //    int gelenAyakkabiID = (int) row["Ayakkabi_Id"];
            //    DataRow ayakkabi = vi.AyakkabiGetir(gelenAyakkabiID);
            //    string ayakkabiRow = "Marka: " + ayakkabi["Marka"] + " (" + row["Fiyat"] +"₺)" +" Stok:" + row["Stok_Miktari"];
            //    cBoxAyakkabilar.Items.Add(ayakkabiRow);
            //}


        
            DataTable dt = vi.Ayakkabi_Stok();
            dataGridAyakkabilar.DataSource = dt;




        }

        private int secilenAyakkabi_Satici_id;
        private void dataGridAyakkabilar_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            secilenAyakkabi_Satici_id = Convert.ToInt32(dataGridAyakkabilar.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnSiparisTamamla_Click(object sender, EventArgs e)
        {
            int miktar = int.Parse(txtMiktar.Text);

            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            vi.SiparisDetayEkle(_sonEklenenSiparisID, secilenAyakkabi_Satici_id, miktar);

            groupBox1.Visible = false;
            panelSepet.Visible = false;

            txtMiktar.Text = "";

            MessageBox.Show("Siparisiniz alındı!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            VeriTabaniIslemleri vi = new VeriTabaniIslemleri();
            DataTable dt = vi.Siparis_detay_getir();
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;

        }
    }
}
