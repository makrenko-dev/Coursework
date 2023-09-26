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
    public partial class Form5 : Form
    {
        private readonly checkUser _user;
        Database database = new Database();
        public Form5(checkUser user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void isAdmin()
        {
            toolStripTextBox1.Enabled = _user.Isadmin;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Form21 frm21 = new Form21();
            this.Hide();
            frm21.ShowDialog();
            this.Show();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = $"{_user.Login}: {_user.Status}";
            isAdmin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form11 frm11 = new Form11();
            this.Hide();
            frm11.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form22 frm22 = new Form22();
            this.Hide();
            frm22.ShowDialog();
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form23 frm23 = new Form23();
            this.Hide();
            frm23.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form25 frm25 = new Form25();
            this.Hide();
            frm25.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form29 frm29 = new Form29();
            this.Hide();
            frm29.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form27 frm27 = new Form27();
            this.Hide();
            frm27.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form30 frm30 = new Form30();
            this.Hide();
            frm30.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form32 frm32 = new Form32();
            this.Hide();
            frm32.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form38 frm38 = new Form38();
            this.Hide();
            frm38.ShowDialog();
            this.Show();
        }
    }
}
