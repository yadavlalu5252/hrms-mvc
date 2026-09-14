using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class TaskMembers
    {
        [Key]
        public int AssignedId { get; set; }

        [ForeignKey("Task")]
        public int? TaskId { get; set; }
        
        public Tasks Task { get; set; }

    }
}
