using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("CustomerActivity")]
    public class CustomerActivity
    {
        [Key]
        public int CustomerActivityId { get; set; }

        [ForeignKey("Locker")]
        public int LockerId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public DateTime InitialRentalDate { get; set; }

        public DateTime FInalRentalDate { get; set; }

        public virtual Locker Locker { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
