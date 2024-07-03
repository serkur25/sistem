using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace SistemAnalizi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void usercheck()
        {
            SqlConnection bag = new SqlConnection("Data Source=KURSAD\\SQLEXPRESS;Initial Catalog=SistemAnalizi;Integrated Security=True;Encrypt=False");
            string sql = "select * from kullanicilar where kadi= '" + textBox1.Text + "' and sifre= '" + textBox2.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(sql, bag);
            da.SelectCommand.Parameters.AddWithValue("@prm1", textBox1.Text);
            da.SelectCommand.Parameters.AddWithValue("@prm2", textBox2.Text);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                Form2 frm2 = new Form2();
                frm2.FormClosed += (sender, args) => this.Close(); // Form2 kapatıldığında Form1'i de kapat
                this.Hide();
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("KULLANICI ADI VEYA ŞİFRE YANLIŞ", "HATA", MessageBoxButtons.OK);
                //label3.Text = "Kullancı Adı veya Şifre Yanlış";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            usercheck();
           //label3.Visible = true;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       


    }
}


