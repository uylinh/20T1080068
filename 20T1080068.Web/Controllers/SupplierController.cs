using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Services.Protocols;
using _20T1080068.Web.Models;
using _20T1080068.BusinessLayers;
using _20T1080068.DomainModels;


namespace _20T1080068.Web.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 6;
        private const string SESSION_CONDITION = "SupplierCondition";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý nhà cung cấp";

            Models.PaginationSearchInput condition
                = Session[SESSION_CONDITION] as Models.PaginationSearchInput;

            if (condition == null)
            {
                condition = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition); // truyền dữ liệu bằng model
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput condition) // int Page , int PageSize , string SearchValue
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);

            Models.SupplierSearchOutput result = new Models.SupplierSearchOutput()
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
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var data = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View("Edit", data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetSupplier(id);
            if (data == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View(data);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Supplier data)
        {
            // kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.SupplierName))
                ModelState.AddModelError(nameof(data.SupplierName), "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(data.ContactName))
                ModelState.AddModelError(nameof(data.ContactName), "tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(data.Country))
                ModelState.AddModelError(nameof(data.Country), "vui lòng chọn quốc gia");

            data.Address = data.Address ?? "";
            data.Phone = data.Phone ?? "";
            data.City = data.City ?? "";
            data.PostalCode = data.PostalCode ?? "";

            if (ModelState.IsValid == false)
            {
                ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp " : "Cập nhật nhà cung cấp ";
                return View("Edit", data);
            }


            if (data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetSupplier(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Xóa nhà cung cấp";
            return View(data);
        }

    }

}