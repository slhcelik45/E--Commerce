using EMisas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EMisas.Controllers
{
    public class UyeController : Controller
    {
        DbMisas db = new DbMisas();
        public static string MemberShipName { get; set; }
        public static string MemberShipPassword { get; set; }
        #region Üye Profil
        public ActionResult Index(int id)
        {
            var gelenUye = db.Musteris.Where(x => x.MusteriId == id).SingleOrDefault();
            if (Convert.ToInt32(Session["KullaniciId"]) != gelenUye.MusteriId)
            {
                return HttpNotFound();
            }
            else
            {
                return View(gelenUye);
            }

        }
        #endregion
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Musteri musteri)
        {
            var gelenMusteri = db.Musteris.Where(x => x.MusteriEmail == musteri.MusteriEmail).SingleOrDefault();
            if (gelenMusteri.MusteriEmail == musteri.MusteriEmail && gelenMusteri.MusteriSifre == musteri.MusteriSifre)
            {
                Session["KullaniciId"] = gelenMusteri.MusteriId;
                Session["KullaniciAdSoyad"] = gelenMusteri.MusteriAdSoyad;
                Session["KullaniciEmail"] = gelenMusteri.MusteriEmail;
                Session["KullaniciYetki"] = gelenMusteri.RolId;
                if (Convert.ToInt32((Session["KullaniciYetki"])) == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Bilgi"] = "Email veya Şİfreyi yanlış Girdiniz !";
                return View();
            }
        }
        #endregion
        #region Logout
        public ActionResult Logout()
        {
            Session["KullaniciId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region Üye kayıt
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Musteri musteri)
        {
            var geleMusteri = db.Musteris.Where(x => x.MusteriEmail == musteri.MusteriEmail).SingleOrDefault();

            if (geleMusteri == null)
            {
                musteri.RolId = 2;
                db.Musteris.Add(musteri);
                db.SaveChanges();
                TempData["Bilgi"] = "Üye Kayıt İşlemi Başarışlı !";               
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Bilgi"] = "Üye Kaydı Var Lütfen Giriş Yapın !";
                return RedirectToAction("Create", "Uye");
            }
        }
        #endregion
        #region Profil Güncelle
        public ActionResult Edit(int id)
        {
            var gelenUye = db.Musteris.Where(x => x.MusteriId == id).SingleOrDefault();
            if (gelenUye == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(gelenUye);
            }
        }

        [HttpPost]
        public ActionResult Edit(Musteri musteri, string uyeYsifre,int id)
        {
            var gelenUye = db.Musteris.Where(x => x.MusteriId ==id).SingleOrDefault();
            if (gelenUye.MusteriSifre == musteri.MusteriSifre)
            {
                gelenUye.MusteriAdSoyad = musteri.MusteriAdSoyad;
                gelenUye.MusteriEmail = musteri.MusteriEmail;
                gelenUye.MusteriSifre = uyeYsifre;
                gelenUye.MusteriTel = musteri.MusteriTel;
                gelenUye.MusteriAdres = musteri.MusteriAdres;

                db.SaveChanges();
                TempData["Bilgi"] = "Güncelleme İşlemi Başarılı.";
                return RedirectToAction("Index", "Uye");
            }
            else
            {
                TempData["Bilgi"] = "Eski şifre Uyşmadı Tekrar Deneyiniz !";
                return RedirectToAction("Edit", "Uye");
            }

           

        }

        #endregion
        public  Musteri SifremiUnuttum(string mail)
        {
            Musteri uye = db.Musteris.Where(x => x.MusteriEmail.Equals(mail)).FirstOrDefault();

            if (uye != null)
            {
                MemberShipName = uye.MusteriAdSoyad;
                MemberShipPassword = uye.MusteriSifre;
                return new Musteri
                {
                    MusteriAdSoyad = uye.MusteriAdSoyad,
                    MusteriSifre = uye.MusteriSifre,
                    MusteriEmail = uye.MusteriEmail

                };
            }
            else { return null; }

        }
        public async Task<ActionResult> MailGonder(Musteri entity)
        {
            
                          
                if (SifremiUnuttum(entity.MusteriEmail) != null)
                {
                    var body = "<b>Adınız Soyadınız :</b>" + MemberShipName + " \n <br> <b> E-Posta :</b>"+entity.MusteriEmail+"\n" + entity.MusteriAdSoyad + " \n <br> <b> Şifre:</b> " + MemberShipPassword + " \n <br>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(entity.MusteriEmail));
                    message.From = new MailAddress("misas@emekdukkani.com", "MİSAŞ");
                    message.Subject = "Hesap Bilgileri";
                    message.Body = string.Format(body, "MİSAŞ", "misas@emekdukkani.com", "MİSAŞ");
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "misas@emekdukkani.com",
                            Password = "DNgw78M9"
                        };
                        smtp.Credentials = credential;
                        smtp.Host = "mail.emekdukkani.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = false;
                        await smtp.SendMailAsync(message);
                        return RedirectToAction("../");
                    }
                }
            
            return RedirectToAction("../");
        }
        public ActionResult SifreUnuttum()
        {
            return View();
        }
    }
}