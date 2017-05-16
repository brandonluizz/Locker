using Locker.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locker.DomainModel;
using Locker.DomainModel.DTO;
using Locker.Infrastructure.Repositories.Interface;

namespace Locker.Application
{
    public class LockerManagement : ILockerManagement
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public LockerManagement(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public LockerManagementResponse AddNewLocker(DomainModel.Locker locker)
        {
            try
            {
                this.unitOfWork.LockerRepository.Add(locker);

                this.unitOfWork.Commit();

                return new LockerManagementResponse(true);
            }
            catch (Exception ex)
            {
                return new LockerManagementResponse(false);
            }
        }

        public LockerManagementResponse AddNewLockerBlock(LockerBlock lockerBlock)
        {
            try
            {
                this.unitOfWork.LockerBlockRepository.Add(lockerBlock);

                this.unitOfWork.Commit();

                return new LockerManagementResponse(true);
            }
            catch (Exception)
            {
                return new LockerManagementResponse(false);
            }
        }

        public IList<LockerBlock> GetAllLockerBlocks()
        {
            return this.unitOfWork.LockerBlockRepository.GetAll();
        }
    }
}
