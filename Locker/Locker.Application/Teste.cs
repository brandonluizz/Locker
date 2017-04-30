using Locker.DomainModel;
using Locker.Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.Application
{
    public class Teste : ITeste
    {
        private readonly ILockerUnitOfWork unitOfWork;

        public Teste(ILockerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void AddNewUser()
        {
            User user = new User("brandon", new DateTime(1996, 12, 01), new DateTime(2017, 12, 01));

            this.unitOfWork.UserRepository.Add(user);
        }
    }
}
