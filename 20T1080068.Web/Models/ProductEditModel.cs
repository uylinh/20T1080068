using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _20T1080068.DomainModels;

namespace _20T1080068.Web.Models
{
    /// <summary>
    /// Hiển thị các mô hình khi sửa mặt hàng
    /// </summary>
    public class ProductEditModel
    {
        /// <summary>
        /// Lấy ra thông tin một mặt hàng
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Lấy ra danh sách thuộc tính của mặt hàng
        /// </summary>
        public List<ProductAttribute> ProductAttributes { get; set; }
        /// <summary>
        /// Lấy ra danh sách ảnh của mặt hàng
        /// </summary>
        public List<ProductPhoto> ProductPhotos { get; set; }
    }
}