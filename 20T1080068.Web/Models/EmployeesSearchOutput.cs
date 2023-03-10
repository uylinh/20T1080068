using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{
    /// <summary>
    /// Lưu trữ kết quả tìm kiếm, phân trang đối với nhân viên
    /// </summary>
    public class EmployeesSearchOutput : PaginationsSearchOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Employees> Data { get; set; }
    }
}