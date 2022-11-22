using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLKhachHangController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLKhachHang
        public ActionResult Index()
        {
            var khachhang = db.KhachHangs.Select(n => n);
            return View(khachhang);
        }
        [HttpDelete]
        public ActionResult XoaTK(int id)
        {
            var taikhoan = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(taikhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}