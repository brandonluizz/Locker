using Locker.DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application.Interfaces
{
    public interface IArduinoCommunicatorManager
    {
        CommunicatorResponse RentalRecorder(string taguid, string arduinoId, string traderId);
    }
}
