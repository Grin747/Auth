using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.Models;
using Auth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ProjectContext _context;

        public AccountController(ProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await _context.Users.FirstOrDefaultAsync(it => it.Login == loginModel.Login);

            if (user == null)
            {
                ModelState.AddModelError("no-user", "Unvalid Login");
                return View(loginModel);
            }

            if (user.Password == loginModel.Password)
            {
                ModelState.AddModelError("no-user", "Unvalid Password");
                return View(loginModel);
            }
            
            await Authenticate(loginModel.Login);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            if (await _context.Users.FirstOrDefaultAsync(it => it.Login == registerModel.Login) == null)
            {
                await _context.Users.AddAsync(new User(registerModel));
                await _context.SaveChangesAsync();
                await Authenticate(registerModel.Login);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("already-exists", "User with this login is already exists");
            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}