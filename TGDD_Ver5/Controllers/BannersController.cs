using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class BannersController : Controller
    {
        private TGDD_Ver6Entities db = new TGDD_Ver6Entities();

        // GET: Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }
        public ActionResult Create()
        {
            Banner tm = new Banner();
            return View(tm);
        }
        [HttpPost]
        public ActionResult Create(Banner tm)
        {
            try
            {
                if (tm.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(tm.UploadImage.FileName);
                    string extent = Path.GetExtension(tm.UploadImage.FileName);
                    filename = filename + extent;
                    tm.ImgBanner = "~/Content/images/" + filename;
                    tm.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                db.Banners.Add(tm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }

        // GET: Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NameBanner,ImgBanner,LinkBanner,Color_BG,ID")] Banner banner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Home_Big_Banner
        public ActionResult Home_Big_Banner()
        {
            Banner banner = db.Banners.Where(s=>s.NameBanner == "Home_Big_Banner").ToList().LastOrDefault();
            return View(banner);
        }
        public ActionResult Home_Carousel_1()
        {
            var banner = db.Banners.Where(s => s.NameBanner == "Home_Carousel_1").ToList();
            return View(banner);
        }
        public ActionResult Home_Carousel_2()
        {
            var banner = db.Banners.Where(s => s.NameBanner == "Home_Carousel_2").ToList();
            return View(banner);
        }
        public ActionResult Home_Carousel_3()
        {
            var banner = db.Banners.Where(s => s.NameBanner == "Home_Carousel_3").ToList();
            return View(banner);
        }
        public ActionResult Home_Carousel_Top()
        {
            var banner = db.Banners.Where(s => s.NameBanner == "Home_Carousel_Top").ToList();
            return View(banner);
        }
        public ActionResult StickySidebar()
        {
            var banners = db.Banners.Where(s => s.NameBanner == "StickySidebar").ToList();
            return View(banners);

        }
    }
}
