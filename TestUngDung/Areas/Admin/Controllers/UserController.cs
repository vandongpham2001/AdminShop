using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var user = new UserDao();
            var model = user.ListALLPaging(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var user = new UserDao();
            var model = user.ListWhereALL(searchString, page = 1, pagesize = 5);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblUserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.Find(user.Username) != null)
                {
                    SetAlert("username đã tồn tại. Mời nhập tên khác", "warning");
                    return RedirectToAction("Index", "User");
                }
                var pass = Encryptor.EncryptMD5(user.Password);
                user.Password = pass;
                var result = dao.Insert(user);
                if (!String.IsNullOrEmpty(result))
                {
                    SetAlert("Tạo mới user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Tạo mới user không thành công", "error");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(String id)
        {
            var dao = new UserDao();
            var model = dao.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(tblUserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (!String.IsNullOrEmpty(user.Password))
                {
                    var pass = Encryptor.EncryptMD5(user.Password);
                    user.Password = pass;
                }
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Cập nhật user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Cập nhật user không thành công", "error");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(String id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}