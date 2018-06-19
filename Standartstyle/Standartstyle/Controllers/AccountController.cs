using Standartstyle.AppCode.BL;
using Standartstyle.AppCode.DAL.Model;
using Standartstyle.AppCode.DAL.Repository;
using Standartstyle.AppCode.Util.Session;
using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Standartstyle.Controllers
{
    public class AccountController : Controller
    {
        private GeneralRepository repo;

        public AccountController()
        {
            repo = new GeneralRepository();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var session = UserSession.Create(model.Name, model.Password);
                    var messageForLogger = string.Format("User login successful.");
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception e)
                {

                }
            }
            ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
            return View(model);
        }

        public ActionResult LogOut()
        {
            UserSession.Remove();
            return RedirectToAction("Login");
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}