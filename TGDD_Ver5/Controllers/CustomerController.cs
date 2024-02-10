using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Customer _user)
        {
            Customer check = database.Customers.Where(s => s.Phone == _user.Phone && s.PasswordUser == _user.PasswordUser).FirstOrDefault();

            if (check == null)
            {
                ViewBag.ErrorInfo = "Tài khoản hoặc mật khẩu sai!";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["Phone"] = check;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(Customer _user)
        {
            if (ModelState.IsValid)
            {
                
                Customer check_phone = database.Customers.Where(s => s.Phone == _user.Phone).FirstOrDefault();

                if (check_phone == null)
                {
                    if (_user.ConfirmPass != _user.PasswordUser)
                    {
                        ViewBag.ErrorRegister = "Vui lòng nhập lại mật khẩu";
                        return View();
                    }
                    else
                    {
                        database.Configuration.ValidateOnSaveEnabled = false;
                        database.Customers.Add(_user);
                        Cart cart = new Cart();
                        cart.Phone_Cus = _user.Phone;
                        database.Carts.Add(cart);
                        database.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    ViewBag.ErrorRegister = "Số điện thoại đã đăng ký";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogoutUser(Customer _user)
        {
            Session.Abandon();
            return RedirectToAction("Index", "Customer");
        }
        public ActionResult Account()
        {
            Customer user = Session["Phone"] as Customer;
            if (user == null)
            {
                return View("Index");
            }
            else
            {
                return View(user);
            }
            
        }
        public ActionResult EditProfile( Customer user)
        {
            database.Entry(user).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Account","Customer");
        }
        public ActionResult Order()
        {
            Customer cus = Session["Phone"] as Customer;
            if (cus == null)
            {
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                var list = database.OrderProes.Where(s => s.PhoneCus == cus.Phone).ToList();
                list.Reverse();
                return View(list);
            }
        }
        public ActionResult OrderDetails(int id)
        {
            Customer cus = Session["Phone"] as Customer;
            OrderDetail check = database.OrderDetails.Where(s => s.OrderPro.PhoneCus == cus.Phone && s.ID_Order == id).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("Error", "Home");
            }

            else {
                if (cus == null)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    return View(database.OrderDetails.Where(s => s.ID_Order == id).ToList());
                }
            }
            
        }
        public ActionResult Manager()
        {
            return View(database.Customers.ToList());
        }
        public ActionResult Details(int? phone)
        {
 
            return View(database.Customers.Where(s=>s.Phone == phone).FirstOrDefault());
        }
        public ActionResult SaveAddress(string city, string district, string ward, string address)
        {
            Customer cus = Session["Phone"] as Customer;
            AddressCu ad = new AddressCu
            {
                AddressCus =address +" "+ ward + " " +district +" "+city,
                PhoneCus = cus.Phone,
            };
            database.AddressCus.Add(ad);
            database.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}