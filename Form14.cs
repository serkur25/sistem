﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemAnalizi
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'sistemAnaliziDataSet.OdemeBılgılerı' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.odemeBılgılerıTableAdapter.Fill(this.sistemAnaliziDataSet.OdemeBılgılerı);
            // TODO: Bu kod satırı 'sistemAnaliziDataSet.ogrenci' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ogrenciTableAdapter.Fill(this.sistemAnaliziDataSet.ogrenci);

            this.reportViewer1.RefreshReport();
        }
    }
}
