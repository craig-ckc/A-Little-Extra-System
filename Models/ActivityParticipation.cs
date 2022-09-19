namespace A_Little_Extra_System.Models
{
    public class ActivityParticipation
    {	
		// foreign key : Activity
		public int ActivityId { get; set; }
		public Activity Activity { get; set; }

		// foreign key : Student
		public String UserId { get; set; }
		public User User { get; set; }
	}
}