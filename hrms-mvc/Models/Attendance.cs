using hrms_mvc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrms_mvc.Models
{
    public class Attendance
    {
	
		public int Id { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }
        public  User User { get; set; }
        [Required]
		public DateTime Date { get; set; }

		public DateTime? Checkin { get; set; }

		public DateTime? Checkout { get; set; }

		public DateTime? Lunchin { get; set; }

		public DateTime? Lunchout { get; set; }

		public decimal WorkHours { get; set; }

		public decimal ProdHours { get; set; }

		public decimal OTHours { get; set; }

		public decimal BreakHours { get; set; }

		public int Late { get; set; }

		public string Status { get; set; }

	

	}
}
