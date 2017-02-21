using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Interact2World.Models
{
    public class HashingPassword
    {
        private string GenerateSalt()
        {
            return Guid.NewGuid().ToString("N");
        }

        // Using SHA 256 ALGO to Generate Hash Password
        public string GetHashPassword(string UserName, string Password)
        {
            string _strSalt = ReadSaltValue(UserName);

            if (_strSalt == null)
                return "";

            string Combined = Password + _strSalt;

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(Combined);
            Byte[] hash = new SHA256CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }


        public string ReadSaltValue(string UserName)
        {
            try
            {
                string _sqlQuery = "SELECT SaltValue FROM Salt WHERE UserName = '" + UserName + "'";

                Connection _objConn = new Connection();

                SqlConnection _sqlCon = _objConn.GetConnection();

                SqlCommand _sqlCmd = new SqlCommand(_sqlQuery, _sqlCon);

                SqlDataReader _reader = _sqlCmd.ExecuteReader();

                while (_reader.Read())
                {
                    return _reader[0].ToString();
                }

                _objConn.CloseConnection(_sqlCon);

                return null;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public bool InsertSalt(string UserName)
        {
            try
            {
                string _strSalt = GenerateSalt();

                Connection _objConn = new Connection();
                SqlConnection _sqlCon = _objConn.GetConnection();

                SqlCommand _sqlCmd = new SqlCommand();
                _sqlCmd.Connection = _sqlCon;
                _sqlCmd.CommandText = "sp_InsertSalt";
                _sqlCmd.CommandType = CommandType.StoredProcedure;

                _sqlCmd.Parameters.Add("UserName", SqlDbType.VarChar, 100).Value = UserName;
                _sqlCmd.Parameters.Add("SaltValue", SqlDbType.VarChar, 50).Value = _strSalt;

                _sqlCmd.ExecuteNonQuery();

                _objConn.CloseConnection(_sqlCon);

                return true;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }


        public void InsertHashPassword(string UserName, string Password)
        {
            string _HashPassword = ReadHashPassword(UserName);

            if (_HashPassword == "")
            {
                 InsertSalt(UserName);

                _HashPassword = GetHashPassword(UserName, Password);

                if (_HashPassword == "")
                {
                    throw new Exception("Error in Inserting Hash Password");
                }

                Connection _objConn = new Connection();

                SqlConnection _sqlCon = _objConn.GetConnection();

                SqlCommand _sqlCmd = new SqlCommand();
                _sqlCmd.Connection = _sqlCon;
                _sqlCmd.CommandText = "sp_UpdatePasswordDetails";
                _sqlCmd.CommandType = CommandType.StoredProcedure;

                _sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = UserName;
                _sqlCmd.Parameters.Add("@OldPassword", SqlDbType.VarChar, 50).Value = Password;
                _sqlCmd.Parameters.Add("@NewPassword", SqlDbType.VarChar, 50).Value = "";
                _sqlCmd.Parameters.Add("@HashPassword", SqlDbType.VarChar, 50).Value = _HashPassword;
                _sqlCmd.Parameters.Add("@LastUpdatedBy", SqlDbType.VarChar, 50).Value = UserName;
                _sqlCmd.Parameters.Add("@LastUpdatedOn", SqlDbType.DateTime).Value = DateTime.Now;
                _sqlCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 100).Value = "";

                _sqlCmd.ExecuteNonQuery();

                _objConn.CloseConnection(_sqlCon);

            }
        }


        private string ReadHashPassword(String UserName)
        {
            try
            {
                Connection _objCon = new Connection();

                SqlConnection _sqlCon = _objCon.GetConnection();

                string _strQuery = "SELECT HashPassword FROM Login WHERE UserName ='" + UserName + "'";

                SqlCommand _sqlCmd = new SqlCommand(_strQuery, _sqlCon);

                string _strHashPassword = _sqlCmd.ExecuteScalar().ToString();

                _objCon.CloseConnection(_sqlCon);

                return _strHashPassword;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
    }
}