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
    public partial class Form6 : Form
    {
        Database database = new Database();
        public Form6()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_nom", "Код номенклатури");
            dataGridView1.Columns.Add("name_nom", "Назва номенклатури");
            dataGridView1.Columns.Add("kod_proiz", "Код виробника");
            dataGridView1.Columns.Add("ed_izm", "Одиниці виміру");
            dataGridView1.Columns.Add("tsina", "Ціна");
            dataGridView1.Columns.Add("kod_typ", "Код типу номенклатури");
            dataGridView1.Columns.Add("kod_pol", "Код статі");
            dataGridView1.Columns.Add("ves", "Вага");
            
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetDecimal(4), record.GetInt32(5), record.GetInt32(6), record.GetDecimal(7), RowState.ModifiesView);

        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Nomenklatura";

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


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form6_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text=="По алфавіту")
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            else if (comboBox1.Text == "По зростанню ціни")
                dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);

            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Для чоловіків")
            {
                dataGridView1.Rows.Clear();
                string searchString = $"select * from Nomenklatura where kod_pol like '%" + "1" + "%'";

                SqlCommand command = new SqlCommand(searchString, database.getConnection());
                database.Openconnection();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ReadSingleRow(dataGridView1, read);
                }

                read.Close();
            }  


            else if (comboBox2.Text == "Для жінок")
            {
                dataGridView1.Rows.Clear();
                string searchString = $"select * from Nomenklatura where kod_pol like '%" + "2" + "%'";

                SqlCommand command = new SqlCommand(searchString, database.getConnection());
                database.Openconnection();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ReadSingleRow(dataGridView1, read);
                }

                read.Close();
            }
        }
    }
}
