using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.DomainModels
{
    /// <summary>
    /// Thông tin tài khoản người dùng
    /// </summary>
    public class UserAccount
    {
        public string UserID { get; set; }
        public string UserName { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Photo { get; set; }
    }
}
