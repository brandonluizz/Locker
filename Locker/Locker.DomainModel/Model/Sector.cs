using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("Sector")]
    public class Sector
    {
        [Key]
        public int SectorId { get; set; }
        
        [ForeignKey("SectorLocation")]
        public int SectorLocationId { get; set; }

        public string SectorName { get; set; }
        
        public virtual SectorLocation SectorLocation { get; set; }
    }
}