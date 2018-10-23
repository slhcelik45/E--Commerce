using EMisas.Models;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMisas.Controllers
{
    public class AdminUrunController : Controller
    {
        DbMisas db = new DbMisas();
        #region Ürün Listele
        // GET: AdminUrun
        public ActionResult Index(int sayfa = 1)
        {
            var Urunler = db.Urunlers.ToList();
            return View(Urunler.OrderByDescending(x => x.UrunId).ToPagedList(sayfa, 10));
        }
        #endregion
        #region Ürün Detay
        // GET: AdminUrun/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion
        #region Urun Ekle
        // GET: AdminUrun/Create
        public ActionResult Create()
        {
            var KategoriList = db.Kategoris.ToList();
            var MarkaList = db.Markas.ToList();
            ViewBag.Marka = MarkaList;
            ViewBag.Kategori = KategoriList;           
            return View();
        }

        // POST: AdminUrun/Create
        [HttpPost]
        public ActionResult Create(Urunler urun, int KategoriID, int MarkaID, HttpPostedFileBase urunResim)
        {
            urun.KategoriId = KategoriID;
            urun.MarkaId = MarkaID;


            if (urunResim != null)
            {
                string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/Upload/Urunler/ " + DosyaAdi + Uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));
                urun.UrunResim = TamYol;
            }
            db.Urunlers.Add(urun);
            db.SaveChanges();
            TempData["Bilgi"] = "Ürün Kayıt İşlemi Başarılı  ";
            return RedirectToAction("Index","AdminUrun");

        }
        #endregion
        #region Ürün Düzenle
        // GET: AdminUrun/Edit/5
        public ActionResult Edit(int id)
        {
            var urun = db.Urunlers.Where(x => x.UrunId == id).SingleOrDefault();
            if (urun==null)
            {
                return HttpNotFound();
            }
            else
            {
                var KategoriList = db.Kategoris.ToList();
                var MarkaList = db.Markas.ToList();
                ViewBag.Kategori = KategoriList;
                ViewBag.Marka = MarkaList;
                return View(urun);
            }            
        }

        // POST: AdminUrun/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Urunler urun,HttpPostedFileBase urunResim)
        {
            var gelenUrun = db.Urunlers.Where(x => x.UrunId ==id).SingleOrDefault();

            gelenUrun.KategoriId = urun.KategoriId;
            gelenUrun.MarkaId = urun.MarkaId;
            gelenUrun.UrunAdi = urun.UrunAdi;
            gelenUrun.UrunAciklama = urun.UrunAciklama;
            gelenUrun.UrunAlışFiyat = urun.UrunAlışFiyat;
            gelenUrun.UrunSatisFiyat = urun.UrunSatisFiyat;
            gelenUrun.UrunSonKullanımTarihi = urun.UrunSonKullanımTarihi;
            gelenUrun.UrunStok = urun.UrunStok;
            gelenUrun.UrunIndirim = urun.UrunIndirim;
            if (gelenUrun.UrunResim!=null )
            {
                string DosyaAdi = gelenUrun.UrunResim;
                string DosyaYolu = Server.MapPath(DosyaAdi);
                FileInfo file = new FileInfo(DosyaYolu);
                if (file.Exists)
                {
                    file.Delete();
                }
                string FileName = Guid.NewGuid().ToString().Replace("-", "");
                string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/Upload/Urunler/" + FileName + Uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));
               
                gelenUrun.UrunResim = TamYol;
            }            
            db.SaveChanges();
            TempData["Bilgi"] = "Ürün Düzenleme İşlemi Başarılı";
            return RedirectToAction("Index", "AdminUrun");
        }
        #endregion
        #region Ürün Sil
        // GET: AdminUrun/Delete/5
        public ActionResult Delete(int Id)
        {
            var gelenUrun = db.Urunlers.Where(x => x.UrunId ==Id).SingleOrDefault();
            if (gelenUrun==null)
            {
                return HttpNotFound();
            }
            try
            {
                if (gelenUrun.UrunResim!=null)
                {
                    string ResimUrl = gelenUrun.UrunResim;
                    string ResimPath = Server.MapPath(ResimUrl);
                    FileInfo dosya = new FileInfo(ResimPath);
                    if (dosya.Exists)
                    {
                        dosya.Delete();
                    }
                    db.Urunlers.Remove(gelenUrun);
                    db.SaveChanges();
                    TempData["Bilgi"] = "Silme İşlemi Başarılı !";
                    return RedirectToAction("Index", "AdminUrun");
                }
            }
            catch (Exception)
            {

                TempData["Bilgi"] = "Silme İşlemi Başarılısız !";
                return RedirectToAction("Index", "AdminUrun");
            }
            return View();
        }      
        #endregion
        #region Ürün İndirim Yap
        public ActionResult Onay(int Id)
        {
            Urunler urun = db.Urunlers.Find(Id);
            if (urun.UrunIndirim == true)
            {
                urun.UrunIndirim = false;
                db.SaveChanges();
                return RedirectToAction("Index", "AdminUrun");
            }
            else if (urun.UrunIndirim == false)
            {
                urun.UrunIndirim = true;
                db.SaveChanges();
                return RedirectToAction("Index", "AdminUrun");
            }
            return View();
        }
        #endregion
      
    }
}
