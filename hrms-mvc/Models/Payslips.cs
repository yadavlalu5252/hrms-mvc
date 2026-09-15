
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Payslips
    {
        
        public int Id { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }
        public User User { get; set; }


        [Required(ErrorMessage = "Month is required.")]
        public string Month { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(2000, 2100, ErrorMessage = "Year must be between 2000 and 2100.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Payslip path is required.")]
        public string PayslipPath { get; set; }

        [Required]
        public DateTime GeneratedOn { get; set; }
    }
}
