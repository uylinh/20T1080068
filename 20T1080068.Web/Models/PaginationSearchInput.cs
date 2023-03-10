using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{
    public class PaginationSearchInput 
    {
        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Giá trị cần tìm
        /// </summary>
        public string SearchValue { get; set; }
    }
}