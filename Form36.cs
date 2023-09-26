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
    public partial class Form36 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form36()
        {
            InitializeComponent();
        }

        private void CreateColumns8()
        {
            dataGridView9.Columns.Add("kod_op", "Код події");
            dataGridView9.Columns.Add("diya", "Подія");
            dataGridView9.Columns.Add("chas", "Дата і Час");
            dataGridView9.Columns.Add("vykon", "Виконавець");

        }

        private void ReadSingleRow8(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetDateTime(2), record.GetString(3));
        }

        private void RefreshDataGrid8(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Journal";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow8(dgv, reader);
            }

            reader.Close();
            database.Closeconnection();
        }
        private void RefreshDataGrid1(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Journal where vykon like '%" + "Адміністратор" + "%'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow8(dgv, reader);
            }

            reader.Close();
            database.Closeconnection();
        }

        private void RefreshDataGrid2(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Journal where vykon like '%" + "Клієнт" + "%'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow8(dgv, reader);
            }

            reader.Close();
            database.Closeconnection();
        }

        private void RefreshDataGrid3(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Journal where diya like '%" + "Створення замовлення" + "%'";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow8(dgv, reader);
            }

            reader.Close();
            database.Closeconnection();
        }
        private void Form36_Load(object sender, EventArgs e)
        {
            CreateColumns8();
            RefreshDataGrid8(dataGridView9);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text== "Адміністратор")
            {
                RefreshDataGrid1(dataGridView9);
            }
            else if (comboBox2.Text == "Клієнт")
            {
                RefreshDataGrid2(dataGridView9);
            }
            else if (comboBox2.Text == "Створення замовлення")
            {
                RefreshDataGrid3(dataGridView9);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid8(dataGridView9);
        }
    }
}
