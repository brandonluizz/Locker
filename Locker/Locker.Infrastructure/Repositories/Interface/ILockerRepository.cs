using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using Locker.DomainModel.DTO;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerRepository
    {
        void Add(DomainModel.Locker locker);

        IEnumerable<DomainModel.Locker> GetAll(int traderId);

        ICollection<DomainModel.Locker> GetAllLockersByLockerBlockId(int lockerBlockId, int traderId);

        DomainModel.Locker GetLockersByPosition(LockerPosition lockerPosition, int traderId);

        void AddRange(IList<DomainModel.Locker> lockers);

        DomainModel.Locker GetById(int lockerId);

        DomainModel.Locker GetByArduinoId(string arduinoId, int traderId);
    }
}
