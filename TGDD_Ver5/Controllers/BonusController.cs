using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class BonusController : Controller
    {
        // GET: Bonus
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Product
        public ActionResult Index()
        {
            return View(database.Bonus.ToList());
        }
        public ActionResult Create()
        {
            Bonu pro = new Bonu();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create(Bonu pro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (pro.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                        string extent = Path.GetExtension(pro.UploadImage.FileName);
                        filename = filename + extent;
                        pro.Img_Bonus = "~/Content/images/" + filename;
                        pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    database.Bonus.Add(pro);
                    database.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorCreate = "ErrorCreate";
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View(database.Bonus.Where(s => s.BonusID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Bonu pro)
        {
            try
            {
                pro = database.Bonus.Where((s) => s.BonusID == id).FirstOrDefault();
                database.Bonus.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error");
            }
        }
    }
}