using Locker.Application.Interfaces;
using Locker.DomainModel.DTO;
using System;
using System.Web.Mvc;
using Locker.DomainModel.Model;

namespace Locker.Presentation.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserAccessManagement userAccessManagement;

        public HomeController(IUserAccessManagement userAccessManagement)
        {
            this.userAccessManagement = userAccessManagement ?? throw new ArgumentNullException(nameof(userAccessManagement));
        }

        public ActionResult Index()
        {
            return View();
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
            
            return Json(response.Success);
        }

        private void SetViewBagWithLoggedUser(User user)
        {
            ViewBag.LoggedUser = user;
        }
    }
}