using A_Little_Extra_System.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class AwardForm
    {
        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}