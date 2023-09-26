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
    public partial class Form35 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form35()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Всі поля мають бути заповнені!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(textBox4.Text.Length>20)
            {
                MessageBox.Show("Нікнейм завеликий!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Ви збираєтеся додати запис до таблиці Замовлення , погоджуєтеся? ", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    database.Openconnection();
                    var nikneim = textBox4.Text;
                   
                    var kod_zamov = int.Parse(comboBox1.SelectedValue.ToString());
                    var kolvo = int.Parse(textBox2.Text);
                    var statusz = textBox1.Text;

                    var addQuery = $"insert into Zamovlenya (nikneim, kod_zamov,kolvo,statusz) values('{nikneim}' , '{kod_zamov}', '{kolvo}', '{statusz}')";
                    var command = new SqlCommand(addQuery, database.getConnection());
                    command.ExecuteNonQuery();


                    var addQuery1 = $"insert into Journal (diya,chas,vykon) values('{"Додано Замовлення"}' , '{DateTime.Now}', '{"Адміністратор"}')";
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

        private void Form35_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Nomenklatura". При необходимости она может быть перемещена или удалена.
            this.nomenklaturaTableAdapter.Fill(this.cosmeticsDataSet.Nomenklatura);

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
