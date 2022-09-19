using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface IParticipantService:IEntityBaseRepository<Activity>
    {
        Task<List<User>> GetActivityPaticipants(int Id);

        Task<List<Activity>> GetActivitiesParticipating(string userId);

        Task AddParticipant(int Id, string userId);

        Task DeleteParticipant(int Id, string userId);
    }
}