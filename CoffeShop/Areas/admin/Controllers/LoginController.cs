using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoffeShop.Models;
using System.Web.Security;

namespace CoffeShop.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Index2()
        //{
        //    return Content(Sifrele.MD5Olustur("1234"));
        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullaniciFormu, string ReturnUrl)
        {


            if (!ModelState.IsValid)
            {
                return View("Index", kullaniciFormu);
            }

            string sifre1 = Sifrele.MD5Olustur(kullaniciFormu.Sifre);

            using (coffemvcdbEntities db = new coffemvcdbEntities())
            {
                var kullanicVarmi = db.kullanicilar.FirstOrDefault(
                    x => x.KullaniciAdi == kullaniciFormu.KullaniciAdi && x.Sifre == sifre1);


                if (kullanicVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullanicVarmi.KullaniciAdi, kullaniciFormu.BeniHatirla);


                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Urunler");
                    }


                }

                ViewBag.Hata = "Kullanıcı Adı veya Şifre Hatalı Girildi!.";
                return View("Index");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}