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
    public partial class Form34 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form34()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("nikneim", "Нікнейм");
            dataGridView1.Columns.Add("kod_zamov", "Код замовлення");
            dataGridView1.Columns.Add("kolvo", "Кількість");
            dataGridView1.Columns.Add("statusz", "Статус замовлення");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox4.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            comboBox2.Text = "";
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetInt32(1), record.GetInt32(2), record.GetString(3), RowState.ModifiesView);
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Zamovlenya";

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

        private void Update()
        {
            database.Openconnection();


            for (int index = 0; index < dataGridView1.Rows.Count - 1; index++)
            {

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[4].Value;
                if (rowState == RowState1.Existed)
                    continue;
                if (rowState == RowState1.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[1].Value);
                    var deleteQuery = $"delete from Zamovlenya where kod_zamov={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState1.Modified)
                {

                    var nikneim = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var kod_zamov = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var kolvo = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var statusz = dataGridView1.Rows[index].Cells[3].Value.ToString();

                    var changeQuery = $"update Zamovlenya set nikneim='{nikneim}', kolvo='{kolvo}', statusz='{statusz}' where kod_zamov='{kod_zamov}'";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();

                }
            }

            database.Closeconnection();
        }

        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[4].Value = RowState.Deleted;
        }

        private void Change()
        {
            var nikneim = textBox4.Text;
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var kod_zamov = int.Parse(comboBox1.SelectedValue.ToString());
            var kolvo = int.Parse(textBox2.Text);
            var statusz = comboBox2.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(nikneim, kod_zamov, kolvo, statusz);
                dataGridView1.Rows[selectedRowIndex].Cells[4].Value = RowState1.Modified;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void Form34_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Nomenklatura". При необходимости она может быть перемещена или удалена.
            this.nomenklaturaTableAdapter.Fill(this.cosmeticsDataSet.Nomenklatura);
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox4.Text = row.Cells[0].Value.ToString();
                comboBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                comboBox2.Text = row.Cells[3].Value.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
            Update();
            ClearFields();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRow();
            Update();
            ClearFields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form35 frm35 = new Form35();
            frm35.Show();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox3.Text == "NEW")
            {
                dataGridView1.Rows.Clear();
                string searchString = $"select * from Zamovlenya where statusz like '%" + "NEW" + "%'";

                SqlCommand command = new SqlCommand(searchString, database.getConnection());
                database.Openconnection();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ReadSingleRow(dataGridView1, read);
                }

                read.Close();
            }

            else if (comboBox3.Text == "В обробці")
            {
                dataGridView1.Rows.Clear();
                string searchString = $"select * from Zamovlenya where statusz like '%" + "В обробці" + "%'";

                SqlCommand command = new SqlCommand(searchString, database.getConnection());
                database.Openconnection();
                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    ReadSingleRow(dataGridView1, read);
                }

                read.Close();
            }

            else if (comboBox3.Text == "Завершений")
            {
                dataGridView1.Rows.Clear();
                string searchString = $"select * from Zamovlenya where statusz like '%" + "Завершений" + "%'";

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
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Zamovlenya where concat (nikneim,kod_zamov,kolvo,statusz) like '%" + textBox3.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            database.Openconnection();
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }

            read.Close();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }
    }
}
