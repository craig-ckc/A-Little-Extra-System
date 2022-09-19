using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class ActivityProgress
    {
        public Activity Activity { get; set; }
        
        public List<User> Participants { get; set; }

        public List<User> Supervisors { get; set; }
    }
}