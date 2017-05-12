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
    public class SectorLocationRepository : ISectorLocationRepository
    {
        private readonly IDbSet<SectorLocation> dbSet;

        public SectorLocationRepository(IDbSet<SectorLocation> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }
    }
}
