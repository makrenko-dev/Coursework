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
    public partial class Form14 : Form
    {
        Database database = new Database();
        public Form14()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("nikneim", "Нікнейм");
            dataGridView1.Columns.Add("kod_zamov", "Код замовлення");
            dataGridView1.Columns.Add("kolvo", "Кількість");
            dataGridView1.Columns.Add("statusz", "Статус замовлення");
    

        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3));

        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            int t=0;
            string queryString = $"select * from Zamovlenya WHERE nikneim=@nikneim";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            var nikneim = textBox2.Text;
            SqlParameter minusParam = new SqlParameter("@nikneim", nikneim);
            command.Parameters.Add(minusParam);
            SqlDataAdapter da = new SqlDataAdapter(command);
            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dataGridView1, reader);
                t++;
            }

            reader.Close();
            database.Closeconnection();
            if(t==0)
            {
                MessageBox.Show("Такого користувача немає!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        private void Form14_Load(object sender, EventArgs e)
        {
            CreateColumns();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
    }
}
