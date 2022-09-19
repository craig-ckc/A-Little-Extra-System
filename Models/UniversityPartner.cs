namespace A_Little_Extra_System.Models
{
    public class UniversityPartner
    {
        public int Id { get; set; }

        public String UserId { get; set; }
        public User User { get; set; }

        public string PatnerName { get; set; }

    }
}