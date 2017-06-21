using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO.Reports
{
    public class UsageOfSectorReport
    {
        public string SectorName { get; set; }
        
        public int TotalOfActivitiesBySector { get; set; }

        public decimal PercentageOfUse { get; set; }

        public int TotalOfActivities { get; set; }
    }
}
