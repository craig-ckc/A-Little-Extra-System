using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class Profile
    {
        public List<Activity> PostedHistory { get; set; }

        public List<Activity> OtherHistory { get; set; }
    }
}