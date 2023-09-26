using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Kursovaya_Makrenko_PZ_20_3
{
    public partial class Form37 : Form
    {
        Database database = new Database();
        public Form37()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox2.Text;
            var password = textBox3.Text;
            if(textBox2.Text=="" || textBox3.Text=="")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           

            
            else
            { 
                
                if (checkuser())
            {
                return;
            }
                string querystring = $"insert into register (login_user, password_user, is_admin) values('{login}' , '{password}', 1)";
              SqlCommand command = new SqlCommand(querystring, database.getConnection());

             database.Openconnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успішно створений!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.ShowDialog();
                this.Show();
            }

            else
                MessageBox.Show("Аккаунт не створений!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            database.Closeconnection();
            }
            
        }

        private Boolean checkuser()
        {
            var loginuser = textBox2.Text;
            var passUser = textBox3.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user, is_admin from register where login_user = '{loginuser}' and password_user='{passUser}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Користувач вже існує!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
                return false;
        }

        private void Form37_Load(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
            textBox2.MaxLength = 50;
            textBox3.MaxLength = 50;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            this.Hide();
            frm2.ShowDialog();
            this.Show();
        }
    }
}
