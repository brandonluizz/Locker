using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel.Model;
using System.Data.Entity;
using Locker.DomainModel.DTO;

namespace Locker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbSet<User> dbSet;

        public UserRepository(IDbSet<User> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet)); ;
        }

        public User GetByLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) { throw new ArgumentNullException(nameof(login)); }

            return this.dbSet.Where(u => u.Login == login).FirstOrDefault();
        }

        public User GetUserByDataAccess(UserAccess userAccess)
        {
            if (userAccess == null) { throw new ArgumentNullException(nameof(userAccess)); }

            return this.dbSet.FirstOrDefault(u => u.Login == userAccess.Login
            && u.Password == userAccess.Password);
        }
    }
}
