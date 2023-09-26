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
    public partial class Form12 : Form
    {
        Database database = new Database();
        public Form12()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            var loginuser = textBox2.Text;
            var passUser = textBox3.Text;

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
                Form5 frm5 = new Form5(user);
                this.Hide();
                frm5.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Аккаунта не существует!", "Неуспешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
