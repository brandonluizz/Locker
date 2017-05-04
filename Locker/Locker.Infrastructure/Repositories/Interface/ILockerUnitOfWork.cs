using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }

        IUserRepository UserRepository { get; }

        void Dispose();

        void Commit();
    }
}
