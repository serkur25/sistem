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
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private List<int> ogretmenIDList = new List<int>();
        private List<string> ogretmenAdList = new List<string>();

        void ders()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT D_ADI FROM ders", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0].ToString());
            }
            bag.Close();
        }


        void sınıf()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT Sınıf_Adı FROM sınıf", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());

            }
            bag.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ders seçildiğinde öğretmenleri listele
            string dersAdi = comboBox4.SelectedItem.ToString();
            SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT D_ID FROM ders WHERE D_ADI = @dersAdi", connection);
            command.Parameters.AddWithValue("@dersAdi", dersAdi);
            int dersID = (int)command.ExecuteScalar(); // Dersin ID'sini al
            connection.Close();

            connection.Open();
            command = new SqlCommand("SELECT O_ID, OGRETMEN_ADI FROM ogretmen WHERE D_ID = @dersID", connection);
            command.Parameters.AddWithValue("@dersID", dersID);
            SqlDataReader reader = command.ExecuteReader();
            comboBox5.Items.Clear(); // Temizle
            ogretmenIDList.Clear(); // ID listesini temizle
            ogretmenAdList.Clear(); // Ad listesini temizle
            while (reader.Read())
            {
                int ogretmenID = (int)reader["O_ID"];
                string ogretmenAd = reader["OGRETMEN_ADI"].ToString();
                comboBox5.Items.Add(ogretmenAd); // Sadece öğretmen adını ekle
                ogretmenIDList.Add(ogretmenID); // ID'yi listeye ekle
                ogretmenAdList.Add(ogretmenAd); // Adı listeye ekle
            }
            connection.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form12_Load(object sender, EventArgs e)
        {
            ders();
            sınıf();

            

            comboBox3.Items.Add("PAZARTESİ");
            comboBox3.Items.Add("SALI");
            comboBox3.Items.Add("ÇARŞAMBA");
            comboBox3.Items.Add("PERŞEMBE");
            comboBox3.Items.Add("CUMA");
            comboBox3.Items.Add("CUMARTESİ");
            comboBox3.Items.Add("PAZAR");

            // ComboBox5 için DisplayMember ve ValueMember'ı ayarla
            comboBox5.DisplayMember = "OGRETMEN_ADI";
            comboBox5.ValueMember = "OGRETMEN_ID";

            dataGridView2.Columns.Add("Sinif", "Sınıf");
            dataGridView2.Columns.Add("Gun", "Gün");
            dataGridView2.Columns.Add("Ders", "Ders");
            dataGridView2.Columns.Add("Ogretmen", "Öğretmen");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Seçili elemanları al
            string sinif = comboBox1.SelectedItem.ToString();
            string gun = comboBox3.SelectedItem.ToString();
            string ders = comboBox4.SelectedItem.ToString();

            // Seçili öğretmenin adını ve ID'sini al
            int selectedIndex = comboBox5.SelectedIndex;
            if (selectedIndex != -1 && selectedIndex < ogretmenIDList.Count)
            {
                int ogretmenID = ogretmenIDList[selectedIndex];
                string ogretmenAd = ogretmenAdList[selectedIndex];

                // Veritabanına ekleme yap
                using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO etut (GUN, S_ID, sınıf, DERS_ID) VALUES (@gun, @ogretmenID, @sinif, @dersID)", connection);
                    command.Parameters.AddWithValue("@gun", gun);
                    command.Parameters.AddWithValue("@ogretmenID", ogretmenID);
                    command.Parameters.AddWithValue("@sinif", sinif);
                    command.Parameters.AddWithValue("@dersID", comboBox4.SelectedIndex + 1); // Dersin ID'sini almak için ComboBox'taki indeksi kullanın
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("DERS PROGRAMI BAŞARIYLA OLUŞTURULDU", "BİLGİ", MessageBoxButtons.OK);

                // DataGridView'e satır ekle
                dataGridView2.Rows.Add(sinif, gun, ders, ogretmenAd);
            }
        }
    }
}
