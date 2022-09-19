using System.Security.Claims;
using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Service;
using A_Little_Extra_System.Data.Static;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace A_Little_Extra_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IActivityService activityService;
        private readonly IAwardService awardsService;
        private readonly IParticipantService participantService;
        private readonly ISupervisorService supervisorService;
        private readonly AppDbContext context;

        public DashboardController(AppDbContext context, IActivityService activityService, IAwardService awardsService, IParticipantService participantService, ISupervisorService supervisorService)
        {
            this.activityService = activityService;
            this.awardsService = awardsService;
            this.participantService = participantService;
            this.supervisorService = supervisorService;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = new Dashboard();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            data.PostedActivities = await activityService.GetPostedActivities(userId);

            if (User.IsInRole(UserRoles.Student))
            {
                data.InvolvedActivities = await participantService.GetActivitiesParticipating(userId);
            }
            else if (User.IsInRole(UserRoles.DepartmentStaff))
            {
                data.InvolvedActivities = await supervisorService.GetActivitiesSupervising(userId);
            }

            return View(data);
        }

        public async Task<IActionResult> Progress(int Id){
            var progress = new ActivityProgress{
                Activity = await activityService.GetByIdAsync(Id),
                Supervisors = await supervisorService.GetActivitySupervisors(Id),
                Participants = await participantService.GetActivityPaticipants(Id)
            };

            return View(progress);
        }

        public async Task<IActionResult> Edit(int Id){
            var activity = await activityService.GetByIdAsync(Id);

            activity.Award = await awardsService.GetAwards(Id);

            var activity_ = new ActivityForm{
                Id = activity.Id,
                UserId = activity.UserId,
                Name = activity.Name,
                Description = activity.Description,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                Points = activity.Points,
                Awards = activity.Award,
            };

            return View(activity_);
        }

        public async Task<IActionResult> CancelActivity(int Id){
            await activityService.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ActivityForm data)
        {
            var award = data.Award;

            if (award.Name != null && award.Description != null)
            {
                if (data.Awards == null) data.Awards = new List<Award>();

                data.Awards.Add(new Award { Name = award.Name, Description = award.Description });

                data.Award.Name = "";
                data.Award.Description = "";

                return View(data);
            }

            if (data.Name == null || data.Description == null || data.EndDate == null || data.StartDate == null || data.Points == null)
            {
                return View(data);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // TODO: implement service method that updates activity and records using the
            await activityService.NewActivityAsync(data, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelParticipation(int Id){
            await participantService.DeleteParticipant(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelSupervision(int Id){
            await supervisorService.DeleteSupervisor(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

    }
}