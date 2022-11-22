namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        public int MaCTPN { get; set; }

        public int MaPN { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        public int? SoLuong { get; set; }

        public int MaSach { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
