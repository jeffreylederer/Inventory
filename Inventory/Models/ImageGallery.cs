using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Inventory.Models
{
    public class ImageGallery
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int ImageSize { get; set; }
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }
        [Required(ErrorMessage = "Please select file")]
        public HttpPostedFileBase File { get; set; }
    }
}