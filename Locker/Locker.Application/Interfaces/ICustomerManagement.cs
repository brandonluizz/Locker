using Locker.DomainModel;
using Locker.DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application.Interfaces
{
    public interface ICustomerManagement
    {
        CustomerManagementResponse AddNewCustomer(Customer customer);

        bool IsAlreadyExistsCustomer(string cpf);

        IEnumerable<Customer> GetAllCustomer(int traderId);

        CustomerManagementResponse EditCustomer(Customer customer);

        CustomerManagementResponse RemoveCustomer(string cpf);
    }
}
