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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

      

        void sınıf()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlCommand komut = new SqlCommand("SELECT Sınıf_Adı FROM sınıf", bag);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                 comboBox10.Items.Add(dr[0].ToString());
            }
            bag.Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            sınıf();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        // Veritabanından seçilen sınıfın etüt programını almak için bir metod
        private DataTable GetEtutProgram(string selectedClass)
        {
            DataTable etutProgram = new DataTable();

            using (SqlConnection connection = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT e.GUN, d.D_ADI AS DERS, o.OGRETMEN_ADI AS OGRETMEN FROM etut e JOIN ders d ON e.DERS_ID = d.D_ID JOIN ogretmen o ON e.S_ID = o.O_ID WHERE e.sınıf = @selectedClass", connection);
                command.Parameters.AddWithValue("@selectedClass", selectedClass);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(etutProgram);
            }

            return etutProgram;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Combobox10'dan seçilen sınıfı alın
            string selectedClass = comboBox10.SelectedItem.ToString();

            // Veritabanından seçilen sınıfın etüt programını alın
            DataTable etutProgram = GetEtutProgram(selectedClass);

            // DataGridView'in veri kaynağını güncelleyin
            dataGridView1.DataSource = etutProgram;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

