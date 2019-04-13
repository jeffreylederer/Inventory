using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class BowlSize
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Bowl Size")]
        public string Size { get; set; }

        public virtual ICollection<Bowl> Bowls { get; set; }

    }
}