using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class CartController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Cart
        public ActionResult Index()
        {
            var user = Session["Phone"] as Customer;
            if (user == null)
            {
                return RedirectToAction("Index","Customer");
            }
            else
            {
                Cart cart = database.Carts.Where(s => s.Phone_Cus == user.Phone).FirstOrDefault();
                if (cart == null) {
                    database.SaveChanges();
                    return PartialView("CartNull");
                }
                else
                {
                    CartDetail check_cart = database.CartDetails.Where(s => s.ID_Cart == cart.ID).FirstOrDefault();
                    if (check_cart == null)
                    {
                        database.SaveChanges();
                        return PartialView("CartNull");

                    }
                    else
                    {
                        return View(cart);
                    }
                }

            }
        }
        public ActionResult AddToCart(CartDetail cart_detail, int id)
        {
            Customer user = Session["Phone"] as Customer;
            if (user != null)
            {
                //Check xem người dùng có giỏ hàng chưa
                Cart cart_check = database.Carts.FirstOrDefault(s => s.Phone_Cus == user.Phone);

                if (cart_check == null)
                {
                    // Tạo giỏ hàng mới nếu chưa có
                    cart_check = new Cart
                    {
                        Phone_Cus = user.Phone,
                    };
                    database.Carts.Add(cart_check);
                }
                
                //Check sản phẩm đã tồn tại trong giỏ hàng chưa
                CartDetail existingDetail = database.CartDetails.FirstOrDefault(s => s.ID_Pro == id && s.ID_Cart == cart_check.ID);
                ProDetail pro = database.ProDetails.FirstOrDefault(s => s.ID == id);
                int ViewQuantity = pro.ViewQuantity ?? 0;
                
                if (ViewQuantity >= 0)
                {
                    if (existingDetail == null)
                    {
                        cart_detail.Quantity = 1;
                        cart_detail.Price = pro.Price;
                        cart_detail.Total = cart_detail.Price * cart_detail.Quantity;
                        cart_detail.ID_Pro = id;
                        cart_detail.ID_Cart = cart_check.ID;
                        database.CartDetails.Add(cart_detail);
                    }
                    else
                    {
                        int Quantity = existingDetail.Quantity ?? 0;
                        if (ViewQuantity > Quantity)
                        {
                            existingDetail.Quantity++;
                            existingDetail.Total = existingDetail.Price * existingDetail.Quantity;
                        }
                        else
                        {
                            TempData["Message_Quantity"] = $"Kho hàng đã hết";
                        }
                    }
                    database.SaveChanges();
                    //Total Value Cart
                    Total_Value_Cart();
                    RemoveVoucher();
                }
                
                

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Customer");
            }
        }
        public void Total_Value_Cart()
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.FirstOrDefault(s => s.Phone_Cus == user.Phone);
            int total_price = 0;
            foreach (var detail in cart.CartDetails)
            {
                int detailTotal = detail.Total ?? 0;
                total_price += detailTotal;
            }
            cart.Total_Price = total_price;

            database.SaveChanges();
        }
        public ActionResult RemovePro(int id)
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.FirstOrDefault(s => s.Phone_Cus == user.Phone);
            try
            {
                CartDetail pro = database.CartDetails.Where(s => s.ID_Pro == id && s.ID_Cart == cart.ID).FirstOrDefault();
                if(pro != null)
                {
                    database.CartDetails.Remove(pro);
                    database.SaveChanges();
                }
                Total_Value_Cart();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult DecreasePro(int id)
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.FirstOrDefault(s => s.Phone_Cus == user.Phone);
            try
            {
                CartDetail pro = database.CartDetails.Where(s => s.ID_Pro == id && s.ID_Cart == cart.ID).FirstOrDefault();
                if (pro != null)
                {
                    if (pro.Quantity > 1)
                    {
                        pro.Quantity -= 1;
                        cart.Total_Temp -= 1;
                        pro.Total = pro.Quantity * pro.Price;
                    }
                   
                    /*Giảm số lượng sp trong giỏ*/
                   
                }
                database.SaveChanges();
                Total_Value_Cart();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult Voucher()
        {
            return PartialView("Voucher");
        }
        [HttpPost]
        public ActionResult ApplyVoucher(string code)
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.Where(s => s.Phone_Cus == user.Phone).FirstOrDefault();
            Voucher voucher = database.Vouchers.Where(s => s.NameVoucher == code).FirstOrDefault();
            if (voucher != null && voucher.MinTotal >0)
            {
                if(cart.Total_Price <= voucher.MinPro)
                {
                    TempData["Messenger"] = $"Đơn hàng phải lớn hơn hoặc bằng {@String.Format("{0:#,0}", voucher.MinPro)}";
                    return RedirectToAction("Index");
                }
                else
                {
                    double discount = voucher.Discount ?? 0;
                    double total = cart.Total_Price ?? 0;
                    total -= (total * discount / 100);
                    cart.Total_Price = (int)total;
                    cart.VoucherID = voucher.ID;
                    database.SaveChanges();
                    TempData["Messenger_Succes"] = $"Bạn đã áp dụng thành công mã: {voucher.NameVoucher} giảm ngay {voucher.Discount}%";
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                TempData["Messenger"] = "Mã giảm giá không tồn tại hoặc đã hết";
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult RemoveVoucher()
        {
            Customer user = Session["Phone"] as Customer;
            Cart cart = database.Carts.Where(s => s.Phone_Cus == user.Phone).FirstOrDefault();
            cart.VoucherID = null;
            Total_Value_Cart();
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Cart_Number()
        {
            Customer user = Session["Phone"] as Customer;
            if(user == null)
            {
                return Content(" ");
            }
            else
            {
                Cart cart = database.Carts.Where(s => s.Phone_Cus == user.Phone).FirstOrDefault();

                var cart_detail = database.CartDetails.Where(s => s.ID_Cart == cart.ID).ToList();
                int totalQuantity = 0;
                foreach (var item in cart_detail)
                {
                    int cart_number = item.Quantity ?? 0;
                    totalQuantity += cart_number;
                }
                if (cart.Total_Temp != totalQuantity)
                {
                    cart.Total_Temp = totalQuantity;
                    database.SaveChanges();
                }
                ViewBag.QuantityCart = totalQuantity;
                return PartialView("Cart_Number");
            }

        }
    }
}