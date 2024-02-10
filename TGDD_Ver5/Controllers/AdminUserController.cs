using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: AdminUser
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminUser _user)
        {
            var check = database.AdminUsers.Where(s => s.NameUser == _user.NameUser && s.PasswordUser == _user.PasswordUser).FirstOrDefault();

            if (check == null)
            {
                ViewBag.ErrorInfo = "Tài khoản hoặc mật khẩu sai!";
                return View("index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["ID_Admin"] = check;
                Session["PasswordUser"] = _user.PasswordUser;
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Delete(string name)
        {
            AdminUser admin = database.AdminUsers.Where(s => s.NameUser == name).FirstOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Delete(string name, AdminUser admin)
        {
            admin = database.AdminUsers.Where(s=>s.NameUser == name).FirstOrDefault();
            database.AdminUsers.Remove(admin);
            database.SaveChanges();
            return View("Manager");
        }
        public ActionResult Edit(string name)
        {
            AdminUser admin = database.AdminUsers.Where(s => s.NameUser == name).FirstOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(string name, AdminUser admin)
        {
            admin = database.AdminUsers.Where(s => s.NameUser == name).FirstOrDefault();
            database.Entry(admin).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return View("Manager");
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(AdminUser _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.AdminUsers.Where(s => s.NameUser == _user.NameUser).FirstOrDefault();

                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.AdminUsers.Add(_user);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "NameUser đã tồn tại";
                    return View();
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult Manager()
        {
            return View(database.AdminUsers.ToList());
        }
    }
}