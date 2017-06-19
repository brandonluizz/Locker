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

        public IEnumerable<Customer> GetAllCustomers(int traderId)
        {
            return this.dbSet.Where(c => c.TraderId == traderId).AsEnumerable();
        }

        public Customer GetByCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) { throw new ArgumentNullException(nameof(cpf)); }

            return this.dbSet.FirstOrDefault(c => c.CustomerCpf == cpf);
        }

        public Customer GetByTagUid(string taguid, int traderId)
        {
            if (string.IsNullOrWhiteSpace(taguid)) { throw new ArgumentNullException(taguid); }

            return this.dbSet.Where(c => c.TagUID == taguid && c.TraderId == traderId).FirstOrDefault();
        }

        public void Remove(Customer customer)
        {
            if (customer == null) { return; }

            this.dbSet.Remove(customer);
        }
    }
}
