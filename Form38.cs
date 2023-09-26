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
    public partial class Form38 : Form
    {
        Database database = new Database();
        public Form38()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.Openconnection();
            string name = "Cosmetics_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "___" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.RootFolder = Environment.SpecialFolder.MyComputer;
            FBD.SelectedPath = @"C:\ДНУ 3 курс\базы данных\Kursovaya_Makrenko_PZ-20-3\bin\Debug\Backup\";
            if (FBD.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string path = FBD.SelectedPath;
            var addQuery = $"BACKUP DATABASE Cosmetics TO DISK='{path}\\{name}.bak' WITH INIT, FORMAT, SKIP; ";
            var command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();
            database.Closeconnection();
            MessageBox.Show("База даних успішно скопійована!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.Openconnection();
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.InitialDirectory = @"C:\ДНУ 3 курс\базы данных\Kursovaya_Makrenko_PZ-20-3\bin\Debug\Backup\";
            if (OFD.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filepath = OFD.FileName;
            var addQuery = $"USE [master]; ALTER DATABASE[Cosmetics] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; USE[master]; EXEC master.dbo.sp_detach_db @dbname = N'Cosmetics', @skipchecks = 'false'; ";
            var command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();
            addQuery = $"RESTORE DATABASE Cosmetics FROM DISK='{filepath}' WITH REPLACE; ";
            command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();
            addQuery = $"USE [Cosmetics];";
            command = new SqlCommand(addQuery, database.getConnection());
            command.ExecuteNonQuery();
            database.Closeconnection();
            MessageBox.Show("База даних успішно відновлена!", "Успіх!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
