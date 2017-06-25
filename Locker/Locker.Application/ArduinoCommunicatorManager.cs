using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel.DTO;
using Locker.Infrastructure.Repositories.Interface;
using Locker.DomainModel;

namespace Locker.Application
{
    public class ArduinoCommunicatorManager : IArduinoCommunicatorManager
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public ArduinoCommunicatorManager(ILockerUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) { throw new ArgumentNullException(nameof(unitOfWork)); }

            this.unitOfWork = unitOfWork;
        }

        public CommunicatorResponse RentalRecorder(string taguid, string arduinoId, string traderId)
        {
            try
            {
                var traderIdConverted = Convert.ToInt32(traderId);

                var customer = this.unitOfWork.CustomerRepository.GetByTagUid(taguid, traderIdConverted);

                var locker = this.unitOfWork.LockerRepository.GetByArduinoId(arduinoId, traderIdConverted);

                this.ManageRental(customer, locker);
                
                this.unitOfWork.Commit();

                return new CommunicatorResponse(true);
            }
            catch (Exception ex)
            {
                return new CommunicatorResponse(false);
            }
        }

        private void ManageRental(Customer customer, DomainModel.Locker locker)
        {
            var lastCustomerActivity = this.unitOfWork.CustomerActivityRepository.GetByCustomer(customer);

            if (lastCustomerActivity != null)
            {
                lastCustomerActivity.FinalRentalDate = DateTime.Now;
                return;
            }

            var customerActivity = new CustomerActivity(customer, locker);

            this.unitOfWork.CustomerActivityRepository.Add(customerActivity);
        }
    }
}
