using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("LockerBlock")]
    public class LockerBlock
    {
        [Key]
        public int LockerBlockId { get; set; }

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
    }
}