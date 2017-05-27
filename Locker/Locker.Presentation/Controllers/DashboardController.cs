using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locker.Infrastructure.Repositories.Interface;
using Locker.Application.Interfaces;

namespace Locker.Presentation.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILockerManagement lockerManagement;

        public DashboardController(ILockerUnitOfWork unitOfWork, ILockerManagement lockerManagement) : base(unitOfWork)
        {
            this.lockerManagement = lockerManagement;
        }

        [HttpGet, Route("pagina-principal")]
        public ActionResult Index()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpGet]
        public JsonResult GetAllLockers()
        {
            var lockers = this.lockerManagement.GetAllLockers(this.LoggedUser.TraderId);

            return Json(lockers, JsonRequestBehavior.AllowGet);
        }
    }
}