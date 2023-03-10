using _20T1080068.BusinessLayers;
using _20T1080068.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20T1080068.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private const int PAGE_SIZE = 4;
        private const string SESSION_CONDITION = "EmployeeCondition";
        private const string STORAGE_UPLOAD_FILE_EMPLOYEE = "Public/Images/Employees";
        /// <summary>
        /// Dữ liệu đầu vào
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý nhân viên";
            Models.PaginationSearchInput condition = new Models.PaginationSearchInput()
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = ""
            };
            if (Session[SESSION_CONDITION] != null)
            {
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
            var data = CommonDataService.ListOfEmployees(condition.Page, condition.PageSize, condition.SearchValue, out rowCount);
            Models.EmployeesSearchOutput result = new Models.EmployeesSearchOutput()
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
        /// Tạo nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            var data = new Employees()
            {
                EmployeeID = 0
            };
            return View("Edit", data);
        }
        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật nhân viên";
            if (id <= 0)
                return RedirectToAction("Index");
            var data = CommonDataService.GetEmployee(id);
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
        public ActionResult Save(Employees data, string Birthday, HttpPostedFileBase UploadPhoto)
        {
            // kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrWhiteSpace(data.FirstName))
            {
                ModelState.AddModelError(nameof(data.FirstName), "Họ và tên đệm không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.LastName))
            {
                ModelState.AddModelError(nameof(data.LastName), "Tên không được để trống");
            }

            DateTime? d = Converter.DYMStringToDateTime(Birthday);
            if (d == null)
            {
                ModelState.AddModelError(nameof(data.BirthDate), $"Định dạng ngày sinh {Birthday} không hợp lệ");
            }
            else if (d.Value < SqlDateTime.MinValue.Value || d.Value > SqlDateTime.MaxValue.Value)
            {
                ModelState.AddModelError(nameof(data.BirthDate), $"Ngày sinh phải từ {SqlDateTime.MinValue.Value:dd/MM/yyyy} đến {SqlDateTime.MaxValue.Value:dd/MM/yyyy}");
            }
            else
            {
                data.BirthDate = d.Value;
            } 
            
            if (UploadPhoto != null)
            {

                string Path = Server.MapPath($"~/Public/Images/Employees");
                string fileName = $"{DateTime.Now.Ticks}_{UploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(Path, fileName);
                UploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
            }
            // kiểm tra xem thử email có bị trùng hay không?
            var employees = CommonDataService.ListOfEmployees(data.Email);
            if (employees.Count > 0 && data.EmployeeID == 0)
                ModelState.AddModelError(nameof(data.Email), "Vui lòng sử dụng email khác");
            else
            {
                data.Email = data.Email;
            }
            data.Notes = data.Notes ?? "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Edit", data);
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Xoá nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Xoá nhân viên";
            if (id <= 0)
                return RedirectToAction("Index");
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}