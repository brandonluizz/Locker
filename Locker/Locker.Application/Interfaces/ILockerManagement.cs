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

        IList<LockerBlock> GetAllLockerBlocks(int traderId);

        LockerManagementResponse AddNewLocker(DomainModel.Locker locker);

        IList<DomainModel.Locker> GetAllLockers(int traderId);

        LockerManagementResponse IsAvailableLockerBlock(int lockerBlockId, int traderId);

        LockerManagementResponse IsAvailableLockerPosition(LockerPosition lockerPosition, int traderId);

        LockerManagementResponse AddArduinoDataInLocker(DomainModel.Locker locker);
    }
}
