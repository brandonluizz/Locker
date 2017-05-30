using Locker.DomainModel;
using Locker.DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application.Interfaces
{
    public interface ISectorManagement
    {
        IList<SectorLocation> GetSectorLocations(int traderId);

        IList<Sector> GetSectors(int traderId);

        SectorManagementResponse AddNewSector(Sector sector);

        SectorManagementResponse AddNewSectorLocation(SectorLocation request);
    }
}
