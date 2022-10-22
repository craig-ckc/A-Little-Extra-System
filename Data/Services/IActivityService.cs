using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Data.ViewModal;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface IActivityService:IEntityBaseRepository<Activity>
    {
        Task DeleteActivityAsync(int id);
        
        Task<IEnumerable<Activity>> GetAllActiveAsync();

        Task<IEnumerable<Activity>> GetPostedActivities(String userId);

        Task<IEnumerable<Activity>> PostedHistory(string userId);

        Task NewActivityAsync(ActivityForm data, String userId);

        Task UpdateActivityAsync(ActivityForm data, String userId);

    }
}