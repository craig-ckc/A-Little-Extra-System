using A_Little_Extra_System.Data;
using A_Little_Extra_System.Data.Service;
using A_Little_Extra_System.Data.Static;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
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
        private readonly IParticipantService participantService;
        private readonly ISupervisorService supervisorService;

        public ProfileController(AppDbContext context, UserManager<User> userManager, IActivityService activityService, IParticipantService participantService, ISupervisorService supervisorService)
        {
            this.activityService = activityService;
            this.userManager = userManager;
            this.participantService = participantService;
            this.supervisorService = supervisorService;
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
                data.OtherHistory = await participantService.ParticipationHistory(userId);
            }
            else if (User.IsInRole(UserRoles.DepartmentStaff))
            {
                data.OtherHistory = await supervisorService.SupervisionHistory(userId);
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateCertificate(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManager.FindByIdAsync(userId);

            var activity = participantService.GetActivityPaticipant(Id, userId);

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 30, 30, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                BaseColor myColor = WebColors.GetRGBColor("0D0C0B");

                Font H1 = new Font(Font.FontFamily.HELVETICA, 40, Font.BOLD, WebColors.GetRGBColor("#0D0C0B"));

                Font p = new Font(Font.FontFamily.HELVETICA, 14, Font.NORMAL, WebColors.GetRGBColor("#7A7A7A"));

                Font span = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, WebColors.GetRGBColor("#FF7F50"));

                var img = iTextSharp.text.Image.GetInstance("wwwroot/images/certificate_badge.png");
                img.Alignment = Element.ALIGN_CENTER;
                img.SpacingAfter = 52;
                document.Add(img);

                Paragraph para0 = new Paragraph("This certificate has of Participation", H1);
                para0.Alignment = Element.ALIGN_CENTER;
                para0.SpacingAfter = 48;
                document.Add(para0);

                Paragraph para1 = new Paragraph("is certificate is presented to", p);
                para1.Alignment = Element.ALIGN_CENTER;
                para1.SpacingAfter = 70;
                document.Add(para1);

                Paragraph para2 = new Paragraph(user.FirstName + " " + user.LastName, H1);
                para2.Alignment = Element.ALIGN_CENTER;
                para2.SpacingAfter = 26;
                document.Add(para2);

                Paragraph para3 = new Paragraph("for your extraordinary contribution and commitment to the " + activity.Result.Name + " activity.", p);
                para3.Alignment = Element.ALIGN_CENTER;
                para3.SpacingAfter = 48;
                document.Add(para3);

                Paragraph para4 = new Paragraph("Date: " + activity.Result.EndDate.ToString("dd MMM yyyy"), p);
                para4.Alignment = Element.ALIGN_CENTER;
                para4.SpacingAfter = 90;
                document.Add(para4);

                Paragraph para5 = new Paragraph("A Little Extra", span);
                para5.Alignment = Element.ALIGN_CENTER;
                document.Add(para5);

                document.Close();
                writer.Close();

                String filename = user.FirstName + "_" + user.LastName + "_" + userId + Id;

                var constatnt = ms.ToArray();
                return File(constatnt, "application/vnd", filename + ".pdf");
            }

        }

        [HttpPost]
        public IActionResult DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }

        public IActionResult Update()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = context.User.Find(userId);

            var data = new ProfileUpdate
            {
                ImageURL = user.ImgUrl,
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

            var user = await userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.ImgUrl = data.ImageURL;
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

                if (!result.Succeeded)
                {
                    return View(data);
                }

            }

            await userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }

}