using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO.Reports
{
    public class RentalCustomerReport
    {
        public int CustomerActivityId { get; set; }

        public int LockerId { get; set; }

        public int NumberOfPositionLocker { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string SectorName { get; set; }

        public string SectorLocationName { get; set; }

        public DateTime InitialRentalDate { get; set; }

        public DateTime FinalRentalDate { get; set; }

        public string FormattedInitialRentalDate
        {
            get
            {
                return this.InitialRentalDate.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }

        public string FormattedFinalRentalDate
        {
            get
            {
                return this.FinalRentalDate.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
    }
}
