using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel
{
    public class Locker
    {
        public int LockerId { get; set; }

        public int SectorId { get; set; }

        public int LockerBlockId { get; set; }

        public int VerticalPositionNumber { get; set; }

        public int HorizontalPositionNumber { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual LockerBlock LockerBLock { get; set; }
    }
}
