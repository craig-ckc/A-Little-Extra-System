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

        //Get: Actors/Create
        public IActionResult Create()
        {
            return View(new ActivityForm());
        }


        public async Task<IActionResult> Details(int Id)
        {
            var activity = await activityService.GetByIdAsync(Id);

            activity.Award = await awardsService.GetAwards(Id);

            return View(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityForm data)
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

    }
}