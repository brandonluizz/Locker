using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("User")]
    public class User
    {
        public User(string userName, DateTime birthDate, DateTime expirationDate)
        {
            this.UserName = userName;
            this.BirthDate = birthDate;
            this.ExpirationDate = expirationDate;
            this.RegistrationDate = DateTime.Now;
        }

        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
