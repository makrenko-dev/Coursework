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
    public partial class Form10 : Form
    {
        Database database = new Database();
        public Form10()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="" || textBox3.Text == "" || comboBox1.Text=="" || comboBox2.Text == "" || textBox6.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Поля мають бути заповнені!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            database.Openconnection();
            var kod_nom = textBox2.Text;
            var name_nom = textBox3.Text;
            int kod_proiz = int.Parse(comboBox1.Text);
            var ed_izm = comboBox2.Text;
            decimal tsina = decimal.Parse(textBox6.Text);
            int kod_typ = int.Parse(comboBox3.Text);
            int kod_pol = 0;
            if (comboBox4.Text == "жіноча")
                kod_pol = 1;
            else
                kod_pol = 2;

            decimal ves = decimal.Parse(textBox9.Text);


            var addQuery = $"insert into Nomenklatura (kod_nom, name_nom, kod_proiz, ed_izm,tsina, kod_typ, kod_pol,ves) values('{kod_nom}' , '{name_nom}', '{kod_proiz}', '{ed_izm}', '{tsina}', '{kod_typ}','{kod_pol}','{ves}' )";
            var command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Запись успешно создана!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);



            database.Closeconnection();
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
    }
}
