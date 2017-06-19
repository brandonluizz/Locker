using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel.Model;
using System.Web;
using Locker.Infrastructure.Repositories.Interface;
using Locker.DomainModel.DTO;
using System.Configuration;

namespace Locker.Application
{
    public class UserAccessManagement : IUserAccessManagement
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public UserAccessManagement(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public UserAccessResponse DoLogin(UserAccess userAccess)
        {
            try
            {
                var user = this.unitOfWork.UserRepository.GetUserByDataAccess(userAccess);

                if (user == null) { return new UserAccessResponse(false); }

                return new UserAccessResponse(user, true);
            }
            catch (Exception ex)
            {
                return new UserAccessResponse(false);
            }            
        }

        public HttpCookie CreateUserCookie(User user)
        {
            var cookie = new HttpCookie(this.GetLoggedUserCookieKey())
            {
                Domain = "locker3478.cloudapp.net",
                Path = "/",
                Expires = DateTime.Now.AddMinutes(60),
                Shareable = true
            };

            cookie["Login"] = user.Login;
            cookie["Email"] = user.Email;
            cookie["Name"] = user.UserName;

            return cookie;
        }

        public string GetLoggedUserCookieKey()
        {
            return ConfigurationManager.AppSettings["LoggedUserCookie"];
        }
    }
}
