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

namespace Kursovaya_Makrenko_PZ_20_3
{
    public partial class Form11 : Form
    {
        Database database = new Database();
        public Form11()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_nom", "Код строки продажу");
            dataGridView1.Columns.Add("name_nom", "Код продажу");
            dataGridView1.Columns.Add("kod_proiz", "Код номенклатури");
            dataGridView1.Columns.Add("ed_izm", "Кількість");
            dataGridView1.Columns.Add("tsina", "Ціна");
         

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetDecimal(4));

        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Str_prod";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }

            reader.Close();
            database.Closeconnection();
        }
        private void Form11_Load(object sender, EventArgs e)
        {

            CreateColumns();
            RefreshDataGrid(dataGridView1);

            decimal sum = 0;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                sum += Convert.ToDecimal(dataGridView1[4, i].Value);
            }
            textBox1.Text = sum.ToString();
        }
    }
}
