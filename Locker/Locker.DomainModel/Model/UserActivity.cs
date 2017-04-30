using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("UserActivity")]
    public class UserActivity
    {
        [Key]
        public int UserActivityId { get; set; }

        [ForeignKey("Locker")]
        public int LockerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime InitialRentalDate { get; set; }

        public DateTime FInalRentalDate { get; set; }

        public virtual Locker Locker { get; set; }

        public virtual User User { get; set; }
    }
}
