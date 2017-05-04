using Locker.DomainModel.DTO;
using Locker.DomainModel.Model;
using System.Web;

namespace Locker.Application.Interfaces
{
    public interface IUserAccessManagement
    {
        UserAccessResponse DoLogin(UserAccess userAccess);

        HttpCookie CreateUserCookie(User user);

        string GetLoggedUserCookieKey(); 
    }
}
