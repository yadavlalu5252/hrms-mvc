using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class EmployeeFamilyDetail
    {
        
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Relation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
