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
    public class CommentsController : Controller
    {
        private TGDD_Ver6Entities db = new TGDD_Ver6Entities();
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Customer).Include(c => c.Product);
            comments.Reverse();
            return View(comments.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        public ActionResult Create(int id, HttpPostedFileBase[] files, int rate)
        {
            Comment cmt = new Comment();
            Customer user = Session["Phone"] as Customer;
            ProDetail prodt = db.ProDetails.Where(s => s.ID == id).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Index","Customer");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    foreach (HttpPostedFileBase file in files)
                    {
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/List_Images"), InputFileName);

                            // Save file to server folder  
                            file.SaveAs(ServerSavePath);


                            string savedFilePath = "~/Content/List_Images/" + InputFileName;

                            ListImage list_image = new ListImage
                            {
                                ImagePro = savedFilePath,
                                CmtID = cmt.ID
                            };
                            db.ListImages.Add(list_image);
                        }

                    }
                    cmt.Content = Request.Form["Content"];
                    cmt.PhoneCus = user.Phone;
                    cmt.ID_Pro = prodt.Product.ID;
                    cmt.Rating = rate;
                    cmt.DateCMT = DateTime.Now;
                    db.Comments.Add(cmt);
                    db.SaveChanges();
                    decimal avg_rating = 0;
                    int i = 0;
                    foreach(var item in prodt.Product.Comments)
                    {
                        avg_rating += item.Rating??0;
                            i++;
                    }
                    avg_rating /= i;
                    prodt.Product.Rating = avg_rating;
                    if(rate < 1)
                    {
                        TempData["Rating"] = "Vui lòng chọn";
                    }
                   
                    
                    db.SaveChanges();
                    return RedirectToAction("Laptop_Detail", "ProductDetail", new {id=id});
                }
                return RedirectToAction("Index", "Home");
            }
           
           
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            var list = db.ListImages.Where(s => s.CmtID == id).ToList();
            foreach (ListImage image in list)
            {
                db.ListImages.Remove(image);

            }
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
