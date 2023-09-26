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
    public partial class Form2 : Form
    {

        Database database = new Database();
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginuser = textBox2.Text;
            var passUser = textBox3.Text;


            if (textBox2.Text == "" || textBox3.Text =="")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user, is_admin from register where login_user = '{loginuser}' and password_user='{passUser}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            table.Rows.Clear();
            adapter.SelectCommand = command;
            adapter.Fill(table);


            if (table.Rows.Count == 1)
            {

                var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));
                MessageBox.Show("Успешно вошли!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form4 frm4 = new Form4(user);
                this.Hide();
                frm4.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Аккаунта не существует!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 50;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form37 frm37 = new Form37();
            this.Hide();
            frm37.ShowDialog();
            this.Show();
        }
    }
}
