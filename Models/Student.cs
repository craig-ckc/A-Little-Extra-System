namespace A_Little_Extra_System.Models
{
    public class Student
    {
        public int Id { get; set; }

        public String UserId { get; set; }
        public User User { get; set; }

        public string FirstName { get; set; }

		public string LastName { get; set; }

        public int Points { get; set; }

        // Relationship with ActivityParticipation (Assciation Entity)
		public List<ActivityParticipation> ActivityParticipation { get; set; }
        
		// Relationship with StudentAward (Assciation Entity)
		public List<StudentAward> StudentAward { get; set; }
    }
}