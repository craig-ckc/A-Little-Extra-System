using A_Little_Extra_System.Models;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class Profile
    {
        public User User { get; set; }
        public IEnumerable<Activity> PostedHistory { get; set; }

        public IEnumerable<Activity> OtherHistory { get; set; }
    }
}