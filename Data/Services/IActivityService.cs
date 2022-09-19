using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface IActivityService:IEntityBaseRepository<Activity>
    {
        Task NewActivityAsync(ActivityForm data, String userId);

        Task<List<Activity>> GetPostedActivities(String userId);

        Task<List<Activity>> PostedHistory(string userId);

        Task<List<Activity>> ParticipationHistory(string userId);

        Task<List<Activity>> SupervisionHistory(string userId);

    }
}