using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class ColorViewController : Controller
    {
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Color
        public ActionResult Index()
        {
            return View(database.Colors.ToList());
        }
        public ActionResult Create()
        {
            Color color = new Color();
            return View(color);
        }
        [HttpPost]
        public ActionResult Create(Color color)
        {
            if (ModelState.IsValid)
            {
                database.Colors.Add(color);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Colors.Where(s => s.ColorID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Color pro)
        {
            try
            {
                pro = database.Colors.Where((s) => s.ColorID == id).FirstOrDefault();
                database.Colors.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult Edit(int id)
        {
            return View(database.Colors.Where(s => s.ColorID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Color pro)
        {
            database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index", "ColorView");
        }
    }
}