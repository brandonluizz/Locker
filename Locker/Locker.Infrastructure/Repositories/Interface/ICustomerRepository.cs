﻿using Locker.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);

        Customer GetByCpf(string cpf);

        IEnumerable<Customer> GetAllCustomers(int traderId);

        void Remove(Customer customer);

        Customer GetByTagUid(string taguid, int traderId);
    }
}
