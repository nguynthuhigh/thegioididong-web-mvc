using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class LaptopController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Laptop
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult TM()
        {
            return View(database.TradeMarks.ToList());
        }
        public ActionResult TradeMark(string tm)
        {
            return View(database.ProDetails.Where(s => s.Product.TradeMark.NameTM.Contains(tm)).ToList());
        }
        public ActionResult FlashSale_Laptop()
        {
            return View(database.ProDetails
                .Where(s=>s.Discount>=30 && s.ViewQuantity>0)
                .ToList());
        }
        public ActionResult Macbook()
        {
            return View(database.ProDetails.Where(s => s.Product.TradeMark.NameTM
            .Contains("Macbook"))
                .ToList()
                .Take(10));
        }
        public ActionResult Gaming()
        {
            return View(database.ProDetails.Where(s => s.Product.Category.NameCate
            .Contains("Gaming") || s.Product.NamePro.Contains("Gaming"))
                .ToList()
                .Take(10));
        }
        public ActionResult Office()
        {
            return View(database.ProDetails.Where(s => s.Product.Category.NameCate
            .Contains("Học tập") || s.Product.TradeMark.NameTM.Contains("HP"))
                .ToList()
                .Take(10));
        }
        public ActionResult Graphic()
        {
            var list = database.ProDetails.Where(s => s.Product.Graphics_Card.Contains("RTX") || s.CPU.Contains("M3")|| s.HardDrive.Contains("1 TB"));
            return View(list.Take(10));
        }
        public ActionResult Thin()
        {
            var list = database.ProDetails.ToList();
            var off_list = new List<ProDetail>();
            foreach(var item in list)
            {
                string input = item.Product.Mass;

                // Loại bỏ các ký tự không phải số
                string numericPart = new string(input.Where(c => char.IsDigit(c) || char.IsPunctuation(c)).ToArray());

                // Chuyển đổi sang kiểu double
                if (double.TryParse(numericPart, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
                {
                    if (result <= 1.5)
                    {
                        off_list.Add(item);
                    }
                }
            }
            return View(off_list.Take(10));
        }
        public ActionResult Luxury()
        {
            return View(database.ProDetails.Where(s => s.Product.Category.NameCate
            .Contains("Sang"))
                .ToList()
                .Take(10));
        }
        public ActionResult Cate(string cate)
        {
            if (cate == "gaming")
            {
                return View(database.ProDetails.Where(s => s.Product.Category.NameCate.Contains("Gaming") || s.Product.NamePro.Contains("Gaming")).ToList());
            }
            else if(cate == "do-hoa-ky-thuat")
            {
                var list = database.ProDetails.Where(s => s.Product.Graphics_Card.Contains("RTX") || s.CPU.Contains("M3") || s.HardDrive.Contains("1 TB"));
                return View(list);
            }
            else if(cate == "van-phong-hoc-tap")
            {
                return View(database.ProDetails.Where(s => s.Product.Category.NameCate
            .Contains("Học tập") || s.Product.TradeMark.NameTM.Contains("HP"))
                .ToList());
            }
            else if(cate == "mong-nhe")
            {
                var list = database.ProDetails.ToList();
                var off_list = new List<ProDetail>();
                foreach (var item in list)
                {
                    string input = item.Product.Mass;

                    // Loại bỏ các ký tự không phải số
                    string numericPart = new string(input.Where(c => char.IsDigit(c) || char.IsPunctuation(c)).ToArray());

                    // Chuyển đổi sang kiểu double
                    if (double.TryParse(numericPart, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
                    {
                        if (result <= 1.5)
                        {
                            off_list.Add(item);
                        }
                    }
                }
                return View(off_list);
            }
            else if(cate == "cao-cap-sang-trong")
            {
                return View(database.ProDetails.Where(s => s.Product.Category.NameCate
                .Contains("Sang"))
                .ToList());
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Partial_Filter()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Filter(string tm,int min, int max)
        {
            string[] trademarks = tm.Split(' ');
            List<ProDetail> result = new List<ProDetail>();
            if (min != 0 && max != 500000000 && tm != "undefined")
            {
                foreach (var trademark in trademarks)
                {
                    var proDetails = database.ProDetails.Where(s => s.Product.TradeMark.NameTM.Contains(trademark) && (s.Price > min && s.Price < max)).ToList();
                    result.AddRange(proDetails);
                }
            }
            else if (tm == "undefined")
            {
                var proDetails = database.ProDetails.Where(s => s.Price > min && s.Price < max).ToList();
                result.AddRange(proDetails);
            }
            else
            {
                foreach (var trademark in trademarks)
                {
                    var proDetails = database.ProDetails.Where(s => s.Product.TradeMark.NameTM.Contains(trademark)).ToList();
                    result.AddRange(proDetails);
                }
            }
            return View(result);
        }
    }
}