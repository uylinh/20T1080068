using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1080068.DomainModels;
namespace _20T1080068.Web.Models
{
    /// <summary>
    /// Lữu trữ kết quả tìm kiếm phân trang
    /// </summary>
    public class CategorySearchOutput : PaginationsSearchOutput
    {
        /// <summary>
        /// dữ liệu đầu ra
        /// </summary>
        public List <Category> data { get; set; }

    }
}