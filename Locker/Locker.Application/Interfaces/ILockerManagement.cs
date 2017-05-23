using Locker.DomainModel;
using Locker.DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application.Interfaces
{
    public interface ILockerManagement
    {
        LockerManagementResponse AddNewLockerBlock(LockerBlock lockerBlock);

        IList<LockerBlock> GetAllLockerBlocks();

        LockerManagementResponse AddNewLocker(DomainModel.Locker locker);

        IList<DomainModel.Locker> GetAllLockers();

        LockerManagementResponse IsAvailableLockerBlock(int lockerBlockId);

        LockerManagementResponse IsAvailableLockerPosition(LockerPosition lockerPosition);
    }
}
