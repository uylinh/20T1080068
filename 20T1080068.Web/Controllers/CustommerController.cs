using _20T1080068.BusinessLayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web.Controllers
{
    [Authorize]
    public class CustommerController : Controller
    {
        private const int PAGE_SIZE = 10;
        private const string SESSION_CONDITION = "CustommerCondition";
        /// <summary>
        ///  Trang nhận đầu vào dữ liệu
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý khách hàng";
            Models.PaginationSearchInput condition = new Models.PaginationSearchInput()
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = ""
            };
            if (Session[SESSION_CONDITION] != null)
            {
                // lỗi runtime nếu chạy mà bị lỗi
                //condition = (Models.PaginationSearchInput) Session[SESSION_CONDITION];
                // NÊN DÙNG khi lỗi không trả về lỗi mà trả về giá trị là null
                condition = Session[SESSION_CONDITION] as Models.PaginationSearchInput;
            }
            return View(condition);
        }
        /// <summary>
        /// Trang hiển thị dữ liệu đầu ra
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            Models.CustomerSearchOutput result = new Models.CustomerSearchOutput()
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
        ///  Tạo khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            var data = new Custommer()
            {
                CustommerID = 0
            };
            return View("Edit", data);
        }

        /// <summary>
        ///  Cập nhật khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật khách hàng";
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Custommer data)
        {
            if (string.IsNullOrWhiteSpace(data.CustommerName))
            {
                ModelState.AddModelError(nameof(data.CustommerName), "Tên khách hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.ContactName))
            {
                ModelState.AddModelError(nameof(data.ContactName), "Tên liên hệ không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Country))
            {
                ModelState.AddModelError(nameof(data.Country), "Vui lòng chọn quốc gia");
            }
            data.Email = data.Email ?? "";
            data.City = data.City ?? "";
            data.Address = data.Address ?? "";
            data.PostalCode = data.PostalCode ?? "";
            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.CustommerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                return View("Edit", data);
            }
            if (data.CustommerID == 0)
            {
                CommonDataService.AddCustomer(data);
            }
            else
            {
                CommonDataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        ///  Xoá khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Xoá khách hàng";
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetCustomer(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}