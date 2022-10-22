using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A_Little_Extra_System.Data.Service
{
    public class SupervisorService : EntityBaseRepository<Activity>, ISupervisorService
    {
        private readonly AppDbContext context;

        public SupervisorService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddSupervision(int Id, string userId)
        {
            if (context.ActivitySupervision.Find(Id, userId) != null) return;

            var entity = new ActivitySupervision
            {
                ActivityId = Id,
                UserId = userId,
                Accepted = false,
            };

            await context.Set<ActivitySupervision>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSupervision(int Id, string userId)
        {
            var entity = context.ActivitySupervision.Find(Id, userId);

            EntityEntry entityEntry = context.Entry<ActivitySupervision>(entity);
            entityEntry.State = EntityState.Deleted;

            await context.SaveChangesAsync();
        }

        public async Task<List<Activity>> GetActivitiesSupervising(string userId)
        {
            var activities = context.Activity.Where(n => n.ActivitySupervision.Any(m => m.UserId == userId)).Where(a => a.EndDate >= DateTime.Today).ToList();

            return activities;
        }

        public async Task<List<User>> GetActivitySupervisors(int Id)
        {
            var activities = context.User.Where(n => n.ActivitySupervision.Any(m => m.ActivityId == Id)).ToList();

            return activities;
        }

        public async Task<List<ActivitySupervision>> GetActivitySupervisorsStatus(int Id)
        {
            var activities = context.ActivitySupervision.Where(m => m.ActivityId == Id).ToList();

            return activities;
        }

        public async Task<IEnumerable<Activity>> SupervisionHistory(string userId)
        {
            var activities = await context.Activity.Where(n => n.ActivitySupervision.Any(m => m.UserId == userId)).Where(a => a.EndDate < DateTime.Today).ToListAsync();

            return activities;
        }

        public async Task UpdateSupervision(int activityId, string userId)
        {
            var entity = context.ActivitySupervision.Find(activityId, userId);

            entity.Accepted = true;

            await context.SaveChangesAsync();
        }
    }
}