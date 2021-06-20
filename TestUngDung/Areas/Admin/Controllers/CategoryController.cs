using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new CategoryDao();
            var model = dao.ListALLPaging(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var category = new CategoryDao();
            var model = category.ListWhereALL(searchString, page = 1, pagesize = 5);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(String id)
        {
            var dao = new CategoryDao();
            var model = dao.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(tblCategory category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                if (dao.Find(category.Id) != null)
                {
                    SetAlert("Mã danh mục đã tồn tại. Mời nhập mã khác", "warning");
                    return RedirectToAction("Create", "Category");

                }
                var result = dao.Insert(category);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Tạo mới danh mục thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Tạo mới danh mục không thành công", "error");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(tblCategory category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var result = dao.Update(category);
                if (result)
                {
                    SetAlert("Cập nhật danh mục thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Cập nhật danh mục không thành công", "error");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(String maDM)
        {
            new CategoryDao().Delete(maDM);
            return RedirectToAction("Index");
        }
    }
}