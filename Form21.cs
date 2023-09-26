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

namespace Kursovaya_Makrenko_PZ_20_3
{
    public partial class Form21 : Form
    {
        Database database = new Database();
        public Form21()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_post", "Код продажі");
            dataGridView1.Columns.Add("day_post", "День");
            dataGridView1.Columns.Add("month_post", "Місяць");
            dataGridView1.Columns.Add("year_prod", "Рік");


        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3));

        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Postavka";

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
        private void Form21_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }
    }
}
