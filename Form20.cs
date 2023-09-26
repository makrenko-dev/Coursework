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
    public partial class Form20 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form20()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox5.Text == "" || textBox4.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Ви збираєтеся додати запис до таблиці Поставок , погоджуєтеся? ", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Openconnection();
                    var kod_post = int.Parse(textBox2.Text);
                    var day_post = int.Parse(textBox5.Text);
                    var month_post = int.Parse(textBox4.Text);
                    var year_post = int.Parse(textBox6.Text);

                    var addQuery = $"insert into Postavka (kod_post, day_post,month_post,year_post) values('{kod_post}' , '{day_post}','{month_post}','{year_post}')";
                    var command = new SqlCommand(addQuery, database.getConnection());
                    command.ExecuteNonQuery();

                    var addQuery1 = $"insert into Journal (diya,chas,vykon) values('{"Додано Поставку"}' , '{DateTime.Now}', '{"Адміністратор"}')";
                    var command1 = new SqlCommand(addQuery1, database.getConnection());
                    command1.ExecuteNonQuery();

                    MessageBox.Show("Запис успішно створений!", "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    database.Closeconnection();
                }
                else if (dialogResult == DialogResult.No)
                {

                }
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
