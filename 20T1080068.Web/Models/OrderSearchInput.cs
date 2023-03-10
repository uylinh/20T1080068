using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20T1080068.Web.Models
{
    public class OrderSearchInput : PaginationSearchInput
    {

        public int  Status { get; set; }
    }
}