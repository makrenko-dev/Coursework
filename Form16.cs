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
    enum RowState2
    {
        Existed,
        New,
        Modified,
        ModifiesView,
        Deleted

    }

    public partial class Form16 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form16()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_prod", "Код продажі");
            dataGridView1.Columns.Add("kod_ka", "Код контрагента");
            dataGridView1.Columns.Add("day_prod", "День");
            dataGridView1.Columns.Add("month_prod", "Місяць");
            dataGridView1.Columns.Add("year_prod", "Рік");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            comboBox1.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
           
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4), RowState.ModifiesView);
        }


        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Prodazh";

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
        private void Form16_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Kontragent". При необходимости она может быть перемещена или удалена.
            this.kontragentTableAdapter.Fill(this.cosmeticsDataSet.Kontragent);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Prodazh". При необходимости она может быть перемещена или удалена.
            this.prodazhTableAdapter.Fill(this.cosmeticsDataSet.Prodazh);
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
                comboBox1.Text = row.Cells[1].Value.ToString();
                textBox5.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
               
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
            string searchString = $"select * from Prodazh where concat (kod_prod, kod_ka,day_prod,month_prod,year_prod ) like '%" + textBox1.Text + "%'";

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

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[5].Value;
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
                    var kod_prod = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var kod_ka = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var day_prod = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var month_prod = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var year_prod = dataGridView1.Rows[index].Cells[4].Value.ToString();
                

                    var changeQuery = $"update Prodazh set kod_ka='{kod_ka}',day_prod='{day_prod}',month_prod='{month_prod}',year_prod='{year_prod}' where kod_prod='{kod_prod}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_prod = int.Parse(textBox2.Text);
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var kod_ka = int.Parse(comboBox1.SelectedValue.ToString());
            var day_prod = int.Parse(textBox5.Text);
            var month_prod = int.Parse(textBox4.Text);
            var year_prod = int.Parse(textBox6.Text);
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_prod, kod_ka, day_prod, month_prod, year_prod);
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
            Form18 frm18 = new Form18();
            frm18.Show();
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
