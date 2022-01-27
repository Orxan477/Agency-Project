using Agency.Utilities;
using Agency.ViewModels.Account;
using Core.Models;
using Data.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Agency.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IConfiguration _configure;
        private RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 IConfiguration configure,
                                 RoleManager<IdentityRole> roleManager,
                                 AppDbContext context,
                                 SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _configure = configure;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser
            {
                FullName=register.FullName,
                UserName=register.UserName,
                Email=register.Email
            };
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var token =await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string link = Url.Action(nameof(VerifyEmail), "Account", new { userid = user.Id, token },
                                                                        Request.Scheme, Request.Host.ToString());
                string body = $"<a href=\"{link}\">Verify Link</a>";
                string subject = "Confirmation Link";
                Email.EmailSend(_configure.GetSection("MailSettings:Email").Value,
                                _configure.GetSection("MailSettings:Password").Value,
                                user.Email, body, subject);
               await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
               //await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
                ViewBag.IsSuccesful = true;
                return View();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }
        public async Task<IActionResult> VerifyEmail(string userid,string token)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) return BadRequest("User Could Not Found");
            var result =await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                Email.EmailSend(_configure.GetSection("MailSettings:Email").Value,
                                _configure.GetSection("MailSettings:Password").Value,
                                user.Email, "Your Email is Confirmed", "Email Confirmed");
                user.IsActivated = true;
                await _context.SaveChangesAsync();
                ViewBag.Confirm = true;
                return RedirectToAction(nameof(Login));
            }
            else return BadRequest();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser user = await _userManager.FindByEmailAsync(login.Email.ToString());
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email and Password is Wrong");
                return View(login);
            }
            if (!user.IsActivated)
            {
                ModelState.AddModelError(string.Empty, "Please Confirmation Email.");
                return View(login);
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your Account is locked. Please wait.");
                return View(login);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email and Password is Wrong");
                return View(login);
            }
            var device = Environment.MachineName.ToString();
            var versionDevice = Environment.OSVersion.ToString();
            string body = $"Hello Dear {user.FullName}. Your account has been logged in from this {device}{versionDevice}." +
                        $"If you have not already done so, be sure to change your password";

            Email.EmailSend(_configure.GetSection("MailSettings:Email").Value,
                                _configure.GetSection("MailSettings:Password").Value,
                                user.Email, body, "Login Information");
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
    }
}
