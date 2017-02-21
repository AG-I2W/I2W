using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;
using BusinessLogic;
using Data;

namespace Interact2World.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string RoleID { get; set; }

        public string RoleType { get; set; }

        public string AccessRights { get; set; }

        public string ExceptionMessage { get; set; }

        public string IsExists { get; set; }

        public bool LoginCheck()
        {
            try
            {
                Login _LoginData = new Login();
                bl_Login _objLogin = new bl_Login();

                _LoginData.UserName = UserName;
                _LoginData.Password = Password;

                if (_objLogin.LoginCheck(_LoginData))
                {
                    IsExists = _LoginData.IsExists;
                    RoleID = _LoginData.RoleID;
                    RoleType = _LoginData.RoleType;
                    AccessRights = _LoginData.AccessRights;

                    return true;
                }
                else
                {
                    return false;
                }
                //Connection _objConn = new Connection();
                //SqlConnection _sqlCon = _objConn.GetConnection();

                //SqlCommand _sqlCmd = new SqlCommand();
                //_sqlCmd.Connection = _sqlCon;
                //_sqlCmd.CommandText = "sp_LoginCheck";
                //_sqlCmd.CommandType = CommandType.StoredProcedure;

                //_sqlCmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = UserName;
                //_sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = Password;

                //HashingPassword _objHashing = new HashingPassword();

                //string strHashPassword = _objHashing.GetHashPassword(UserName, Password);

                //_sqlCmd.Parameters.Add("@HashPassword", SqlDbType.VarChar, 50).Value = strHashPassword;

                //SqlParameter uIsExists = new SqlParameter("@IsExists", SqlDbType.Char, 2);
                //uIsExists.Direction = ParameterDirection.Output;
                //_sqlCmd.Parameters.Add(uIsExists);

                //SqlParameter uRoleType = new SqlParameter("@RoleType", SqlDbType.VarChar, 20);
                //uRoleType.Direction = ParameterDirection.Output;
                //_sqlCmd.Parameters.Add(uRoleType);

                //SqlParameter uAccessRights = new SqlParameter("@AccessRights", SqlDbType.VarChar, 20);
                //uAccessRights.Direction = ParameterDirection.Output;
                //_sqlCmd.Parameters.Add(uAccessRights);

                //SqlParameter uUserType = new SqlParameter("@RoleId", SqlDbType.VarChar, 50);
                //uUserType.Direction = ParameterDirection.Output;
                //_sqlCmd.Parameters.Add(uUserType);

                //_sqlCmd.ExecuteNonQuery();

                //IsExists = Convert.ToString(_sqlCmd.Parameters["@IsExists"].Value).Trim();
                //RoleID = Convert.ToString(_sqlCmd.Parameters["@RoleId"].Value).Trim();
                //RoleType = Convert.ToString(_sqlCmd.Parameters["@RoleType"].Value).Trim();
                //AccessRights = Convert.ToString(_sqlCmd.Parameters["@AccessRights"].Value).Trim();

                //_objConn.CloseConnection(_sqlCon);

                //if (IsExists.ToString().ToUpper().Trim() == "N")
                //{
                //    throw new Exception("Incorrect User Name or Password");
                //}
                //else if (strHashPassword.Trim() == "")
                //{
                //    _objHashing.InsertHashPassword(UserName, Password);
                //}

            }
            catch (Exception exp)
            {
                ExceptionMessage = exp.Message;
                return false;
            }
        }


        public List<MenuItem> ReadMenu()
        {
            List<MenuItem> _lstMenuData = new List<MenuItem>();

            try
            {
                bl_Menu _objMenu = new bl_Menu();

                _lstMenuData = _objMenu.ReadMenu(UserName, RoleID);
            }
            catch(Exception exp)
            {
                ExceptionMessage = exp.Message;
            }
            return _lstMenuData;
        }
    }
}