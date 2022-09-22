using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Service;
using A_Little_Extra_System.Data.Static;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace A_Little_Extra_System.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IActivityService activityService;

        public ProfileController(AppDbContext context, UserManager<User> userManager, IActivityService activityService)
        {
            this.activityService = activityService;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = new Profile
            {
                User = context.User.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                PostedHistory = await activityService.PostedHistory(userId),
            };

            if (User.IsInRole(UserRoles.Student))
            {
                data.OtherHistory = await activityService.ParticipationHistory(userId);
            }
            else if (User.IsInRole(UserRoles.DepartmentStaff))
            {
                data.OtherHistory = await activityService.SupervisionHistory(userId);
            }

            return View(data);
        }

        public IActionResult Update()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = context.User.Find(userId);

            var data = new ProfileUpdate
            {
                ImgUrl = "https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg",
                FirstName = user.FirstName,
                LastName = user.LastName,
                PatnerName = user.PatnerName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ProfileUpdate data)
        {
            if (data.CurrentPassword != null)
            {
                if (!ModelState.IsValid) return View(data);
            }
            else
            {
                if (data.FirstName == null || data.LastName == null || data.PatnerName == null || data.Email == null || data.PhoneNumber == null) return View(data);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManager.FindByIdAsync(userId);;

            if (user != null)
            {
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.PatnerName = data.PatnerName;
                user.UserName = data.FirstName;
                user.Email = data.Email;
                user.PhoneNumber = data.PhoneNumber;
            }

            if (data.CurrentPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(user, data.CurrentPassword, data.Password);

                if(!result.Succeeded){
                    return View(data);
                }

            }

            await userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }

}