using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class ArduinoComunnicationController : Controller
    {
        private readonly IArduinoCommunicatorManager communicatorManager;

        public ArduinoComunnicationController(IArduinoCommunicatorManager communicatorManager)
        {
            if (communicatorManager == null) { throw new ArgumentNullException(nameof(communicatorManager)); }

            this.communicatorManager = communicatorManager;
        }

        [HttpGet, Route("RentalRegistratation/{taguid}/{arduinoId}/{traderId}")]
        public JsonResult RentalRegistratation(string taguid, string arduinoId, string traderId)
        {
            try
            {
                var response = this.communicatorManager.RentalRecorder(taguid, arduinoId, traderId);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}