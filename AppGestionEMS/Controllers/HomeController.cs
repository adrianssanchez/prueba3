using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppGestionEMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de la página.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Información de contacto.";

            return View();
        }
    }
}