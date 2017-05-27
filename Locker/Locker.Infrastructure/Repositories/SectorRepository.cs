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
    public class SectorRepository : ISectorRepository
    {
        private readonly IDbSet<Sector> dbSet;

        public SectorRepository(IDbSet<Sector> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public void Add(Sector sector)
        {
            if (sector == null) { throw new ArgumentNullException(nameof(sector)); }

            this.dbSet.Add(sector);
        }

        public IList<Sector> GetAll(int traderId)
        {
            return this.dbSet.Where(s => s.TraderId == traderId).ToList();
        }
    }
}
