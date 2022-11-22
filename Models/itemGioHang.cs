using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeDangQuang_DoAnTotNghiep.Models
{
    public class itemGioHang
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal ThanhTien { get; set; }


        public itemGioHang(int iMaSach)
        {
            using (DoAnDB db = new DoAnDB())
            {
                this.MaSach = iMaSach;
                Sach sachs = db.Saches.Single(n => n.MaSach.Equals(iMaSach));
                TenSach = sachs.TenSach;
                HinhAnh = sachs.Anh;
                DonGia = sachs.Gia;
                SoLuong = 1;
                ThanhTien = DonGia * SoLuong;
            }
        }
        public itemGioHang() { }
    }
}