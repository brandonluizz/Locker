using Locker.DomainModel.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("Customer")]
    public class Customer
    {
        public Customer(string customerName, DateTime birthDate, DateTime expirationDate)
        {
            this.CustomerName = customerName;
            this.BirthDate = birthDate;
            this.ExpirationDate = expirationDate;
            this.RegistrationDate = DateTime.Now;
        }

        [Key]
        public int CustomerId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public string CustomerName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual Trader Trader { get; set; }
    }
}
