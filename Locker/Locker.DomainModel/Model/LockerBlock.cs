namespace Locker.DomainModel
{
    public class LockerBlock
    {
        public int LockerBlockId { get; set; }

        public int TotalNumberOfVerticalLockers { get; set; }

        public int TotalNumberOfHorizontalLockers { get; set; }

        public int TotalNumberOfLockers { get; set; }
    }
}