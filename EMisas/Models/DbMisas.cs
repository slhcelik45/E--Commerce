namespace EMisas.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbMisas : DbContext
    {
        public DbMisas()
            : base("name=DbMisas")
        {
        }

        public virtual DbSet<Duyurular> Duyurulars { get; set; }
        public virtual DbSet<Kargo> Kargoes { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Marka> Markas { get; set; }
        public virtual DbSet<Musteri> Musteris { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Sipari> Siparis { get; set; }
        public virtual DbSet<SiparisDetay> SiparisDetays { get; set; }
        public virtual DbSet<SiparisDurum> SiparisDurums { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Tur> Turs { get; set; }
        public virtual DbSet<Urunler> Urunlers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Duyurular>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<Duyurular>()
                .Property(e => e.KisaAciklama)
                .IsUnicode(false);

            modelBuilder.Entity<Duyurular>()
                .Property(e => e.Aciklama)
                .IsUnicode(false);

            modelBuilder.Entity<Duyurular>()
                .Property(e => e.Resim)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .Property(e => e.Adres)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Kargo>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Kargo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kategori>()
                .Property(e => e.KategoriAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Marka>()
                .Property(e => e.MarkaAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.MusteriAdSoyad)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.MusteriEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.MusteriTel)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.MusteriSifre)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .Property(e => e.MusteriAdres)
                .IsUnicode(false);

            modelBuilder.Entity<Musteri>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Musteri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .Property(e => e.RolAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Sipari>()
                .Property(e => e.ToplamTutar)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SiparisDetay>()
                .Property(e => e.UrunSatisFiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SiparisDetay>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.SiparisDetay)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SiparisDurum>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<SiparisDurum>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.SiparisDurum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tur>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<Urunler>()
                .Property(e => e.UrunAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Urunler>()
                .Property(e => e.UrunAciklama)
                .IsUnicode(false);

            modelBuilder.Entity<Urunler>()
                .Property(e => e.UrunSatisFiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Urunler>()
                .Property(e => e.UrunAlışFiyat)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Urunler>()
                .Property(e => e.UrunResim)
                .IsUnicode(false);

            modelBuilder.Entity<Urunler>()
                .HasMany(e => e.SiparisDetays)
                .WithRequired(e => e.Urunler)
                .WillCascadeOnDelete(false);
        }
    }
}
