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
    public class UserRepository : IUserRepository
    {
        private readonly IDbSet<User> dbSet;

        public UserRepository(IDbSet<User> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public void Add(User user)
        {
            if (user == null) { throw new ArgumentNullException(nameof(user)); }

            this.dbSet.Add(user);
        }
    }
}
