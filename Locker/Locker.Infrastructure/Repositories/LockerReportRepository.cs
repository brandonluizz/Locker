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

        public IList<RentalCustomerReport> GetRentalByCustomerReport(int traderId)
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
                                        WHERE s.TraderId = {traderId}";
            
            return this.context.Database.SqlQuery<RentalCustomerReport>(query).ToList();
        }

        public IList<UsingOfLockerReport> GetUseOfLockerReport(int traderId)
        {
            var query = $@"SELECT ca.[InitialRentalDate] as Date,
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
	                            group by ca.[InitialRentalDate],
			                            lb.[TotalNumberOfLockers],
			                            sl.[SectorLocationName],
			                            s.[SectorName]";

            return this.context.Database.SqlQuery<UsingOfLockerReport>(query).ToList();
        }
    }
}
