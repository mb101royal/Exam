using Indigo.Contexts;
using Indigo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Indigo.Controllers
{
    public class HomeController : Controller
    {
        readonly IndigoDbContext _context;

        public HomeController(IndigoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var postsFromDb = await _context.Posts.ToListAsync();

            return View(postsFromDb);
        }
    }
}