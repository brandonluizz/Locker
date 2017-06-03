using Locker.Application.Interfaces;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class LockerReportController : BaseController
    {
        private ILockerReport report;

        public LockerReportController(ILockerUnitOfWork unitOfWork, ILockerReport report) : base(unitOfWork)
        {
            this.report = report;
        }

        public ActionResult Index()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpGet, Route("relatorio/locacao-por-cliente")]
        public ActionResult RentalByCustomerReport()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpGet]
        public JsonResult GetRentalByCustomerReport()
        {
            var response = this.report.GetRentalByCustomerReport(this.LoggedUser.TraderId);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("relatorio/utilizacao-de-armarios")]
        public ActionResult UsingOfLockerReport()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpGet]
        public JsonResult GetUsingOfLockerReport()
        {
            var response = this.report.GetUsingLockerReport(this.LoggedUser.TraderId);

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}