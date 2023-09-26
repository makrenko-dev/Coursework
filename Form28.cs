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
    public partial class Form28 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form28()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox1.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Ви збираєтеся додати запис до таблиці Банки , погоджуєтеся? ", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Openconnection();
                    var kod_bank = int.Parse(textBox2.Text);

                    var name_bank = textBox1.Text;
                    var city_b = textBox8.Text;


                    var addQuery = $"insert into Bank (kod_bank, name_bank,city_b) values('{kod_bank}' , '{name_bank}','{city_b}')";
                    var command = new SqlCommand(addQuery, database.getConnection());
                    command.ExecuteNonQuery();

                    var addQuery1 = $"insert into Journal (diya,chas) values('{"Додано запис у таблицю Банки"}' , '{DateTime.Now}')";
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsLetter(number) && number != 8 && number != 32)
            {
                e.Handled = true;
                MessageBox.Show("Вводити можна лише букви", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Form28_Load(object sender, EventArgs e)
        {

        }
    }
}
