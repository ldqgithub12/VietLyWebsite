using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using PagedList;

namespace LeDangQuang_DoAnTotNghiep.Controllers
{
    public class HomeController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Home
        //Hiển thị trên trang index
        public ActionResult Index()
        {
            var danhmuc = db.DanhMucs.Select(n => n);
            return View(danhmuc);
        }
        public ActionResult XuHuong()
        {
            var sach = db.Saches.Select(n => n).Take(10);
            return View(sach);
        }
        public ActionResult Xemdanhsachsach(string id, int? page)
        {
            var sach = db.Saches.Where(n => n.MaDM.Equals(id)).Select(n => n).ToList();
            if (sach.Count > 0)
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(sach.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                ViewBag.Error = "Danh mục này hiện chưa có cuốn sách nào !!";
                return View(sach.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult XemChiTietSach(int id)
        {
            var sach = db.Saches.Find(id);
            return View(sach);
        }

        public ActionResult Sachlienquan(string id)
        {
            var sach = db.Saches.Where(n => n.MaDM.Equals(id)).Select(n => n).Take(4);
            return View(sach);
        }
        //Bình luận 
        public ActionResult XemBinhLuan(int id)
        {
            var binhluan = db.DanhGias.Where(n => n.MaSach.Equals(id)).Select(n => n);
            ViewBag.Masach = id;
            return View(binhluan);
        }

        [HttpPost]
        public ActionResult ThemBinhLuan(int maSach, int rating, string customerComment, string strURL)
        {
            DanhGia dg = new DanhGia();
            dg.MaSach = maSach;
            dg.NoiDung = customerComment;
            dg.Rating = rating;
            dg.MaKH = Convert.ToInt32(Session["idUser"]);
            dg.DateDG = DateTime.Now;
            db.DanhGias.Add(dg);
            db.SaveChanges();
            return Redirect(strURL);
        }

        //Đăng ký, đăng nhập, đăng xuất
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string matkhau)
        {
            if (ModelState.IsValid)
            {
                var user = db.KhachHangs.Where(u => u.Email.Equals(email) &&
               u.MatKhau.Equals(matkhau)).ToList();
                if (user.Count() > 0)
                {
                    //sử dụng Session: add Session
                    Session["HoTen"] = user.FirstOrDefault().HoTen;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["idUser"] = user.FirstOrDefault().MaKH;
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
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "MaKH,HoTen,DiaChi,Email,SoDT,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                GuiEmail("Xác nhận đăng ký", "ledangquang443@gmail.com", "bookshoplequang@gmail.com", "Quang123google", "Đăng ký tài khoản thành công. Cảm ơn bạn vì đã tin tưởng chúng tôi !!");
                return RedirectToAction("Login");
            }
            return View(khachHang);
        }
        
        [HttpGet]
        public ActionResult XemThongTinCaNhan()
        {
            var thongtin = db.KhachHangs.Find(Session["idUser"]);
            return View(thongtin);
        }
        [HttpGet]
        public ActionResult CapNhatThongTinCaNhan(int id)
        {
            var khacHang = db.KhachHangs.Find(id);
            return View(khacHang);
        }
        [HttpPost]
        public ActionResult CapNhatThongTinCaNhan([Bind(Include ="MaKH,HoTen,DiaChi,Email,SoDT,MatKhau")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("XemThongTinCaNhan");
            }
            return View(khachHang);
        }
      
        public ActionResult ChangePassword(string oldpassword, string newpassword)
        {
            int id = (int)Session["idUser"];
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH.Equals(id));
            if (oldpassword != kh.MatKhau)
            {
                return View();
            }
            kh.MatKhau = newpassword;
            db.SaveChanges();
            return RedirectToAction("Login", "Home");
        }

        //Thông tin đơn hàng
        public ActionResult XemDonHang(int? page)
        {
            int idUser = (int)Session["idUser"];
            var donhang = db.DonHangs.Where(n => n.MaKH == idUser).Select(n => n).ToList();
            if (donhang.Count > 0)
            {
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(donhang.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                ViewBag.Error = "Chưa có đơn hàng nào !!";
                return View(donhang.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult XemChiTietDonHang(int id)
        {
            var chitiet = db.ChiTietDonHangs.Where(n=>n.MaDonHang.Equals(id)).Select(n=>n);
            return View(chitiet);
        }
        public ActionResult DaNhanHang(int id)
        {
            DonHang dh = db.DonHangs.Single(n => n.MaDonHang.Equals(id));
            dh.DaThanhToan = true;
            db.SaveChanges();
            GuiEmail("Xác nhận thanh toán", "ledangquang443@gmail.com", "bookshoplequang@gmail.com", "Quang123google", "Thanh toán thành công. Cảm ơn bạn vì đã tin tưởng chúng tôi !!");
            return RedirectToAction("XemDonHang");
        }
        public ActionResult DaHuy(int id)
        {
            DonHang dh = db.DonHangs.Single(n => n.MaDonHang.Equals(id));
            dh.DaHuy = true;
            db.SaveChanges();
            return RedirectToAction("XemDonHang");
        }
        //Tìm kiếm
        [HttpPost]
        public ActionResult TimKiem(string searchString)
        {
            var sach = db.Saches.Where(n => n.TenSach.Contains(searchString)).Select(n=>n).ToList();
            return View(sach);
        }
        public void GuiEmail(string title, string toEmail, string fromEmail, string Password, string Content)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(toEmail);
            mail.Subject = title;
            mail.Body = Content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}