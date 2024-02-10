using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Product
        public ActionResult Index()
        {
            var list = database.Products.ToList();
            list.Reverse();
            return View(list);
        }
        public ActionResult Create()
        {
            Product pro = new Product();
            ViewBag.TradeMarkID = new SelectList(database.TradeMarks, "ID", "NameTM");
            ViewBag.CategoryID = new SelectList(database.Categories, "IDCate", "NameCate");
            return View(pro);
        }
        [HttpPost]
        public ActionResult Create( Product pro, HttpPostedFileBase[] files)
        {
            try {
                if (ModelState.IsValid)
                {
                    if (pro.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                        string extent = Path.GetExtension(pro.UploadImage.FileName);
                        filename = filename + extent;
                        pro.ImagePro = "~/Content/images/" + filename;
                        pro.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    database.Products.Add(pro);
                    database.SaveChanges();
                    foreach (HttpPostedFileBase file in files)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);
                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/List_Images"), InputFileName);

                            // Save file to server folder  
                            file.SaveAs(ServerSavePath);


                            string savedFilePath = "~/Content/List_Images/" + InputFileName;

                            ListImage list_images = new ListImage
                            {
                                ImagePro = savedFilePath,
                                ProID = pro.ID
                            };
                            database.ListImages.Add(list_images);
                            database.SaveChanges();
                        }

                    }
                }
                ViewBag.TradeMarkID = new SelectList(database.TradeMarks, "ID", "NameTM", pro.TradeMarkID);
                ViewBag.CategoryID = new SelectList(database.Categories, "IDCate", "NameCate", pro.CategoryID);
                return RedirectToAction("Index");
            }
            catch { 
                ViewBag.ErrorCreate = "ErrorCreate";
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            try
            {
                var list = database.ListImages.Where(s => s.ProID == id).ToList();
                foreach (ListImage image in list)
                {
                    database.ListImages.Remove(image);
                }
                pro = database.Products.Where((s) => s.ID == id).FirstOrDefault();
                database.Products.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorDelete"] = "Sản phẩm đang đăng bán không thể xóa";
                return RedirectToAction("Delete","Product", new {id =pro.ID});
            }
        }
        public ActionResult Edit(int id)
        {
            Product pro = database.Products.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.TradeMarkID = new SelectList(database.TradeMarks, "ID", "NameTM", pro.TradeMarkID);
            ViewBag.CategoryID = new SelectList(database.Categories, "IDCate", "NameCate", pro.CategoryID);
            return View(database.Products.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Product pro, HttpPostedFileBase[] files, HttpPostedFileBase imageFile, string existingImage)
        {
            if (files != null && files.Length > 0 && files[0] != null)
            {
                var old_list = database.ListImages.Where(s => s.ProID == pro.ID).ToList();
                foreach (ListImage image in old_list)
                {
                    database.ListImages.Remove(image);
                }
                database.SaveChanges();
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/List_Images"), InputFileName);

                        // Save file to server folder  
                        file.SaveAs(ServerSavePath);


                        string savedFilePath = "~/Content/List_Images/" + InputFileName;

                        ListImage list_images = new ListImage
                        {
                            ImagePro = savedFilePath,
                            ProID = pro.ID
                        };
                        database.ListImages.Add(list_images);
                        database.SaveChanges();
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }
                }
            }
            string imagePath = existingImage;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                imageFile.SaveAs(path);
                imagePath = "~/Content/images/" + fileName;
            }
            pro.ImagePro = imagePath;
            database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            ViewBag.TradeMarkID = new SelectList(database.TradeMarks, "ID", "NameTM");
            ViewBag.CategoryID = new SelectList(database.Categories, "IDCate", "NameCate");
            return RedirectToAction("Index","Product");
        }
    }
}