using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class Dashboard
    {
        public int Id;
        public List<Activity> PostedActivities { get; set; }

        public List<Activity> InvolvedActivities { get; set; }
    }
}