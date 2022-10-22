using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class Dashboard
    {
        public int Id;
        public IEnumerable<Activity> PostedActivities { get; set; }

        public IEnumerable<Activity> InvolvedActivities { get; set; }
    }
}