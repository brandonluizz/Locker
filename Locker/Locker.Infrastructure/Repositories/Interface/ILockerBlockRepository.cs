using Locker.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerBlockRepository
    {
        void Add(LockerBlock lockerBlock);

        IEnumerable<LockerBlock> GetAll(int traderId);

        LockerBlock GetById(int lockerBlockId, int traderId);

        LockerBlock GetLastLockerBlocker();
    }
}
