using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Service;
using A_Little_Extra_System.Data.Static;
using A_Little_Extra_System.Data.ViewModal;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace A_Little_Extra_System.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext context;
        private readonly IActivityService activityService;

        public ProfileController(AppDbContext context, IActivityService activityService)
        {
            this.activityService = activityService;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var data = new Profile{
                PostedHistory = await activityService.PostedHistory(userId),
            };

            if(User.IsInRole(UserRoles.Student)){
                data.OtherHistory = await activityService.ParticipationHistory(userId);
            } else if(User.IsInRole(UserRoles.DepartmentStaff)){
                data.OtherHistory = await activityService.SupervisionHistory(userId);
            }

            return View(data);
        }
    }

}