using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListALLPaging(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var product = new ProductDao();
            var model = product.ListWhereALL(searchString, page = 1, pagesize = 5);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public void SetViewBag(string selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryId = new SelectList(dao.ListAllCategoryId(), "Id", "Name", selectedId);
        }
        [HttpGet]
        public ActionResult Edit(String id)
        {
            var dao = new ProductDao();
            var model = dao.Find(id);
            SetViewBag();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(tblProduct product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (dao.Find(product.Id) != null)
                {
                    SetAlert("Mã sản phẩm đã tồn tại. Mời nhập mã khác", "warning");
                    return RedirectToAction("Create", "Product");

                }
                var result = dao.Insert(product);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Tạo mới sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Tạo mới sản phẩm không thành công", "error");
                }
            }
            SetViewBag();
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(tblProduct product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Cập nhật sản phẩm không thành công", "error");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(String maSP)
        {
            new ProductDao().Delete(maSP);
            return RedirectToAction("Index");
        }
        public ActionResult ViewDetail(string id)
        {
            var dao = new ProductDao();
            var model = dao.Find(id);
            return View(model);
        }
    }
}