using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class TicketResolution
    {
        [Key]
        public int ResolutionId { get; set; }

        [Required]
        public int TicketId { get; set; }

        [Required]
        public int ResolvedBy { get; set; }

        [Required]
        public string? Solution { get; set; }

        public string? ResolutionNotes { get; set; }

        [Required]
        public DateTime ResolvedDate { get; set; }

        
        public Tickets? Ticket { get; set; }

        public User? ResolvedByUser { get; set; }
    }
}
