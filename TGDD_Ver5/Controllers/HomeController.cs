using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class HomeController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult MostSearch()
        {
            return View(database.MostSearcheds.Where(s => s.Point > 5).ToList());
        }
        public ActionResult Search(string query)
        {
            MostSearched search = database.MostSearcheds.Where(s => s.Keyword == query).FirstOrDefault();
            if (search == null)
            {
                MostSearched new_key = new MostSearched
                {
                    Point = 1,
                    Keyword = query,
                };
                database.MostSearcheds.Add(new_key);
            }
            else
            {
                int oldPoint = search.Point ?? 0;
                oldPoint++;
                search.Point = oldPoint;
            }
            database.SaveChanges();
            return View(database.ProDetails.Where(s => s.Product.NamePro.Contains(query) || s.Product.Category.NameCate.Contains(query)).ToList());
        }
        public ActionResult MacbookWeek()
        {
             return View(database.ProDetails
            .Where(s => s.Product.TradeMark.NameTM.Contains("Macbook") && s.ViewQuantity > 0)
            .OrderByDescending(s => s.SoldQuantity)
            .Take(5)
            .ToList());
        }
        public ActionResult FlashSale()
        {
            return View(database.ProDetails
           .Where(s => s.Discount > 20)
           .ToList());
        }
        public ActionResult SaleLaptopGaming()
        {
            return View(database.ProDetails
           .Where(s=>s.Discount>5 && s.Product.Category.NameCate.Contains("Gaming"))
           .ToList());
        }
        public ActionResult TopSeller()
        {
            var details = database.ProDetails
            .Where(s=> s.ViewQuantity > 0)
            .OrderByDescending(s => s.SoldQuantity) 
            .Take(10);
            return View(details);
        }
        public ActionResult RecommendToday()
        {
            var details = database.ProDetails
            .Where(s=>s.ViewQuantity>5)
            .Take(25);
            return View(details);
        }
        public ActionResult AVA()
        {
            return PartialView("AVA");
        }
    }
}