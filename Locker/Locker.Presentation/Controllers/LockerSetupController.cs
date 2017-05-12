using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class LockerSetupController : Controller
    {
        [HttpGet, Route("configuracao")]
        public ActionResult Index()
        {
            return View();
        }
    }
}