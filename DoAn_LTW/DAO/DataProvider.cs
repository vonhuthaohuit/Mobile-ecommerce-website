using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_LTW.DAO
{
    public class DataProvider
    {
        private static DataProvider instance; // Ctrl + R + E

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider()
        {

        }
        private string linkConnect = @"";

        public DataTable ExecuteQuery(string query, object[] paramester = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection Connection = new SqlConnection(linkConnect))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand(query, Connection);

                if (paramester != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paramester[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                Connection.Close();
            }

            return data;
        }

        public int ExecuteNoQuery(string query, object[] paramester = null)
        {
            int data = 0;
            using (SqlConnection Connection = new SqlConnection(linkConnect))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand(query, Connection);

                if (paramester != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paramester[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                Connection.Close();
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] paramester = null)
        {
            object data = 0;
            using (SqlConnection Connection = new SqlConnection(linkConnect))
            {
                Connection.Open();

                SqlCommand command = new SqlCommand(query, Connection);

                if (paramester != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, paramester[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                Connection.Close();
            }

            return data;
        }

        public DataTable Datafile(string TruyVan)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(TruyVan, linkConnect);
            da.Fill(ds, "Temp");

            return ds.Tables[0];
        }
    }
}
