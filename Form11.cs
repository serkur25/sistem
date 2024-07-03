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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemAnalizi
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        void ders()
        {
            using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlCommand komut = new SqlCommand("SELECT Sınıf_Adı FROM sınıf", bag);
                bag.Open();
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    comboBox2.Items.Add(dr[0].ToString());
                }
            }
        }

        string GetClassID(string className)
        {
            string classID = null;
            using (SqlConnection baglanti = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlCommand command = new SqlCommand("SELECT S_ID FROM sınıf WHERE Sınıf_Adı = @ClassName", baglanti);
                command.Parameters.AddWithValue("@ClassName", className);
                baglanti.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    classID = result.ToString();
                }
            }
            return classID;
        }

        void yenile()
        {
            using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Ogrenci_ID, OGRENCI_ADI, CINSIYET, TC, OGRENCI_TEL, DOGUM_TARIHI, S_ID, Sınıf_Adı FROM ogrenci", bag);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Ogrenci_ID"].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ADI";
                dataGridView1.Columns[2].HeaderText = "CİNSİYET";
                dataGridView1.Columns[3].HeaderText = "TC";
                dataGridView1.Columns[4].HeaderText = "ÖĞRENCİ TEL NO";
                dataGridView1.Columns[5].HeaderText = "DOĞUM TARİHİ";
                dataGridView1.Columns[6].HeaderText = "SINIF";
                dataGridView1.Columns["S_ID"].Visible = false;
            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            ders();
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kız");
            yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult cevap = MessageBox.Show("Emin Misin", "Silinecek", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    int ogrenciID = Convert.ToInt32(selectedRow.Cells["Ogrenci_ID"].Value);

                    using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                    {
                        string query = "DELETE FROM ogrenci WHERE Ogrenci_ID = @OgrenciID";
                        using (SqlCommand komut = new SqlCommand(query, bag))
                        {
                            komut.Parameters.AddWithValue("@OgrenciID", ogrenciID);
                            bag.Open();
                            komut.ExecuteNonQuery();
                            MessageBox.Show("KAYIT SİLİNDİ");
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox5.Clear();
                        }
                    }
                    yenile();
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir öğrenci seçin.");
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

                object cellValue = dataGridView1.CurrentRow.Cells[5].Value;
                if (cellValue != DBNull.Value && cellValue != null)
                {
                    DateTime dogumTarihi = (DateTime)cellValue;
                    string sadeceTarih = dogumTarihi.ToString("dd.MM.yyyy");
                    textBox5.Text = sadeceTarih;
                }
                else
                {
                    textBox5.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
            {
                string className = comboBox2.Text;
                string classID = GetClassID(className);

                if (!string.IsNullOrEmpty(classID))
                {
                    using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                    {
                        bag.Open();

                        SqlCommand komut = new SqlCommand("UPDATE ogrenci SET OGRENCI_ADI=@prm1, CINSIYET=@prm2, TC=@prm3, OGRENCI_TEL=@prm4, DOGUM_TARIHI=@prm5, Sınıf_Adı=@prm6, S_ID=@prm7 WHERE Ogrenci_ID=@prm8", bag);
                        komut.Parameters.AddWithValue("@prm1", textBox2.Text);
                        komut.Parameters.AddWithValue("@prm2", comboBox1.SelectedItem.ToString());
                        komut.Parameters.AddWithValue("@prm3", textBox1.Text);
                        komut.Parameters.AddWithValue("@prm4", textBox3.Text);
                        komut.Parameters.AddWithValue("@prm5", Convert.ToDateTime(textBox5.Text));
                        komut.Parameters.AddWithValue("@prm6", comboBox2.SelectedItem.ToString());
                        komut.Parameters.AddWithValue("@prm7", classID);
                        komut.Parameters.AddWithValue("@prm8", textBox7.Text);
                        komut.ExecuteNonQuery();
                    }

                    MessageBox.Show("Güncelleme Başarılı", "İŞLEM", MessageBoxButtons.OK);
                    yenile();
                }
                else
                {
                    MessageBox.Show("Tüm Bilgilerin Seçili Olduğundan Emin Olun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir cinsiyet ve sınıf seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    
    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
