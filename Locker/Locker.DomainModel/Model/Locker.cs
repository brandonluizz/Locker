using Locker.DomainModel.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Locker.DomainModel
{
    [Table("Locker")]
    public class Locker
    {
        public Locker()
        {

        }

        public Locker(int lockerBlockId, int verticalPosition, int horizontalPosition, int numberOfPositionLocker)
        {
            this.LockerBlockId = lockerBlockId;
            this.VerticalPositionNumber = verticalPosition;
            this.HorizontalPositionNumber = horizontalPosition;
            this.NumberOfPositionLocker = numberOfPositionLocker;
            this.IsActive = true;
            this.IsUsing = false;
        }

        [Key]
        public int LockerId { get; set; }

        [ForeignKey("LockerBlock")]
        public int LockerBlockId { get; set; }

        public string ArduinoId { get; set; }

        public int NumberOfPositionLocker { get; set; }

        public int VerticalPositionNumber { get; set; }

        public int HorizontalPositionNumber { get; set; }

        public bool IsUsing { get; set; }

        public bool IsActive { get; set; }

        public virtual LockerBlock LockerBlock { get; set; }

        public void SetArduinoId(string arduinoId)
        {
            if (string.IsNullOrWhiteSpace(arduinoId)) { throw new ArgumentNullException(nameof(arduinoId)); }

            this.ArduinoId = arduinoId;
        }
    }
}
