﻿using System.ComponentModel.DataAnnotations;

namespace Indigo.ViewModels.PostsViewModel
{
    public class PostEditViewModel
    {
        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Title is required"), MaxLength(32)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required"), MaxLength(256)]
        public string Description { get; set; }
    }
}
