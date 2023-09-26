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
    public partial class Form8 : Form
    {

        Database database = new Database();
        public Form8()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetString(0), record.GetDecimal(1), record.GetInt32(2), record.GetInt32(3), record.GetInt32(4), record.GetInt32(5), record.GetString(6));

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                
                    if(radioButton10.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=10", database.getConnection());
                        
                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }

                    else if(radioButton9.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=11", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }

                    else if(radioButton8.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=12", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }

                    else if(radioButton7.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=13", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }
                    else if (radioButton11.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=2", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }
                    else if(radioButton6.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2 AND P.kod_var=3", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }

                    else
                    {
                        dataGridView1.Rows.Clear();

                        SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=2", database.getConnection());

                        database.Openconnection();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ReadSingleRow(dataGridView1, reader);
                        }

                        reader.Close();
                        database.Closeconnection();
                    }
               
            }
            else if(radioButton2.Checked)
            {
                if (radioButton5.Checked)
                {
                    dataGridView1.Rows.Clear();

                    SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=1 AND P.kod_var=4", database.getConnection());

                    database.Openconnection();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }

                    reader.Close();
                    database.Closeconnection();
                }

                else if (radioButton6.Checked)
                {
                    dataGridView1.Rows.Clear();

                    SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=1 AND P.kod_var=3", database.getConnection());

                    database.Openconnection();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }

                    reader.Close();
                    database.Closeconnection();
                }
                else
                {
                    dataGridView1.Rows.Clear();

                    SqlCommand cmd = new SqlCommand("SELECT C.name_nom, C.tsina,C.kod_typ, C.kod_pol,P.kod_rek,P.kod_var,D.rek FROM Nomenklatura AS C JOIN Recomendation AS P ON P.kod_nom = C.kod_nom JOIN Var_rec AS D ON D.kod_var = P.kod_var AND C.kod_pol=1", database.getConnection());

                    database.Openconnection();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReadSingleRow(dataGridView1, reader);
                    }

                    reader.Close();
                    database.Closeconnection();
                }
            }
            
           
        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("name_nom", "Назва номенклатури");
            dataGridView1.Columns.Add("tsina", "Ціна");
            dataGridView1.Columns.Add("kod_typ", "Код типу");
            dataGridView1.Columns.Add("kod_pol", "Стать");
            dataGridView1.Columns.Add("kod_rek", "Код рекомендації");
            dataGridView1.Columns.Add("kod_var", "Код варіанту рекомендації");
            dataGridView1.Columns.Add("rek", "Рекомендація");
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            CreateColumns();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton6.Visible = true;
                radioButton5.Visible = true;
            }

            if (radioButton2.Checked == false)
            {
                radioButton6.Visible = true;
                radioButton5.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton10.Visible = true;
                radioButton9.Visible = true;
                radioButton8.Visible = true;
                radioButton7.Visible = true;
                radioButton11.Visible = true;

            }

            if (radioButton1.Checked == false)
            {

                radioButton10.Visible = false;
                radioButton9.Visible = false;
                radioButton8.Visible = false;
                radioButton7.Visible = false;
                radioButton11.Visible = false;
            }
        }
    }
}
