namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiparisDetay")]
    public partial class SiparisDetay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SiparisDetay()
        {
            Siparis = new HashSet<Sipari>();
        }

        public int SiparisDetayId { get; set; }

        public int? MusteriId { get; set; }

        public int UrunId { get; set; }

        public int Adet { get; set; }

        [Column(TypeName = "money")]
        public decimal UrunSatisFiyat { get; set; }

        public virtual Musteri Musteri { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }

        public virtual Urunler Urunler { get; set; }
    }
}
