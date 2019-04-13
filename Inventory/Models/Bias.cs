using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Bias
    {
     
        [Key]
        public int Id { get; set; }

        [Display(Name = "Bias")]
        public string BiasSize { get; set; }

        public virtual ICollection<Bowl> Bowls { get; set; }
    }
}