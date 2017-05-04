using Locker.Infrastructure.EntityFramework;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories
{
    public class LockerUnitOfWork : ILockerUnitOfWork
    {
        private readonly LockerContext context;

        public LockerUnitOfWork(LockerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private CustomerRepository customerRepository;

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (this.customerRepository == null)
                {
                    this.customerRepository = new CustomerRepository(this.context.Customer);
                }

                return this.customerRepository;
            }
        }

        private UserRepository userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context.User);
                }

                return this.userRepository;
            }
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
