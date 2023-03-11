using _20T1080068.BusinessLayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;


namespace _20T1080068.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
       
            private const string SHOPPING_CART = "ShoppingCart";
            private const string ERROR_MESSAGE = "ErrorMessage";
            private const string SESSION_CONDITION = "OrderCondition";
            private const string CUSTOMERID = "CustomerID";
            private const string EMPLOYEEID = "EmployeeID";
            private const int PAGE_SIZE = 4;
            

            /// <summary>
            /// Tìm kiếm, phân trang
            /// </summary>
            /// <returns></returns>
            public ActionResult Index()
        {
            //TODO: Code chức năng tìm kiếm, phân trang cho đơn hàng

            ViewBag.Title = "Quản lý đơn hàng ";

            Models.OrderSearchInput condition
                = Session[SESSION_CONDITION] as Models.OrderSearchInput;

            if (condition == null)
            {
                condition = new Models.OrderSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE + 4,
                    SearchValue = "",
                    Status = 0
                };
            }

            return View(condition); // truyền dữ liệu bằng model
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Search(Models.OrderSearchInput condition)//(int page = 1, string searchValue = "")
        {

            int rowCount = 0;
            var data = OrderDataService.ListOfOrders(condition.Page, condition.PageSize, condition.Status, condition.SearchValue, out rowCount);

            Models.OrderSearchOutput result = new Models.OrderSearchOutput()
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
        /// Xem thông tin và chi tiết của đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            //TODO: Code chức năng lấy và hiển thị thông tin của đơn hàng và chi tiết của đơn hàng
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            Order order = OrderDataService.GetOrder(id);
            List<OrderDetail> orderDetails = OrderDataService.ListOrderDetails(id);
            Models.OrderDetailsModel result = new Models.OrderDetailsModel()
            {
                Order = order,
                OrderDetails = orderDetails
            };
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            return View(result);
        }
        /// <summary>
        /// Giao diện Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        [Route("EditDetail/{orderID}/{productID}")]
        public ActionResult EditDetail(int orderID = 0, int productID = 0)
        {
            //DONE: Code chức năng để lấy chi tiết đơn hàng cần edit
            if (orderID < 0)
            {
                return RedirectToAction("Index");
            }
            if (productID < 0)
            {
                return RedirectToAction($"Details/{orderID}");
            }
            OrderDetail orderDetail = OrderDataService.GetOrderDetail(orderID, productID);
            if (orderDetail == null)
            {
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }
        /// <summary>
        /// Giao diện Bổ sung mặt hàng trong chi tiết đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateDetail(int orderID = 0, int productID = 0)
            
        {
            if (orderID <= 0 || productID <= 0)
            {
                return View();
            }
            var order = OrderDataService.GetOrder(orderID);
            if (order.Status == OrderStatus.INIT)
            {
                Product data = ProductDataService.GetProduct(productID);
                return View(productID.ToString());
            }
            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateDetail(OrderDetail data)
        {
            //DONE: Code chức năng để cập nhật chi tiết đơn hàng
            // kiểm tra dữ liệu đầu vào
            // Mã mặt hàng
            if (data.ProductID <= 0)
            {
                TempData[ERROR_MESSAGE] =$"Mã đơn hàng không tồn tại";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            // Số lượng
            if (data.Quantity < 1)
            {

                TempData[ERROR_MESSAGE] = $"Số lượng không hợp lệ ";
                return RedirectToAction($"Details/{data.OrderID}");
            }

            // Đơn giá
            if (data.SalePrice < 1)
            {

                TempData[ERROR_MESSAGE] = $"Đơn giá không hợp lệ";
                return RedirectToAction($"Details/{data.OrderID}");
            }

            // Cập nhật chi tiết 1 đơn hàng nếu kiểm tra đúng hết
            OrderDataService.SaveOrderDetail(data.OrderID, data.ProductID, data.Quantity, data.SalePrice);
            return RedirectToAction($"Details/{data.OrderID}");

        }
        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteDetail/{orderID}/{productID}")]
        public ActionResult DeleteDetail(int orderID = 0, int productID = 0)
        { //DONE: Code chức năng xóa 1 chi tiết trong đơn hàng
            if (orderID < 0)
            {
                return RedirectToAction("Index");
            }
            if (productID < 0)
            {
                return RedirectToAction($"Details/{orderID}");
            }

            // Xoá chi tiết 1 đơn hàng nếu kiểm tra đúng hết
            bool isDeleted = OrderDataService.DeleteOrderDetail(orderID, productID);
            if (!isDeleted)
            {
                TempData[ERROR_MESSAGE] = "Không thể xoá mặt hàng này";
                return RedirectToAction($"Details/{productID}");
            }
            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            //DONE: Code chức năng để xóa đơn hàng (nếu được phép xóa)
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            // Xoá đơn hàng ở trạng thái vừa tạo, bị huỷ hoặc bị từ chối
            if (data.Status == OrderStatus.INIT
                || data.Status == OrderStatus.CANCEL
                || data.Status == OrderStatus.REJECTED)
            {
                bool isDeleted = OrderDataService.DeleteOrder(id);
                if (!isDeleted)
                {
                    TempData[ERROR_MESSAGE] = $"Xoá đơn hàng thất bại";
                    return RedirectToAction($"Details/{data.OrderID}");
                }
                return RedirectToAction("Index");
            }
            TempData[ERROR_MESSAGE] = $"Xoá đơn hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
            return RedirectToAction($"Details/{data.OrderID}");
        }
        /// <summary>
        /// Chấp nhận đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Accept(int id = 0)
        {
            //DONE: Code chức năng chấp nhận đơn hàng (nếu được phép)
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            bool isAccepted = OrderDataService.AcceptOrder(id);
            if (!isAccepted)
            {
                TempData[ERROR_MESSAGE] =
                    $"Chấp nhận đơn hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Xác nhận chuyển đơn hàng cho người giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shipping(int id = 0, int shipperID = 0, int countProducts = 0)
        {
            //DONE: Code chức năng chuyển đơn hàng sang trạng thái đang giao hàng (nếu được phép)
            if (id < 0)
            {
                return RedirectToAction("Index");
            }
            if (Request.HttpMethod == "GET")
            {
                ViewBag.OrderID = id;
                ViewBag.CountProducts = countProducts;
                return View();
            }

            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            if (shipperID <= 0)
            {
                TempData[ERROR_MESSAGE] = "Bạn phải chọn đơn vị vận chuyển";
                return RedirectToAction($"Details/{id}");
            }
            if (countProducts <= 0)
            {
                TempData[ERROR_MESSAGE] = "Không có mặt hàng nào để chuyển giao";
                return RedirectToAction($"Details/{id}");
            }
            bool isShipped = OrderDataService.ShipOrder(id, shipperID);
            if (!isShipped)
            {
                TempData[ERROR_MESSAGE] =
                    $"Xác nhận chuyển đơn hàng cho người giao hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Ghi nhận hoàn tất thành công đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Finish(int id = 0)
        {
            //DONE: Code chức năng ghi nhận hoàn tất đơn hàng (nếu được phép)
            if (id < 0)
            {
                return RedirectToAction("Index");
            }

            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            bool isFinished = OrderDataService.FinishOrder(id);
            if (!isFinished)
            {
                TempData[ERROR_MESSAGE] =
                    $"Xác nhận hoàn tất đơn hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cancel(int id = 0)
        {
            //DONE: Code chức năng hủy đơn hàng (nếu được phép)
            if (id < 0)
            {
                return RedirectToAction("Index");
            }

            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            bool isCanceled = OrderDataService.CancelOrder(id);
            if (!isCanceled)
            {
                TempData[ERROR_MESSAGE] =
                    $"Hủy bỏ đơn hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(int id = 0)
        {
            //DONE: Code chức năng từ chối đơn hàng (nếu được phép)
            if (id < 0)
            {
                return RedirectToAction("Index");
            }

            Order data = OrderDataService.GetOrder(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            bool isRejected = OrderDataService.RejectOrder(id);
            if (!isRejected)
            {
                TempData[ERROR_MESSAGE] =
                    $"Từ chối đơn hàng thất bại vì trạng thái đơn hàng hiện tại là: {data.StatusDescription}";
                return RedirectToAction($"Details/{data.OrderID}");
            }
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Sử dụng 1 biến session để lưu tạm giỏ hàng (danh sách các chi tiết của đơn hàng) trong quá trình xử lý.
        /// Hàm này lấy giỏ hàng hiện đang có trong session (nếu chưa có thì tạo mới giỏ hàng rỗng)
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetShoppingCart()
        {
            List<OrderDetail> shoppingCart = Session[SHOPPING_CART] as List<OrderDetail>;
            if (shoppingCart == null)
            {
                shoppingCart = new List<OrderDetail>();
                Session[SHOPPING_CART] = shoppingCart;
            }
            return shoppingCart;
        }
        /// <summary>
        /// Giao diện lập đơn hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            ViewBag.CustomerID = TempData[CUSTOMERID] ?? "";
            ViewBag.EmployeeID = TempData[EMPLOYEEID] ?? "";
            return View(GetShoppingCart());
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult SearchProducts(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(page, PAGE_SIZE, searchValue, 0, 0, out rowCount);
            ViewBag.Page = page;
            return View(data);
        }
        /// <summary>
        /// Bổ sung thêm hàng vào giỏ hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(OrderDetail data)
        {
            if (data == null)
            {
                TempData[ERROR_MESSAGE] = "Dữ liệu không hợp lệ";
                return RedirectToAction("Create");
            }
            if (data.SalePrice <= 0 || data.Quantity <= 0)
            {
                TempData[ERROR_MESSAGE] = "Giá bán và số lượng không hợp lệ";
                return RedirectToAction("Create");
            }

            List<OrderDetail> shoppingCart = GetShoppingCart();
            var existsProduct = shoppingCart.FirstOrDefault(m => m.ProductID == data.ProductID);

            if (existsProduct == null) //Nếu mặt hàng cần được bổ sung chưa có trong giỏ hàng thì bổ sung vào giỏ
            {

                shoppingCart.Add(data);
            }
            else //Trường hợp mặt hàng cần bổ sung đã có thì tăng số lượng và thay đổi đơn giá
            {
                existsProduct.Quantity += data.Quantity;
                existsProduct.SalePrice = data.SalePrice;
            }
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="id">Mã mặt hàng</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            int index = shoppingCart.FindIndex(m => m.ProductID == id);
            if (index >= 0)
                shoppingCart.RemoveAt(index);
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Khởi tạo đơn hàng (với phần thông tin chi tiết của đơn hàng là giỏ hàng đang có trong session)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Init(int customerID = 0, int employeeID = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                TempData[ERROR_MESSAGE] = "Không thể tạo đơn hàng với giỏ hàng trống";
                return RedirectToAction("Create");
            }

            if (customerID == 0 || employeeID == 0)
            {
                TempData[ERROR_MESSAGE] = "Vui lòng chọn khách hàng và nhân viên phụ trách";
                return RedirectToAction("Create");
            }

            int orderID = OrderDataService.InitOrder(customerID, employeeID, DateTime.Now, shoppingCart);

            Session.Remove(SHOPPING_CART); //Xóa giỏ hàng 

            return RedirectToAction($"Details/{orderID}");
        }
    }
}