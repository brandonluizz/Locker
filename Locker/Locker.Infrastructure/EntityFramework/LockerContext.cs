using Locker.DomainModel;
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

        public DbSet<User> User { get; set; }

        public DbSet<UserActivity> UserActivity { get; set; }

        public DbSet<Sector> Sector { get; set; }

        public DbSet<SectorLocation> SectorLocation { get; set; }

        public DbSet<DomainModel.Locker> Locker { get; set; }

        public DbSet<LockerBlock> LockerBlock { get; set; }
    }
}
