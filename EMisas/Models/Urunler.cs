namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urunler")]
    public partial class Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urunler()
        {
            SiparisDetays = new HashSet<SiparisDetay>();
        }

        [Key]
        public int UrunId { get; set; }

        public int? KategoriId { get; set; }

        public int? MarkaId { get; set; }

        [StringLength(150)]
        public string UrunAdi { get; set; }

        [StringLength(250)]
        public string UrunAciklama { get; set; }

        [Column(TypeName = "money")]
        public decimal UrunSatisFiyat { get; set; }

        public bool UrunIndirim { get; set; }

        public int? UrunStok { get; set; }

        public DateTime? UrunSonKullanımTarihi { get; set; }

        [Column(TypeName = "money")]
        public decimal? UrunAlışFiyat { get; set; }

        [StringLength(250)]
        public string UrunResim { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Marka Marka { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetay> SiparisDetays { get; set; }
    }
}
