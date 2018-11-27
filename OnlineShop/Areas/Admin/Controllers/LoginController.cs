using Model.Dao;
using OnlineShop.Areas.Admin.Models;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;


namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
               
                var dao = new UserDao();
                var result = dao.Login(model.username, Encryptor.MD5Hash(model.password));
                if(result==1)
                {
                    var user = dao.GetById(model.username);
                    var UserSession = new UserLogin();
                    UserSession.UserName = user.Username;
                    UserSession.UserID = user.ID;
                    Session.Add(Commoncontants.USER_SESSION, UserSession);
                    return RedirectToAction("Index", "Home");
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("Index");
        }
    }
}