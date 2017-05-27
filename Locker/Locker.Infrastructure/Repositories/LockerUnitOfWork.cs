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
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.context.User);
                }

                return this.userRepository;
            }
        }

        private LockerRepository lockerRepository;

        public ILockerRepository LockerRepository
        {
            get
            {
                if (this.lockerRepository == null)
                {
                    this.lockerRepository = new LockerRepository(this.context.Locker);
                }

                return this.lockerRepository;
            }
        }

        private LockerBlockRepository lockerBlockRepository;

        public ILockerBlockRepository LockerBlockRepository
        {
            get
            {
                if (this.lockerBlockRepository == null)
                {
                    this.lockerBlockRepository = new LockerBlockRepository(this.context.LockerBlock);
                }

                return lockerBlockRepository;
            }
        }

        private CustomerActivityRepository customerActivityRepository;

        public ICustomerActivityRepository CustomerActivityRepository
        {
            get
            {
                if (this.customerActivityRepository == null)
                {
                    this.customerActivityRepository = new CustomerActivityRepository(this.context.CustomerActivity);
                }

                return this.customerActivityRepository;
            }
        }

        private SectorRepository sectorRepostory;

        public ISectorRepository SectorRepository
        {
            get
            {
                if (this.sectorRepostory == null)
                {
                    this.sectorRepostory = new SectorRepository(this.context.Sector);
                }

                return this.sectorRepostory;
            }
        }

        private SectorLocationRepository sectorLocationRepository;

        public ISectorLocationRepository SectorLocationRepository
        {
            get
            {
                if (this.sectorLocationRepository == null)
                {
                    this.sectorLocationRepository = new SectorLocationRepository(this.context.SectorLocation);
                }

                return this.sectorLocationRepository;
            }
        }

        private TraderRepository traderRepository;

        public ITraderRepository TraderRepository
        {
            get
            {
                if (this.traderRepository == null)
                {
                    this.traderRepository = new TraderRepository(this.context.Trader);
                }

                return this.traderRepository;
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
