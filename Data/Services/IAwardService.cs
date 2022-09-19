using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.Service
{
    public interface IAwardService:IEntityBaseRepository<Activity>
    {
        Task<List<Award>> GetAwards(int Id);
    }
}