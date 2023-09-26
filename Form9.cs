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

    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiesView,
        Deleted

    }
    public partial class Form9 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form9()
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
            dataGridView1.Columns.Add("isNew", string.Empty);

        }

        private void ClearFields()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox6.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox9.Text = "";
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

        private void Form9_Load(object sender, EventArgs e)
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
                comboBox1.Text = row.Cells[2].Value.ToString();
                comboBox2.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                comboBox3.Text = row.Cells[5].Value.ToString();
                comboBox4.Text = row.Cells[6].Value.ToString();
                textBox9.Text = row.Cells[7].Value.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form10 frm10 = new Form10();
            frm10.Show();
        }

        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"select * from Nomenklatura where concat (kod_nom, name_nom, kod_proiz, ed_izm,tsina, kod_typ, kod_pol, ves) like '%" + textBox1.Text + "%'";

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
                dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
        }

        private void Update()
        {
            database.Openconnection();


            for (int index = 0; index < dataGridView1.Rows.Count-1; index++)
            {

                var rowState = (RowState)dataGridView1.Rows[index].Cells[8].Value;
                if (rowState == RowState.Existed)
                    continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from Nomenklatura where kod_nom={id}";

                    var command = new SqlCommand(deleteQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var kod_nom = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name_nom = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var kod_proiz = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var ed_izm = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var tsina = dataGridView1.Rows[index].Cells[4].Value.ToString();
                     var kod_typ= dataGridView1.Rows[index].Cells[5].Value.ToString();
                     var kod_pol = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var ves = dataGridView1.Rows[index].Cells[7].Value.ToString();
                    var changeQuery = $"update Nomenklatura set name_nom='{name_nom}', kod_proiz='{kod_proiz}', ed_izm='{ed_izm}', tsina='{tsina}', kod_typ='{kod_typ}', kod_pol='{kod_pol}', ves='{ves}' where kod_nom='{kod_nom}'";

                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            database.Closeconnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteRow();
            ClearFields();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Change()
        {
            var kod_nom = textBox2.Text;
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var name_nom = textBox3.Text;
            int kod_proiz = int.Parse(comboBox1.Text);
            var ed_izm = comboBox2.Text;
            decimal tsina = decimal.Parse(textBox6.Text);
            int kod_typ = int.Parse(comboBox3.Text);
            int kod_pol = int.Parse(comboBox4.Text); 
            decimal ves = decimal.Parse(textBox9.Text);

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(kod_nom, name_nom, kod_proiz, ed_izm, tsina, kod_typ, kod_pol, ves);
                dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState.Modified;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Change();
            Update();
            ClearFields();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DeleteRow();
            Update();
            ClearFields();
        }
    }
}
