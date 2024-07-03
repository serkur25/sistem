namespace SistemAnalizi
{
    partial class Form14
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ogrenciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sistemAnaliziDataSet = new SistemAnalizi.SistemAnaliziDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ogrenciTableAdapter = new SistemAnalizi.SistemAnaliziDataSetTableAdapters.ogrenciTableAdapter();
            this.ogretmenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ogretmenBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.odemeBılgılerıBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.odemeBılgılerıTableAdapter = new SistemAnalizi.SistemAnaliziDataSetTableAdapters.OdemeBılgılerıTableAdapter();
            this.ogrenciBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ogrenciBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistemAnaliziDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogretmenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogretmenBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.odemeBılgılerıBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogrenciBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ogrenciBindingSource
            // 
            this.ogrenciBindingSource.DataMember = "ogrenci";
            this.ogrenciBindingSource.DataSource = this.sistemAnaliziDataSet;
            // 
            // sistemAnaliziDataSet
            // 
            this.sistemAnaliziDataSet.DataSetName = "SistemAnaliziDataSet";
            this.sistemAnaliziDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ogrenciBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemAnalizi.Report2.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1409, 750);
            this.reportViewer1.TabIndex = 0;
            // 
            // ogrenciTableAdapter
            // 
            this.ogrenciTableAdapter.ClearBeforeFill = true;
            // 
            // ogretmenBindingSource
            // 
            this.ogretmenBindingSource.DataMember = "ogretmen";
            this.ogretmenBindingSource.DataSource = this.sistemAnaliziDataSet;
            // 
            // ogretmenBindingSource1
            // 
            this.ogretmenBindingSource1.DataMember = "ogretmen";
            this.ogretmenBindingSource1.DataSource = this.sistemAnaliziDataSet;
            // 
            // odemeBılgılerıBindingSource
            // 
            this.odemeBılgılerıBindingSource.DataMember = "OdemeBılgılerı";
            this.odemeBılgılerıBindingSource.DataSource = this.sistemAnaliziDataSet;
            // 
            // odemeBılgılerıTableAdapter
            // 
            this.odemeBılgılerıTableAdapter.ClearBeforeFill = true;
            // 
            // ogrenciBindingSource1
            // 
            this.ogrenciBindingSource1.DataMember = "ogrenci";
            this.ogrenciBindingSource1.DataSource = this.sistemAnaliziDataSet;
            // 
            // Form14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1409, 750);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form14";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ÖĞRENCİLER";
            this.Load += new System.EventHandler(this.Form14_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ogrenciBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sistemAnaliziDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogretmenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogretmenBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.odemeBılgılerıBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ogrenciBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private SistemAnaliziDataSet sistemAnaliziDataSet;
        private System.Windows.Forms.BindingSource ogrenciBindingSource;
        private SistemAnaliziDataSetTableAdapters.ogrenciTableAdapter ogrenciTableAdapter;
        private System.Windows.Forms.BindingSource ogretmenBindingSource;
        private System.Windows.Forms.BindingSource ogretmenBindingSource1;
        private System.Windows.Forms.BindingSource odemeBılgılerıBindingSource;
        private SistemAnaliziDataSetTableAdapters.OdemeBılgılerıTableAdapter odemeBılgılerıTableAdapter;
        private System.Windows.Forms.BindingSource ogrenciBindingSource1;
    }
}