using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Controllers;
using CoffeShop.Models;

namespace CoffeShop.Areas.admin.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler
        public ActionResult Index()
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }
        }

        public ActionResult YeniUrun()
        {
            var model = new urunler();
            return View("UrunForm", model);
        }

        public ActionResult Guncelle(int id)
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.urunler.Find(id);

                if (model == null)
                {
                    return HttpNotFound();
                }

                return View("UrunForm", model);
            }
        }

        public ActionResult Kaydet(urunler gelenUrun)
        {

            if (!ModelState.IsValid)
            {
                return View("UrunForm", gelenUrun);
            }

            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                if (gelenUrun.ID == 0) //yeni ürün kayıt kontrolü
                {

                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen bir resim ekleyiniz";
                        return View("UrunForm", gelenUrun);
                    }

                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    gelenUrun.UrunFoto = fotoAdi;
                    db.urunler.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urun"] = "Ürün başarılı bir şekilde eklendi.";

                }
                else // güncelleme işlemi
                {
                    var guncellenecekVeri = db.urunler.Find(gelenUrun.ID);

                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.UrunFoto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }
                    db.Entry(guncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "Ürün başarılı bir şekilde güncellendi.";
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Sil(int id)
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var silinecekUrun = db.urunler.Find(id);

                if (silinecekUrun==null)
                {
                    return HttpNotFound();
                }

                db.urunler.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["urun"] = "Ürün başarılı bir şekilde silindi";

                return RedirectToAction("Index", "Urunler");
            }
        }
    }
}