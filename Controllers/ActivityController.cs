using System.Security.Claims;
using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Service;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A_Little_Extra_System.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService activityService;
        private readonly IAwardService awardsService;
        private readonly IParticipantService participantService;
        private readonly ISupervisorService supervisorService;
        private readonly AppDbContext context;

        public ActivityController(AppDbContext context, IActivityService activityService, IAwardService awardsService, IParticipantService participantService, ISupervisorService supervisorService)
        {
            this.activityService = activityService;
            this.awardsService = awardsService;
            this.participantService = participantService;
            this.supervisorService = supervisorService;
            this.context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await activityService.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var data = await activityService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResults = data.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();

                return View(nameof(Index), filteredResults);
            }

            return View(nameof(Index), data);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var activity = await activityService.GetByIdAsync(Id);

            activity.Award = await awardsService.GetAwards(Id);

            return View(activity);
        }

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View(new ActivityForm { StartDate = DateTime.Today, EndDate = DateTime.Today, AwardPos = -1, });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityForm data)
        {
            var award = data.Award;

            if (data.AwardPos == 0) return View(data);

            if (data.AwardPos != -1) data.Awards.RemoveAt(data.AwardPos);

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

            await activityService.NewActivityAsync(data, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddParticipant(int Id)
        {
            await participantService.AddParticipant(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSupervisor(int Id)
        {
            await supervisorService.AddSupervisor(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelActivity(int Id)
        {
            await supervisorService.AddSupervisor(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

    }
}