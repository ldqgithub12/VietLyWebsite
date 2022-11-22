using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLDanhMucController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLDanhMuc
        public ActionResult Index()
        {
            var danhmuc = db.DanhMucs.Select(n=>n);
            return View(danhmuc);
        }
        [HttpGet]
        public ActionResult ThemDM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDM([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DanhMucs.Add(danhMuc);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(danhMuc);
            }
        }
        [HttpGet]
        public ActionResult SuaDM(string id)
        {
            var danhMuc = db.DanhMucs.Find(id);
            return View(danhMuc);
        }
        [HttpPost]
        public ActionResult SuaDM([Bind(Include = "MaDM,TenDM")] DanhMuc danhMuc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(danhMuc).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                return View(danhMuc);
            }
        }
        [HttpDelete]
        public ActionResult XoaDM(string id)
        {
            var danhMuc = db.DanhMucs.Find(id);
            db.DanhMucs.Remove(danhMuc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}