using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel.DTO.Reports;
using Locker.Infrastructure.Repositories.Interface;

namespace Locker.Application
{
    public class LockerReport : ILockerReport
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public LockerReport(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }

        public IList<RentalCustomerReport> GetRentalByCustomerReport(int traderId)
        {
            return this.unitOfWork.ReportRepository.GetRentalByCustomerReport(traderId);
        }
    }
}
