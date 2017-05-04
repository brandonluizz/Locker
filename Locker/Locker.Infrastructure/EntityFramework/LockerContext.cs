using Locker.DomainModel;
using Locker.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.EntityFramework
{
    public class LockerContext: DbContext
    {
        public LockerContext() : base("name=LockerConnection")
        {
        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<CustomerActivity> CustomerActivity { get; set; }

        public DbSet<Sector> Sector { get; set; }

        public DbSet<SectorLocation> SectorLocation { get; set; }

        public DbSet<DomainModel.Locker> Locker { get; set; }

        public DbSet<LockerBlock> LockerBlock { get; set; }

        public DbSet<User> User { get; set; }
    }
}
