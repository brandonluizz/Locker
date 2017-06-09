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
                customer.FormattCpj();

                this.unitOfWork.CustomerRepository.Add(customer);

                this.unitOfWork.Commit();

                return new CustomerManagementResponse(true);
            }
            catch (Exception ex)
            {
                return new CustomerManagementResponse(false);
            }
        }

        public CustomerManagementResponse EditCustomer(Customer customer)
        {
            try
            {
                var myCustomer = this.unitOfWork.CustomerRepository.GetByCpf(customer.CustomerCpf);

                this.SetNewDataInCustomer(myCustomer, customer);

                this.unitOfWork.Commit();

                return new CustomerManagementResponse(true);
            }
            catch (Exception)
            {
                return new CustomerManagementResponse(false);
            }
        }

        private void SetNewDataInCustomer(Customer myCustomer, Customer customer)
        {
            myCustomer.CustomerName = customer.CustomerName;
            myCustomer.BirthDate = customer.BirthDate;
            myCustomer.TagUID = customer.TagUID;
        }

        public IEnumerable<Customer> GetAllCustomer(int traderId)
        {
            return this.unitOfWork.CustomerRepository.GetAllCustomers(traderId);
        }

        public bool IsAlreadyExistsCustomer(string cpf)
        {
            var customer = this.unitOfWork.CustomerRepository.GetByCpf(cpf);

            return customer != null;
        }

        public CustomerManagementResponse RemoveCustomer(string cpf)
        {
            try
            {
                var customer = this.unitOfWork.CustomerRepository.GetByCpf(cpf);

                this.unitOfWork.CustomerActivityRepository.Remove(customer);
                this.unitOfWork.CustomerRepository.Remove(customer);
                this.unitOfWork.Commit();

                return new CustomerManagementResponse(true);
            }
            catch (Exception)
            {
                return new CustomerManagementResponse(true);
            }
        }
    }
}
