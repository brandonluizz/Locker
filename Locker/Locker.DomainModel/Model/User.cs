using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locker.DomainModel.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("Trader")]
        public int TraderId { get; set; }

        public string UserName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreationDateTime { get; set; }

        public virtual Trader Trader { get; set; }
    }
}
