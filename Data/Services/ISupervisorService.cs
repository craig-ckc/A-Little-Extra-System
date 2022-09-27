using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface ISupervisorService:IEntityBaseRepository<Activity>
    {
        Task AddSupervisor(int Id, string userId);

        Task DeleteSupervisor(int Id, string userId);

        Task<List<Activity>> GetActivitiesSupervising(string userId);

        Task<List<User>> GetActivitySupervisors(int Id);

        Task<List<ActivitySupervision>> GetActivitySupervisorsStatus(int Id);
    }
}