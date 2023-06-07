using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Models;

namespace CoffeShop.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }


        [Route("hakkimizda")]
        public ActionResult Hakkimizda()
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.hakkimizda.Find(1);
                return View(model);
            }

        }


        [Route("urunler")]
        public ActionResult Urunler()
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.urunler.Where(x => x.UrunDurum == true).OrderBy(x => x.UrunSira).ToList();
                return View(model);
            }

        }


        [Route("urunler/{id}/{baslik}")]
        public ActionResult UrunDetay(int id)
        {
            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var model = db.urunler.Where(x => x.UrunDurum == true && x.ID == id).FirstOrDefault();
                if (model==null)
                {
                    return HttpNotFound();
                }

                return View(model);
            }
        }



        [Route("magaza")]
        public ActionResult Magaza()
        {
            return View();
        }

        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}