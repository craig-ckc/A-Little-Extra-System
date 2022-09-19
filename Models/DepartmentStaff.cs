using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Models
{
    public class DepartmentStaff
    {
        public int Id { get; set; }

        public String UserId { get; set; }
        public User User { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public string FirstName { get; set; }

		public string LastName { get; set; }

        // Relationship with ActivitySupervision (Assciation Entity)
		public List<ActivitySupervision> ActivitySupervision { get; set; }

    }
}