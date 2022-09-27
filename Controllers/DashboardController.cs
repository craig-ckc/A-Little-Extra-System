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

        [HttpGet]
        public async Task<IActionResult> Progress(int Id)
        {
            var progress = new ActivityProgress
            {
                Activity = await activityService.GetByIdAsync(Id),
                Participants = await participantService.GetActivityPaticipants(Id),
                Supervisors = await supervisorService.GetActivitySupervisors(Id),
                SupervisorsStatus = await supervisorService.GetActivitySupervisorsStatus(Id),
            };

            return View(progress);
        }

        // public async Task<IActionResult> RejectSupervision(int Id){
        //     await supervisorService.DeleteSupervisor(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        //     return RedirectToAction(nameof(Index));
        // }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var activity = await activityService.GetByIdAsync(Id);

            activity.Award = await awardsService.GetAwards(Id);

            var activity_ = new ActivityForm
            {
                Id = activity.Id,
                UserId = activity.UserId,
                Name = activity.Name,
                Description = activity.Description,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                Points = activity.Points,
                Awards = activity.Award,
                AwardPos = -1,
            };

            return View(activity_);
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

            if (data.AwardPos != -1)
            {
                data.AwardPos = -1;
                return View(data);
            }
            
            await activityService.UpdateActivityAsync(data, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAward(ActivityForm data)
        {
            if (data.AwardPos == -1) return RedirectToAction(nameof(Edit), new {data = data});
        
            var award = data.Awards[data.AwardPos];

            data.Awards.RemoveAt(data.AwardPos);

            if (award.Id > 1) await awardsService.DeleteAsync(award.Id);

            return RedirectToAction(nameof(Edit), new {Id = data.Id});
        }

        [HttpPost]
        public async Task<IActionResult> EditAward(ActivityForm data)
        {
            if (data.AwardPos == -1) return RedirectToAction(nameof(Edit), new {data = data});
        
            var award = data.Awards[data.AwardPos];

            if (award.Id > 1) await awardsService.UpdateAsync(award.Id, award);

            return RedirectToAction(nameof(Edit), new {Id = data.Id});
        }

        public async Task<IActionResult> CancelActivity(int Id)
        {
            await activityService.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelParticipation(int Id)
        {
            await participantService.DeleteParticipant(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelSupervision(int Id)
        {
            await supervisorService.DeleteSupervisor(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

    }
}