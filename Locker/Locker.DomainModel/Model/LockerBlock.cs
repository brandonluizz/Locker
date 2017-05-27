using Locker.DomainModel.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Locker.DomainModel
{
    [Table("LockerBlock")]
    public class LockerBlock
    {
        [Key]
        public int LockerBlockId { get; set; }

        [ForeignKey("Sector")]
        public int SectorId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public int TotalNumberOfVerticalLockers { get; set; }

        public int TotalNumberOfHorizontalLockers { get; set; }

        public int TotalNumberOfLockers
        {
            get
            {
                return this.TotalNumberOfHorizontalLockers * this.TotalNumberOfVerticalLockers;
            }
            set { }
        }

        public virtual Sector Sector { get; set; }

        public virtual Trader Trader { get; set; }

        public void SetTraderId(int traderId)
        {
            this.TraderId = traderId;
        }
    }
}