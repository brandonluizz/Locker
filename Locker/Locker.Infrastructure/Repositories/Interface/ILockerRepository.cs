using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerRepository
    {
        void Add(DomainModel.Locker locker);

        IList<DomainModel.Locker> GetAll();

        IList<DomainModel.Locker> GetAllLockersByLockerBlockId(int lockerBlockId);
    }
}
