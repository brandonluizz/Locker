using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO
{
    public class LockerBlockWithLockers
    {
        public LockerBlockWithLockers(IEnumerable<LockerWithCustomerActivity> lockerWithCustomerActivity, int lockerBlockId, string sectorName)
        {
            this.LockerBlockId = lockerBlockId.ToString();
            this.Lockers = lockerWithCustomerActivity;
            this.SectorName = sectorName;
        }

        [JsonProperty("BlockId")]
        public string LockerBlockId { get; set; }

        [JsonProperty("SectorName")]
        public string SectorName { get; set; }

        [JsonProperty("Locker")]
        public IEnumerable<LockerWithCustomerActivity> Lockers { get; set; }
    }
}
