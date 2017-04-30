using Locker.DomainModel;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}