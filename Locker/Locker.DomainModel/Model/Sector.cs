using Locker.DomainModel.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Locker.DomainModel
{
    [Table("Sector")]
    public class Sector
    {
        [Key]
        public int SectorId { get; set; }
        
        [ForeignKey("SectorLocation")]
        public int SectorLocationId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public string SectorName { get; set; }
        
        public virtual SectorLocation SectorLocation { get; set; }

        public virtual Trader Trader { get; set; }

        public void SetTraderId(int traderId)
        {
            this.TraderId = traderId;
        }
    }
}