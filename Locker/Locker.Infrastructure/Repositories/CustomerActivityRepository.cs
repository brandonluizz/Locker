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
    public class CustomerActivityRepository : ICustomerActivityRepository
    {
        private readonly IDbSet<CustomerActivity> dbSet;

        public CustomerActivityRepository(IDbSet<CustomerActivity> dbSet)
        {
            this.dbSet = dbSet ?? throw new ArgumentNullException(nameof(dbSet));
        }

        public void Remove(Customer customer)
        {
            var activities = this.GetAllByCustomer(customer);

            foreach (var activity in activities)
            {
                this.dbSet.Remove(activity);
            }            
        }

        private IEnumerable<CustomerActivity> GetAllByCustomer(Customer customer)
        {
            return this.dbSet.Where(a => a.CustomerId == customer.CustomerId).AsEnumerable();
        }
    }
}
