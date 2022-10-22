using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface IParticipantService:IEntityBaseRepository<Activity>
    {
        Task AddParticipant(int Id, string userId);
        Task DeleteParticipant(int Id, string userId);
        Task<IEnumerable<Activity>> ParticipationHistory(string userId);
        Task<List<Activity>> GetActivitiesParticipating(string userId);
        Task<List<User>> GetActivityPaticipants(int Id);
        Task<Activity> GetActivityPaticipant(int Id, String userId);
    }
}