using _20T1080068.DataLayers.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1080068.DomainModels;
using System.Data;
using System.Data.SqlClient;

namespace _20T1080068.Datalayers.SQLServer
{
    public class EmpolyeeAccountDAL : _BaseDAL , IUserAccountDAL
    {
        public EmpolyeeAccountDAL(String conmectionString) : base(conmectionString){
        }

        public UserAccount Authorize(string username, string password)
        {
            UserAccount data = null;
            using (var conmection = OpenConnection())
            {
                var cmd= conmection.CreateCommand();
                cmd.CommandText = "SELECT * From Employees WHERE Email=@Email AND Password=@Password  ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", username);
                cmd.Parameters.AddWithValue("@Password", password);


                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {

                            
                                UserID = Convert.ToString(dbReader[ "EmployeeID"]),
                                UserName = Convert.ToString(dbReader["Email"]),
                                FullName = $"{dbReader[ "FirstName"]} {dbReader["LastName"]}",
                                Email = Convert.ToString(dbReader["Email"]),
                                Photo = Convert.ToString(dbReader["Photo"]),
                                Password = " ",
                                RoleName = " "

                         };


                    }
                     dbReader.Close();
                }
                    conmection.Close();
            }
            return data;
        }


        public bool ChangePassword(string userName, string oldPassword, string newPassword)

        { bool result = false;
            // so sánh mật khẩu cũ trong database
            UserAccount userAccount = Authorize(userName, oldPassword);
            if (userAccount == null) return result;
            using (SqlConnection connection = OpenConnection()) {
                SqlCommand cmd = new SqlCommand();
                // cập nhật mật khẩu mới
                cmd.CommandText = @"UPDATE Employees
                                    SET Password = @Password
                                    WHERE Email = @Email";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Password", newPassword);
                cmd.Parameters.AddWithValue("@Email", userName);
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            };
            return result;
        }
    }
    
}
