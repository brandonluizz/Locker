using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locker.DomainModel
{
    [Table("SectorLocation")]
    public class SectorLocation
    {
        [Key]
        public int SectorLocationId { get; set; }

        public string SectorLocationName { get; set; }
    }
}