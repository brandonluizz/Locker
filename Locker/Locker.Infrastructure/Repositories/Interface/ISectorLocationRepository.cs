using Locker.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Infrastructure.Repositories.Interface
{
    public interface ISectorLocationRepository
    {
        IEnumerable<SectorLocation> GetSectorLocations(int traderId);

        void Add(SectorLocation sectorLocation);
    }
}
