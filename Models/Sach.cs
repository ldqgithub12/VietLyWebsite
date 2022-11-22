namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            DanhGias = new HashSet<DanhGia>();
        }

        [Key]
        public int MaSach { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage ="Tên sách không được bỏ trống")]
        public string TenSach { get; set; }

        [Required(ErrorMessage ="Số lượng không được bỏ trống")]
        public int SoLuong { get; set; }

        [StringLength(50)]
        public string Anh { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage ="Đơn giá không được bỏ trống")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage ="Tên tác giả không được bỏ trống")]
        [StringLength(50)]
        public string TacGia { get; set; }

        [Required]
        [StringLength(10)]
        public string MaTL { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
