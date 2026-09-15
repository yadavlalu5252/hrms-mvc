using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class Role
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Role Name is required.")]
        public string? RoleName { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<User>? Users { get; set; }
    }
}
