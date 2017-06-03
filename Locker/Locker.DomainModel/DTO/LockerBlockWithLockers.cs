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
        public LockerBlockWithLockers(IList<Locker> lockers, int lockerBlockId)
        {
            this.LockerBlockId = lockerBlockId.ToString();
            this.Lockers = lockers;
        }

        [JsonProperty("BlockId")]
        public string LockerBlockId { get; set; }

        [JsonProperty("Locker")]
        public IList<Locker> Lockers { get; set; }
    }
}
