using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["idNV"] != null)
            {
                var thongke = db.ChiTietDonHangs.Where(n => n.DonHang.TrangThai == true && n.DonHang.DaThanhToan==true).Select(n => n);
                var nguoidk = db.KhachHangs.Select(n => n);
                var donhang = db.DonHangs.Where(n=>n.TrangThai == false);
                int sodh = donhang.Count();
                int tongnguoi = nguoidk.Count();
                int tongsp = Convert.ToInt32(thongke.Sum(n => n.SoLuong));
                decimal tongtien = Convert.ToDecimal(thongke.Sum(n => n.SoLuong * n.GiaBan));
                ViewBag.Tongsp = tongsp;
                ViewBag.Tongtien = tongtien.ToString("#,##");
                ViewBag.Tongnguoi = tongnguoi;
                ViewBag.Sodh = sodh;
                return View(thongke);
            }
            else
            {
                return RedirectToAction("DangNhap");
            }
        }
        public ActionResult Thongke()
        {
            var query = (from sach in db.Saches
                         let soluongban = 0
                         let tongSl = (from cthd in db.ChiTietDonHangs
                                       join hd in db.DonHangs on cthd.MaDonHang equals hd.MaDonHang
                                       where cthd.MaSach == sach.MaSach
                                       select cthd.SoLuong
                                       ).Sum()
                         where tongSl > 0
                         orderby tongSl descending
                         select new
                         {
                             sach.TenSach,
                             soluongban = tongSl
                         }
                        ).Take(10);

            List<int?> lstdoanhthu = new List<int?>();
            List<String> tensach = new List<string>();
            foreach (var item in query)
            {
                tensach.Add(item.TenSach);
                lstdoanhthu.Add(item.soluongban);
            }
            var month = tensach.ToList();
            var money = lstdoanhthu.ToList();
            ViewBag.Month = month.ToList();
            ViewBag.Money = lstdoanhthu.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string email, string matKhau)
        {
            if (ModelState.IsValid)
            {
                var user = db.NhanViens.Where(u => u.Email.Equals(email) &&
               u.MatKhau.Equals(matKhau)).ToList();
                if (user.Count() > 0)
                {
                    //sử dụng Session: add Session
                    Session["TenNV"] = user.FirstOrDefault().HoTen;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["idNV"] = user.FirstOrDefault().MaNV;
                    Session["Quyen"] = user.FirstOrDefault().MaQuyen;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công!";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("DangNhap", "Home");
        }
    }
}