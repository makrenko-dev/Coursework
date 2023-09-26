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
    public partial class Form30 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form30()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_spiv", "Код співробітника");
            dataGridView1.Columns.Add("name_spiv", "ПІБ співробітника");
            dataGridView1.Columns.Add("kod_pos", "Код посади");
            dataGridView1.Columns.Add("name_pos", "Посада");
            dataGridView1.Columns.Add("sum_zar", "Зарплата");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), RowState.ModifiesView);
        }


        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"SELECT C.kod_spiv, C.name_spiv, C.kod_pos, P.name_pos, C.sum_zar FROM Spivrobitnyk AS C JOIN Posada AS P ON P.kod_pos = C.kod_pos";

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

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[5].Value;
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

                    var kod_spiv = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name_spiv = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var kod_pos = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var name_pos = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var sum_zar = dataGridView1.Rows[index].Cells[4].Value.ToString();

                    var changeQuery = $"update Spivrobitnyk set name_spiv='{name_spiv}', kod_pos='{kod_pos}', sum_zar='{sum_zar}' where kod_spiv='{kod_spiv}'";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();

                    var changeQuery1 = $"update Posada set name_pos = '{name_pos}' where kod_pos='{kod_pos}'";
                    var command1 = new SqlCommand(changeQuery1, database.getConnection());
                    command1.ExecuteNonQuery();

                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_spiv = int.Parse(textBox2.Text);
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var name_spiv = textBox1.Text;
            var kod_pos = int.Parse(comboBox1.SelectedValue.ToString());
            var name_pos = textBox3.Text;
            var sum_zar = int.Parse(textBox5.Text);


            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_spiv, name_spiv, kod_pos, name_pos, sum_zar);
                dataGridView1.Rows[selectedRowIndex].Cells[5].Value = RowState1.Modified;
            }
        }
        private void Form30_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Posada". При необходимости она может быть перемещена или удалена.
            this.posadaTableAdapter.Fill(this.cosmeticsDataSet.Posada);
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
                comboBox1.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
               
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form31 frm31 = new Form31();
            frm31.ShowDialog();
            this.Show();
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

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

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue.ToString()=="1")
            {
                textBox3.Text = "Адміністратор";
            }
            else
            {
                textBox3.Text = "Директор";
            }
        }
    }
}
