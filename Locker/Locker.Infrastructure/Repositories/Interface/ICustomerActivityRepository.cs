using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ICustomerActivityRepository
    {
        void Remove(Customer customer);

        IEnumerable<CustomerActivity> GetAll(int traderId);

        CustomerActivity GetByCustomer(Customer customer);

        void Add(CustomerActivity customerActivity);
    }
}
