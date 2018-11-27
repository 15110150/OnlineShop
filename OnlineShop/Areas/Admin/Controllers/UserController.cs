using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            
            var dao = new UserDao();
            var model = dao.List(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var user = new UserDao().ViewDetail(ID);
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var emcryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = emcryptedMd5Pas;

                long id = dao.Insert(user);
                if(id>0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tài khoản thất bại");
                }
            }
            return View("Create");

        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if(!string.IsNullOrEmpty(user.Password))
                {
                    var emcryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = emcryptedMd5Pas;
                }

                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tài khoản thất bại");
                }
            }
            return View("Edit");

        }
        public ActionResult Delete(int id)
        {
            var dao = new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}