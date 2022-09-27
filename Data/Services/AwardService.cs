using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;
using Microsoft.EntityFrameworkCore;

namespace A_Little_Extra_System.Data.Service
{
    public class AwardService : EntityBaseRepository<Award>, IAwardService
    {
        private readonly AppDbContext context;

        public AwardService(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Award>> GetAwards(int activityId)
        {
            var activities = context.Award.Where(n => n.ActivityId == activityId).ToList();

            return activities;
        }
    }
}