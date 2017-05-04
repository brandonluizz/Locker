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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbSet<Customer> dbSet;

        public CustomerRepository(IDbSet<Customer> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public void Add(Customer customer)
        {
            if (customer == null) { throw new ArgumentNullException(nameof(customer)); }

            this.dbSet.Add(customer);
        }
    }
}
