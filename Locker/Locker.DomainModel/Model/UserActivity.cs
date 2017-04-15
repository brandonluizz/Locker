using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel
{
    public class UserActivity
    {
        public int UserActivityId { get; set; }

        public int LockerId { get; set; }

        public DateTime InitialRentalDate { get; set; }

        public DateTime FInalRentalDate { get; set; }

        public virtual Locker Locker { get; set; }

        public virtual User User { get; set; }
    }
}
