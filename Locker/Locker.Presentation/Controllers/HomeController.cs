using Locker.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private ITeste teste;

        public HomeController(ITeste teste)
        {
            this.teste = teste ?? throw new ArgumentNullException(nameof(teste));
        }

        public ActionResult Index()
        {
            teste.AddNewUser();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}