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
    public partial class Form19 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form19()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_post", "Код продажі");
            dataGridView1.Columns.Add("day_post", "День");
            dataGridView1.Columns.Add("month_post", "Місяць");
            dataGridView1.Columns.Add("year_prod", "Рік");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";   
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), RowState.ModifiesView);
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
        private void Form19_Load(object sender, EventArgs e)
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
                textBox5.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox6.Text = row.Cells[3].Value.ToString();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Postavka where concat (kod_post,day_post,month_post,year_post) like '%" + textBox1.Text + "%'";

            SqlCommand command = new SqlCommand(searchString, database.getConnection());
            database.Openconnection();
            SqlDataReader read = command.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }

            read.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
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
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Postavka where kod_post={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState1.Modified)
                {
                    var kod_post = dataGridView1.Rows[index].Cells[0].Value.ToString();  
                    var day_post = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var month_post = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var year_post = dataGridView1.Rows[index].Cells[3].Value.ToString();


                    var changeQuery = $"update Prodazh set day_post='{day_post}',month_post='{month_post}',year_post='{year_post}' where kod_post='{kod_post}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_post = int.Parse(textBox2.Text);
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var day_post = int.Parse(textBox5.Text);
            var month_post = int.Parse(textBox4.Text);
            var year_post = int.Parse(textBox6.Text);
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_post, day_post, month_post, year_post);
                dataGridView1.Rows[selectedRowIndex].Cells[2].Value = RowState1.Modified;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form20 frm20 = new Form20();
            frm20.Show();
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
