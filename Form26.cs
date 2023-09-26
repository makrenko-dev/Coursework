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
    public partial class Form26 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form26()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox1.Text == "" || textBox8.Text == "" || textBox5.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Ви збираєтеся додати запис до таблиці Виробники і Адреса , погоджуєтеся? ", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Openconnection();
                    var kod_proiz = int.Parse(textBox2.Text);
                    var kod_addr = int.Parse(textBox1.Text);
                    var name_a = textBox8.Text;
                    var city = textBox5.Text;
                    var street = textBox4.Text;
                    var house = int.Parse(textBox6.Text);
                    var office = int.Parse(textBox7.Text);
                    var phone = int.Parse(textBox3.Text);

                    var addQuery = $"insert into Vyrobyk (kod_proiz, kod_addr) values('{kod_proiz}' , '{kod_addr}')";
                    var command = new SqlCommand(addQuery, database.getConnection());
                    command.ExecuteNonQuery();
                    var changeQuery1 = $"insert into Adress (kod_addr, name_a, city, street, house, office, phone) values('{kod_addr}' , '{name_a}' , '{city}' , '{street}' , '{house}' , '{office}' , '{phone}')";
                    var command1 = new SqlCommand(changeQuery1, database.getConnection());
                    command1.ExecuteNonQuery();

                    var addQuery1 = $"insert into Journal (diya,chas) values('{"Додано запис у таблицю Виробники і Адреса"}' , '{DateTime.Now}')";
                    var command2 = new SqlCommand(addQuery1, database.getConnection());
                    command2.ExecuteNonQuery();

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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
