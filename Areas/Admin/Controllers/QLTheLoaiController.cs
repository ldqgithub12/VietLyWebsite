using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLTheLoaiController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLTheLoai
        public ActionResult Index()
        {
            var theloai = db.TheLoais.Select(n => n);
            return View(theloai);
        }
        [HttpGet]
        public ActionResult ThemTL()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemTL([Bind(Include = "MaTL,TenTL")] TheLoai theLoai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TheLoais.Add(theLoai);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(theLoai);
            }
        }
        [HttpGet]
        public ActionResult SuaTL(string id)
        {
            var theLoai = db.TheLoais.Find(id);
            return View(theLoai);
        }
        [HttpPost]
        public ActionResult SuaTL([Bind(Include = "MaTL,TenTL")] TheLoai theLoai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(theLoai).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(theLoai);
            }
        }
        [HttpDelete]
        public ActionResult XoaTL(string id)
        {
            var theLoai = db.TheLoais.Find(id);
            db.TheLoais.Remove(theLoai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}