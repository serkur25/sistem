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
using System.Windows.Markup;
using System.Text.RegularExpressions;

namespace SistemAnalizi
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        string GetClassID(string className)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlCommand command = new SqlCommand("SELECT S_ID FROM sınıf WHERE Sınıf_Adı = @ClassName", baglanti);
                command.Parameters.AddWithValue("@ClassName", className);
                baglanti.Open();
                string classID = command.ExecuteScalar()?.ToString();
                baglanti.Close();
                return classID;
            }
        }

        private void RefreshDataGridView()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT OGRENCI_ADI,CINSIYET,TC,OGRENCI_TEL,DOGUM_TARIHI,S_ID,Sınıf_Adı FROM ogrenci", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        



        void ders()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT Sınıf_Adı from sınıf", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());  

            }
            bag.Close();
        }

        void toplam()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM ogrenci", bag);
            int toplamOgrenciSayisi = (int)komut.ExecuteScalar();
            label11.Text = toplamOgrenciSayisi.ToString();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            toplam();
            ders();
            RefreshDataGridView();
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kız");
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            SqlDataAdapter da = new SqlDataAdapter("SELECT OGRENCI_ADI, CINSIYET, TC, OGRENCI_TEL, DOGUM_TARIHI, Sınıf_Adı FROM ogrenci", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].HeaderText = "ADI";
            dataGridView2.Columns[1].HeaderText = "CİNSİYET";
            dataGridView2.Columns[2].HeaderText = "TC";
            dataGridView2.Columns[3].HeaderText = "ÖĞRENCİ TEL NO";
            dataGridView2.Columns[4].HeaderText = "DOĞUM TARİHİ";
            dataGridView2.Columns[5].HeaderText = "SINIF";
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Boş Veri Girilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string className = comboBox2.SelectedItem.ToString();
            string classID = GetClassID(className); // Sınıf ID'sini almak için uygun bir şekilde tanımlanması gerekiyor.


            using (SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                bag.Open();

                SqlCommand komut = new SqlCommand("INSERT INTO ogrenci(OGRENCI_ADI, CINSIYET, TC, OGRENCI_TEL, DOGUM_TARIHI, Sınıf_Adı, S_ID) VALUES (@prm1, @prm2, @prm3, @prm4, @prm5, @prm6, @prm7)", bag);
                komut.Parameters.AddWithValue("@prm1", textBox2.Text); // OGRENCI_ADI
                komut.Parameters.AddWithValue("@prm2", comboBox1.SelectedItem.ToString()); // CINSIYET
                komut.Parameters.AddWithValue("@prm3", textBox1.Text); // TC
                komut.Parameters.AddWithValue("@prm4", textBox3.Text); // OGRENCI_TEL
                komut.Parameters.AddWithValue("@prm5", textBox4.Text); // DOGUM_TARIHI
                komut.Parameters.AddWithValue("@prm6", className); // Sınıf_Adı
                komut.Parameters.AddWithValue("@prm7", classID);
                komut.ExecuteNonQuery();
            }

            MessageBox.Show("Kaydedildi", "İŞLEM", MessageBoxButtons.OK);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            

            // Öğrenci ekledikten sonra DataGridView'i yenile
            RefreshDataGridView();

            // S_ID sütununu DataGridView'de gizle
            dataGridView2.Columns["S_ID"].Visible = false;

            toplam();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form11 frm11 = new Form11();
            frm11.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakamları ve backspace'i kabul et
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Eğer 2. karaktere geldiyse ve bir nokta değilse, noktayı otomatik olarak ekle
            if (textBox4.Text.Length == 2 && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                textBox4.Text += ".";
                textBox4.SelectionStart = textBox4.Text.Length;
                e.Handled = true;
            }

            // Eğer 5. karaktere geldiyse ve bir nokta değilse, noktayı otomatik olarak ekle
            if (textBox4.Text.Length == 5 && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                textBox4.Text += ".";
                textBox4.SelectionStart = textBox4.Text.Length;
                e.Handled = true;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form14 form14 = new Form14();   
            form14.ShowDialog();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
