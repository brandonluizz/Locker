using Locker.DomainModel.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("SectorLocation")]
    public class SectorLocation
    {
        [Key]
        public int SectorLocationId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public string SectorLocationName { get; set; }

        public virtual Trader Trader { get; set; }
    }
}