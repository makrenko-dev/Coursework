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
    public partial class Form13 : Form
    {
        Database database = new Database();
        public Form13()
        {
            InitializeComponent();
        }
       
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.Openconnection();
            var nikneim = textBox2.Text;
            int kod_zamov = int.Parse(comboBox1.SelectedValue.ToString());
            int kolvo = int.Parse(textBox1.Text);
            var statusz = textBox3.Text;
           
            var addQuery = $"insert into Zamovlenya (nikneim, kod_zamov, kolvo, statusz) values('{nikneim}' , '{kod_zamov}', '{kolvo}', '{statusz}')";
            var command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();

            var addQuery1 = $"insert into Journal (diya,chas,vykon) values('{"Створення замовлення"}' , '{DateTime.Now}', '{"Клієнт"}')";
            var command1 = new SqlCommand(addQuery1, database.getConnection());
            command1.ExecuteNonQuery();

            MessageBox.Show("Заказ успешно создан!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

            database.Closeconnection();
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cosmeticsDataSet.Nomenklatura". При необходимости она может быть перемещена или удалена.
            this.nomenklaturaTableAdapter.Fill(this.cosmeticsDataSet.Nomenklatura);

        }
    }
}
