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

        public LockerManagementResponse IsAvailableLockerPosition(LockerPosition lockerPosition, int traderId)
        {
            try
            {
                var locker = this.unitOfWork.LockerRepository.GetLockersByPosition(lockerPosition, traderId);

                bool isAvailable = locker == null;

                return new LockerManagementResponse(isAvailable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LockerManagementResponse AddNewLocker(DomainModel.Locker locker)
        {
            try
            {
                this.unitOfWork.LockerRepository.Add(locker);

                this.unitOfWork.Commit();

                return new LockerManagementResponse(true);
            }
            catch (Exception)
            {
                return new LockerManagementResponse(false);
            }
        }

        public LockerManagementResponse AddNewLockerBlock(LockerBlock lockerBlock)
        {
            try
            {
                this.unitOfWork.LockerBlockRepository.Add(lockerBlock);

                this.SetLockerBlockId(lockerBlock);

                this.AddLockersForBlock(lockerBlock);

                this.unitOfWork.Commit();

                return new LockerManagementResponse(true);
            }
            catch (Exception ex)
            {
                return new LockerManagementResponse(false);
            }
        }

        private void SetLockerBlockId(LockerBlock lockerBlock)
        {
            var block = this.unitOfWork.LockerBlockRepository.GetLastLockerBlocker();
            int lockerBlockId = 1;

            if (block != null) { lockerBlockId = block.LockerBlockId + 1; }

            lockerBlock.LockerBlockId = lockerBlockId;
        }

        private void AddLockersForBlock(LockerBlock lockerBlock)
        {
            var lockers = this.GenerateVerticalLockers(lockerBlock);

            this.unitOfWork.LockerRepository.AddRange(lockers);
        }

        private IList<DomainModel.Locker> GenerateVerticalLockers(LockerBlock lockerBlock)
        {
            var lockers = new List<DomainModel.Locker>();

            int lockerPosition = 1;

            for (int verticalPosition = 1; verticalPosition <= lockerBlock.TotalNumberOfVerticalLockers; verticalPosition++)
            {
                for (int horizontalPosition = 1; horizontalPosition <= lockerBlock.TotalNumberOfHorizontalLockers; horizontalPosition++)
                {

                    var locker = new DomainModel.Locker(lockerBlock.LockerBlockId, verticalPosition, horizontalPosition, lockerPosition);
                    lockers.Add(locker);

                    lockerPosition++;
                }
            }

            return lockers;
        }

        public IList<LockerBlock> GetAllLockerBlocks(int traderId)
        {
            return this.unitOfWork.LockerBlockRepository.GetAll(traderId);
        }

        public IList<DomainModel.Locker> GetAllLockers(int traderId)
        {
            return this.unitOfWork.LockerRepository.GetAll(traderId);
        }

        public LockerManagementResponse IsAvailableLockerBlock(int lockerBlockId, int traderId)
        {
            try
            {
                var lockers = this.unitOfWork.LockerRepository.GetAllLockersByLockerBlockId(lockerBlockId, traderId);

                bool isAvailable = this.VeriryIfIsAvailableLockerBlock(lockers);

                return new LockerManagementResponse(isAvailable);
            }
            catch (Exception)
            {
                return new LockerManagementResponse(false);
            }
        }

        private bool VeriryIfIsAvailableLockerBlock(IList<DomainModel.Locker> lockers)
        {
            if (lockers.Count == default(int)) { return true; }

            int limit = lockers.Select(l => l.LockerBlock.TotalNumberOfLockers).FirstOrDefault();

            int totalUsed = lockers.Count;

            return limit > totalUsed;
        }

        public LockerManagementResponse AddArduinoDataInLocker(DomainModel.Locker locker)
        {
            try
            {
                var entityLocker = this.unitOfWork.LockerRepository.GetById(locker.LockerId);

                entityLocker.SetArduinoId(locker.ArduinoId);

                this.unitOfWork.Commit();

                return new LockerManagementResponse(true);
            }
            catch (Exception)
            {
                return new LockerManagementResponse(false);
            }
        }

        public IList<LockerBlockWithLockers> GetAllLockersByLockerBlocks(int traderId)
        {
            try
            {
                var blocks = this.unitOfWork.LockerBlockRepository.GetAll(traderId);

                var lockers = this.unitOfWork.LockerRepository.GetAll(traderId);

                var blockWithLockers = this.GenerateBlockWithLockers(blocks, lockers);

                return blockWithLockers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IList<LockerBlockWithLockers> GenerateBlockWithLockers(IList<LockerBlock> blocks, IList<DomainModel.Locker> lockers)
        {
            var blockWithLockers = new List<LockerBlockWithLockers>();

            foreach (var block in blocks)
            {
                var lockerOfActualBlock = lockers.Where(l => l.LockerBlockId == block.LockerBlockId).ToList();

                if (lockerOfActualBlock.Count == default(int)) { continue; }

                string sectorName = block.Sector.SectorName;

                var blockWithLocker = new LockerBlockWithLockers(lockerOfActualBlock, block.LockerBlockId, sectorName);

                blockWithLockers.Add(blockWithLocker);
            }

            return blockWithLockers;
        }
    }
}
