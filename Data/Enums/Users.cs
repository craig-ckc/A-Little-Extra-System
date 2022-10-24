
using System.ComponentModel.DataAnnotations;

namespace A_Little_Extra_System.Data.Enums
{
    public enum Users
    {
        [Display(Name="Student")]
        Student,
        [Display(Name="DepartmentStaff")]
        DepartmentStaff,
        [Display(Name="UniversityPartner")]
        UniversityPartner,
    }
}