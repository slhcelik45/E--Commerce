namespace EMisas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sipari
    {
        [Key]
        public int SiparisId { get; set; }

        public int SiparisDetayId { get; set; }

        public int MusteriId { get; set; }

        public int SiparisDurumId { get; set; }

        public int KargoId { get; set; }

        public DateTime SiparisTarihi { get; set; }

        [Column(TypeName = "money")]
        public decimal ToplamTutar { get; set; }

        public bool SepetteMi { get; set; }

        public virtual Kargo Kargo { get; set; }

        public virtual Musteri Musteri { get; set; }

        public virtual SiparisDetay SiparisDetay { get; set; }

        public virtual SiparisDurum SiparisDurum { get; set; }
    }
}
