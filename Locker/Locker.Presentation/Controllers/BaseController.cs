using Locker.DomainModel.Model;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Locker.Presentation.Controllers
{
    public class BaseController : Controller
    {
        private User LoggedUserWithoutRequest = new User();

        private readonly ILockerUnitOfWork unitOfWork;

        public BaseController(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public User LoggedUser
        {
            get
            {
                if (this.Request != null)
                {
                    HttpCookie userCookie = this.Request.Cookies.Get(this.GetLoggedUserCookieKey());

                    if (userCookie == null) { return null; }

                    int minutes = 60;

                    userCookie.Expires = DateTime.Now.AddMinutes(minutes);
                    userCookie.Domain = "localhost";
                    Response.Cookies.Set(userCookie);

                    string login = userCookie["Login"];

                    User user = this.unitOfWork.UserRepository.GetByLogin(login);

                    SetViewBagWithLoggedUser(user);

                    return user;
                }
                else { return LoggedUserWithoutRequest; }
            }
            set
            {
                if (this.Request == null)
                {
                    SetViewBagWithLoggedUser(value);
                    LoggedUserWithoutRequest = value;
                }
            }
        }

        public HttpCookie UserCookie
        {
            get
            {
                HttpCookie userCookie = this.Request.Cookies.Get(this.GetLoggedUserCookieKey());

                return userCookie;
            }
            set
            {

            }
        }

        private void SetViewBagWithLoggedUser(User user)
        {
            ViewBag.LoggedUser = user;
        }

        public string GetLoggedUserCookieKey()
        {
            return ConfigurationManager.AppSettings["LoggedUserCookie"];
        }
    }
}