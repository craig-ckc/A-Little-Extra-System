using A_Little_Extra_System.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_Little_Extra_System.Models
{
    public class Award : IEntityBase
	{
		public int Id { get; set; }

		// foreign key : Activity
		[Required]
		public int ActivityId { get; set; }
		public Activity Activity { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		// Relationship with StudentAward (Assciation Entity)
		public List<StudentAward> StudentAward { get; set; }
    }
}