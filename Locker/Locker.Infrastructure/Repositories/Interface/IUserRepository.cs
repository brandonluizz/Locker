using Locker.DomainModel.DTO;
using Locker.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface IUserRepository
    {
        User GetUserByDataAccess(UserAccess userAccess);

        User GetByLogin(string login);
    }
}
