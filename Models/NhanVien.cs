namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required(ErrorMessage ="Họ tên không được bỏ trống")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "ntext")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage ="Email không được bỏ trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SoDT { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        public bool MaQuyen { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
