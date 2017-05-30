using Locker.DomainModel.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Locker.DomainModel
{
    [Table("Locker")]
    public class Locker
    {
        [Key]
        public int LockerId { get; set; }

        [ForeignKey("LockerBlock")]
        public int LockerBlockId { get; set; }

        public int NumberOfPositionLocker { get; set; }

        public int VerticalPositionNumber { get; set; }

        public int HorizontalPositionNumber { get; set; }

        public bool IsUsing { get; set; }

        public bool IsActive { get; set; }

        public virtual LockerBlock LockerBlock { get; set; }
    }
}
