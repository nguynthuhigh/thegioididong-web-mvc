using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;
namespace TGDD_Ver5.Controllers
{
    public class PaymentController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Payment
        public ActionResult Index(int id)
        {
            Customer cus = Session["Phone"] as Customer;
            OrderPro order = database.OrderProes.Where(s => s.PhoneCus == cus.Phone && s.ID == id).FirstOrDefault();
            if (order == null)
            {
                return RedirectToAction("Error", "Home");
            }
           
            TempData["OrderId"] = id;
            return View(order);
        }
        [HttpPost]
        public ActionResult Index()
        {

            Customer cus = Session["Phone"] as Customer;
            if (cus == null)
            {
                return RedirectToAction("Index", "Customer");
            }
            int id;
            if (TempData["OrderId"] != null && int.TryParse(TempData["OrderId"].ToString(), out id))
            {
                
                OrderPro order = database.OrderProes.Where(s => s.PhoneCus == cus.Phone && s.ID == id).FirstOrDefault();
                if (order != null)
                {
                    return View(order);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Payment(int id, string PaymentMethod)
        {
            OrderPro orderPro = database.OrderProes.Where(s => s.ID == id).FirstOrDefault();
            int total_order = orderPro.Total_Price ?? 0;

            Payment pay = new Payment
            {
                PaymentDate = DateTime.UtcNow,
                Oder_ID = id,
                PaymentAmount = total_order,
                PaymentID = id,
                PaymentMethod = PaymentMethod,
            };

            orderPro.PaymentID = pay.PaymentID;
            database.Payments.Add(pay);
            database.SaveChanges();
            return RedirectToAction("Notification", "Payment");
        }
        public ActionResult Confirm(int id)
        {
            OrderPro orderPro = database.OrderProes.Where(s => s.ID == id).FirstOrDefault();
            Voucher voucher = database.Vouchers.Where(s => s.ID == orderPro.ID_Voucher).FirstOrDefault();
            
            foreach (var item in orderPro.OrderDetails)
            {

                int vc = voucher.MinTotal ?? 0;
                vc--;
                voucher.MinTotal = vc;

                int ViewQuantity = item.ProDetail.ViewQuantity ?? 0;
                int Quantity = item.Quantity ?? 0;
                ViewQuantity -= Quantity;
                item.ProDetail.ViewQuantity = ViewQuantity;
                int SoldQuantity = item.ProDetail.SoldQuantity ?? 0;
                SoldQuantity += Quantity;
                item.ProDetail.SoldQuantity = SoldQuantity;
            }
            orderPro.Status_Order = true;
            database.SaveChanges();
            return RedirectToAction("Index", "Order");
        }
        public ActionResult Remove(int id)
        {
            OrderPro orderPro = database.OrderProes.Where(s => s.ID == id).FirstOrDefault();
            orderPro.Status_Order = false;
            database.SaveChanges();
            return View(orderPro);
        }
        public ActionResult Notification()
        {
            return View();
        }

    }
}