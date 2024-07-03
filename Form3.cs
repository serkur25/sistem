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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

       
        void yenile()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT OGRETMEN_ADI, CINSIYET, TC, OGRETMEN_TEL, DERS  FROM ogretmen", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }

        }

        string GetClassID(string dersName)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                SqlCommand command = new SqlCommand("SELECT D_ID FROM ders WHERE D_ADI = @ClassName", baglanti);
                command.Parameters.AddWithValue("@ClassName", dersName);
                baglanti.Open();
                string dersID = command.ExecuteScalar().ToString();
                baglanti.Close();
                return dersID;
            }
        }

        void toplam()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) from ogretmen", bag);
            int toplamogretmen=(int)komut.ExecuteScalar();
            label11.Text= toplamogretmen.ToString();


        }

        void ders()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT D_ADI from ders",bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr[0].ToString());

            }
            bag.Close();



        }

        private void Form3_Load(object sender, EventArgs e)
        {
            toplam();            
            ders();
            comboBox1.Items.Add("Erkek");
            comboBox1.Items.Add("Kız");
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            SqlDataAdapter da = new SqlDataAdapter("SELECT OGRETMEN_ADI, CINSIYET, TC, OGRETMEN_TEL, DOGUM_TARIHI, DERS  FROM ogretmen", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].HeaderText = "ADI";
            dataGridView2.Columns[1].HeaderText = "CİNSİYET";
            dataGridView2.Columns[2].HeaderText = "TC";
            dataGridView2.Columns[3].HeaderText = "ÖĞRETMEN TEL NO";
            dataGridView2.Columns[4].HeaderText = "DOGUM TARİHİ";
            dataGridView2.Columns[5].HeaderText = "DERS";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dersName = comboBox3.SelectedItem.ToString();
            string dersID = GetClassID(dersName);

            using(SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                bag.Open();

                SqlCommand komut = new SqlCommand("INSERT INTO ogretmen(OGRETMEN_ADI, CINSIYET, TC, OGRETMEN_TEL, DOGUM_TARIHI, DERS, D_ID) VALUES (@prm1, @prm2, @prm3, @prm4, @prm5, @prm6, @prm7)", bag);
                komut.Parameters.AddWithValue("@prm1", textBox2.Text); // OGRENCI_ADI
                komut.Parameters.AddWithValue("@prm2", comboBox1.SelectedItem.ToString()); // CINSIYET
                komut.Parameters.AddWithValue("@prm3", textBox1.Text); // TC
                komut.Parameters.AddWithValue("@prm4", textBox3.Text); // OGRENCI_TEL
                komut.Parameters.AddWithValue("@prm5", textBox4.Text); // DOGUM_TARIHI
                komut.Parameters.AddWithValue("@prm6", dersName); // Sınıf_Adı
                komut.Parameters.AddWithValue("@prm7", dersID);
                komut.ExecuteNonQuery();
            }

            MessageBox.Show("KAYDEDİLDİ", "İŞLEM", MessageBoxButtons.OK);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            yenile();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form15 form15 = new Form15();
                form15.ShowDialog();
        }
    }
}
