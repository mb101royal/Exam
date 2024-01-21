using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels.PostsViewModel
{
    public class PostsDetailsViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
