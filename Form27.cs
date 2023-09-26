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
    public partial class Form27 : Form
    {

        Database database = new Database();
        int selectedRow;
        public Form27()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_bank", "Код банку");
            dataGridView1.Columns.Add("name_bank", "Назва банку");
            dataGridView1.Columns.Add("city_b", "Назва");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox8.Text = "";

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), RowState.ModifiesView);
        }


        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Bank";

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

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[3].Value;
                if (rowState == RowState1.Existed)
                    continue;
                if (rowState == RowState1.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Bank where kod_bank={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState1.Modified)
                {

                    var kod_bank = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name_bank = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var city_b = dataGridView1.Rows[index].Cells[2].Value.ToString();
                   


                    var changeQuery = $"update Bank set name_bank='{name_bank}',city_b='{city_b}'  where kod_bank='{kod_bank}'";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();

                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_bank = int.Parse(textBox2.Text);
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var name_bank = textBox1.Text;
            var city_b = textBox8.Text;
           

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_bank, name_bank, city_b);
                dataGridView1.Rows[selectedRowIndex].Cells[3].Value = RowState1.Modified;
            }
        }
        private void Form27_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox8.Text = row.Cells[2].Value.ToString();
                

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
            Update();
            ClearFields();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form28 frm28 = new Form28();
            frm28.Show();
        }
    }
}
