using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLDonHangController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLDonHang
        public ActionResult Index()
        {
            var donhang = db.DonHangs.Where(n=>n.TrangThai == false).OrderBy(n=>n.NgayDat);
            return View(donhang);
        }
        public ActionResult XemChiTietDonHang(int id)
        {
            var chitiet = db.ChiTietDonHangs.Where(n => n.MaDonHang.Equals(id)).Select(n => n);
            ViewBag.TongTien = chitiet.Sum(n => n.GiaBan * n.SoLuong);
            ViewBag.TongTien = Convert.ToInt32(ViewBag.TongTien);
            return View(chitiet);
        }
        public ActionResult GiaoChoDVVanChuyen()
        {
            var dagiao = db.DonHangs.Where(n=>n.TrangThai == true && n.DaGiao == false).OrderBy(n=>n.NgayDat);
            return View(dagiao);
        }
        public ActionResult DonBiHuy()
        {
            var dagiao = db.DonHangs.Where(n => n.TrangThai == true && n.DaHuy == true).OrderBy(n => n.NgayDat);
            return View(dagiao);
        }
        public ActionResult DonThanhToan()
        {
            var dagiao = db.DonHangs.Where(n => n.TrangThai == true && n.DaGiao == true).OrderBy(n => n.NgayDat);
            return View(dagiao);
        }
        public ActionResult DaThanhToan(int id)
        {
            DonHang dh = db.DonHangs.Single(n => n.MaDonHang.Equals(id));
            dh.DaThanhToan = true;
            db.SaveChanges();
            GuiEmail("Xác nhận đơn hàng", "ledangquang443@gmail.com", "bookshoplequang@gmail.com", "Quang123google", "Đơn hàng của đã được thanh toán. Cảm ơn bạn vì đã tin tưởng chúng tôi !!");
            return RedirectToAction("DonThanhToan");
        }
        public ActionResult DaGiao(int id)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang.Equals(id));
            dh.DaGiao = true;
            db.SaveChanges();
            GuiEmail("Xác nhận đơn hàng", "ledangquang443@gmail.com", "bookshoplequang@gmail.com", "Quang123google", "Đơn hàng của bạn đã được nhà sách Việt Lý" +
                "giao cho đơn vị vận chuyển. Sau khi nhận được hàng, quý khách vui lòng đăng nhập vào website bằng email đăng ký, mật khẩu" +
                "là: 123456, chọn mục: Đơn hàng của tôi và bấm vào nút Đã nhận để thành toán. Xin cảm ơn!");
            return RedirectToAction("GiaoChoDVVanChuyen");
        }
        public ActionResult DuyetDonHang(int id)
        {
            DonHang dh = db.DonHangs.Single(n => n.MaDonHang.Equals(id));
            dh.TrangThai = true;
            db.SaveChanges();
            GuiEmail("Xác nhận đơn hàng", "ledangquang443@gmail.com", "bookshoplequang@gmail.com", "Quang123google", "Đơn hàng của bạn đã được duyệt. Cảm ơn bạn vì đã tin tưởng chúng tôi !!");
            return RedirectToAction("Index");
        }
        public void GuiEmail(string title, string toEmail,string fromEmail,string Password, string Content)
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