using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class Bowl
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength]
        public byte[] Picture { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [ForeignKey("BowlSize")]
        [Display(Name = "Size")]
        public int BowlSizeId { get; set; }

        [ForeignKey("Bias")]
        public int BiasId { get; set; }

        [ForeignKey("Weight")]
        public int WeightId { get; set; }

        [Display(Name = "In a locker")]
        public bool InLocker { get; set; }

        [Display(Name = "Locker Owner")]
        public string OwnerName { get; set; }

        
        public string Comment { get; set; }

        
        public virtual BowlSize BowlSize { get; set; }

        public virtual Bias Bias { get; set; }

        public virtual Weight Weight { get; set; }




    }
}