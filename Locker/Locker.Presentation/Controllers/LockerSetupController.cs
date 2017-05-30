using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locker.Infrastructure.Repositories.Interface;
using Locker.Application.Interfaces;
using Locker.DomainModel;
using Locker.DomainModel.DTO;

namespace Locker.Presentation.Controllers
{
    public class LockerSetupController : BaseController
    {
        private readonly ISectorManagement sectorManagement;
        private readonly ILockerManagement lockerManagement;

        public LockerSetupController(ILockerUnitOfWork unitOfWork, ISectorManagement sectorManagement,
            ILockerManagement lockerManagement) : base(unitOfWork)
        {
            this.sectorManagement = sectorManagement;
            this.lockerManagement = lockerManagement;
        }

        [HttpGet, Route("configuracao")]
        public ActionResult Index()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpGet]
        public JsonResult GetSectorLocations()
        {
            var sectorLocations = this.sectorManagement.GetSectorLocations(this.LoggedUser.TraderId);

            return Json(sectorLocations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNewSector(Sector request)
        {
            request.SetTraderId(this.LoggedUser.TraderId);

            var respose = this.sectorManagement.AddNewSector(request);

            return Json(respose);
        }

        [HttpGet]
        public JsonResult GetAllSectors()
        {
            var sectors = this.sectorManagement.GetSectors(this.LoggedUser.TraderId);

            return Json(sectors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddNewLockerBlock(LockerBlock request)
        {
            var response = this.lockerManagement.AddNewLockerBlock(request);

            return Json(response);
        }

        [HttpPost]
        public JsonResult AddNewLocker(DomainModel.Locker request)
        {
            var response = this.lockerManagement.AddNewLocker(request);

            return Json(response);
        }

        [HttpGet]
        public JsonResult GetAllLockerBlocks()
        {
            var lockerBlocks = this.lockerManagement.GetAllLockerBlocks();

            return Json(lockerBlocks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllLockers()
        {
            var lockers = this.lockerManagement.GetAllLockers(this.LoggedUser.TraderId);

            return Json(lockers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsAvailableLockerBlock(int lockerBlockId)
        {
            var response = this.lockerManagement.IsAvailableLockerBlock(lockerBlockId, this.LoggedUser.TraderId);

            return Json(response);
        }

        [HttpPost]
        public JsonResult IsAvailableLockerPosition(LockerPosition lockerPosition)
        {
            var response = this.lockerManagement.IsAvailableLockerPosition(lockerPosition, this.LoggedUser.TraderId);

            return Json(response);
        }

        [HttpPost]
        public JsonResult AddNewSectorLocation(SectorLocation request)
        {
            request.TraderId = this.LoggedUser.TraderId;

            var response = this.sectorManagement.AddNewSectorLocation(request);

            return Json(response);
        }
    }
}