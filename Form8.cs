using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemAnalizi
{
    public partial class Form8 : Form
    {
        int dershaneYillikOdemesi = 120000;
        string connectionString = "Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False";

        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            OgrenciBilgileriniGetir();
        }

        private void OgrenciBilgileriniGetir()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci_ID, OGRENCI_ADI, TC, OGRENCI_TEL FROM Ogrenci", connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                    dataGridView2.Columns["Ogrenci_ID"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öğrenci bilgileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                textBox1.Text = selectedRow.Cells["OGRENCI_ADI"].Value.ToString();
                textBox2.Text = selectedRow.Cells["TC"].Value.ToString();
                textBox3.Text = selectedRow.Cells["OGRENCI_TEL"].Value.ToString();
                string ogrenciID = selectedRow.Cells["Ogrenci_ID"].Value.ToString();
                OdemeBilgileriniGetir(ogrenciID);
            }
        }

        private void OdemeBilgileriniGetir(string ogrenciID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT O.odeme_id, O.ogrenci_id, S.OGRENCI_ADI, O.odeme_tutari, " +
                        "CONVERT(NVARCHAR, O.odeme_tarihi, 104) AS ODEME_TARIHI, " +
                        "CASE WHEN O.pesin = 'True' THEN 'ÖDEME KALMAMIŞTIR' ELSE CAST(O.taksit_numarasi AS NVARCHAR) END AS ODEME_DURUMU " +
                        "FROM Odeme O INNER JOIN Ogrenci S ON O.ogrenci_id = S.Ogrenci_ID WHERE O.ogrenci_id = @ogrenci_id", connection);
                    da.SelectCommand.Parameters.AddWithValue("@ogrenci_id", ogrenciID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dt.Columns["odeme_id"].ColumnMapping = MappingType.Hidden;
                    dt.Columns["ogrenci_id"].ColumnMapping = MappingType.Hidden;
                    dataGridView1.DataSource = dt;

                    int toplamOdemeMiktari = GetToplamOdemeMiktari(ogrenciID);
                    label4.Text = toplamOdemeMiktari.ToString();

                    if (toplamOdemeMiktari >= dershaneYillikOdemesi)
                    {
                        MessageBox.Show("Kalan ödemeniz bulunmamaktadır.");
                    }
                    else if (toplamOdemeMiktari > 120000)
                    {
                        MessageBox.Show("Ödeme miktarı hatalıdır. Ödemenizden fazla miktar girmeyiniz.");
                    }
                    // Eğer mesaj kutusu gösterildiyse, burada işlemi sonlandırarak diğer öğrenciye geçmeyi sağlayın.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme bilgileri getirilirken bir hata oluştu: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string ogrenciID = dataGridView2.SelectedRows[0].Cells["Ogrenci_ID"].Value.ToString();
                decimal odemeTutari;

                if (!decimal.TryParse(textBox4.Text, out odemeTutari))
                {
                    MessageBox.Show("Lütfen geçerli bir ödeme tutarı girin.");
                    return;
                }

                int toplamOdemeMiktari = GetToplamOdemeMiktari(ogrenciID);
                bool odemesiKalmayanOgrenci = (toplamOdemeMiktari >= dershaneYillikOdemesi);

                if (odemesiKalmayanOgrenci)
                {
                    MessageBox.Show("Ödemesi kalmayan bir öğrenci seçildi. Ödeme yapılamaz.");
                    return;
                }

                if (odemeTutari + toplamOdemeMiktari > dershaneYillikOdemesi)
                {
                    MessageBox.Show("Ödeme miktarı hatalıdır. Ödemenizden fazla miktar girmeyiniz.");
                    return;
                }

                DateTime odemeTarihi = DateTime.Now;
                int taksitNumarasi = GetToplamOdemeSayisi(ogrenciID) + 1;

                string sorgu = "INSERT INTO Odeme (ogrenci_id, odeme_tutari, odeme_tarihi, taksit_numarasi, pesin) VALUES (@ogrenci_id, @odeme_tutari, @odeme_tarihi, @taksit_numarasi, @pesin)";
                bool pesin = (odemeTutari + toplamOdemeMiktari >= dershaneYillikOdemesi) ? true : false;

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand komut = new SqlCommand(sorgu, connection);
                        komut.Parameters.AddWithValue("@ogrenci_id", ogrenciID);
                        komut.Parameters.AddWithValue("@odeme_tutari", odemeTutari);
                        komut.Parameters.AddWithValue("@odeme_tarihi", odemeTarihi);
                        komut.Parameters.AddWithValue("@taksit_numarasi", taksitNumarasi);
                        komut.Parameters.AddWithValue("@pesin", pesin);

                        connection.Open();
                        komut.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ödeme başarıyla kaydedildi.");
                    OdemeBilgileriniGetir(ogrenciID);
                    OgrenciBilgileriniGetir();
                    UpdateOgrenciToplamOdeme(ogrenciID);

                    // TextBox'ların içeriğini temizle
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    // Ödemesi kalmayan öğrenci seçildiyse, ödeme yapma işlemi devre dışı bırak
                    if (odemesiKalmayanOgrenci)
                    {
                        button1.Enabled = false;
                        textBox4.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ödeme kaydedilirken bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen ödeme yapılacak bir öğrenci seçin.");
            }
        }

        private int GetToplamOdemeMiktari(string ogrenciID)
        {
            int toplamOdemeMiktari = 0;
            string sorgu = "SELECT SUM(odeme_tutari) FROM Odeme WHERE ogrenci_id = @ogrenci_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand komut = new SqlCommand(sorgu, connection);
                komut.Parameters.AddWithValue("@ogrenci_id", ogrenciID);

                try
                {
                    connection.Open();
                    object result = komut.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        toplamOdemeMiktari = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Toplam ödeme miktarı alınırken bir hata oluştu: " + ex.Message);
                }
            }

            return toplamOdemeMiktari;
        }

        private int GetToplamOdemeSayisi(string ogrenciID)
        {
            int toplamOdemeSayisi = 0;
            string sorgu = "SELECT COUNT(*) FROM Odeme WHERE ogrenci_id = @ogrenci_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand komut = new SqlCommand(sorgu, connection);
                komut.Parameters.AddWithValue("@ogrenci_id", ogrenciID);

                try
                {
                    connection.Open();
                    toplamOdemeSayisi = (int)komut.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ödeme sayısı alınırken bir hata oluştu: " + ex.Message);
                }
            }

            return toplamOdemeSayisi;
        }

        private void UpdateOgrenciToplamOdeme(string ogrenciID)
        {
            int toplamOdemeMiktari = GetToplamOdemeMiktari(ogrenciID);
            string sorgu = "UPDATE Ogrenci SET toplam_odeme_miktari = @toplamOdemeMiktari WHERE Ogrenci_ID = @ogrenciID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand komut = new SqlCommand(sorgu, connection);
                    komut.Parameters.AddWithValue("@toplamOdemeMiktari", toplamOdemeMiktari);
                    komut.Parameters.AddWithValue("@ogrenciID", ogrenciID);

                    connection.Open();
                    komut.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form13 form13 = new Form13();
            form13.ShowDialog();
        }
    }
    }




