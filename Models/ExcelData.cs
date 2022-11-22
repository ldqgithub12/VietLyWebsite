using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeDangQuang_DoAnTotNghiep.Models
{
    public class ExcelData
    {
        public DateTime NgayDat { get; set; }
        public string TenNhaCC { get; set; }
        public string TenSach { get; set; }
        public int? SoLuong { get; set; }
        public decimal? GiaBan { get; set; }
    }
}