using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A_Little_Extra_System.Data.Service
{
    public class ActivityService : EntityBaseRepository<Activity>, IActivityService
    {
        private readonly AppDbContext context;
        public ActivityService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Activity>> GetPostedActivities(string userId)
        {
            var activities = context.Activity.Where(n => n.UserId == userId).Where(a => a.EndDate >= DateTime.Today).ToList();

            return activities;
        }

        public async Task NewActivityAsync(ActivityForm data, String userId)
        {
            var activity = new Activity()
            {
                Name = data.Name,
                UserId = userId,
                Description = data.Description,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                Points = data.Points,
            };

            var responce = await context.Activity.AddAsync(activity);
            await context.SaveChangesAsync();

            if (data.Awards == null) return;

            foreach (var item in data.Awards)
            {
                var award = new Award()
                {
                    ActivityId = activity.Id,
                    Name = item.Name,
                    Description = item.Description,
                };
                await context.Award.AddAsync(award);
            }

            await context.SaveChangesAsync();
        }

        public async Task UpdateActivityAsync(ActivityForm data, String userId)
        {
            var activity = await context.Activity.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (activity != null)
            {
                activity.Name = data.Name;
                activity.UserId = userId;
                activity.Description = data.Description;
                activity.StartDate = data.StartDate;
                activity.EndDate = data.EndDate;
                activity.Points = data.Points;
                await context.SaveChangesAsync();
            };


            if (data.Awards == null) return;

            foreach (var item in data.Awards)
            {
                var award = context.Award.FirstOrDefault(n => n.Id == item.Id);

                if (award != null)
                {
                    award.ActivityId = activity.Id;
                    award.Name = item.Name;
                    award.Description = item.Description;

                    await context.SaveChangesAsync();
                }
                else
                {
                    award = new Award()
                    {
                        ActivityId = activity.Id,
                        Name = item.Name,
                        Description = item.Description,
                    };
                    await context.Award.AddAsync(award);
                }

            }

            await context.SaveChangesAsync();
        }

        public async Task<List<Activity>> PostedHistory(string userId)
        {
            var activities = context.Activity.Where(n => n.UserId == userId).Where(a => a.EndDate < DateTime.Today).ToList();

            return activities;
        }

        public async Task<List<Activity>> ParticipationHistory(string userId)
        {
            var activities = context.Activity.Where(n => n.ActivityParticipation.Any(m => m.UserId == userId)).Where(a => a.EndDate < DateTime.Today).ToList();

            return activities;
        }

        public async Task<List<Activity>> SupervisionHistory(string userId)
        {
            var activities = context.Activity.Where(n => n.ActivitySupervision.Any(m => m.UserId == userId)).Where(a => a.EndDate < DateTime.Today).ToList();

            return activities;
        }
    }
}