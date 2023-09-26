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
    public partial class Form29 : Form
    {
        Database database = new Database();
        public Form29()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_rah", "Код рахунку");
            dataGridView1.Columns.Add("kod_bank", "Код банку");
            dataGridView1.Columns.Add("name_bank", "Назва банку");
            dataGridView1.Columns.Add("kod_ka", "Код контрагенту");
            dataGridView1.Columns.Add("kod_addr", "Код адреси");
            dataGridView1.Columns.Add("name_a", "Назва");
            dataGridView1.Columns.Add("city", "Місто");
            dataGridView1.Columns.Add("street", "Вулиця");
            dataGridView1.Columns.Add("house", "Дім");
            dataGridView1.Columns.Add("office", "Офіс");
            dataGridView1.Columns.Add("phone", "Телефон");


        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), record.GetString(5), record.GetString(6), record.GetString(7), record.GetInt32(8), record.GetInt32(9), record.GetInt32(10));

        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"SELECT C.kod_rah, C.kod_bank, P.name_bank, N.kod_ka, B.kod_addr, B.name_a, B.city, B.street, B.house, B.office, B.phone  FROM Bank_rah AS C JOIN Bank AS P ON P.kod_bank = C.kod_bank JOIN Kontragent AS N ON N.kod_ka = C.kod_ka JOIN Adress AS B ON N.kod_addr = B.kod_addr ";

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
        private void Form29_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);

        }
    }
}
