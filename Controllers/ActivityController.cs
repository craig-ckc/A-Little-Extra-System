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
            var data = new ActivityView
            {
                Activities = await activityService.GetAllActiveAsync(),
            };

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ActivityView data)
        {

            data.Activities = await activityService.GetAllActiveAsync();

            // check all activities that contain the string in either the Name or Description
            if (!string.IsNullOrEmpty(data.SearchString))
            {
                data.Activities = data.Activities.Where(n => n.Name.Contains(data.SearchString) || n.Description.Contains(data.SearchString));
            }

            switch (data.DateRange)
            {
                case "Today":
                    data.Activities = data.Activities.Where(a => a.EndDate >= DateTime.Today && a.StartDate <= DateTime.Today.AddDays(1));
                    break;
                case "This week":
                    data.Activities = data.Activities.Where(a => a.EndDate >= DateTime.Today && a.StartDate <= DateTime.Today.AddDays(7));
                    break;
                case "This month":
                    data.Activities = data.Activities.Where(a => a.EndDate >= DateTime.Today && a.StartDate <= DateTime.Today.AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Today.Day));
                    break;
                case "This year":
                    int daysInYear = DateTime.IsLeapYear(DateTime.Today.Year) ? 366 : 365;
                    data.Activities = data.Activities.Where(a => a.EndDate >= DateTime.Today && a.StartDate <= DateTime.Today.AddDays(daysInYear - DateTime.Today.DayOfYear));
                    break;
                default:
                    break;
            }

            switch (data.PointsRange)
            {
                case "1 - 20":
                data.Activities = data.Activities.Where(n => n.Points >= 1 & n.Points <= 20);
                    break;
                case "20 - 40":
                data.Activities = data.Activities.Where(n => n.Points >= 20 & n.Points <= 40);
                    break;
                case "40 - 60":
                data.Activities = data.Activities.Where(n => n.Points >= 40 & n.Points <= 60);
                    break;
                case "60 - 80":
                data.Activities = data.Activities.Where(n => n.Points >= 60 & n.Points <= 80);
                    break;
                case "80 - 100":
                data.Activities = data.Activities.Where(n => n.Points >= 80 & n.Points <= 100);
                    break;
                default:
                    break;
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
            await supervisorService.AddSupervision(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelActivity(int Id)
        {
            await supervisorService.AddSupervision(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction(nameof(Index));
        }

    }
}