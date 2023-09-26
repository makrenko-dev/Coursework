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
    enum RowState1
    {
        Existed,
        New,
        Modified,
        ModifiesView,
        Deleted

    }

    public partial class Form15 : Form
    {
        Database database = new Database();
        int selectedRow;

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("kod_var", "Код варіанту");
            dataGridView1.Columns.Add("rek", "Фільтр");
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiesView);
        }


        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"select * from Var_rec";

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
        public Form15()
        {
            InitializeComponent();
        }

        private void Form15_Load(object sender, EventArgs e)
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
                textBox3.Text = row.Cells[1].Value.ToString();
               
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
            string searchString = $"select * from Var_rec where concat (kod_var, rek) like '%" + textBox1.Text + "%'";

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
        private void DeleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState1.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[2].Value = RowState1.Deleted;
        }

        private void Update()
        {
            database.Openconnection();


            for (int index = 0; index < dataGridView1.Rows.Count - 1; index++)
            {

                var rowState = (RowState1)dataGridView1.Rows[index].Cells[2].Value;
                if (rowState == RowState1.Existed)
                    continue;
                if (rowState == RowState1.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Var_rec where kod_var={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState1.Modified)
                {
                    var kod_var = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var rek = dataGridView1.Rows[index].Cells[1].Value.ToString();
                   
                    var changeQuery = $"update Var_rec set rek='{rek}' where kod_var='{kod_var}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.Closeconnection();
        }

        private void Change()
        {
            var kod_var = textBox2.Text;
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var rek = textBox3.Text;
           

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_var, rek);
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
            Form17 frm17 = new Form17();
            frm17.Show();
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
