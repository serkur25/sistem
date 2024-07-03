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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void RefreshDataGridView()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Sınıf_Adı FROM sınıf", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void Form9_Load(object sender, EventArgs e)
        {


            RefreshDataGridView();
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            SqlDataAdapter da = new SqlDataAdapter("SELECT Sınıf_Adı FROM sınıf", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["Sınıf_Adı"].HeaderText = "SINIF ADI";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                bag.Open();

                SqlCommand komut = new SqlCommand("INSERT INTO sınıf(Sınıf_Adı) VALUES (@prm1)", bag);
                komut.Parameters.AddWithValue("@prm1", textBox2.Text);
                komut.ExecuteNonQuery();
            }

            MessageBox.Show("KAYDEDİLDİ", "İŞLEM", MessageBoxButtons.OK);

            textBox2.Clear();
            RefreshDataGridView();
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

                    // Sınıf adını seçilen satırdan alınır
                    string sınıfAdı = selectedRow.Cells["Sınıf_Adı"].Value.ToString();

                    // Bağlantı oluşturulur
                    using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                    {
                        // Sorgu oluşturulur
                        string query = "DELETE FROM sınıf WHERE Sınıf_Adı = @SINIFADI";

                        // Komut oluşturulur
                        using (SqlCommand komut = new SqlCommand(query, bag))
                        {
                            // Parametre eklenir
                            komut.Parameters.AddWithValue("@SINIFADI", sınıfAdı);

                            // Bağlantı açılır
                            bag.Open();

                            // Komut çalıştırılır
                            komut.ExecuteNonQuery();
                            RefreshDataGridView();
                            MessageBox.Show("KAYIT SİLİNDİ");
                        }
                    }
                }
            }
        }
    }
}
