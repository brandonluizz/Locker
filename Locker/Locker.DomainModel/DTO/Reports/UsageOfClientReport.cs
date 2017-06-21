using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO.Reports
{
    public class UsageOfClientReport
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int TotalOfActivities { get; set; }
    }
}
