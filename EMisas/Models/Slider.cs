namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Baslik { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        [Required]
        [StringLength(500)]
        public string ResimUrl { get; set; }

        public bool AktifMi { get; set; }

        public DateTime? EklemeTarihi { get; set; }
    }
}
