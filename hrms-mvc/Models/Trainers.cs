using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Trainers
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "FirsName is compulsory")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "LastName is compulsory")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Role is compulsory")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email is compulsory")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Descripion is compulsory")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is compulsory")]
        public string Status { get; set; }

        [Required(ErrorMessage = "ProfilePicture is compulsory")]
        public string ProfilePicture { get; set; }
        [Required(ErrorMessage = "Phone is compulsory")]
        public string? Phone { get; set; }
    }
}
