using _20T1080068.BusinessLayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web.Controllers
{  /// <summary>
   /// 
   /// </summary>
    [Authorize]
    [RoutePrefix("product")] // ông này nữa =))
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SESSION_CONDITION = "ProductCondition";
        private const string STORAGE_UPLOAD_FILE_PRODUCT = "Public/Images/Products";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Models.ProductSearchInput condition = new Models.ProductSearchInput()
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
                CategoryID = 0,
                SupplierID = 0,
            };
            // trường hợp có session
            if (Session[SESSION_CONDITION] != null)
            {
                condition = Session[SESSION_CONDITION] as Models.ProductSearchInput;
            }
            return View(condition);
        }

        public ActionResult Search(Models.ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page, condition.PageSize, condition.SearchValue,
                        condition.CategoryID, condition.SupplierID, out rowCount);
            Models.ProductSearchOutput result = new Models.ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SESSION_CONDITION] = condition;
            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Product()
            {
                ProductID = 0
            };
            return View(data);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            Product product = ProductDataService.GetProduct(id);
            List<ProductAttribute> productAttributes = ProductDataService.ListAttributes(id);
            List<ProductPhoto> productPhotos = ProductDataService.ListPhotos(id);
            if (product == null || productAttributes == null || productPhotos == null)
            {
                return RedirectToAction("Index");
            }
            Models.ProductEditModel data = new Models.ProductEditModel()
            {
                Product = product,
                ProductAttributes = productAttributes,
                ProductPhotos = productPhotos
            };
            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Product data, HttpPostedFileBase productPhoto)
        {
            // kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError(nameof(data.ProductName), "Tên của mặt hàng không được để trống");
            }
            if (data.CategoryID == 0)
            {
                ModelState.AddModelError(nameof(data.CategoryID), "Vui lòng chọn loại hàng");
            }
            if (data.SupplierID == 0)
            {
                ModelState.AddModelError(nameof(data.SupplierID), "Vui lòng chọn nhà cung cấp");
            }
            if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError(nameof(data.Unit), "Đơn vị tính của mặt hàng không được để trống");
            }
            if (data.Price.Equals(null))
            {
                ModelState.AddModelError(nameof(data.Price), "Giá của mặt hàng không được để trống");
            }
            if (data.ProductID == 0 && productPhoto == null)
            {
                ModelState.AddModelError(nameof(data.Photo), "Vui lòng thêm ảnh");
            }
            // xử lý nghiệp vụ upload file
            if (productPhoto != null)
            {
                string storage = Server.MapPath($"~/Public/Images/Product");
                string fileName = $"{DateTime.Now.Ticks}-{productPhoto.FileName}";
                string filePath = System.IO.Path.Combine(storage, fileName);
                productPhoto.SaveAs(filePath);
                data.Photo = $"/{STORAGE_UPLOAD_FILE_PRODUCT}/{fileName}";
            }
            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Bổ sung mặt hàng";
                    return View("Create", data);
                }
                ViewBag.Title = "Cập nhật mặt hàng";
                return View("Edit", data);
            }

            // gọi thêm và sửa mặt hàng từ tầng business
            if (data.ProductID == 0)
            {
                ProductDataService.AddProduct(data);
            }
            else
            {
                ProductDataService.UpdateProduct(data);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            Product data = ProductDataService.GetProduct(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")] // cấu hình được này nè
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            if (productID <= 0)
            {
                return RedirectToAction("Index");
            }
            ProductPhoto data = null;
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    data = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = productID
                    };
                    return View(data);
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    if (photoID < 0)
                    {
                        return RedirectToAction("Index");
                    }
                    data = ProductDataService.GetPhoto(photoID);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                case "delete":
                    ProductDataService.DeletePhoto(photoID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Lưu dữ liệu cho hình ảnh
        /// </summary>
        /// <param name="data"></param>
        /// <param name="displayOrder"></param>
        /// <param name="isHidden"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {
            // kiểm tra dữ liệu đầu vào
            //if (data.PhotoID == 0 && uploadPhoto == null) {
            //    ModelState.AddModelError(nameof(data.Photo), "Vui lòng thêm hình ảnh");
            //}
            if (string.IsNullOrWhiteSpace(data.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị hình ảnh không được để trống");
            }
            else if (data.DisplayOrder < 1)
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị hình ảnh phải là một số tự nhiên dương");
            }
            List<ProductPhoto> productPhotos = ProductDataService.ListPhotos(data.ProductID);
            bool isUsedDisplayOrder = false;
            foreach (ProductPhoto item in productPhotos)
            {
                if (item.DisplayOrder == data.DisplayOrder && data.PhotoID != item.PhotoID)
                {
                    isUsedDisplayOrder = true;
                    break;
                }
            }
            if (isUsedDisplayOrder)
            {
                ModelState.AddModelError("DisplayOrder",
                    $"Thứ tự hiển thị {data.DisplayOrder} của hình ảnh đã được sử dụng trước đó");
            }

            data.Description = data.Description ?? "";
            data.IsHidden = Convert.ToBoolean(data.IsHidden.ToString());
            // xử lý nghiệp vụ upload file
            if (uploadPhoto != null)
            {
                string storage = Server.MapPath($"~/{STORAGE_UPLOAD_FILE_PRODUCT}");
                string fileName = $"{DateTime.Now.Ticks}-{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(storage, fileName);
                uploadPhoto.SaveAs(filePath);
                data.Photo = $"/{STORAGE_UPLOAD_FILE_PRODUCT}/{fileName}";
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.PhotoID == 0 ? "Bổ sung ảnh" : "Thay đổi ảnh";
                return View("Photo", data);
            }

            // thực hiện thêm hoặc cập nhật
            if (data.PhotoID == 0)
            {
                ProductDataService.AddPhoto(data);
            }
            else
            {
                ProductDataService.UpdatePhoto(data);
            }
            return RedirectToAction($"Edit/{data.ProductID}");
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
            if (productID < 0)
            {
                return RedirectToAction("Index");
            }
            ProductAttribute data = null;
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    data = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = productID,
                    };
                    return View(data);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    if (attributeID < 0)
                    {
                        return RedirectToAction("Index");
                    }
                    data = ProductDataService.GetAttribute(attributeID);
                    if (data == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(data);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction($"Edit/{productID}"); //return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Lưu dữ liệu của thuộc tính mặt hàng
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            // kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.AttributeName))
            {
                ModelState.AddModelError(nameof(data.AttributeName), "Tên thuộc tính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.AttributeValue))
            {
                ModelState.AddModelError(nameof(data.AttributeValue), "Giá trị thuộc tính không được để trống");
            }

            if (string.IsNullOrWhiteSpace(data.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị thuộc tính không được để trống");
            }
            else if (data.DisplayOrder < 1)
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị thuộc tính phải là một số tự nhiên dương");
            }
            List<ProductAttribute> productAttributes = ProductDataService.ListAttributes(data.ProductID);
            bool isUsedDisplayOrder = false;
            foreach (ProductAttribute item in productAttributes)
            {
                if (item.DisplayOrder == data.DisplayOrder && data.AttributeID != item.AttributeID)
                {
                    isUsedDisplayOrder = true;
                    break;
                }
            }
            if (isUsedDisplayOrder)
            {
                ModelState.AddModelError("DisplayOrder",
                        $"Thứ tự hiển thị {data.DisplayOrder} của thuộc tính đã được sử dụng trước đó");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.AttributeID == 0 ? "Bổ sung thuộc tính" : "Thay đổi thuộc tính";
                return View("Attribute", data);
            }

            // thực hiện thêm hoặc cập nhật
            if (data.AttributeID == 0)
            {
                ProductDataService.AddAttribute(data);
            }
            else
            {
                ProductDataService.UpdateAttribute(data);
            }
            return RedirectToAction($"Edit/{data.ProductID}");
        }
    }
}