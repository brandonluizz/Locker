namespace Locker.DomainModel
{
    public class Sector
    {
        public int SectorId { get; set; }

        public string SectorName { get; set; }

        public int SectorLocationId { get; set; }

        public virtual SectorLocation SectorLocation { get; set; }
    }
}