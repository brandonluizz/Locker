using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO.Reports
{
    public class UsingOfLockerReport
    {
        public DateTime Date { get; set; }
        
        public string SectorLocationName { get; set; }

        public string SectorName { get; set; }

        public int TotalNumberOfLockers { get; set; }

        public int TotalLockersOfUsing { get; set; }

        public int PercentageOfUse { get; set; }

        public string FormattedPercentageOfUse
        {
            get
            {
                return this.PercentageOfUse.ToString() + "%";
            }
        }

        public string FormattedDate
        {
            get
            {
                return this.Date.ToString("dd-MM-yyyy");
            }
        }
    }
}
