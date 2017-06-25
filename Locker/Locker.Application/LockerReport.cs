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

        public IEnumerable<RentalCustomerReport> GetRentalByCustomerReport(int traderId)
        {
            return this.unitOfWork.ReportRepository.GetRentalByCustomerReport(traderId);
        }

        public ICollection<UsageOfSectorReport> GetUsageOfSectorReport(int traderId, string initialDateString, string finalDateString)
        {           
            string initialDate = this.ConverStringToDatetime(initialDateString);
            string finalDate = this.ConverStringToDatetime(finalDateString);

            var report =  this.unitOfWork.ReportRepository.GetUsageOfSectorReport(traderId, initialDate, finalDate);

            this.SetPorcentageOfReport(report);

            return report;
        }

        private void SetPorcentageOfReport(ICollection<UsageOfSectorReport> report)
        {
            var totalOfActivities = report.Select(r => r.TotalOfActivitiesBySector).Sum();

            foreach (var item in report)
            {
                item.TotalOfActivities = totalOfActivities;
                item.PercentageOfUse = ((decimal)item.TotalOfActivitiesBySector / totalOfActivities) * 100;
            }
        }

        private string ConverStringToDatetime(string initialDateString)
        {
            var date = Convert.ToDateTime(initialDateString);

            string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");

            return formattedDate;
        }

        public IEnumerable<UsingOfLockerReport> GetUsingLockerReport(int traderId)
        {
            return this.unitOfWork.ReportRepository.GetUseOfLockerReport(traderId);
        }

        public IEnumerable<UsageOfClientReport> GetUsageOfClientReport(int traderId, string initialDateString, string finalDateString)
        {
            string initialDate = this.ConverStringToDatetime(initialDateString);
            string finalDate = this.ConverStringToDatetime(finalDateString);

            var report = this.unitOfWork.ReportRepository.GetUsageByClient(traderId, initialDate, finalDate);
            
            return report;
        }

        public IEnumerable<UsageOfHourAndSectorReport> GetUsageOfHourAndSectorReport(int traderId, string initialDateString, string finalDateString)
        {
            string initialDate = this.ConverStringToDatetime(initialDateString);
            string finalDate = this.ConverStringToDatetime(finalDateString);

            var report = this.unitOfWork.ReportRepository.UsageOfHourAndSector(traderId, initialDate, finalDate);

            return report;
        }
    }
}
