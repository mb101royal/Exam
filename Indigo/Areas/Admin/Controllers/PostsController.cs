using Indigo.Contexts;
using Indigo.Helpers;
using Indigo.Models;
using Indigo.ViewModels.PostsViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Indigo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostsController : Controller
    {
        readonly IndigoDbContext _context;

        public PostsController(IndigoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var postsFromDb = await _context.Posts.Select(t => new PostsDetailsViewModel
            {
                Id = t.Id,
                Title = t.Title,
                ImageUrl = t.ImageUrl,
                Description = t.Description,
            }).ToListAsync();

            return View(postsFromDb);
        }

        // Create

        // Get
        [HttpGet]
        public IActionResult Create()
            => View();

        // Post
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {

                if (!vm.ImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "File type is not correct");

                    return View();
                }

                float imageSize = 300;

                if (!vm.ImageFile.IsCorrectSize(imageSize))
                {
                    ModelState.AddModelError("ImageFile", $"Image size limited by {imageSize} kb");

                    return View();
                }

                Post newPost = new()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    ImageUrl = vm.ImageFile.SaveImageAsync(PathConstants.ImagesFileLocation).Result,
                };

                await _context.AddAsync(newPost);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // Edit

        // Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.IsValidId()) return BadRequest();

            return View();
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, PostEditViewModel vm)
        {
            if (!id.IsValidId()) return View(vm);

            if (ModelState.IsValid)
            {
                var postFromDb = await _context.Posts.FindAsync(id);

                if (postFromDb == null) return NotFound();

                if (!vm.ImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "File type is not correct");

                    return View();
                }

                float imageSize = 300;

                if (!vm.ImageFile.IsCorrectSize(imageSize))
                {
                    ModelState.AddModelError("ImageFile", $"Image size limited by {imageSize} kb");

                    return View();
                }

                postFromDb.ImageUrl.DeleteFile();

                postFromDb.Title = vm.Title;
                postFromDb.Description = vm.Description;
                postFromDb.ImageUrl = vm.ImageFile.SaveImageAsync(PathConstants.ImagesFileLocation).Result;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.IsValidId()) return BadRequest();

            var postFromDb = await _context.Posts.FindAsync(id);

            if (postFromDb == null) return NotFound();

            postFromDb.ImageUrl.DeleteFile();

            _context.Remove(postFromDb);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
