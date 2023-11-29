using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaEasy
{
    internal class DataControler
    {
        public SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=DESKTOP-H3T94T5;Initial Catalog=PharmaEasy;Integrated Security=True;";
            return sqlConnection;
        }
        public DataSet GetData(String Qyery)
        {

            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = Qyery;
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }
        public int GetScalerFunctios(string Query)
        {
            SqlConnection connection = GetSqlConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            connection.Open();
            cmd.CommandText = Query;
            int q = (int)cmd.ExecuteScalar();
            connection.Close();
            return q;
        }
        public void Add_Users_to_Database(String Query)
        {
            SqlConnection sqlConnection = GetSqlConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.CommandText = Query;
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

        }

    }
}
