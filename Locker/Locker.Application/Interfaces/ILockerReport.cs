using Locker.DomainModel.DTO.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application.Interfaces
{
    public interface ILockerReport
    {
        IList<RentalCustomerReport> GetRentalByCustomerReport(int traderId);
    }
}
