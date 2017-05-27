using Locker.DomainModel.Model;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories
{
    public class TraderRepository : ITraderRepository
    {
        private readonly IDbSet<Trader> dbSet;

        public TraderRepository(IDbSet<Trader> dbSet)
        {
            this.dbSet = dbSet;
        }

        public IList<Trader> GetAll()
        {
            return this.dbSet.ToList();
        }
    }
}
