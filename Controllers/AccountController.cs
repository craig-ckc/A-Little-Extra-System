using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Static;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace A_Little_Extra_System.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly AppDbContext context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        public IActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Login login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                if (!user.isActive)
                {
                    TempData["Error"] = "Account was deactivated, create a new one";
                    return View(login);
                }

                var passwordCheck = await userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong password. Please, try again!";
                return View(login);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(login);
        }

        public IActionResult Register()
        {
            return View(new Register());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Register register)
        {
            if (!ModelState.IsValid) return View(register);

            var user = await userManager.FindByEmailAsync(register.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(register);
            }

            var newAppUser = new User()
            {
                ImgUrl = "https://support.pega.com/sites/default/files/pega-user-image/471/REG-470114.png",
                FirstName = register.FirstName,
                LastName = register.LastName,
                PatnerName = register.PatnerName,
                UserName = (register.UserRole == UserRoles.UniversityPartner) ? register.PatnerName : register.FirstName,
                Email = register.Email,
                EmailConfirmed = true,
                PhoneNumber = register.PhoneNumber,
                isActive = true,
            };

            var responce = await userManager.CreateAsync(newAppUser, register.Password);

            if (responce.Succeeded)
    
                switch (register.UserRole)
                {
                    case "0":
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.Student);
                        break;
                    case "1":
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.DepartmentStaff);
                        break;
                    case "2":
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.UniversityPartner);
                        break;
                    default:
                        break;
                }
            else
            {
                TempData["Error"] = responce.ToString();
                return View(register);
            }

            TempData["Success"] = "You have been successfully registered!";
            return View(register);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete()
        {
            return RedirectToAction("Index", "HomeController");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}