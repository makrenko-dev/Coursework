using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya_Makrenko_PZ_20_3
{
     class Database
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=MARIALAPTOP;Initial Catalog=Cosmetics;Integrated Security=true;");
        public void Openconnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void Closeconnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
