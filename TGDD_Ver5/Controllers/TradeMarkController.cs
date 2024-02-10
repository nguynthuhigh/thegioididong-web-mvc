using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class TradeMarkController : Controller
    {
        // GET: TradeMark
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: TradeMark
        public ActionResult Index()
        {

            return View(database.TradeMarks.ToList());
        }
        public ActionResult Create()
        {
            TradeMark tm = new TradeMark();
            return View(tm);
        }
        [HttpPost]
        public ActionResult Create(TradeMark tm)
        {
            try
            {
                if (tm.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(tm.UploadImage.FileName);
                    string extent = Path.GetExtension(tm.UploadImage.FileName);
                    filename = filename + extent;
                    tm.Logo = "~/Content/images/" + filename;
                    tm.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.TradeMarks.Add(tm);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
        public ActionResult Details(int id)
        {
            return View(database.TradeMarks.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Delete(int id)
        {
            return View(database.TradeMarks.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, TradeMark tm)
        {
            try
            {
                tm = database.TradeMarks.Where((s) => s.ID == id).FirstOrDefault();
                database.TradeMarks.Remove(tm);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["DeleteTM"] = "Không thể xóa nhãn hiệu, phải xóa sản phẩm của thương hiệu này trước";
                return RedirectToAction("Delete", "TradeMark", new {id = tm.ID}) ;
            }
        }
        public ActionResult Edit(int id)
        {
            return View(database.TradeMarks.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(TradeMark tm, HttpPostedFileBase imageFile, string existingImage)
        {
            string imagePath = existingImage; 

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                imageFile.SaveAs(path);
                imagePath = "~/Content/images/" + fileName; 
            }
            tm.Logo = imagePath;
            database.Entry(tm).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}