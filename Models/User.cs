using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Models
{
    public class User:IdentityUser
    {
        public string ImgUrl { get; set; }
        
        public string? FirstName { get; set; }

        public int? DepartmentID { get; set; }
        public Department Department { get; set; }

		public string? LastName { get; set; }

        public string? PatnerName { get; set; }

        public Boolean isActive { get; set; }

        public int Points { get; set; }

        // Relationship with Activity (Assciation Entity)
        public List<Activity> Activity { get; set; }

        // Relationship with ActivitySupervision (Assciation Entity)
		public List<ActivitySupervision> ActivitySupervision { get; set; }

        // Relationship with ActivityParticipation (Assciation Entity)
		public List<ActivityParticipation> ActivityParticipation { get; set; }
        
		// Relationship with StudentAward (Assciation Entity)
		public List<StudentAward> StudentAward { get; set; }
    }
}