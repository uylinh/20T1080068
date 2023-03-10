using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1080068.DomainModels
{
    public class Employees
    {
        /// <summary>
        ///  Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// Họ và tên đệm
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Ảnh
        /// </summary>
        public string Photo{ get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>

        public string Notes { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }

}

