using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface ISupervisorService : IEntityBaseRepository<Activity>
    {
        Task AddSupervision(int Id, string userId);
        Task DeleteSupervision(int Id, string userId);
        Task UpdateSupervision(int activityId, string userId);
        Task<IEnumerable<Activity>> SupervisionHistory(string userId);
        Task<List<Activity>> GetActivitiesSupervising(string userId);
        Task<List<ActivitySupervision>> GetActivitySupervisorsStatus(int Id);
        Task<List<User>> GetActivitySupervisors(int Id);
    }
}