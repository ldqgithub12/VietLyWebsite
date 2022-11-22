using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLSachController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLSach
        public ActionResult Index()
        {
            var sach = db.Saches.Select(n => n);
            return View(sach);
        }
        public ActionResult ThemSach()
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM");
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL");
            return View();
        }

        // POST: Admin/SACHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemSach([Bind(Include = "MaSach,TenSach,SoLuong,Anh,MoTa,Gia,TacGia,MaTL,MaDM")] Sach sACH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sACH.Anh = "";
                    var f = Request.Files["Anhtg"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/Images/" + FileName);
                        f.SaveAs(UploadPath);
                        sACH.Anh = FileName;
                    }
                    db.Saches.Add(sACH);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sACH.MaDM);
                ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sACH.MaTL);
                return View(sACH);
            }
        }

        public ActionResult SuaSach(int id)
        {
            Sach sACH = db.Saches.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sACH.MaDM);
            ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sACH.MaTL);
            return View(sACH);
        }

        // POST: Admin/SACHes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSach([Bind(Include = "MaSach,TenSach,SoLuong,Anh,MoTa,Gia,TacGia,MaTL,MaDM")] Sach sACH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["Anhtg"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/Images/" + FileName);
                        f.SaveAs(UploadPath);
                        sACH.Anh = FileName;
                    }
                    db.Entry(sACH).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu " + ex.Message;
                ViewBag.MaDM = new SelectList(db.DanhMucs, "MaDM", "TenDM", sACH.MaDM);
                ViewBag.MaTL = new SelectList(db.TheLoais, "MaTL", "TenTL", sACH.MaTL);
                return View(sACH);
            }
        }

        [HttpDelete]
        public ActionResult XoaSach(int id)
        {
            var sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}