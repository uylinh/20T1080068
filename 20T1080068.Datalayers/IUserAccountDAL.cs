using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20T1080068.DomainModels;
namespace _20T1080068.Datalayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quàn đến tài khoản  của người dùng
    /// </summary>
     public interface IUserAccountDAL {
        /// <summary>
        /// Kiểm tra tên đăng nhập và mật khẩu của người dùng
        /// có hợp lệ hay không
        /// Nếu hợp lệ thì sẽ trả về thông tin người dùng
        /// Ngược lại trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// Đổi mật khẩu của người dùng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
