using _20T1080068.DataLayers.SQLServer;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.Datalayers.SQLServer
{
    public class CustommerAccountDAL : _BaseDAL, IUserAccountDAL
    {
        public CustommerAccountDAL (string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserAccount Authorize(string userName, string password)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            bool result = false;
            // so sánh mật khẩu cũ trong database
            UserAccount userAccount = Authorize(userName, oldPassword);
            if (userAccount == null) return result;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                // cập nhật mật khẩu mới
                cmd.CommandText = @"UPDATE Customer
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
