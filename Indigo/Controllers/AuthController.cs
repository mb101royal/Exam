using Indigo.Models;
using Indigo.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indigo.Controllers
{
    public class AuthController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register

        // Get
        [HttpGet]
        public IActionResult Register() 
            => View();

        //Post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
                return View(userModel);

            AppUser user = new()
            {
                Fullname = userModel.Fullname,
                Email = userModel.Email,
                UserName = userModel.Username,
                EmailConfirmed = true, // for Dev environment only!
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return View(userModel);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Index", "Home");
        }

        // Login

        // Get
        [HttpGet]
        public IActionResult Login()
            => View();

        //Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UsernameOrEmail) ?? 
                    await _userManager.FindByNameAsync(model.UsernameOrEmail);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "User doesn't exist");

                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        public IActionResult AccessDenied()
            => View();
    }
}
