using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        public ActionResult Index()
        {
            return View(database.Categories.ToList());
        }
        [HttpPost]
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                return View(database.Categories.ToList());
            }
            else return View(database.Categories.Where(s => s.NameCate.Contains(search)).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                database.Categories.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorCreate = "The category already exists";
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Categories.Where(s => s.IDCate == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Categories.Where(s => s.IDCate == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Categories.Where(s => s.IDCate == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = database.Categories.Where((s) => s.IDCate == id).FirstOrDefault();
                database.Categories.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorDelete"] = "Không thể xóa loại hàng này vì đang đăng bán";
                return View();
            }
        }
    }
}