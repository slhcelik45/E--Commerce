namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Musteri")]
    public partial class Musteri
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Musteri()
        {
            Siparis = new HashSet<Sipari>();
            SiparisDetays = new HashSet<SiparisDetay>();
        }

        public int MusteriId { get; set; }

        public int? RolId { get; set; }

        [Required]
        [StringLength(150)]
        public string MusteriAdSoyad { get; set; }

        [Required]
        [StringLength(100)]
        public string MusteriEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string MusteriTel { get; set; }

        [Required]
        [StringLength(15)]
        public string MusteriSifre { get; set; }

        [Required]
        public string MusteriAdres { get; set; }

        public virtual Rol Rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sipari> Siparis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetay> SiparisDetays { get; set; }
    }
}
