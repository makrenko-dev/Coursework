using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya_Makrenko_PZ_20_3
{
    public partial class Form4 : Form
    {

        private readonly checkUser _user;
        Database database = new Database();
   
        public Form4(checkUser user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void isAdmin()
        {
            toolStripTextBox1.Enabled = _user.Isadmin;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form9 frm9 = new Form9();
            this.Hide();
            frm9.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form15 frm15 = new Form15();
            this.Hide();
            frm15.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form16 frm16 = new Form16();
            this.Hide();
            frm16.ShowDialog();
            this.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"{_user.Login}: {_user.Status}";
            isAdmin();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form19 frm19 = new Form19();
            this.Hide();
            frm19.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form34 frm34 = new Form34();
            this.Hide();
            frm34.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form36 frm36 = new Form36();
            this.Hide();
            frm36.ShowDialog();
            this.Show();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form19 frm19 = new Form19();
            this.Hide();
            frm19.ShowDialog();
            this.Show();
        }
    }
}
