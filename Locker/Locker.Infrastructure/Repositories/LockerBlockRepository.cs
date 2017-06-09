using Locker.DomainModel;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories
{
    public class LockerBlockRepository : ILockerBlockRepository
    {
        private readonly IDbSet<LockerBlock> dbSet;

        public LockerBlockRepository(IDbSet<LockerBlock> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public void Add(LockerBlock lockerBlock)
        {
            if (lockerBlock == null) { throw new ArgumentNullException(nameof(lockerBlock)); }

            this.dbSet.Add(lockerBlock);
        }

        public IEnumerable<LockerBlock> GetAll(int traderId)
        {
            return this.dbSet.Where(l => l.Sector.TraderId == traderId).AsEnumerable();
        }

        public LockerBlock GetById(int lockerBlockId, int traderId)
        {
            return this.dbSet.Where(l => l.LockerBlockId == lockerBlockId 
            && l.Sector.TraderId == traderId).FirstOrDefault();
        }

        public LockerBlock GetLastLockerBlocker()
        {
            return this.dbSet.OrderByDescending(b => b.LockerBlockId).FirstOrDefault();
        }
    }
}
