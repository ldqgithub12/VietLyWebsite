using LeDangQuang_DoAnTotNghiep.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDangQuang_DoAnTotNghiep.Areas.Admin.Controllers
{
    public class QLNhapController : Controller
    {
        DoAnDB db = new DoAnDB();
        // GET: Admin/QLNhap
        public ActionResult Index()
        {
            var thongtin = db.ChiTietPhieuNhaps.Select(n => n);
            return View(thongtin);
        }
        public ActionResult SachSapHet()
        {
            var sach = db.Saches.Where(n => n.SoLuong < 10).Select(n => n);
            return View(sach);
        }
        [HttpGet]
        public ActionResult CapNhatSL(int id)
        {
            ViewBag.MaNCC = new SelectList(db.NhaCCs,"MaNCC","TenNhaCC");
            var sach = db.Saches.Find(id);
            return View(sach);
        }

        [HttpPost]
        public ActionResult CapNhatSL(int MaSach, int MaNCC,int SLnhap,decimal gianhap)
        {
            PhieuNhap pn = new PhieuNhap();
            pn.NgayNhap = DateTime.Now;
            pn.MaNCC = MaNCC;
            db.PhieuNhaps.Add(pn);
            db.SaveChanges();
            ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
            ctpn.MaPN = pn.MaPN;
            ctpn.DonGia = gianhap;
            ctpn.SoLuong = SLnhap;
            ctpn.MaSach = MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach.Equals(MaSach));
            sach.SoLuong += SLnhap;
            db.ChiTietPhieuNhaps.Add(ctpn);
            db.SaveChanges();
            return RedirectToAction("SachSapHet","QLNhap");
        }
        public void ExportExcel()
        {
            List<ExcelData> sach = db.ChiTietPhieuNhaps.Select(n => new ExcelData
            {
                NgayDat = n.PhieuNhap.NgayNhap,
                TenNhaCC = n.PhieuNhap.NhaCC.TenNhaCC,
                TenSach = n.Sach.TenSach,
                SoLuong = n.SoLuong,
                GiaBan = n.DonGia
            }).ToList();
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
            ws.Cells["A1"].Value = "Date";
            ws.Cells["B1"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);
            ws.Cells["A2"].Value = "Report: ";
            ws.Cells["B2"].Value = "Sách sắp hết được nhập thêm";

            ws.Cells["A4"].Value = "Ngày đặt";
            ws.Cells["B4"].Value = "Tên nhà cung cấp";
            ws.Cells["C4"].Value = "Tên sách";
            ws.Cells["D4"].Value = "Số lượng";
            ws.Cells["E4"].Value = "Giá nhập";
            int rowStart = 5;
            foreach (var item in sach)
            {
                /* ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;*/
                ws.Cells[string.Format("A{0}", rowStart)].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", item.NgayDat);
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.TenNhaCC;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.TenSach;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SoLuong;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.GiaBan;
                rowStart++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }
}