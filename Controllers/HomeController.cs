using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Guest guest;
            Pidu pidu;
            ViewBag.Message = "Ootan sind oma peole! Palun tule kindlasti!";
            ViewBag.Tittle = "Kutse kõik";
            string[] peod = new string[12]{"Jaanuari pidu","Veebruari pidu", "Märts pidu", "Aprill pidu", "Mai pidu", "Juuni pidu", "Juuli pidu", "August pidu", "September pidu", "Oktoober pidu", "November pidu", "Detsember pidu" };
            int hour = DateTime.Now.Hour;
            int month = DateTime.Now.Month;

            /*string prazdnik = "";
            if (DateTime.Now.Month==1){prazdnik = "Jaanuari pidu";}
            else if (DateTime.Now.Month == 2){prazdnik = "Veebruari pidu";}
            else if (DateTime.Now.Month == 3){prazdnik = "Märts pidu";}
            else if (DateTime.Now.Month == 4){prazdnik = "Aprill pidu";}
            else if (DateTime.Now.Month == 5){prazdnik = "Mai pidu";}
            else if (DateTime.Now.Month == 6){prazdnik = "Juuni pidu";}
            else if (DateTime.Now.Month == 7){prazdnik = "Juuli pidu";}
            else if (DateTime.Now.Month == 8){prazdnik = "August pidu";}
            else if (DateTime.Now.Month == 9){prazdnik = "September pidu";}
            else if (DateTime.Now.Month == 10){ prazdnik = "Oktoober pidu";}
            else if (DateTime.Now.Month == 11){prazdnik = "November pidu";}
            else if (DateTime.Now.Month == 12){prazdnik = "Detsember pidu";}*/
   

            ViewBag.Message = "Ootan sind oma peole! " + peod[month-1]  + ". Palun tule kindlasti!";

            if (hour <= 16)
            {
                ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast";
            }
            else if(hour > 16)
            {
                ViewBag.Greeting = hour < 20 ? "Tere õhtu!" : "Tere öö";
            }
            //ViewBag.Greeting = hour < 10 ? "Tere hommikust!" : "Tere päevast";
            return View();
        }
        public static string email;
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Tittle = "Kutse kõik";

            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            //E_mailGuest(guest);
            if (ModelState.IsValid)
            {
                email = guest.Email;
                db.Guests.Add(guest);
                db.SaveChanges();
                ViewBag.Greeting = guest.Email;
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Meeledus(Guest guest)
        {
            E_mailGuest(guest);
            return View();
        }
        /*public ViewResult Thanks(Guest guest)
         {
             string komu = guest.Email;
                 WebMail.SmtpServer = "smtp.gmail.com";
                 WebMail.SmtpPort = 587;
                 WebMail.EnableSsl = true;
                 WebMail.UserName = "programmeeriminetthk2@gmail.com";
                 WebMail.Password = "2.kuursus tarpv20";
                 WebMail.From = "programmeeriminetthk2@gmail.com";
                 WebMail.Send(komu, "Napominanie", "ne zabud pridi");
                 ViewBag.Message = "Kiri on saatnud!";
         }*/



        public void E_mail(Guest guest)
        {

            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "programmeeriminetthk2@gmail.com";
                WebMail.Password = "2.kuursus tarpv20";
                WebMail.From = "programmeeriminetthk2@gmail.com";
                WebMail.Send("programmeeriminetthk2@gmail.com", "Vastus kutsele",guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahjul! Ei saa kirja saada!!!";
            }



        }

        MailMessage message = new MailMessage();
        public void E_mailGuest(Guest guest)
        {
                string komu = guest.Email;
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "programmeeriminetthk2@gmail.com";
                WebMail.Password = "2.kuursus tarpv20";
                WebMail.From = "programmeeriminetthk2@gmail.com";
                //WebMail.Send(komu, "Napominanie","ne zabud pridi");
                WebMail.Send(email, "Meeldetuletus", "Ärge unustage tulla puhkusele :) ");

        }

        GuestContext db = new GuestContext();
        PiduContext dpb = new PiduContext();
        [Authorize] // - Данное представление Guests сможет увидеть только авторизированный пользователь !!!!!!!!!!!!!!!!!!!!_ПОСТАВЬ ОБРАТНО КОГДА НАДО БУДЕТ, НЕ ОСТАВЛЯЙ ЗАКОММЕНТИРОВАННЫМ_!!!!!!!!!!!!!!!!
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }
        [Authorize]
        public ActionResult Pidus()
        {
            IEnumerable<Pidu> pidu = dpb.Pidus;
            return View(pidu);
        }


        /*-----------------Сreate_Guest-------------------*/
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        /*-----------------Сreate_Pidu-------------------*/
        [HttpGet]

        public ActionResult CreatePidu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePidu(Pidu pidu)
        {
            dpb.Pidus.Add(pidu);
            dpb.SaveChanges();
            return RedirectToAction("Pidus");
        }
        /*----------------Delete_Guest--------------------*/
        [HttpGet]

        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g==null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        /*----------------Delete_Pidus--------------------*/
        [HttpGet]

        public ActionResult DeletePidu(int id)
        {
            Pidu g = dpb.Pidus.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("DeletePidu")]
        public ActionResult DeleteConfirmedPidu(int id)
        {
            Pidu g = dpb.Pidus.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            dpb.Pidus.Remove(g);
            dpb.SaveChanges();
            return RedirectToAction("Pidus");
        }


        /*------------------------Edit_Guests--------------------------*/
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
        /*------------------------Edit_Pidus--------------------------*/
        [HttpGet]
        public ActionResult EditPidu(int? id)
        {
            Pidu gp = dpb.Pidus.Find(id);
            if (gp == null)
            {
                return HttpNotFound();
            }
            return View(gp);
        }
        [HttpPost, ActionName("EditPidu")]
        public ActionResult EditConfirmedPidu(Pidu pidu)
        {
            dpb.Entry(pidu).State = EntityState.Modified;
            dpb.SaveChanges();
            return RedirectToAction("Pidus");
        }
        /*---------------------------------*/
        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View(guests);
        }
        public ActionResult NoAccept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == false);
            return View(guests);
        }

    }
}