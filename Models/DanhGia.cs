namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGia")]
    public partial class DanhGia
    {
        [Key]
        public int MaDG { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string NoiDung { get; set; }

        public int? Rating { get; set; }

        public DateTime? DateDG { get; set; }

        public int MaKH { get; set; }

        public int MaSach { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
