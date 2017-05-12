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
    public class LockerBlockRepository: ILockerBlockRepository
    {
        private readonly IDbSet<LockerBlock> dbSet;

        public LockerBlockRepository(IDbSet<LockerBlock> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }
    }
}
