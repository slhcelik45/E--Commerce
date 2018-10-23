namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Duyurular")]
    public partial class Duyurular
    {
        [Key]
        public int DuyuruId { get; set; }

        public int? TurId { get; set; }

        [Required]
        [StringLength(150)]
        public string Adi { get; set; }

        [Required]
        [StringLength(250)]
        public string KisaAciklama { get; set; }

        [Required]
        public string Aciklama { get; set; }

        [Required]
        [StringLength(250)]
        public string Resim { get; set; }

        public virtual Tur Tur { get; set; }
    }
}
