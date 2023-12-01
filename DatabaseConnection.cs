using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_OOP_and_DSA
{
    internal class DatabaseConnection
    {
        public DatabaseConnection()
        {
            DatabaseConnect();
        }
        public SqlConnection DatabaseConnect()
        {
            string connectionString;
            SqlConnection cn;
            connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=LibraryManagementSystem;Integrated Security=True";
            cn = new SqlConnection(connectionString);
            return cn;
        }
        public List<string[]> QueryDatabase(SqlConnection cn)
        {
            List<string[]> results = new List<string[]>();
            try
            {
                cn.Open();
                SqlCommand cmd;
                SqlDataReader dataReader;
                String sql;
                string[] output;
                sql = "SELECT * FROM Books";
                cmd = new SqlCommand(sql, cn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    output = new string[9];
                    for (int i = 1; i < dataReader.FieldCount - 1; i++)
                    {
                        output[i] = dataReader.GetString(i);
                    }
                    results.Add(output);
                }
                cn.Close();
                
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return results;
        }
        public List<string[]> QueryDatabase(SqlConnection cn, string filter)
        {
            List<string[]> results = new List<string[]>();
            try
            {
                cn.Open();
                SqlCommand cmd;
                SqlDataReader dataReader;
                String sql;
                string[] output = new string[9];
                sql = "SELECT * FROM Books WHERE books_category = " + filter + "OR book_status = " + filter;
                cmd = new SqlCommand(sql, cn);
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    for (int i = 1; i < dataReader.FieldCount - 1; i++)
                    {
                        output[i] = dataReader.GetString(i);
                    }
                    results.Add(output);
                    Debug.WriteLine(output);
                }
                cn.Close();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return results;
        }
    }
}
