﻿using System;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 frm7 = new Form7();
            this.Hide();
            frm7.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            this.Hide();
            frm6.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form8 frm8 = new Form8();
            this.Hide();
            frm8.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form13 frm13 = new Form13();
            this.Hide();
            frm13.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form14 frm14 = new Form14();
            this.Hide();
            frm14.ShowDialog();
            this.Show();
        }
    }
}
