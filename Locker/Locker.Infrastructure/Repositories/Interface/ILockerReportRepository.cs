﻿using Locker.DomainModel.DTO.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ILockerReportRepository
    {
        IEnumerable<RentalCustomerReport> GetRentalByCustomerReport(int traderId);

        IEnumerable<UsingOfLockerReport> GetUseOfLockerReport(int traderId);

        ICollection<UsageOfSectorReport> GetUsageOfSectorReport(int traderId, string initialDate, string finalDate);

        IEnumerable<UsageOfClientReport> GetUsageByClient(int traderId, string initialDate, string finalDate);

        IEnumerable<UsageOfHourAndSectorReport> UsageOfHourAndSector(int traderId, string initialDate, string finalDate);
    }
}
