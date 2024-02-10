using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class OrderController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Order
        public ActionResult Index()
        {
            AdminUser admin = Session["ID_Admin"] as AdminUser;
            if(admin != null)
            {
                var list = database.OrderProes.ToList();
                list.Reverse();
                int revenue = 0;
                foreach (var item in list)
                {
                    if (item.Status_Order == null)
                    { 
                        int price = item.Total_Price ?? 0;
                        revenue += price;
                    }
                }
                return View(list);
            }
            return RedirectToAction("Index", "AdminUser");
        }
        public ActionResult Search(bool? query)
        {
            var list = database.OrderProes.Where(s=>s.Status_Order == query).ToList();
            list.Reverse();

            return View(list);
        }
        public ActionResult Details(int id)
        {
            return View(database.OrderDetails.Where(s=>s.ID_Order == id).ToList());
        }
        public ActionResult Details_Popup(int id)
        {
            return View(database.OrderDetails.Where(s => s.ID_Order == id).ToList());
        }
        public ActionResult AddToOrder_Test(int[] ids)
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.Where(s => s.Phone_Cus == user.Phone).FirstOrDefault();
            if (user != null && ids != null && ids.Length > 0)
            {
                OrderPro order = new OrderPro
                {
                    PhoneCus = user.Phone,
                    ID_Cart = cart.ID,
                    AddressCus = Request.Form["AddressCus"],
                };
                foreach (int id in ids)
                {
                    CartDetail cart_detail = database.CartDetails.FirstOrDefault(s => s.ID_Pro == id && s.Cart.Phone_Cus == user.Phone);
                    if (cart_detail != null)
                    {
                        OrderDetail order_detail = new OrderDetail
                        {

                            ID_Order = order.ID,
                            ID_Pro = id,
                            Total = cart_detail.Total,
                            Quantity = cart_detail.Quantity,
                            Price = cart_detail.Total,

                        };
                        database.OrderDetails.Add(order_detail);
                        database.CartDetails.Remove(cart_detail);
                    }
                }
                order.ID_Voucher = cart.VoucherID;
                order.Total_Price = cart.Total_Price;

                order.Date_Order = DateTime.Now;
                database.OrderProes.Add(order);
                database.SaveChanges();
                return RedirectToAction("Index","Payment", new { id = order.ID });

            }
            return RedirectToAction("Index", "Customer");

        }

    }
}