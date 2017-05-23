using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO
{
    public class LockerPosition
    {
        public int LockerBlockId { get; set; }

        public int HorizontalPositionNumber { get; set; }

        public int VerticalPositionNumber { get; set; }
    }
}
