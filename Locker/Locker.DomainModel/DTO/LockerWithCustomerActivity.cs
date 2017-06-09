using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO
{
    public class LockerWithCustomerActivity
    {
        public LockerWithCustomerActivity(Locker locker, CustomerActivity customerActivity)
        {
            this.Locker = locker;
            this.CustomerActivity = customerActivity;
        }

        public Locker Locker { get; set; }

        public CustomerActivity CustomerActivity { get; set; }
    }
}
