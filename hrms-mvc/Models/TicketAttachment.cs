using System.ComponentModel.DataAnnotations;

namespace hrms_mvc.Models
{
    public class TicketAttachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required]
        public int TicketId { get; set; }

        [Required]
        [StringLength(150)]
        public string? FileName { get; set; }

        [Required]
        [StringLength(500)]
        public string? FilePath { get; set; }

        [Required]
        public int UploadedBy { get; set; }

        [Required]
        public DateTime UploadedDate { get; set; }

        
        public Tickets? Ticket { get; set; }

        public User? UploadedByUser { get; set; }
    }
}
