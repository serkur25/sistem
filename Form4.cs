using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemAnalizi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        void ders()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT D_ADI from ders", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());

            }
            bag.Close();

        }


            // Dersin ID'sini almak için SQL sorgusu
            string GetClassID(string className)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlCommand command = new SqlCommand("SELECT D_ID FROM ders WHERE D_ADI = @ClassName", baglanti);
                command.Parameters.AddWithValue("@ClassName", className);
                baglanti.Open();
                string classID = command.ExecuteScalar()?.ToString();
                baglanti.Close();
                return classID;
            }
        }



        // DataGridView'i yenilemek için fonksiyon
        void RefreshDataGridView()
        {
            // Öğretmen tablosundan verileri çekip DataGridView'e yükler
            using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT O_ID, OGRETMEN_ADI, CINSIYET, TC, OGRETMEN_TEL, DOGUM_TARIHI, D_ID, DERS FROM ogretmen", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            ders();
            // ComboBox'a cinsiyet seçeneklerini ekler
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kız");
            // DataGridView'i yeniler
            RefreshDataGridView();
            // Görüntülenen sütun başlıklarını ayarlar
            dataGridView1.Columns["O_ID"].Visible = false;
            dataGridView1.Columns["D_ID"].Visible = false;
            dataGridView1.Columns[1].HeaderText = "ADI";
            dataGridView1.Columns[2].HeaderText = "CİNSİYET";
            dataGridView1.Columns[3].HeaderText = "TC";
            dataGridView1.Columns[4].HeaderText = "ÖĞRETMEN TEL NO";
            dataGridView1.Columns[5].HeaderText = "DOĞUM TARİHİ";
            dataGridView1.Columns[6].HeaderText = "DERS";
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // Seçilen satırdaki verileri ilgili TextBox ve ComboBox'lara yerleştirir
                textBox1.Text = row.Cells["O_ID"].Value.ToString();
                textBox2.Text = row.Cells["OGRETMEN_ADI"].Value.ToString();
                comboBox1.Text = row.Cells["CINSIYET"].Value.ToString();
                textBox3.Text = row.Cells["TC"].Value.ToString();
                textBox4.Text = row.Cells["OGRETMEN_TEL"].Value.ToString();
                comboBox2.Text = row.Cells["DERS"].Value.ToString();
                textBox7.Text = row.Cells["D_ID"].Value.ToString();

                object cellValue = row.Cells["DOGUM_TARIHI"].Value;
                if (cellValue != DBNull.Value && cellValue != null)
                {
                    // Hücredeki değerin DateTime türüne dönüştürülebilir olduğunu kontrol eder
                    if (DateTime.TryParse(cellValue.ToString(), out DateTime dogumTarihi))
                    {
                        string sadeceTarih = dogumTarihi.ToString("dd.MM.yyyy");
                        textBox5.Text = sadeceTarih;
                    }
                    else
                    {
                        // Hücredeki değer DateTime türüne dönüştürülemezse boş bir string ata
                        textBox5.Text = "";
                    }
                }
                else
                {
                    textBox5.Text = "";
                }
            
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string className = comboBox2.Text;
                string classID = GetClassID(className);

                using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                {
                    bag.Open();
                    // Seçilen öğretmenin bilgilerini günceller
                    SqlCommand komut = new SqlCommand("UPDATE ogretmen SET OGRETMEN_ADI=@prm1, CINSIYET=@prm2, TC=@prm3, OGRETMEN_TEL=@prm4, DOGUM_TARIHI=@prm5, DERS=@prm6, D_ID=@prm7 WHERE O_ID=@prm8", bag);
                    komut.Parameters.AddWithValue("@prm1", textBox2.Text);
                    komut.Parameters.AddWithValue("@prm2", comboBox1.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@prm3", textBox3.Text);
                    komut.Parameters.AddWithValue("@prm4", textBox4.Text);
                    komut.Parameters.AddWithValue("@prm5", !string.IsNullOrEmpty(textBox5.Text) ? Convert.ToDateTime(textBox5.Text) : (object)DBNull.Value);
                    komut.Parameters.AddWithValue("@prm6", comboBox2.SelectedItem.ToString());
                    komut.Parameters.AddWithValue("@prm7", classID);
                    komut.Parameters.AddWithValue("@prm8", textBox1.Text);

                    komut.ExecuteNonQuery();
                }

                MessageBox.Show("Kaydedildi", "İŞLEM", MessageBoxButtons.OK);
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Lütfen bir cinsiyet seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult cevap = MessageBox.Show("Emin Misin", "Silinecek", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    // Seçilen satırın verilerini almak için DataGridView'den seçili satırı bulunur
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Öğrenci ID'sini seçilen satırdan alınır
                    int ogrenciID = Convert.ToInt32(selectedRow.Cells["O_ID"].Value);

                    // Bağlantı oluşturulur
                    using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                    {
                        // Sorgu oluşturulur
                        string query = "DELETE FROM ogretmen WHERE O_ID = @OgretmenID";

                        // Komut oluşturulur
                        using (SqlCommand komut = new SqlCommand(query, bag))
                        {
                            // Parametre eklenir
                            komut.Parameters.AddWithValue("@OgretmenID", ogrenciID);

                            // Bağlantı açılır
                            bag.Open();

                            // Komut çalıştırılır
                            komut.ExecuteNonQuery();
                             MessageBox.Show("KAYIT SİLİNDİ");
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox5.Clear();
                            textBox3.Clear();
                            /*textBox6.Clear();*/
                        }
                    }

                    // Sütun ayarlarını tekrar yükle
                    Form4_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.");
            }
        }
    }
}
