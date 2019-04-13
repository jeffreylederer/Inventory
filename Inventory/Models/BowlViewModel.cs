using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inventory.Models
{
    public class BowlViewModel
    {
        [MaxLength]
        public byte[] Picture { get; set; }

        [Key]
        public int Id { get; set; }
    }
}