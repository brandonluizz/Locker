using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locker.Infrastructure.Repositories.Interface;

namespace Locker.Presentation.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(ILockerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet, Route("pagina-principal")]
        public ActionResult Index()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }
    }
}