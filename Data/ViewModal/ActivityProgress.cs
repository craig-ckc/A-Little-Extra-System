using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class ActivityProgress
    {
        public Activity Activity { get; set; }

        public IEnumerable<User> Participants { get; set; }

        public IEnumerable<User> Supervisors { get; set; }

        public IEnumerable<ActivitySupervision> SupervisorsStatus { get; set; }

        public int SuperviosrPos { get; set; }
    }
}