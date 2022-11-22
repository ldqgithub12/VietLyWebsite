namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DanhGias = new HashSet<DanhGia>();
            DonHangs = new HashSet<DonHang>();
        }

        [Key]
        public int MaKH { get; set; }

        [Required(ErrorMessage ="Tên khách hàng không được bỏ trống")]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Column(TypeName = "ntext")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage ="Email không được bỏ trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(15)]
        public string SoDT { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được bỏ trống")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
