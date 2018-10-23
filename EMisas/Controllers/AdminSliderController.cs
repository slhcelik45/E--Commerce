using EMisas.Models;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMisas.Controllers
{
    public class AdminSliderController : Controller
    {
        DbMisas db = new DbMisas();
        #region Sldier Listele
        public ActionResult Index(int sayfa = 1)
        {
            var sliderlist = db.Sliders.ToList();
            return View(sliderlist.OrderBy(x => x.Id).ToPagedList(sayfa, 10));
        }
        #endregion
        #region Sldier Ekle
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Slider slider, HttpPostedFileBase sliderResim)
        {
            
                if (sliderResim != null)
                {
                    string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzati = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYol = "/Upload/Slider/" + dosyaAdi + Uzati;
                    Request.Files[0].SaveAs(Server.MapPath(TamYol));
                    slider.ResimUrl = TamYol;
                }
                db.Sliders.Add(slider);
                db.SaveChanges();
                TempData["Bilgi"] = "Slider Kayıt İşlemi Başarılı  ";
                return RedirectToAction("Index", "AdminSlider");    
        }
        #endregion
        #region Sldier Dzüenle
        public ActionResult Edit(int id)
        {
            var gelenSlider = db.Sliders.Where(x => x.Id == id).SingleOrDefault();
            if (gelenSlider == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(gelenSlider);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, Slider slider, HttpPostedFileBase sliderResim)
        {
            var gelenSlider = db.Sliders.Where(x => x.Id == id).SingleOrDefault();

            gelenSlider.Baslik = slider.Baslik;
            gelenSlider.Aciklama = slider.Aciklama;
            gelenSlider.EklemeTarihi = slider.EklemeTarihi;
            gelenSlider.AktifMi = slider.AktifMi;
            if (sliderResim!=null)
            {
                string dosyaAdi = gelenSlider.ResimUrl;
                string dosyaYolu = Server.MapPath(dosyaAdi);
                FileInfo file = new FileInfo(dosyaYolu);
                if (file.Exists)
                {
                    file.Delete();
                }
                string fileName = Guid.NewGuid().ToString().Replace("-", "");
                string filePath = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/Upload/Slider/" + fileName + filePath;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));
                gelenSlider.ResimUrl = TamYol;
            }
            //gelenSlider.ResimUrl = slider.ResimUrl;
            db.SaveChanges();
            TempData["Bilgi"] = "Slider Düzenleme İşlemi Başarılı";
            return RedirectToAction("Index", "AdminSlider");
            
        }
        #endregion
        #region Slider Sil
        public ActionResult Delete(int id)
        {
            var gelenSlider = db.Sliders.Where(x => x.Id == id).SingleOrDefault();

            if (gelenSlider==null)
            {
                return HttpNotFound();
            }
            try
            {
                if (gelenSlider.ResimUrl!=null)
                {
                    string dosyaAdi = gelenSlider.ResimUrl;
                    string dosyaYol = Server.MapPath(dosyaAdi);
                    FileInfo file = new FileInfo(dosyaYol);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    db.Sliders.Remove(gelenSlider);
                    db.SaveChanges();
                    TempData["Bilgi"] = "Silme İşlemi Başarılı !";
                    return RedirectToAction("Index", "AdminSlider");
                }

            }
            catch (Exception)
            {

                TempData["Bilgi"] = "Silme İşlemi Başarılısız !";
                return RedirectToAction("Index", "AdminSlider");
            }
            return View();     
        }              
        #endregion
        #region Slider Akti - Pasif
        public ActionResult Onay(int Id)
        {
            Slider gelenSlider = db.Sliders.Find(Id);
            if (gelenSlider.AktifMi== true)
            {
                gelenSlider.AktifMi = false;
                db.SaveChanges();
                return RedirectToAction("Index", "AdminSlider");
            }
            else if (gelenSlider.AktifMi == false)
            {
                gelenSlider.AktifMi = true;
                db.SaveChanges();
                return RedirectToAction("Index", "AdminSlider");
            }
            return View();
        }
        #endregion

    }
}
