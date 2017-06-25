using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel.DTO.Reports;
using Locker.Infrastructure.EntityFramework;
using System.Data.SqlClient;

namespace Locker.Infrastructure.Repositories
{
    public class LockerReportRepository : ILockerReportRepository
    {
        private LockerContext context;

        public LockerReportRepository(LockerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<RentalCustomerReport> GetRentalByCustomerReport(int traderId)
        {
            string query = $@"SELECT		ca.[CustomerActivityId],
			                            ca.[LockerId],
			                            l.[NumberOfPositionLocker],
			                            ca.[CustomerId],
			                            c.[CustomerName],
			                            s.[SectorName],
			                            sl.[SectorLocationName],
			                            ca.[InitialRentalDate],
			                            ca.[FinalRentalDate]
		                            FROM [dbo].[CustomerActivity] AS ca
			                            INNER JOIN [dbo].[Customer] AS c
			                            ON ca.[CustomerId] = c.[CustomerId]
			                            INNER JOIN [dbo].[Locker] AS l
			                            ON ca.[LockerId] = l.[LockerId]
			                            INNER JOIN [dbo].[LockerBlock] AS lb
			                            ON l.[LockerBlockId] = lb.[LockerBlockId]
			                            INNER JOIN [dbo].[Sector] AS s
			                            ON lb.[SectorId] = s.[SectorId]
			                            INNER JOIN [dbo].[SectorLocation] AS sl
			                            ON s.[SectorLocationId] = sl.[SectorLocationId]
                                        WHERE s.TraderId = {traderId}
                                        and ca.[FinalRentalDate] is not null";

            return this.context.Database.SqlQuery<RentalCustomerReport>(query).AsEnumerable();
        }

        public IEnumerable<UsageOfClientReport> GetUsageByClient(int traderId, string initialDate, string finalDate)
        {
            var query = $@"select c.CustomerId, c.CustomerName, sum((case when ca.CustomerId is not null then 1 else 0 end)) as TotalOfActivities
			                    from Customer c
			                    left join CustomerActivity ca on
			                    c.CustomerId = ca.CustomerId
			                    and c.TraderId = {traderId}
			                    and cast(ca.InitialRentalDate as date) between @initialDate and @finalDate
		                    group by c.CustomerName,
					                    c.CustomerId";

            SqlParameter[] parameters = new SqlParameter[]
             {
                 new SqlParameter("initialDate", initialDate),
                 new SqlParameter("finalDate", finalDate)
             };

            return this.context.Database.SqlQuery<UsageOfClientReport>(query, parameters).ToList();
        }

        public ICollection<UsageOfSectorReport> GetUsageOfSectorReport(int traderId, string initialDate, string finalDate)
        {
            string query = $@"select s.SectorName, count(1) as TotalOfActivitiesBySector
                                from CustomerActivity ca
	                                INNER JOIN [dbo].[Locker] AS l
	                                ON ca.[LockerId] = l.[LockerId]
	                                INNER JOIN [dbo].[LockerBlock] AS lb
	                                ON l.[LockerBlockId] = lb.[LockerBlockId]
	                                INNER JOIN [dbo].[Sector] AS s
	                                ON lb.[SectorId] = s.[SectorId]
	                                INNER JOIN [dbo].[SectorLocation] AS sl
	                                ON s.[SectorLocationId] = sl.[SectorLocationId]
	                                where s.TraderId = {traderId}
	                                and cast(ca.InitialRentalDate as date) between @initialDate and @finalDate
                                group by s.SectorName";

            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("initialDate", initialDate),
                 new SqlParameter("finalDate", finalDate)
            };

            return this.context.Database.SqlQuery<UsageOfSectorReport>(query, parameters).ToList();
        }

        public IEnumerable<UsingOfLockerReport> GetUseOfLockerReport(int traderId)
        {
            var query = $@"SELECT CAST(ca.InitialRentalDate as date) as 'Date',
SUBSTRING(CONVERT(VARCHAR,CAST(ca.InitialRentalDate as time)),1,2) as 'Hour',
                            sl.[SectorLocationName],
                            s.[SectorName],
                            lb.[TotalNumberOfLockers],
                            count( 1 ) as 'TotalLockersOfUsing',
                            PercentageOfUse = ((count(1)*100)/lb.[TotalNumberOfLockers])
	                            FROM [dbo].[CustomerActivity] AS ca
		                            INNER JOIN [dbo].[Locker] AS l
		                            ON ca.[LockerId] = l.[LockerId]
		                            INNER JOIN [dbo].[LockerBlock] AS lb
		                            ON l.[LockerBlockId] = lb.[LockerBlockId]
		                            INNER JOIN [dbo].[Sector] AS s
		                            ON lb.[SectorId] = s.[SectorId]
		                            INNER JOIN [dbo].[SectorLocation] AS sl
		                            ON s.[SectorLocationId] = sl.[SectorLocationId]
		                            WHERE s.TraderId = {traderId}
	                            group by CAST(ca.[InitialRentalDate] as date),
										SUBSTRING(CONVERT(VARCHAR,CAST(ca.[InitialRentalDate] as time)),1,2),
			                            lb.[TotalNumberOfLockers],
			                            sl.[SectorLocationName],
			                            s.[SectorName]";

            return this.context.Database.SqlQuery<UsingOfLockerReport>(query).AsEnumerable();
        }

        public IEnumerable<UsageOfHourAndSectorReport> UsageOfHourAndSector(int traderId, string initialDate, string finalDate)
        {
            string query = $@"select s.SectorName, datepart(hour, ca.InitialRentalDate) as Hour, count(1) as TotalOfActivities
                                    from CustomerActivity ca
	                                    INNER JOIN [dbo].[Locker] AS l
	                                    ON ca.[LockerId] = l.[LockerId]
	                                    INNER JOIN [dbo].[LockerBlock] AS lb
	                                    ON l.[LockerBlockId] = lb.[LockerBlockId]
	                                    INNER JOIN [dbo].[Sector] AS s
	                                    ON lb.[SectorId] = s.[SectorId]
	                                    INNER JOIN [dbo].[SectorLocation] AS sl
	                                    ON s.[SectorLocationId] = sl.[SectorLocationId]
	                                    and ca.InitialRentalDate between @initialDate and @finalDate
                                    group by s.SectorName,
                                    datepart(hour, ca.InitialRentalDate)";

            SqlParameter[] parameters = new SqlParameter[]
               {
                     new SqlParameter("initialDate", initialDate),
                     new SqlParameter("finalDate", finalDate)
               };

            return this.context.Database.SqlQuery<UsageOfHourAndSectorReport>(query, parameters).ToList();
        }
    }
}
