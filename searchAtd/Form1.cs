using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using System.IO;


namespace searchAtd
{
    public partial class Form1 : Form
    {
       // private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.BorderStyle = BorderStyle.None;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            this.dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            this.dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10f);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
           
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
          


            //   System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            try
            {
                DataSet result;
                string filePath = @"D:\data.xlsx";
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        result = reader.AsDataSet();
                        DataTable dataTable = result.Tables[0];
                        dataGridView1.DataSource = dataTable;
                        
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.Columns["Column0"].HeaderText = "Compte";
                        dataGridView1.Columns["Column1"].HeaderText = "Nom";
                        dataGridView1.Columns["Column2"].HeaderText = "Identifiant_F";
                        dataGridView1.Columns["Column3"].HeaderText = "Code_pers";
                    }

                }

            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error accessing the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the Excel file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int index;
            if (compt.Checked) { index = 0; }else if (nom.Checked) { index = 1; }else index = 2;
            this.dataGridView1.DataSource = (object)new BindingSource()
            {
                DataSource = this.dataGridView1.DataSource,
                Filter = string.Format("CONVERT(" + this.dataGridView1.Columns[index].DataPropertyName + ", System.String) like '%" + this.textBox1.Text.Replace("'", "''") + "%'")
            };

           
        }
    }
    }

