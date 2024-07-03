namespace SistemAnalizi
{
    partial class Form13
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.sistemAnaliziDataSet = new SistemAnalizi.SistemAnaliziDataSet();
            this.odemeBılgılerıBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.odemeBılgılerıTableAdapter = new SistemAnalizi.SistemAnaliziDataSetTableAdapters.OdemeBılgılerıTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.sistemAnaliziDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.odemeBılgılerıBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.odemeBılgılerıBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemAnalizi.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // sistemAnaliziDataSet
            // 
            this.sistemAnaliziDataSet.DataSetName = "SistemAnaliziDataSet";
            this.sistemAnaliziDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // Form13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Form13";
            this.Text = "Form13";
            this.Load += new System.EventHandler(this.Form13_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sistemAnaliziDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.odemeBılgılerıBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private SistemAnaliziDataSet sistemAnaliziDataSet;
        private System.Windows.Forms.BindingSource odemeBılgılerıBindingSource;
        private SistemAnaliziDataSetTableAdapters.OdemeBılgılerıTableAdapter odemeBılgılerıTableAdapter;
    }
}