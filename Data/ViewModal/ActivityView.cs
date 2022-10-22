using A_Little_Extra_System.Data.Base;
using A_Little_Extra_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_Little_Extra_System.Data.ViewModal
{
    public class ActivityView
    {
        public IEnumerable<Activity>? Activities { get; set; }

        public string SearchString { get; set; }

        public string DateRange { get; set; }

        public string PointsRange { get; set; }
    }
}