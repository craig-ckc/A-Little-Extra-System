using A_Little_Extra_System.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_Little_Extra_System.Models
{
    public class Activity : IEntityBase
    {
        [Required]
        public int Id { get; set; }

        // foreign key : Activity
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Points")]
        public int Points { get; set; }

        public Boolean Active { get; set; }

        // Relationship with ActivityParticipation (Assciation Entity)
        public List<ActivityParticipation> ActivityParticipation { get; set; }

        // Relationship with ActivitySupervision (Assciation Entity)
        public List<ActivitySupervision> ActivitySupervision { get; set; }

        // Relationship with Award 
        public List<Award> Award { get; set; }
    }
}