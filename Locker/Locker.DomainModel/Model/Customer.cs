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

        public void FormattCpj()
        {
            this.CustomerCpf = this.CustomerCpf.Replace(".", "");
        }

        public DateTime ExpirationDate { get; set; }

        public string TagUID { get; set; }

        public string FormattedBirthDate
        {
            get
            {
                return this.BirthDate.ToString("dd-MM-yyyy");
            }
        }

        public string FormattedRegistrationDate
        {
            get
            {
                return this.RegistrationDate.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }

        public virtual Trader Trader { get; set; }
    }
}
