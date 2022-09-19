using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class ActivityForm
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date required")]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date required")]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Points")]
        [Required(ErrorMessage = "Points required")]
        public int Points { get; set; }

        public AwardForm Award { get; set; }

        public List<Award> Awards { get; set; }
    }
}