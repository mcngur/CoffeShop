using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Models;
using CoffeShop.Controllers;

namespace CoffeShop.Areas.admin.Controllers
{
    [Authorize]
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.hakkimizda.First();
                return View(model);

            }
            
        }

        public ActionResult HakkimizdaGuncelle()
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult Kaydet(hakkimizda GelenVeri)
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var GuncellenecekVeri = db.hakkimizda.First();

                if (!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", GelenVeri);
                }

                //upload kontrolü
                if (GelenVeri.fotoFile!=null)
                {
                    GelenVeri.Foto = Seo.DosyaAdiDuzenle(GelenVeri.fotoFile.FileName);
                    GelenVeri.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"),Path.GetFileName(GelenVeri.Foto)));
                }

                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                TempData["hakkimdaGuncelle"] = " ";
                return RedirectToAction("Index", "Hakkimizda");
            }
        }
    }
}