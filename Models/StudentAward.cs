namespace A_Little_Extra_System.Models
{
    public class StudentAward
    {
		// foreign key : Award
		public int AwardId { get; set; }
		public Award Award { get; set; }

		// foreign key : Student
		public String UserId { get; set; }
		public User User { get; set; }
	}
}