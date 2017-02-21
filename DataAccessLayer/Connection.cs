using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Interact2World.Models
{
    public class Connection
    {
        public SqlConnection GetConnection()
        {
            try
            {
                ReadXml read = new ReadXml();

                SqlConnection conn = new SqlConnection(read.GetConnectionString());

                OpenConnection(conn);

                return conn;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void OpenConnection(SqlConnection conn)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public void CloseConnection(SqlConnection conn)
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }

}