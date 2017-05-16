using Locker.Application.Interfaces;
using Locker.DomainModel.DTO;
using System;
using System.Web.Mvc;
using Locker.DomainModel.Model;
using Locker.Infrastructure.Repositories.Interface;

namespace Locker.Presentation.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserAccessManagement userAccessManagement;

        public HomeController(IUserAccessManagement userAccessManagement, 
            ILockerUnitOfWork unitOfWork): base (unitOfWork)
        {
            this.userAccessManagement = userAccessManagement ?? throw new ArgumentNullException(nameof(userAccessManagement));
        }

        public ActionResult Index()
        {
            if (this.LoggedUser != null) { return RedirectToAction("Index", "Dashboard"); }

            return View();
        }

        [HttpGet, Route("sair")]
        public ActionResult Logoff()
        {
            if(this.UserCookie == null) { return RedirectToAction("Index"); }

            this.UserCookie.Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies.Set(this.UserCookie);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DoLogin(UserAccess userAccess)
        {
            var response = this.userAccessManagement.DoLogin(userAccess);

            if (response.Success)
            {
                var userCookie = this.userAccessManagement.CreateUserCookie(response.User);

                if (Request.Cookies[this.userAccessManagement.GetLoggedUserCookieKey()] != null) { Response.Cookies.Set(userCookie); }
                else { Response.Cookies.Add(userCookie); }

                this.LoggedUser = response.User;

                this.SetViewBagWithLoggedUser(response.User);
            }
            
            return Json(response);
        }

        private void SetViewBagWithLoggedUser(User user)
        {
            ViewBag.LoggedUser = user;
        }
    }
}