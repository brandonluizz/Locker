using Locker.DomainModel.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
            this.RegistrationDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now.AddYears(2);
        }

        [Key]
        public int CustomerId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerCpf { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string TagUID { get; set; }

        public virtual Trader Trader { get; set; }
    }
}
