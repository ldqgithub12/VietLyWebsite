namespace LeDangQuang_DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public int MaDonHang { get; set; }

        public DateTime NgayDat { get; set; }

        [Column(TypeName = "ntext")]
        public string DiaChiNhan { get; set; }

        public bool? TrangThai { get; set; }

        public bool? DaGiao { get; set; }

        public bool? DaThanhToan { get; set; }

        public bool? DaHuy { get; set; }

        public int MaKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
