namespace A_Little_Extra_System.Models
{
    public class ActivitySupervision
    {
		// foreign key : Activity
		public int ActivityId { get; set; }
		public Activity Activity { get; set; }

		// foreign key : DepartmentStaff
		public String UserId { get; set; }
		public User User { get; set; }

		public bool Accepted { get; set; }
	}
}