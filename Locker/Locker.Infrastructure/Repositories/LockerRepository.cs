using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using System.Data.SqlClient;
using Locker.DomainModel.DTO;

namespace Locker.Infrastructure.Repositories
{
    public class LockerRepository : ILockerRepository
    {
        private readonly IDbSet<DomainModel.Locker> dbSet;

        public LockerRepository(IDbSet<DomainModel.Locker> dbSet)
        {
            this.dbSet = dbSet;
        }

        public void Add(DomainModel.Locker locker)
        {
            if (locker == null) { throw new ArgumentNullException(nameof(locker)); }

            this.dbSet.Add(locker);
        }

        public void AddRange(IList<DomainModel.Locker> lockers)
        {
            foreach (var locker in lockers)
            {
                this.dbSet.Add(locker);
            }
        }

        public IEnumerable<DomainModel.Locker> GetAll(int traderId)
        {
            return this.dbSet.Where(l => l.LockerBlock.Sector.TraderId == traderId
            && l.IsActive).AsEnumerable();
        }

        public ICollection<DomainModel.Locker> GetAllLockersByLockerBlockId(int lockerBlockId, int traderId)
        {
            return this.dbSet.Where(l => l.LockerBlockId == lockerBlockId
            && l.LockerBlock.Sector.TraderId == traderId
            && l.IsActive).OrderBy(l => l.NumberOfPositionLocker).ToList();
        }

        public DomainModel.Locker GetByArduinoId(string arduinoId, int traderId)
        {
            if (string.IsNullOrWhiteSpace(arduinoId)) { throw new ArgumentNullException(nameof(arduinoId)); }

            return this.dbSet.Where(l => l.ArduinoId == arduinoId && l.LockerBlock.Sector.TraderId == traderId).FirstOrDefault();
        }

        public DomainModel.Locker GetById(int lockerId)
        {
            return this.dbSet.Where(l => l.LockerId == lockerId).FirstOrDefault();
        }

        public DomainModel.Locker GetLockersByPosition(LockerPosition lockerPosition, int traderId)
        {
            if (lockerPosition == null) { throw new ArgumentNullException(nameof(lockerPosition)); }

            return this.dbSet.Where(l => l.HorizontalPositionNumber == lockerPosition.HorizontalPositionNumber
                                            && l.VerticalPositionNumber == lockerPosition.VerticalPositionNumber
                                            && l.LockerBlockId == lockerPosition.LockerBlockId
                                            && l.LockerBlock.Sector.TraderId == traderId
                                            && l.IsActive).FirstOrDefault();
        }
    }
}
