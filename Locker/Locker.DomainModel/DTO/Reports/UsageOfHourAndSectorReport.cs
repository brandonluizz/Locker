using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO.Reports
{
    public class UsageOfHourAndSectorReport
    {
        public string SectorName { get; set; }

        public int Hour { get; set; }

        public int TotalOfActivities { get; set; }
    }
}
