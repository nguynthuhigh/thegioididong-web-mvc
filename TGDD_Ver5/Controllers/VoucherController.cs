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
    public class VoucherController : Controller
    {
        // GET: Voucher
        TGDD_Ver6Entities database = new TGDD_Ver6Entities();
        public ActionResult Index()
        {
            return View(database.Vouchers.ToList());
        }
        public ActionResult Create()
        {
            Voucher vc = new Voucher();
            return View(vc);
        }
        [HttpPost]
        public ActionResult Create(Voucher vc)
        {   
            database.Vouchers.Add(vc);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            return View(database.Vouchers.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Voucher pro)
        {
            try
            {
                pro = database.Vouchers.Where((s) => s.ID == id).FirstOrDefault();
                database.Vouchers.Remove(pro);
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
            return View(database.Vouchers.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Voucher cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}