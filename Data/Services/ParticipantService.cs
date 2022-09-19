using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A_Little_Extra_System.Data.Service
{
    public class ParticipantService : EntityBaseRepository<Activity>, IParticipantService
    {
        private readonly AppDbContext context;

        public ParticipantService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddParticipant(int Id, string userId)
        {
            if(context.ActivityParticipation.Find( Id, userId) != null) return;
            
            var entity = new ActivityParticipation
            {
                ActivityId = Id,
                UserId = userId,
            };

            await context.Set<ActivityParticipation>().AddAsync(entity);
            await context.SaveChangesAsync();

        }

        public async Task DeleteParticipant(int Id, string userId)
        {
            var entity = context.ActivityParticipation.Find( Id, userId);

            EntityEntry entityEntry = context.Entry<ActivityParticipation>(entity);
            entityEntry.State = EntityState.Deleted;

            await context.SaveChangesAsync();
        }

        public async Task<List<Activity>> GetActivitiesParticipating(string userId)
        {
            var activities = context.Activity.Where(n => n.ActivityParticipation.Any(m => m.UserId == userId)).ToList();

            return activities;
        }

        public async Task<List<User>> GetActivityPaticipants(int Id)
        {
            var activities = context.User.Where(n => n.ActivityParticipation.Any(m => m.ActivityId == Id)).ToList();

            return activities;
        }

    }
}