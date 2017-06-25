using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO
{
    public class CommunicatorResponse
    {
        public CommunicatorResponse(bool success)
        {
            this.Success = success;
        }

        public bool Success { get; set; }
    }
}
