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

        public void Add(SectorLocation sectorLocation)
        {
            if (sectorLocation == null) { throw new ArgumentNullException(nameof(sectorLocation)); }

            this.dbSet.Add(sectorLocation);
        }

        public IList<SectorLocation> GetSectorLocations(int traderId)
        {
            return this.dbSet.Where(sl => sl.TraderId == traderId).ToList();
        }
    }
}
