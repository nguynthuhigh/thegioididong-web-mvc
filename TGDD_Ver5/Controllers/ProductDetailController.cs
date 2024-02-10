using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGDD_Ver5.Models;

namespace TGDD_Ver5.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        // GET: Product
        
        public ActionResult Index()
        {
            var list = database.ProDetails.Where(s => s.ViewQuantity > 0).ToList();
            list.Reverse();
            return View(list);
        }
        public ActionResult SearchAdmin(string query)
        {
            return View(database.ProDetails.Where(s => s.Product.NamePro.Contains(query) || s.Product.Category.NameCate.Contains(query)));
        }

        public ActionResult Create()
        {
            ProDetail pro = new ProDetail();
            ViewBag.ColorID = new SelectList(database.Colors, "ColorID", "ColorName");
            ViewBag.ProID = new SelectList(database.Products, "ID", "NamePro");
            ViewBag.BonusID = new SelectList(database.Bonus, "BonusID", "Bonus_Name");
            return View(pro);
        }
        [HttpPost]

        public ActionResult Create(ProDetail pro, HttpPostedFileBase[] files)
        {
            ViewBag.ColorID = new SelectList(database.Colors, "ColorID", "ColorName", pro.ColorID);
            ViewBag.ProID = new SelectList(database.Products, "ID", "NamePro", pro.ProID);
            ViewBag.BonusID = new SelectList(database.Bonus, "BonusID", "Bonus_Name", pro.BonusID);
            try
            {             
                pro.SoldQuantity = 0;
                double op = pro.Old_Price;
                double dc = pro.Discount ?? 0;
                double np = op - (op * (dc /100));
                pro.Price = (int)np;
                double point = np / 1000;
                pro.Point = (int)point;
                database.ProDetails.Add(pro);
                /* double point = np / 100;
                pro.Product.Chip = (int)point;*/
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

                        ListImage_2 list_images = new ListImage_2
                        {
                            ImagePro = savedFilePath,
                            ProID = pro.ID
                        };
                        database.ListImage_2.Add(list_images);
                        database.SaveChanges();
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorInput"] = "Giá trị không hợp lệ";
                return View("Create");
            }
        }
        public ActionResult Details(int id)
        {
            ProDetail pro = database.ProDetails.Where(s => s.ID == id).FirstOrDefault();
            if (pro != null)
            {
                return View(pro);
            }
            else return RedirectToAction("Error","Home");

        }
        public ActionResult Delete(int id)
        {
            return View(database.ProDetails.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, ProDetail pro)
        {
            var list = database.ListImage_2.Where(s => s.ProID == id).ToList();
            foreach (ListImage_2 image in list)
            {
                database.ListImage_2.Remove(image);
                    
            }
            database.SaveChanges();
            pro = database.ProDetails.Where((s) => s.ID == id).FirstOrDefault();
            database.ProDetails.Remove(pro);
            database.SaveChanges();
            return RedirectToAction("Index");
          
        }
        public ActionResult Edit(int id)
        {
            ProDetail product = database.ProDetails.Where(s => s.ID == id).FirstOrDefault();
            ViewBag.ColorID = new SelectList(database.Colors, "ColorID", "ColorName",product.ColorID);
            ViewBag.ProID = new SelectList(database.Products, "ID", "NamePro",product.ProID);
            ViewBag.BonusID = new SelectList(database.Bonus, "BonusID", "Bonus_Name",product.BonusID);
            return View(database.ProDetails.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(ProDetail pro, HttpPostedFileBase[] files)
        {
            try
            {
                if (files != null && files.Length > 0 && files[0] != null)
                {
                    var old_list = database.ListImage_2.Where(s => s.ProID == pro.ID).ToList();
                    foreach (ListImage_2 image in old_list)
                    {
                        database.ListImage_2.Remove(image);
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

                            ListImage_2 list_images = new ListImage_2
                            {
                                ImagePro = savedFilePath,
                                ProID = pro.ID
                            };
                            database.ListImage_2.Add(list_images);
                            database.SaveChanges();
                            ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        }
                    }
                }
                pro.Caculator_Price();
                ViewBag.ColorID = new SelectList(database.Colors, "ColorID", "ColorName", pro.ColorID);
                ViewBag.ProID = new SelectList(database.Products, "ID", "NamePro", pro.ProID);
                ViewBag.BonusID = new SelectList(database.Bonus, "BonusID", "Bonus_Name", pro.BonusID);
                database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index", "ProductDetail");
            }
            catch
            {
                TempData["ErrorInput"] = "Giá trị không hợp lệ";
                return RedirectToAction("Edit", "ProductDetail", new {id=pro.ID});
            }
        }


        public ActionResult Laptop_Detail(int id)
        {
            ProDetail pro = database.ProDetails.Where(s => s.ID == id).FirstOrDefault();
            if (pro == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(pro);
            }
            
        }
        public ActionResult ListProduct()
        {
            return View(database.ProDetails.Where(s => s.ViewQuantity == 0).ToList());
        }
    }
}