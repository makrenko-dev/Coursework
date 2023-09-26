using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kursovaya_Makrenko_PZ_20_3
{
    public partial class Form7 : Form
    {

        Database database = new Database();
        public Form7()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT C.kod_opl, C.kod_prod,P.name_st FROM Oplata AS C JOIN Status_opl AS P ON P.kod_st = C.kod_st AND C.kod_opl=@name", database.getConnection());
            int name = int.Parse(textBox1.Text);
            SqlParameter minusParam = new SqlParameter("@name", name);
            cmd.Parameters.Add(minusParam);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            database.Openconnection();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow1(dataGridView1, reader);
            }

            reader.Close();
            database.Closeconnection();
        }

        private void CreateColumns()
        {
            dataGridView2.Columns.Add("kod_prod", "Код продажу");
            dataGridView2.Columns.Add("kod_ka", "Код контрагента");
            dataGridView2.Columns.Add("day_prod", "День доставки");
            dataGridView2.Columns.Add("month_prod", "Місяць доставки");
            dataGridView2.Columns.Add("year_prod", "Рік доставки");
        }

        private void CreateColumns1()
        {
            dataGridView1.Columns.Add("kod_opl", "Код оплати");
            dataGridView1.Columns.Add("kod_prod", "Код продажу");
            
            dataGridView1.Columns.Add("kod_st", "Статус оплати");


        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4));

        }

        private void ReadSingleRow1(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2));

        }
        private void button2_Click(object sender, EventArgs e)
        {


            dataGridView2.Rows.Clear();

            string queryString = $"select * from Prodazh WHERE kod_prod=@name";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            int name = int.Parse(textBox2.Text);
            SqlParameter minusParam = new SqlParameter("@name", name);
            command.Parameters.Add(minusParam);
            SqlDataAdapter da = new SqlDataAdapter(command);
            database.Openconnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dataGridView2, reader);
            }

            reader.Close();
            database.Closeconnection();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            CreateColumns();
            CreateColumns1();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише цифри", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
    }
}
