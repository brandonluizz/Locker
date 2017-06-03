using Locker.DomainModel.DTO.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerReportRepository
    {
        IList<RentalCustomerReport> GetRentalByCustomerReport(int traderId);
    }
}
