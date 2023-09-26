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
    public partial class Form23 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form23()
        {
            InitializeComponent();
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_ka", "Код контрагента");
            dataGridView1.Columns.Add("kod_addr", "Код адреси");
            dataGridView1.Columns.Add("name_a", "Назва");
            dataGridView1.Columns.Add("city", "Місто");
            dataGridView1.Columns.Add("street", "Вулиця");
            dataGridView1.Columns.Add("house", "Дім");
            dataGridView1.Columns.Add("office", "Офіс");
            dataGridView1.Columns.Add("phone", "Телефон");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox8.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetInt32(5), record.GetInt32(6), record.GetInt32(7), RowState.ModifiesView);
        }


        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"SELECT C.kod_ka, C.kod_addr, P.name_a, P.city, P.street, P.house, P.office, P.phone FROM Kontragent AS C JOIN Adress AS P ON P.kod_addr = C.kod_addr";

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
        private void Form23_Load(object sender, EventArgs e)
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
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
                textBox3.Text = row.Cells[7].Value.ToString();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void Update()
        {
            database.Openconnection();


            for (int index = 0; index < dataGridView1.Rows.Count - 1; index++)
            {

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[8].Value;
                if (rowState == RowState1.Existed)
                    continue;
                if (rowState == RowState1.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Prodazh where kod_prod={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState1.Modified)
                {
                   
                    var kod_ka = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var kod_addr = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var name_a = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var city = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var street = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var house = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var office = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var phone = dataGridView1.Rows[index].Cells[7].Value.ToString();


                   var changeQuery = $"update Kontragent set kod_addr='{kod_addr}' where kod_ka='{kod_ka}'";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                    
                    var changeQuery1 = $"update Adress set name_a = '{name_a}',city = '{city}',street = '{street}',house = '{house}',office = '{office}',phone = '{phone}' where kod_addr='{kod_addr}'";
                    var command1 = new SqlCommand(changeQuery1, database.getConnection());
                    command1.ExecuteNonQuery();
                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_ka = int.Parse(textBox2.Text);
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var kod_addr = int.Parse(textBox1.Text);
            var name_a = textBox8.Text;
            var city = textBox5.Text;
            var street = textBox4.Text;
            var house = int.Parse(textBox6.Text);
            var office = int.Parse(textBox7.Text);
            var phone = int.Parse(textBox3.Text);

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_ka, kod_addr, name_a, city, street, house, office, phone);
                dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState1.Modified;
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form24 frm24 = new Form24();
            frm24.Show();
        }
    }
}
