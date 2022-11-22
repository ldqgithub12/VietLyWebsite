using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLNhanVienController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLNhanVien
        public ActionResult Index()
        {
            var nhanvien = db.NhanViens.Select(n => n);
            return View(nhanvien);
        }
        [HttpGet]
        public ActionResult ThemNV()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemNV([Bind(Include = "MaNV,MaQuyen,HoTen,DiaChi,Email,SoDT,MatKhau")] NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
                return View(nhanVien);
            }
        }
        [HttpGet]
        public ActionResult SuaNV(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View(nhanVien);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaNV([Bind(Include = "MaNV,MaQuyen,HoTen,DiaChi,Email,SoDT,MatKhau")] NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
                return View(nhanVien);
            }
        }

        [HttpDelete]
        public ActionResult XoaNV(int id)
        {
            var nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}