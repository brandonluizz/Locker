using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel.Model
{
    [Table("Trader")]
    public class Trader
    {
        [Key]
        public int TraderId { get; set; }

        public string TraderName { get; set; }

        public string TraderCnpj { get; set; }

        public DateTime CreationDateTime { get; set; }

        public bool IsActive { get; set; }
    }
}
