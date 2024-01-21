using System.ComponentModel.DataAnnotations;

namespace Indigo.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Title is required"), MaxLength(32)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required"), MaxLength(256)]
        public string Description { get; set; }
    }
}
