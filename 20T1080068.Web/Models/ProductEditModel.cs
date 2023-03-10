using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1080068.DomainModels;

namespace _20T1080068.Web.Models
{
    public class ProductEditModel : Product
    {
        /// <summary>
        /// Lấy ra danh sách thuộc tính mặt hàng
        /// </summary>
        public List<ProductPhoto> ListOfProductPhotos { get; set;}
        /// <summary>
        /// Lấy ra danh sách ảnh của mặt hàng
        /// </summary>
        public List<ProductAttribute> ListOfProductAttribute { get; set; }

    }
}