using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("Locker")]
    public class Locker
    {
        [Key]
        public int LockerId { get; set; }

        [ForeignKey("Sector")]
        public int SectorId { get; set; }

        [ForeignKey("LockerBLock")]
        public int LockerBlockId { get; set; }

        public int VerticalPositionNumber { get; set; }

        public int HorizontalPositionNumber { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual LockerBlock LockerBLock { get; set; }
    }
}
