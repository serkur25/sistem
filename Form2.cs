using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemAnalizi
{
    public partial class Form2 : Form
    {
       
        private Rectangle normalBounds;
        public Form2()
        {
            InitializeComponent();
           
        }

       
        void sınıf()
        {

            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select  Sınıf_Adı FROM sınıf",bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "SINIF ADI";

        }

        void ders()
        {

            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select  D_ADI FROM ders", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].HeaderText = "DERS ADI";


            SqlCommand komut = new SqlCommand("SELECT COUNT(*) from ders", bag);
            int toplamders = (int)komut.ExecuteScalar();
            label12.Text = toplamders.ToString();

        }

        void ogretmen()
        {

            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select  OGRETMEN_ADI, OGRETMEN_TEL, DERS FROM ogretmen", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            dataGridView3.Columns[0].HeaderText = "ÖĞRETMEN ADI";
            dataGridView3.Columns[1].HeaderText = "TELEFON NUMARASI";
            dataGridView3.Columns[2].HeaderText = "DERS";

            SqlCommand komut = new SqlCommand("SELECT COUNT(*) from ogretmen", bag);
            int toplamogretmen = (int)komut.ExecuteScalar();
            label10.Text = toplamogretmen.ToString();

        }

        void ogrenci()
        {

            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            bag.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select  OGRENCI_ADI, OGRENCI_TEL, Sınıf_Adı FROM ogrenci", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView4.DataSource = dt;
            dataGridView4.Columns[0].HeaderText = "ÖĞRETMEN ADI";
            dataGridView4.Columns[1].HeaderText = "TELEFON NUMARASI";
            dataGridView4.Columns[2].HeaderText = "DERS";

            SqlCommand komut = new SqlCommand("SELECT COUNT(*) from ogrenci", bag);
            int toplamogrenci = (int)komut.ExecuteScalar();
            label9.Text = toplamogrenci.ToString();

        }

       


        private void Form2_Load(object sender, EventArgs e)
        {
            sınıf();
            ders();
            ogretmen();
            ogrenci();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
                form9.ShowDialog();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            form10.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.ShowDialog();
            /*SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            SqlDataAdapter da = new SqlDataAdapter("SELECT D_Adı FROM ders", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "DERS ADI";*/
        }


        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            SqlDataAdapter da = new SqlDataAdapter("SELECT E.GÜN,E.SAAT,S.S_ID,D.D_ADI,OGRETMEN_ADI FROM etut E JOIN sınıf S ON (E.S_ID=S.S_ID) JOIN ogretmen O ON (O.O_ID=S.O_ID) JOIN ders D ON (D.D_ID=O.D_ID)", bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "GÜN";
            dataGridView1.Columns[1].HeaderText = "SAAT";
            dataGridView1.Columns[2].HeaderText = "SINIF";
            dataGridView1.Columns[3].HeaderText = "DERS";
            dataGridView1.Columns[4].HeaderText = "ÖĞRETMEN ADI";

        }
      
                   

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
          
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_DoubleClick(object sender, EventArgs e)
        {
            

        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
