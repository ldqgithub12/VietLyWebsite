using LeDangQuang_DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        DoAnDB db = new DoAnDB();
        public List<itemGioHang> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<itemGioHang> lstGioHang = Session["GioHang"] as List<itemGioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<itemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int MaSach, string strURL)
        {
            //Kiểm tra xem sách có tồn tại trong cơ sở dữ liệu hay không
            Sach sach = db.Saches.SingleOrDefault(s => s.MaSach.Equals(MaSach));
            if (sach == null)
            {
                //Trả về trang lỗi
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<itemGioHang> lstGioHang = LayGioHang();
            // Nếu sản phẩm đó đã tồn tại trong giỏ hàng
            itemGioHang sachcheck = lstGioHang.SingleOrDefault(s => s.MaSach.Equals(MaSach));
            if (sachcheck != null)
            {
                //tăng số lượng lên
                sachcheck.SoLuong++;
                //Tính lại thành tiền
                sachcheck.ThanhTien = sachcheck.SoLuong * sachcheck.DonGia;
                //Trả lại trang hiện tại
                return Redirect(strURL);
            }
            //Tạo item giỏ hàng mới
            itemGioHang itgioHang = new itemGioHang(MaSach);
            //Thêm item đó vào giỏ hàng
            lstGioHang.Add(itgioHang);
            //Trả về trang hiện tại
            return Redirect(strURL);
        }
        public int TinhTongSoLuong()
        {
            List<itemGioHang> lstGiohang = Session["GioHang"] as List<itemGioHang>;
            if (lstGiohang == null)
            {
                return 0;
            }
            else
            {
                return lstGiohang.Sum(n => n.SoLuong);
            }
        }

        public decimal TinhTongTien()
        {
            List<itemGioHang> lstGiohang = Session["GioHang"] as List<itemGioHang>;
            if (lstGiohang == null)
            {
                return 0;
            }
            else
            {
                return lstGiohang.Sum(n => n.SoLuong*n.DonGia);
            }
        }
        public ActionResult XemGioHang()
        {
            List<itemGioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TinhTongTien().ToString("#,##");
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();

            return PartialView();
        }
        public ActionResult XoaGioHang(int id)
        {
            List<itemGioHang> lstGiohang = LayGioHang();
            itemGioHang sachcheck = lstGiohang.Single(n => n.MaSach.Equals(id));
            lstGiohang.Remove(sachcheck);
            return RedirectToAction("XemGioHang");
        }
        [HttpPost]
        public ActionResult SuaGioHang(int id , int soluongmoi)
        {
            List<itemGioHang> lstgioHang = LayGioHang();
            itemGioHang itemUpdate = lstgioHang.Find(n => n.MaSach.Equals(id));
            itemUpdate.SoLuong = soluongmoi;
            itemUpdate.ThanhTien = itemUpdate.SoLuong * itemUpdate.ThanhTien;
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DatHang()
        {
            KhachHang khcdn = new KhachHang();
            if (Session["idUser"] != null)
            {
                DonHang dh = new DonHang();
                dh.NgayDat = DateTime.Now;
                dh.MaKH = Convert.ToInt32(Session["idUser"]);
                dh.TrangThai = false;
                dh.DaGiao = false;
                dh.DaThanhToan = false;
                dh.DaHuy = false;
                dh.DiaChiNhan = Request["diachinhanhang"];
                db.DonHangs.Add(dh);
                db.SaveChanges();
                List<itemGioHang> lstgioHang = LayGioHang();
                foreach (var item in lstgioHang)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDonHang = dh.MaDonHang;
                    ctdh.MaSach = item.MaSach;
                    ctdh.SoLuong = item.SoLuong;
                    ctdh.GiaBan = item.DonGia;
                    db.ChiTietDonHangs.Add(ctdh);
                }
                db.SaveChanges();
                Session["GioHang"] = null;
                return RedirectToAction("Dathangthanhcong");
            }
            else
            {
                khcdn.HoTen = Request["tenkh"];
                khcdn.DiaChi = Request["dckh"];
                khcdn.Email = Request["emailkh"];
                khcdn.SoDT = Request["sdtkh"];
                khcdn.MatKhau = "123456";
                db.KhachHangs.Add(khcdn);
                db.SaveChanges();
                DonHang dh = new DonHang();
                dh.NgayDat = DateTime.Now;
                dh.MaKH = khcdn.MaKH;
                dh.TrangThai = false;
                dh.DaGiao = false;
                dh.DaThanhToan = false;
                dh.DaHuy = false;
                dh.DiaChiNhan = Request["diachinhanhang"];
                db.DonHangs.Add(dh);
                db.SaveChanges();
                List<itemGioHang> lstgioHang = LayGioHang();
                foreach (var item in lstgioHang)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDonHang = dh.MaDonHang;
                    ctdh.MaSach = item.MaSach;
                    ctdh.SoLuong = item.SoLuong;
                    ctdh.GiaBan = item.DonGia;
                    db.ChiTietDonHangs.Add(ctdh);
                }
                db.SaveChanges();
                Session["GioHang"] = null;
                return RedirectToAction("Dathangthanhcong");
            }
        }

        public ActionResult Dathangthanhcong()
        {
            return View();
        }
    }
}