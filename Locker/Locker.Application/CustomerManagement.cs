using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using Locker.DomainModel.DTO;
using Locker.Infrastructure.Repositories.Interface;

namespace Locker.Application
{
    public class CustomerManagement : ICustomerManagement
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public CustomerManagement(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public CustomerManagementResponse AddNewCustomer(Customer customer)
        {
            try
            {
                this.unitOfWork.CustomerRepository.Add(customer);

                this.unitOfWork.Commit();

                return new CustomerManagementResponse(true);
            }
            catch (Exception ex)
            {
                return new CustomerManagementResponse(false);
            }
        }

        public bool IsAlreadyExistsCustomer(string cpf)
        {
            var customer = this.unitOfWork.CustomerRepository.GetByCpf(cpf);

            return customer != null;
        }
    }
}
