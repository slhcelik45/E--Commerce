using EMisas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMisas.Controllers
{
    public class HomeController : Controller
    {
        DbMisas db = new DbMisas();
        public static int UrunID { get; set; }
        public static List<SiparisDetay> siparisListele = null;
        // GET: Home
        public ActionResult Index()
        {
            List<Urunler> urunler = db.Urunlers.Where(x => x.UrunIndirim == true).ToList();
            return View(urunler);
        }
        #region Kategori Partial
        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        #endregion
        #region Marka Partial
        public ActionResult MarkaPartial()
        {
            return View(db.Markas.ToList());
        }
        #endregion
        #region Kategorik Ürünler
        public ActionResult KategoriUrunler(int id)
        {
            var kategorikUrunler = db.Urunlers.Where(x => x.KategoriId == id).ToList();
            return View(kategorikUrunler);
        }
        #endregion
        #region Marka Ürünler
        public ActionResult MarkaUrunler(int id)
        {
            var markaUrunler = db.Urunlers.Where(x => x.MarkaId == id).ToList();
            return View(markaUrunler);
        }
        #endregion
        #region Ürün Detay
        public ActionResult UrunDetay(int id)
        {
            UrunID = id;
            var urunDetay = db.Urunlers.Where(x => x.UrunId == id).FirstOrDefault();
            return View(urunDetay);
        }
        #endregion
        #region Sepet At     
        public ActionResult SepetAt(SiparisDetay entity,Urunler urun)
        {
            if (Convert.ToInt32(Session["KullaniciId"]) > 0)
            {
                SiparisDetay Gsiparis = new SiparisDetay();
                var gelenUrun = db.Urunlers.Where(x => x.UrunId == UrunID).SingleOrDefault();

                Gsiparis.MusteriId = Convert.ToInt32(Session["KullaniciId"]);
                Gsiparis.UrunId = UrunID;
                Gsiparis.Adet = entity.Adet;//==null ? -1: (int)entity.UrunStok;
                Gsiparis.UrunSatisFiyat = gelenUrun.UrunSatisFiyat;

                db.SiparisDetays.Add(Gsiparis);
                db.SaveChanges();
                return RedirectToAction("../");
            }
            return RedirectToAction("../");

        }
        #endregion
        public ActionResult SepetIslemleri()
        {
            int kID = Convert.ToInt32(Session["KullaniciId"]);
            siparisListele = db.SiparisDetays.Where(x => x.MusteriId == kID).ToList();
            return View(siparisListele);
        }
        [HttpPost]
        public ActionResult SepetIslemlerix()
        {
            List<SiparisDetay> k = siparisListele;
            foreach (SiparisDetay item in k)
            {
                Sipari s = new Sipari();
                s.SiparisDetayId = item.SiparisDetayId;
                s.MusteriId = (int)item.MusteriId;
                s.SiparisDurumId = 1;
                s.KargoId = 1;
                s.SiparisTarihi = DateTime.Now;
                s.ToplamTutar = item.Adet * item.UrunSatisFiyat;
                s.SepetteMi = true;
                db.Siparis.Add(s);
               
            }
            db.SaveChanges();
            return RedirectToAction("../");
        }
    }
}