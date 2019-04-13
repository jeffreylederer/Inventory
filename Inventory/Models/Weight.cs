using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Weight
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Weight")]
        public string BowlWeight { get; set; }

        public virtual ICollection<Bowl> Bowls { get; set; }
    }
}