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

        public void Add(CustomerActivity customerActivity)
        {
            if (customerActivity == null) { throw new ArgumentNullException(nameof(customerActivity)); }

            this.dbSet.Add(customerActivity);
        }

        public IEnumerable<CustomerActivity> GetAll(int traderId)
        {
            return this.dbSet.Where(c => c.Customer.TraderId == traderId).ToList();
        }

        public CustomerActivity GetByCustomer(Customer customer)
        {
            if (customer == null) { throw new ArgumentNullException(nameof(customer)); }

            return this.dbSet.Where(ca => ca.CustomerId == customer.CustomerId
            && ca.FinalRentalDate == null).FirstOrDefault();
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
