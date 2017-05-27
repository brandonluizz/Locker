using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using Locker.Infrastructure.Repositories.Interface;
using Locker.DomainModel.DTO;

namespace Locker.Application
{
    public class SectorManagement : ISectorManagement
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public SectorManagement(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public SectorManagementResponse AddNewSector(Sector sector)
        {
            try
            {
                this.unitOfWork.SectorRepository.Add(sector);

                this.unitOfWork.Commit();

                return new SectorManagementResponse(true);
            }
            catch (Exception)
            {
                return new SectorManagementResponse(true);
            }
        }

        public IList<SectorLocation> GetSectorLocations(int traderId)
        {
            return this.unitOfWork.SectorLocationRepository.GetSectorLocations(traderId);
        }

        public IList<Sector> GetSectors(int traderId)
        {
            return this.unitOfWork.SectorRepository.GetAll(traderId);
        }
    }
}
