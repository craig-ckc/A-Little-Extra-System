using A_Little_Extra_System.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        // Relationship with DepartmentStaff (Assciation Entity)
        public List<User> User { get; set; }
    }
}