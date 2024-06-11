using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace VeriTabanı
{
    internal class VeriTabaniIslemleri
    {

        string connectionString = "Server=CIHATALI\\SQLEXPRESS;Database=veri;Trusted_Connection=True;";
        private SqlConnection connection;
        public VeriTabaniIslemleri()
        {
            connection = new SqlConnection(connectionString);
        }

        public void BaglantiAc()
        {
            // SqlConnection nesnesi oluşturun ve bağlantıyı açın

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bağlantı hatası: " + ex.Message);
            }

        }
        public void BaglantiKapat()
        {
            // SqlConnection nesnesi oluşturun ve bağlantıyı açın


            try
            {
                if(connection.State == ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bağlantı hatası: " + ex.Message);
            }

        }
        public void MusteriEkle(Musteri musteri)
        {
            string storedProcedureName = "MusteriEkle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Ad", musteri.Ad));
                command.Parameters.Add(new SqlParameter("@Soyad", musteri.Soyad));
                command.Parameters.Add(new SqlParameter("@Sifre", musteri.Sifre));
                command.Parameters.Add(new SqlParameter("@Mail", musteri.Mail));
                command.Parameters.Add(new SqlParameter("@Telefon", musteri.Telefon));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public void SaticiEkle(Satici satici)
        {
            string storedProcedureName = "SaticiEkle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Ad", satici.Ad));
                command.Parameters.Add(new SqlParameter("@Soyad", satici.Soyad));
                command.Parameters.Add(new SqlParameter("@Sifre", satici.Sifre));
                command.Parameters.Add(new SqlParameter("@Mail", satici.Mail));
                command.Parameters.Add(new SqlParameter("@Telefon", satici.Telefon));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }

        }
        public void AyakkabiEkle(Ayakkabi ayakkabi)
        {
            string storedProcedureName = "AyakkabiEkle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Marka", ayakkabi.Marka));
                command.Parameters.Add(new SqlParameter("@Model", ayakkabi.Model));
                command.Parameters.Add(new SqlParameter("@Renk", ayakkabi.Renk));
                command.Parameters.Add(new SqlParameter("@Beden", ayakkabi.Beden));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public void AyakkabiStokEkle(int StokMiktari, float Fiyat, int SaticiID, int SecilenAyakkabi)
        {
            string storedProcedureName = "AyakkabiSaticiEkle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Ayakkabi_Id", SecilenAyakkabi));
                command.Parameters.Add(new SqlParameter("@Satici_Id", SaticiID));
                command.Parameters.Add(new SqlParameter("@Stok_Miktari", StokMiktari));
                command.Parameters.Add(new SqlParameter("@Fiyat", Fiyat));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public void AyakkabiStokGuncelle(int StokMiktari, float Fiyat, int SecilenAyakkabiSatici)
        {
            string storedProcedureName = "AyakkabiSaticiGuncelle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Ayakkabi_Satici_Id", SecilenAyakkabiSatici));
                command.Parameters.Add(new SqlParameter("@Stok_Miktari", StokMiktari));
                command.Parameters.Add(new SqlParameter("@Fiyat", Fiyat));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public void AyakkabiSil(int ayakkabiID)
        {
            string storedProcedureName = "AyakkabiSil";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Ayakkabi_Id", ayakkabiID));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public void AyakkabiGuncelle(Ayakkabi ayakkabi)
        {
            string storedProcedureName = "AyakkabiGuncelle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Marka", ayakkabi.Marka));
                command.Parameters.Add(new SqlParameter("@Model", ayakkabi.Model));
                command.Parameters.Add(new SqlParameter("@Renk", ayakkabi.Renk));
                command.Parameters.Add(new SqlParameter("@Beden", ayakkabi.Beden));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }
        public DataTable Ayakkabilar()
        {
            string storedProcedureName = "Ayakkabilar";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BaglantiKapat();
                return dt;
            }
        }
        public DataTable Siparis_detay_getir()
        {
            string storedProcedureName = "Siparis_Detay_Getir";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BaglantiKapat();
                return dt;
            }
        }
        public DataTable Ayakkabi_Stok()
        {
            string storedProcedureName = "AyakkabiSaticilar";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                BaglantiKapat();
                return dt;
            }
        }
        public Musteri MusteriKullaniciAdiSifreKontrolEt(string mailAdresi, string sifre)
        {
            Musteri musteri = new Musteri();
            string storedProcedureName = "MusteriGirisKontrol";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Mail", mailAdresi));
                command.Parameters.Add(new SqlParameter("@Sifre", sifre));
                
                try
                {
                    BaglantiAc();
                    // Eğer sonuçları almak istiyorsanız:
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            musteri.Musteri_Id = Convert.ToInt32(reader["Musteri_Id"]);
                            musteri.Ad = reader["Ad"].ToString();
                            musteri.Soyad = reader["Soyad"].ToString();
                            musteri.Sifre = reader["Sifre"].ToString();
                            musteri.Mail = reader["Mail"].ToString();
                            musteri.Telefon = reader["Telefon"].ToString();
                            return musteri;
                        }
                    }
                    // Eğer sonuç döndürmeyen bir saklı yordam çağırıyorsanız:
                    // command.ExecuteNonQuery();
                    return musteri;
                }
                catch (Exception ex)
                {
                    return musteri;
                }
                finally
                {
                    BaglantiKapat();
                }
            }
        }
        public Satici SaticiKullaniciAdiSifreKontrolEt(string mailAdresi, string sifre)
        {
            Satici satici = new Satici();
            string storedProcedureName = "SaticiGirisKontrol";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Mail", mailAdresi));
                command.Parameters.Add(new SqlParameter("@Sifre", sifre));
                
                try
                {
                    BaglantiAc();
                    // Eğer sonuçları almak istiyorsanız:
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            satici.Satici_Id = Convert.ToInt32(reader["Satici_Id"]);
                            satici.Ad = reader["Ad"].ToString();
                            satici.Soyad = reader["Soyad"].ToString();
                            satici.Sifre = reader["Sifre"].ToString();
                            satici.Mail = reader["Mail"].ToString();
                            satici.Telefon = reader["Telefon"].ToString();
                            return satici;
                        }
                    }
                    // Eğer sonuç döndürmeyen bir saklı yordam çağırıyorsanız:
                    // command.ExecuteNonQuery();
                    return satici;
                }
                catch (Exception ex)
                {
                    return satici;
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                }
                finally
                {
                    BaglantiKapat();
                }
            }
        }


        public DataTable AyakkabilariGetir()
        {
            
            string storedProcedureName = "TumSaticilaraAitTumAyakkabilar";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    BaglantiAc();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Verileri DataGridView'e bağlama
                    return dataTable;

                    // Eğer sonuç döndürmeyen bir saklı yordam çağırıyorsanız:
                    // command.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                    return new DataTable();
                }
                finally
                {
                    BaglantiKapat();
                }
            }

        }

        public DataRow AyakkabiGetir(int AyakkabiID)
        {
            string storedProcedureName = "AyakkabiGetir";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Ayakkabi_Id", AyakkabiID);

                DataTable dt = new DataTable();

                try
                {
                    BaglantiAc();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                    return null;
                }
                finally
                {
                    BaglantiKapat();
                }
            }
        }

        public int SiparisOlustur(int musteriMusteriId)
        {
            Siparis sip = new Siparis();
            sip.Musteri_Id = musteriMusteriId;
            sip.Tarih = DateTime.Now;
            string storedProcedureName = "SiparisEkle";
            int sonEklenenSiparisId;

            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Musteri_Id", sip.Musteri_Id));
                command.Parameters.Add(new SqlParameter("@Tarih", sip.Tarih));

                // ExecuteScalar kullanarak son eklenen Siparis_Id değerini al
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    sonEklenenSiparisId = Convert.ToInt32(result);
                }
                else
                {
                    // Hata durumunda geçerli bir değer döndür veya uygun bir hata işleme gerçekleştirin
                    sonEklenenSiparisId = -1; // veya hata kodu
                }

                BaglantiKapat();
            }

            return sonEklenenSiparisId;
        }

        public void SiparisDetayEkle(int SiparisID, int AyakkaciSaticiID, float Miktar)
        {
            string storedProcedureName = "SiparisDetayEkle";
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                BaglantiAc();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Siparis_Id", SiparisID));
                command.Parameters.Add(new SqlParameter("@Ayakkabi_Satici_Id", AyakkaciSaticiID));
                command.Parameters.Add(new SqlParameter("@Miktar", Miktar));
                command.ExecuteNonQuery();
                BaglantiKapat();
            }
        }

        public List<Siparis> SiparisleriAl(int musteriMusteriId)
        {
            List<Siparis> siparisListesi = new List<Siparis>();
            string storedProcedureName = "SiparisleriAl";//Düzeltmen Lazım
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@MusteriId", musteriMusteriId));

                try
                {
                    BaglantiAc();
                    // Eğer sonuçları almak istiyorsanız:
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Siparis sip = new Siparis();
                            sip.Musteri_Id = Convert.ToInt32(reader["Musteri_Id"]);
                            sip.Siparis_Id = Convert.ToInt32(reader["Siparis_Id"]);
                            sip.Tarih = Convert.ToDateTime(reader["Tarih"]);
                            siparisListesi.Add(sip);
                        }
                    }
                    return siparisListesi;
                }
                catch (Exception ex)
                {
                    return siparisListesi;
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                }
                finally
                {
                    BaglantiKapat();
                }
            }
        }
    }
    public class Satici
    {
        public int Satici_Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
    }
    public class Musteri
    {
        public int Musteri_Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
    }
    public class Ayakkabi
    {
        public int Ayakkabi_Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string Renk { get; set; }
        public int Beden { get; set; }
    }
    public class Ayakkabi_Satici
    {
        public int Ayakkabi_Satici_Id { get; set; }
        public int Ayakkabi_Id { get; set; }
        public int Satici_Id { get; set; }
        public double Stok_Miktari { get; set; }
        public double Fiyat { get; set; }
    }
    public class Siparis
    {
        public int Siparis_Id { get; set; }
        public int Musteri_Id { get; set; }
        public DateTime Tarih { get; set; }
    }
    public class Siparis_Detay
    {
        public int Siparis_Detay_Id { get; set; }
        public int Siparis_Id { get; set; }
        public int Ayakkabi_Satici_Id { get; set; }
        public double Miktar { get; set; }
    }
}
