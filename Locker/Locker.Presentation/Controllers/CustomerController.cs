using Locker.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Locker.Infrastructure.Repositories.Interface;
using Locker.Application.Interfaces;

namespace Locker.Presentation.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerManagement customerManagement;

        public CustomerController(ILockerUnitOfWork unitOfWork, ICustomerManagement customerManagement) : base(unitOfWork)
        {
            this.customerManagement = customerManagement;
        }

        [HttpGet, Route("meus-clientes")]
        public ActionResult Index()
        {
            if (this.LoggedUser == null) { return RedirectToAction("Index", "Home"); }

            return View();
        }

        [HttpPost]
        public JsonResult AddNewCustomer(Customer customer)
        {
            customer.TraderId = this.LoggedUser.TraderId;

            var response = this.customerManagement.AddNewCustomer(customer);

            return Json(response);
        }

        [HttpPost]
        public JsonResult IsAlreadyExistsCustomer(string cpf)
        {
            var response = this.customerManagement.IsAlreadyExistsCustomer(cpf);

            return Json(response);
        }
    }
}