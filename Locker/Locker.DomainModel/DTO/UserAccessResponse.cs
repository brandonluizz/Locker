using Locker.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.DTO
{
    public class UserAccessResponse
    {
        public UserAccessResponse(bool success)
        {
            this.Success = success;
        }

        public UserAccessResponse(User user, bool success)
        {
            this.User = user ?? throw new ArgumentNullException(nameof(user)); ;
            this.Success = success;
        }

        public User User { get; set; } 

        public bool Success { get; set; }
    }
}
